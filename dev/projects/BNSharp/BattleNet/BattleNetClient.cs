﻿using BNSharp.BattleNet.Core;
using BNSharp.BattleNet.Warden;
using BNSharp.Chat;
using BNSharp.Networking;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BNSharp.BattleNet
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BattleNetClient : IBattleNetClient, IBattleNetChatConnectionEventSource, ISingleChannelClientEventSource<ChatUser>
    {
        private AsyncConnectionBase _connection;
        private NetworkBufferStorage _storage;
        private IBattleNetSettings _settings;
        private Dictionary<BncsPacketId, ParseCallback> _packetToParserMap;
        private BattleNetClientChannel _channel;

        const string EMOTE_1 = "/me ";
        const string EMOTE_2 = "/emote ";
        const string COMMAND_START = "/";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public BattleNetClient(IBattleNetSettings settings)
        {
            _connection = new AsyncConnectionBase(settings.Gateway.ServerHost, settings.Gateway.ServerPort);
            _storage = _connection.NetworkBuffers;
            _settings = settings;

            InitializeParseDictionaries();

            this._channel = new BattleNetClientChannel(this);
        }

        #region Parsing support
        internal ParseCallback RegisterParseCallback(BncsPacketId packetId, ParseCallback callback)
        {
            ParseCallback current;
            _packetToParserMap.TryGetValue(packetId, out current);
            _packetToParserMap[packetId] = callback;
            return current;
        }

        private void InitializeParseDictionaries()
        {
            //m_customEventSink = new EventSink(this);

            _packetToParserMap = new Dictionary<BncsPacketId, ParseCallback>()
            {
                //{ BncsPacketId.EnterChat, HandleEnterChat },
                { BncsPacketId.GetChannelList, HandleGetChannelList },
                { BncsPacketId.ChatEvent, HandleChatEvent },
                //{ BncsPacketId.CheckAd, HandleCheckAd },
                { BncsPacketId.Ping, HandlePing },
                //{ BncsPacketId.ReadUserData, HandleUserProfileRequest },
                { BncsPacketId.LogonResponse, HandleLogonResponse },
                //{ BncsPacketId.Profile, HandleProfile },
                { BncsPacketId.LogonResponse2, HandleLogonResponse2 },
                { BncsPacketId.CreateAccount2, HandleCreateAccount2 },
                //{ BncsPacketId.WarcraftGeneral, HandleWarcraftGeneral },
                //{ BncsPacketId.NewsInfo, HandleNewsInfo },
                { BncsPacketId.AuthInfo, HandleAuthInfo },
                { BncsPacketId.AuthCheck, HandleAuthCheck },
                //{ BncsPacketId.AuthAccountCreate, HandleAuthAccountCreate },
                //{ BncsPacketId.AuthAccountLogon, HandleAuthAccountLogon },
                //{ BncsPacketId.AuthAccountLogonProof, HandleAuthAccountLogonProof },
                //{ BncsPacketId.AuthAccountChange, HandleAuthAccountChange },
                //{ BncsPacketId.AuthAccountChangeProof, HandleAuthAccountChangeProof },
                //{ BncsPacketId.Warden, HandleWarden },
                //{ BncsPacketId.FriendsList, HandleFriendsList },
                //{ BncsPacketId.FriendsAdd, HandleFriendAdded },
                //{ BncsPacketId.FriendsUpdate, HandleFriendUpdate },
                //{ BncsPacketId.FriendsRemove, HandleFriendRemoved },
                //{ BncsPacketId.FriendsPosition, HandleFriendMoved },
                //{ BncsPacketId.ClanFindCandidates, HandleClanFindCandidates },
                //{ BncsPacketId.ClanInviteMultiple, HandleClanInviteMultiple },
                //{ BncsPacketId.ClanCreationInvitation, HandleClanCreationInvitation },
                //{ BncsPacketId.ClanDisband, HandleDisbandClan },
                //{ BncsPacketId.ClanMakeChieftan, HandleClanMakeChieftan },
                //{ BncsPacketId.ClanInfo, HandleClanInfo },
                //{ BncsPacketId.ClanQuitNotify, HandleClanQuitNotify },
                //{ BncsPacketId.ClanInvitation, HandleClanInvitation },
                //{ BncsPacketId.ClanRemoveMember, HandleClanRemoveMember },
                //{ BncsPacketId.ClanInvitationResponse, HandleClanInvitationResponse },
                //{ BncsPacketId.ClanRankChange, HandleClanRankChange },
                //{ BncsPacketId.ClanMOTD, HandleClanMotd },
                //{ BncsPacketId.ClanMemberList, HandleClanMemberList },
                //{ BncsPacketId.ClanMemberRemoved, HandleClanMemberRemoved },
                //{ BncsPacketId.ClanMemberStatusChanged, HandleClanMemberStatusChanged },
                //{ BncsPacketId.ClanMemberRankChange, HandleClanMemberRankChange },
                //{ BncsPacketId.ClanMemberInformation, HandleClanMemberInformation }
            };
        }
        #endregion


        #region IChatConnection Members

        public async Task<bool> ConnectAsync()
        {
            bool ok = await _connection.ConnectAsync();
            if (ok)
            {
                CultureInfo ci = CultureInfo.CurrentCulture;
                RegionInfo ri = RegionInfo.CurrentRegion;
                TimeSpan ts = DateTime.UtcNow - DateTime.Now;

                ((IChatConnectionEventSource)this).OnConnected();

                await _connection.SendAsync(new byte[] { 1 });

                BncsPacket pck = new BncsPacket(BncsPacketId.AuthInfo, _storage.Acquire());
                pck.InsertInt32(0);
                pck.InsertDwordString("IX86");
                pck.InsertDwordString(_settings.Client.ProductCode);
                pck.InsertInt32(_settings.VersionByte);
                pck.InsertDwordString(ci.TwoLetterISOLanguageName + ri.TwoLetterISORegionName);
                pck.InsertByteArray(_connection.LocalEP.Address.GetAddressBytes());
                pck.InsertInt32((int)ts.TotalMinutes);
                pck.InsertInt32(ci.LCID);
                pck.InsertInt32(ci.LCID);
                pck.InsertCString(ri.ThreeLetterISORegionName, Encoding.UTF8);
                pck.InsertCString(ri.DisplayName, Encoding.UTF8);

                await pck.SendAsync(_connection);

                if (_settings.PingMethod == PingKind.ZeroMs)
                {
                    pck = new BncsPacket(BncsPacketId.Ping, _storage.Acquire());
                    pck.InsertInt32(new Random().Next());
                    await pck.SendAsync(_connection);
                }

                Listen();
            }
            return ok;
        }

        public void Disconnect()
        {
            _connection.Close();
        }

        private async void Listen()
        {
            while (_connection.IsConnected)
            {
                NetworkBuffer nextPacket = _storage.Acquire();
                NetworkBuffer result = await _connection.ReceiveAsync(nextPacket, 0, 4, true);
                if (result == null) return; // disconnected
                BncsReader reader = new BncsReader(result);
                
                if (reader.Length > 4)
                {
                    result = await _connection.ReceiveAsync(nextPacket, 4, reader.Length - 4);
                    if (result == null) return; // disconnected
                }
                else if (reader.Length == 4)
                {
                    // packet is complete
                }
                else
                {
                    throw new ProtocolViolationException("The Battle.net server indicated an invalid packet size.");
                }

                DispatchPacketHandler(reader);
                _storage.Release(nextPacket);
            }
        }

        private void DispatchPacketHandler(BncsReader packet)
        {
            ParseCallback result;
            if (_packetToParserMap.TryGetValue(packet.PacketID, out result))
            {
                result(packet);
            }
            else
            {
                switch (packet.PacketID)
                {
                    #region SID_NULL
                    case BncsPacketId.Null:
                        break;
                    #endregion
                    default:
                        Trace.WriteLine(packet.PacketID, "Unhandled packet");
                        break;
                }
            }
        }

        public void Send(string text)
        {
            if (IsConnected)
            {
                BncsPacket pck = new BncsPacket(BncsPacketId.ChatCommand, _storage.Acquire());
                pck.InsertCString(text, Encoding.UTF8);
                pck.SendAsync(_connection);

                if (text.StartsWith(EMOTE_1, StringComparison.OrdinalIgnoreCase) || text.StartsWith(EMOTE_2, StringComparison.OrdinalIgnoreCase))
                {
                    // do nothing, but we need this case first so that command sent doesn't fire for emotes.
                }
                else if (text.StartsWith(COMMAND_START, StringComparison.Ordinal))
                {
                    //OnCommandSent(new InformationEventArgs(text));
                }
                else
                {
                    ChatMessageEventArgs<UserFlags> cme = new ChatMessageEventArgs<UserFlags>(ChatEventType.Talk, UserFlags.None, "(me)", text);
                    _channel.HandleChatMessageEvent(cme);
                    //ChatMessageEventArgs<UserFlags> cme = new ChatMessageEventArgs<UserFlags>(ChatEventType.Talk, UserFlags.None, this._uniqueUN, text);
                    OnMessageSent(text);
                }
            }
        }

        public void CreateAccount(string accountName, string password)
        {
            bool isClientWar3 = (_settings.Client.Equals(ClassicProduct.Warcraft3Retail) || _settings.Client.Equals(ClassicProduct.Warcraft3Expansion));
            if (isClientWar3)
            {
                CreateAccountNLS();
            }
            else
            {
                CreateAccountOld();
            }
        }

        /// <summary>
        /// Allows the client to continue logging in if the login has stopped due to a non-existent username or password.
        /// </summary>
        /// <remarks>
        /// <para>If a <see>LoginFailed</see> event occurs, the client is not automatically disconnected.  The UI can then present an interface
        /// by which the user may modify the client's <see>Settings</see> instance with proper login information.  Once this has been done, the 
        /// user may then call this method to attempt to log in again.</para>
        /// <para>This method does not need to be called after the <see>AccountCreated</see> event.</para>
        /// </remarks>
        public void ContinueLogin()
        {
            bool isClientWar3 = (_settings.Client.Equals(ClassicProduct.Warcraft3Retail) || _settings.Client.Equals(ClassicProduct.Warcraft3Expansion));
            if (isClientWar3)
            {
                LoginAccountNLS();
            }
            else
            {
                LoginAccountOld();
            }
        }

        public bool IsConnected
        {
            get { return _connection.IsConnected; }
        }

        public event EventHandler Connected;
        public event EventHandler Disconnected;

        public event EventHandler<string> MessageSent;

        public event EventHandler<AccountCreationEventArgs> AccountCreated;
        public event EventHandler<AccountCreationFailedEventArgs> AccountCreationFailed;

        #endregion

        #region ISingleChannelClient<ChatUser> Members

        public IChannel<ChatUser, UserFlags> Channel
        {
            get { return _channel; }
        }

        public void JoinChannel(string channelName)
        {
            
        }

        #endregion

        #region ConnectionErrorOccurred event
        /// <summary>
        /// When overridden by a derived class, provides error information from the current connection.
        /// </summary>
        /// <param name="message">Human-readable information about the error.</param>
        /// <param name="ex">An internal exception containing the error details.</param>
        protected virtual void OnConnectionErrorOccurred(string message, Exception ex)
        {
            if (ConnectionErrorOccurred != null)
                ConnectionErrorOccurred(this, new ConnectionErrorEventArgs(message, ex));
        }

        /// <summary>
        /// Represents an error that occurred.
        /// </summary>
        public event EventHandler<ConnectionErrorEventArgs> ConnectionErrorOccurred;
        #endregion

        public IWardenModule WardenHandler { get; set; }
    }
}
