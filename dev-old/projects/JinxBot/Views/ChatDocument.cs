﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BNSharp.Net;
using JinxBot.Controls.Docking;
using BNSharp;
using JinxBot.Controls;
using System.Globalization;
using Tmr = System.Timers.Timer;
using BNSharp.BattleNet.Stats;
using System.Threading;
using JinxBot.Views.Chat;
using BNSharp.BattleNet;
using JinxBot.Plugins.UI;
using System.Diagnostics;
using JinxBot.Configuration;

namespace JinxBot.Views
{
    public partial class ChatDocument : DockableDocument, IChatTab
    {
        private BattleNetClient m_client;
        private string m_userName;
        private bool m_inChat;
        private DateTime m_enteredChat;
        private ProfileResourceProvider m_prp;
        private ClientProfile m_profile;

        /// <summary>
        /// Initializes a new <see>Chatdocument</see> with no client.
        /// </summary>
        /// <remarks>
        /// <para>This is provided as a constructor for subclassing that does not want to use the default event registration setup.  When using the
        /// <see>ChatDocument(BattleNetClient)</see> constructor, client events are registered automatically at 
        /// <see cref="Priority">Low priority</see>.</para>
        /// </remarks>
        protected ChatDocument()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a new <see>ChatDocument</see> to handle the specified client.
        /// </summary>
        /// <param name="client">The client to handle.</param>
        public ChatDocument(BattleNetClient client)
            : this()
        {
            m_client = client;
            m_prp = ProfileResourceProvider.GetForClient(client);
            m_profile = client.Settings as ClientProfile;

            SetupEventRegistration();
        }

        private void SetupEventRegistration()
        {
            __ChannelDidNotExist = new ServerChatEventHandler(ChannelDidNotExist);
            __ChannelListReceived = new ChannelListEventHandler(ChannelListReceived);
            __ChannelWasFull = new ServerChatEventHandler(ChannelWasFull);
            __ChannelWasRestricted = new ServerChatEventHandler(ChannelWasRestricted);
            __ClientCheckFailed = new ClientCheckFailedEventHandler(ClientCheckFailed);
            __ClientCheckPassed = new EventHandler(ClientCheckPassed);
            __CommandSent = new InformationEventHandler(CommandSent);
            __Connected = new EventHandler(Connected);
            __Disconnected = new EventHandler(Disconnected);
            __EnteredChat = new EnteredChatEventHandler(EnteredChat);
            __Error = new ErrorEventHandler(Error);
            __Information = new InformationEventHandler(Information);
            __InformationReceived = new ServerChatEventHandler(InformationReceived);
            __JoinedChannel = new ServerChatEventHandler(JoinedChannel);
            __LoginFailed = new LoginFailedEventHandler(LoginFailed);
            __LoginSucceeded = new EventHandler(LoginSucceeded);
            __MessageSent = new ChatMessageEventHandler(MessageSent);
            __ServerBroadcast = new ServerChatEventHandler(ServerBroadcast);
            __ServerErrorReceived = new ServerChatEventHandler(m_client_ServerErrorReceived);
            __UserEmoted = new ChatMessageEventHandler(UserEmoted);
            __UserFlagsChanged = new UserEventHandler(UserFlagsChanged);
            __UserJoined = new UserEventHandler(UserJoined);
            __UserLeft = new UserEventHandler(UserLeft);
            __UserShown = new UserEventHandler(UserShown);
            __UserSpoke = new ChatMessageEventHandler(UserSpoke);
            __WardenUnhandled = new EventHandler(WardenUnhandled);
            __WhisperReceived = new ChatMessageEventHandler(WhisperReceived);
            __WhisperSent = new ChatMessageEventHandler(WhisperSent);
            __UserProfileReceived = new UserProfileEventHandler(UserProfileReceived);

            m_client.RegisterUserProfileReceivedNotification(Priority.Low, __UserProfileReceived);
            m_client.RegisterChannelDidNotExistNotification(Priority.Low, __ChannelDidNotExist);
            m_client.RegisterChannelListReceivedNotification(Priority.Low, __ChannelListReceived);
            m_client.RegisterChannelWasFullNotification(Priority.Low, __ChannelWasFull);
            m_client.RegisterChannelWasRestrictedNotification(Priority.Low, __ChannelWasRestricted);
            m_client.RegisterClientCheckFailedNotification(Priority.Low, __ClientCheckFailed);
            m_client.RegisterClientCheckPassedNotification(Priority.Low, __ClientCheckPassed);
            m_client.RegisterCommandSentNotification(Priority.Low, __CommandSent);
            m_client.RegisterConnectedNotification(Priority.Low, __Connected);
            m_client.RegisterDisconnectedNotification(Priority.Low, __Disconnected);
            m_client.RegisterEnteredChatNotification(Priority.Low, __EnteredChat);
            m_client.RegisterErrorNotification(Priority.Low, __Error);
            m_client.RegisterInformationNotification(Priority.Low, __Information);
            m_client.RegisterInformationReceivedNotification(Priority.Low, __InformationReceived);
            m_client.RegisterJoinedChannelNotification(Priority.Low, __JoinedChannel);
            m_client.RegisterLoginFailedNotification(Priority.Low, __LoginFailed);
            m_client.RegisterLoginSucceededNotification(Priority.Low, __LoginSucceeded);
            m_client.RegisterMessageSentNotification(Priority.Low, __MessageSent);
            m_client.RegisterServerBroadcastNotification(Priority.Low, __ServerBroadcast);
            m_client.RegisterServerErrorReceivedNotification(Priority.Low, __ServerErrorReceived);
            m_client.RegisterUserEmotedNotification(Priority.Low, __UserEmoted);
            m_client.RegisterUserFlagsChangedNotification(Priority.Low, __UserFlagsChanged);
            m_client.RegisterUserJoinedNotification(Priority.Low, __UserJoined);
            m_client.RegisterUserLeftNotification(Priority.Low, __UserLeft);
            m_client.RegisterUserShownNotification(Priority.Low, __UserShown);
            m_client.RegisterUserSpokeNotification(Priority.Low, __UserSpoke);
            m_client.RegisterWardenUnhandledNotification(Priority.Low, __WardenUnhandled);
            m_client.RegisterWhisperReceivedNotification(Priority.Low, __WhisperReceived);
            m_client.RegisterWhisperSentNotification(Priority.Low, __WhisperSent);
        }

        private UserProfileEventHandler __UserProfileReceived;
        void UserProfileReceived(object sender, UserProfileEventArgs e)
        {
            List<ChatNode> nodesToAdd = new List<ChatNode>() { new ChatNode("User Profile received for ", CssClasses.UserProfileReceivedNotification), new ChatNode(e.Profile.UserName, CssClasses.UsernameOther) };
            foreach (UserProfileKey key in e.Profile)
            {
                nodesToAdd.Add(ChatNode.NewLine);
                nodesToAdd.Add(new ChatNode(string.Concat(key.ToString(), ":"), CssClasses.UserProfileKey));
                if (key.Equals(UserProfileKey.TotalTimeLogged))
                {
                    int totalSeconds;
                    if (int.TryParse(e.Profile[key], out totalSeconds))
                    {
                        TimeSpan ts = TimeSpan.FromSeconds(totalSeconds);
                        nodesToAdd.Add(new ChatNode(string.Format("{0} day{4}, {1} hour{5}, {2} minute{6}, {3} second{7}.",
                            (int)ts.TotalDays, ts.Hours, ts.Minutes, ts.Seconds, ((int)ts.TotalDays == 1) ? string.Empty : "s",
                            ts.Hours == 1 ? string.Empty : "s", ts.Minutes == 1 ? string.Empty : "s", ts.Seconds == 1 ? string.Empty : "s"), CssClasses.UserProfileValue));
                    }
                    else
                    {
                        nodesToAdd.Add(new ChatNode(e.Profile[key], CssClasses.UserProfileValue));
                    }
                }
                else
                {
                    nodesToAdd.Add(new ChatNode(e.Profile[key], CssClasses.UserProfileValue));
                }
            }
            chat.AddChat(nodesToAdd);
        }

        #region client-driven events
        private UserEventHandler __UserShown;
        void UserShown(object sender, UserEventArgs e)
        {
            AnnounceUser(e);
        }

        private ChatMessageEventHandler __WhisperSent;
        void WhisperSent(object sender, ChatMessageEventArgs e)
        {
            chat.AddChat(new ChatNode("You whisper to ", CssClasses.WhisperHeader), new ChatNode(e.Username, CssClasses.WhisperOtherUser), new ChatNode(": ", CssClasses.WhisperHeader), new ChatNode(e.Text, CssClasses.WhisperText));
        }

        private ChatMessageEventHandler __WhisperReceived;
        void WhisperReceived(object sender, ChatMessageEventArgs e)
        {
            chat.AddChat(new ChatNode(e.Username, CssClasses.WhisperOtherUser), 
                new ChatNode(" whispers: ", CssClasses.WhisperHeader), 
                new ChatNode(e.Text, CssClasses.WhisperText));
        }

        private EventHandler __WardenUnhandled;
        void WardenUnhandled(object sender, EventArgs e)
        {
            chat.AddChat(new ChatNode("WARNING: Warden was requested but unhandled.  You may be disconnected.", CssClasses.UnhandledWarden));
        }

        private ChatMessageEventHandler __UserSpoke;
        void UserSpoke(object sender, ChatMessageEventArgs e)
        {
            chat.AddChat(new ChatNode("[", CssClasses.SpeakBracketsOtherUsers), 
                new ChatNode(e.Username, CssClasses.SpeakOtherUsername), 
                new ChatNode("]: ", CssClasses.SpeakBracketsOtherUsers), 
                new ChatNode(e.Text, CssClasses.SpeakText));
        }

        private UserEventHandler __UserLeft;
        void UserLeft(object sender, UserEventArgs e)
        {
            chat.AddChat(new ChatNode(e.User.Username, CssClasses.UsernameOther), new ChatNode(" left the channel.", CssClasses.LeftChannel));
        }

        private UserEventHandler __UserJoined;
        void UserJoined(object sender, UserEventArgs e)
        {
            AnnounceUser(e);
        }

        private UserEventHandler __UserFlagsChanged;
        void UserFlagsChanged(object sender, UserEventArgs e)
        {

        }

        private void AnnounceUser(UserEventArgs e)
        {
            ChatUser user = e.User;
            UserStats us = e.User.Stats;
            string imgID = m_prp.Icons.GetImageIdFor(user.Flags, us);
            ChatNode productNode = ChatNode.Empty;
            if (m_profile.IncludeIconsInChat)
            {
                Image img = m_prp.Icons.GetImageFor(user.Flags, us);
                productNode = new ImageChatNode(string.Concat(imgID, ".jpg"),
                    img, imgID);
            }

            switch (us.Product.ProductCode)
            {
                case "DRTL":
                case "DSHR":
                case "SSHR":
                case "STAR":
                case "SEXP":
                case "JSTR":
                case "W2BN":
                    StarcraftStats ss = us as StarcraftStats;
                    if (ss == null)
                        chat.AddChat(new ChatNode(user.Username, CssClasses.UsernameOther), productNode,
                            new ChatNode(string.Format(" joined the channel with {0}.", us.Product.Name), CssClasses.JoinedChannel));
                    else
                    {
                        chat.AddChat(new ChatNode(user.Username, CssClasses.UsernameOther), productNode, new ChatNode(string.Format(" joined the channel with {0} ({1} win{2}, ladder rating {3}, rank {4}).", ss.Product.Name, ss.Wins, ss.Wins != 1 ? "s" : string.Empty,
                            ss.LadderRating, ss.LadderRank), CssClasses.JoinedChannel));
                    }
                    break;
                case "D2DV":
                case "D2XP":
                    Diablo2Stats ds = us as Diablo2Stats;
                    if (ds == null)
                        chat.AddChat(new ChatNode(user.Username, CssClasses.UsernameOther), productNode, new ChatNode(string.Format(" joined the channel with {0}.", us.Product.Name), CssClasses.JoinedChannel));
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(" joined the channel with ");
                        sb.Append(ds.Product.Name);
                        if (ds.IsRealmCharacter)
                        {
                            sb.Append(" as ");
                            sb.Append(ds.CharacterName);
                            sb.AppendFormat(", a level {0}", ds.Level);
                            if (ds.IsHardcoreCharacter)
                                sb.Append(" hardcore");
                            if (ds.IsLadderCharacter)
                                sb.Append(" ladder");
                            if (ds.IsExpansionCharacter)
                                sb.Append(" Expansion ");
                            else
                                sb.Append(" Classic ");
                            sb.Append(ds.CharacterClass);
                            sb.Append(".");

                            chat.AddChat(new ChatNode(ds.UserName, CssClasses.UsernameOther), new ChatNode(sb.ToString(), CssClasses.JoinedChannel));
                        }
                        else
                        {
                            chat.AddChat(new ChatNode(user.Username, CssClasses.UsernameOther), productNode, new ChatNode(string.Format(" joined the channel with {0}.", us.Product.Name), CssClasses.JoinedChannel));
                        }
                    }
                    break;
                case "WAR3":
                case "W3XP":
                    Warcraft3Stats ws = us as Warcraft3Stats;
                    if (ws == null)
                        chat.AddChat(new ChatNode(user.Username, CssClasses.UsernameOther), productNode, new ChatNode(string.Format(" joined the channel with {0}.", us.Product.Name), CssClasses.JoinedChannel));
                    else
                    {
                        if (string.IsNullOrEmpty(ws.ClanTag))
                        {
                            chat.AddChat(new ChatNode(user.Username, CssClasses.UsernameOther), productNode, new ChatNode(string.Format(" joined the channel with {0}, {1} icon tier {2}, level {3}.", ws.Product.Name, ws.IconRace, ws.IconTier, ws.Level), CssClasses.JoinedChannel));
                        }
                        else
                        {
                            chat.AddChat(new ChatNode(user.Username, CssClasses.UsernameOther), productNode, new ChatNode(string.Format(" of clan {0} joined the channel with {1}, {2} icon tier {3}, level {4}.", ws.ClanTag, ws.Product.Name, ws.IconRace, ws.IconTier, ws.Level), CssClasses.JoinedChannel));
                        }
                    }
                    break;
                default:
                    chat.AddChat(new ChatNode(user.Username, CssClasses.UsernameOther), productNode, new ChatNode(string.Format(" joined the channel with {0} ({1}).", us.Product.Name, us.LiteralText), CssClasses.JoinedChannel));
                    break;

            }
            if (user.Flags != UserFlags.None)
            {
                chat.AddChat(new ChatNode(user.Username, CssClasses.UsernameOther), new ChatNode(string.Format(" had the following flags: {0}", user.Flags), CssClasses.JoinedChannel));
            }
        }

        private ChatMessageEventHandler __UserEmoted;
        void UserEmoted(object sender, ChatMessageEventArgs e)
        {
            chat.AddChat(new ChatNode("<", CssClasses.EmoteText), new ChatNode(e.Username, CssClasses.EmoteOtherUsername), new ChatNode(string.Concat(" ", e.Text, ">"), CssClasses.EmoteText));
        }

        private ServerChatEventHandler __ServerErrorReceived;
        void m_client_ServerErrorReceived(object sender, ServerChatEventArgs e)
        {
            chat.AddChat(new ChatNode("[Error]: ", CssClasses.ServerInfo), new ChatNode(e.Text, CssClasses.Error));
        }

        private ServerChatEventHandler __ServerBroadcast;
        void ServerBroadcast(object sender, ServerChatEventArgs e)
        {
            chat.AddChat(new ChatNode("[Server]: ", CssClasses.ServerInfo), new ChatNode(e.Text, CssClasses.Broadcast));
        }

        private ChatMessageEventHandler __MessageSent;
        void MessageSent(object sender, ChatMessageEventArgs e)
        {
            if (m_inChat)
            {
                chat.AddChat(new ChatNode("[", CssClasses.SpeakBracketsOtherUsers), new ChatNode(m_userName, CssClasses.SpeakOtherUsername), new ChatNode("]: ", CssClasses.SpeakBracketsOtherUsers), new ChatNode(e.Text, CssClasses.SpeakText));
            }
        }

        private EventHandler __LoginSucceeded;
        void LoginSucceeded(object sender, EventArgs e)
        {
            chat.AddChat(new ChatNode("Login succeeded!", CssClasses.LoginSucceeded));
        }

        private LoginFailedEventHandler __LoginFailed;
        void LoginFailed(object sender, LoginFailedEventArgs e)
        {
            chat.AddChat(new ChatNode("Login failed.", CssClasses.LoginFailed));
            m_client.CreateAccount();
        }

        private ServerChatEventHandler __JoinedChannel;
        void JoinedChannel(object sender, ServerChatEventArgs e)
        {
            chat.AddChat(new ChatNode("Joined Channel: ", CssClasses.JoiningChannel), new ChatNode(e.Text, CssClasses.ChannelName));
            ChannelFlags flags = (ChannelFlags)e.Flags;
            if ((flags & ChannelFlags.SilentChannel) == ChannelFlags.SilentChannel)
                chat.AddChat(new ChatNode("This is a silent channel.", CssClasses.SilentChannel));

            ThreadStart ts = delegate
            {
                this.Text = string.Format(CultureInfo.CurrentCulture, "Chat Channel: {0}", e.Text);
                this.TabText = this.Text;
            };

            if (InvokeRequired)
                BeginInvoke(ts);
            else
                ts();
        }

        private ServerChatEventHandler __InformationReceived;
        void InformationReceived(object sender, ServerChatEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Text))
                chat.AddChat(new ChatNode("[Server]: ", CssClasses.ServerInfo), new ChatNode(e.Text, CssClasses.ServerInfo));
        }

        private InformationEventHandler __Information;
        void Information(object sender, InformationEventArgs e)
        {
            chat.AddChat(new ChatNode(e.Information, CssClasses.ClientInfo));
        }

        private ErrorEventHandler __Error;
        void Error(object sender, ErrorEventArgs e)
        {
            chat.AddChat(new ChatNode(e.Error, CssClasses.Error));
            if (e.IsDisconnecting)
            {
                chat.AddChat(new ChatNode("This error is causing you to disconnect.", CssClasses.ErrorDisconnecting));
            }
        }

        private EnteredChatEventHandler __EnteredChat;
        void EnteredChat(object sender, EnteredChatEventArgs e)
        {
            Product clientProduct = Product.GetByProductCode(m_client.Settings.Client.ToUpperInvariant());
            string imgID = m_prp.Icons.GetImageIdFor(UserFlags.None, UserStats.CreateDefault(clientProduct));
            Image userImg = ProfileResourceProvider.GetForClient(m_client).Icons.GetImageFor(UserFlags.None, UserStats.CreateDefault(clientProduct));
            chat.AddChat(new ChatNode("Entered chat as ", CssClasses.EnteringChat),
                new ImageChatNode(string.Concat(imgID, ".jpg"),
                    userImg, clientProduct.Name),
                new ChatNode(e.UniqueUsername, CssClasses.UsernameOther));
            m_userName = e.UniqueUsername;
            m_inChat = true;
            m_enteredChat = DateTime.Now;
        }

        private EventHandler __Disconnected;
        void Disconnected(object sender, EventArgs e)
        {
            chat.AddChat(new ChatNode("Disconnected.", CssClasses.Disconnected));
            m_inChat = false;
            m_userName = null;
        }

        private EventHandler __Connected;
        void Connected(object sender, EventArgs e)
        {
            chat.AddChat(new ChatNode("Connected!", CssClasses.Connected));
        }

        private InformationEventHandler __CommandSent;
        void CommandSent(object sender, InformationEventArgs e)
        {
            chat.AddChat(new ChatNode(e.Information, CssClasses.CommandSent));
        }

        private EventHandler __ClientCheckPassed;
        void ClientCheckPassed(object sender, EventArgs e)
        {
            chat.AddChat(new ChatNode("Versioning passed!", CssClasses.VersioningPassed));
        }

        private ClientCheckFailedEventHandler __ClientCheckFailed;
        void ClientCheckFailed(object sender, ClientCheckFailedEventArgs e)
        {
            switch (e.Reason)
            {
                case ClientCheckFailureCause.BannedCdKey:
                    chat.AddChat(new ChatNode("Your primary CD key (for example, Warcraft III: Reign of Chaos, or Diablo II) is banned.", CssClasses.VersioningFailed));
                    break;
                case ClientCheckFailureCause.BannedExpCdKey:
                    chat.AddChat(new ChatNode("Your secondary CD key (for example, Warcraft III: The Frozen Throne, or Diablo II: The Lord of Destruction) is banned.", CssClasses.VersioningFailed));
                    break;
                case ClientCheckFailureCause.CdKeyInUse:
                    chat.AddChat(new ChatNode(string.Format(CultureInfo.CurrentCulture, "Your primary CD key (for example, Warcraft III: Reign of Chaos, or Diablo II) is currently in use by '{0}'.", e.AdditionalInformation), CssClasses.VersioningFailed));
                    break;
                case ClientCheckFailureCause.ExpCdKeyInUse:
                    chat.AddChat(new ChatNode(string.Format(CultureInfo.CurrentCulture, "Your secondary CD key (for example, Warcraft III: The Frozen Throne, or Diablo II: The Lord of Destruction) is currently in use by '{0}'.", e.AdditionalInformation), CssClasses.VersioningFailed));
                    break;
                case ClientCheckFailureCause.InvalidCdKey:
                    chat.AddChat(new ChatNode("Your primary CD key (for example, Warcraft III: Reign of Chaos, or Diablo II) is invalid.", CssClasses.VersioningFailed));
                    break;
                case ClientCheckFailureCause.InvalidExpCdKey:
                    chat.AddChat(new ChatNode("Your secondary CD key (for example, Warcraft III: The Frozen Throne, or Diablo II: The Lord of Destruction) is invalid.", CssClasses.VersioningFailed));
                    break;
                case ClientCheckFailureCause.InvalidVersion:
                    chat.AddChat(new ChatNode("Your client version is invalid.  This most commonly is due to game files that do not match your version byte.", CssClasses.VersioningFailed));
                    break;
                case ClientCheckFailureCause.NewerVersion:
                    chat.AddChat(new ChatNode("Your client version is newer than the version currently on Battle.net.", CssClasses.VersioningFailed));
                    break;
                case ClientCheckFailureCause.OldVersion:
                    chat.AddChat(new ChatNode(string.Format(CultureInfo.CurrentCulture, "Your client version is out-of-date.  You should download and apply '{0}' to patch, and verify that your version byte is correct.", e.AdditionalInformation), CssClasses.VersioningFailed));
                    break;
                case ClientCheckFailureCause.WrongProduct:
                    chat.AddChat(new ChatNode("Your primary CD key (for example, Warcraft III: Reign of Chaos, or Diablo II) is for the incorrect product.  This typically means that you are trying to log in with the wrong kind of CD key (for instance, logging in with Diablo 2 while using a Starcraft CD key), but could also indicate that your CD keys (if you are using two) are backwards.", CssClasses.VersioningFailed));
                    break;
                case ClientCheckFailureCause.WrongExpProduct:
                    chat.AddChat(new ChatNode("Your secondary CD key (for example, Warcraft III: The Frozen Throne, or Diablo II: The Lord of Destruction) is for the incorrect product.  This typically means that you are trying to log in with the wrong kind of CD key (for instance, logging in with Diablo 2: Lord of Destruction while using a Starcraft CD key), but could also indicate that your CD keys are entered backwards.", CssClasses.VersioningFailed));
                    break;
                default:
                    chat.AddChat(new ChatNode(string.Format(CultureInfo.CurrentCulture, "Versioning check failed with an unknown response code (0x{0:x8}).", (int)e.Reason), CssClasses.VersioningFailed));
                    break;
            }
        }

        private ServerChatEventHandler __ChannelWasRestricted;
        void ChannelWasRestricted(object sender, ServerChatEventArgs e)
        {
            chat.AddChat(new ChatNode("Could not join ", CssClasses.Error), new ChatNode(e.Text, CssClasses.ChannelRestricted), new ChatNode(" because it was restricted.", CssClasses.Error));
        }

        private ServerChatEventHandler __ChannelWasFull;
        void ChannelWasFull(object sender, ServerChatEventArgs e)
        {
            chat.AddChat(new ChatNode("Could not join ", CssClasses.Error), new ChatNode(e.Text, CssClasses.ChannelFull), new ChatNode(" because it was full.", CssClasses.Error));
        }

        private ChannelListEventHandler __ChannelListReceived;
        void ChannelListReceived(object sender, ChannelListEventArgs e)
        {
            List<ChatNode> nodesToAdd = new List<ChatNode>();
            nodesToAdd.Add(new ChatNode("Available Channels:", CssClasses.ChannelListHeader));
            foreach (string s in e.Channels)
            {
                nodesToAdd.Add(ChatNode.NewLine);
                nodesToAdd.Add(new ChatNode(" - ", CssClasses.ChannelBullet));
                nodesToAdd.Add(new ChatNode(s, CssClasses.ChannelListItem));
            }
            chat.AddChat(nodesToAdd);
        }

        private ServerChatEventHandler __ChannelDidNotExist;
        void ChannelDidNotExist(object sender, ServerChatEventArgs e)
        {
            chat.AddChat(new ChatNode(e.Text, CssClasses.ChannelDNE));
        }
        #endregion

        private void chat_MessageReady(object sender, MessageEventArgs e)
        {
            if (m_inChat)
            {
                if (e.Message.Length < 224)
                {
                    m_client.SendMessage(e.Message);
                }
            }
            else
            {
                chat.AddChat(new ChatNode("You are not yet in chat!", Color.YellowGreen));
            }
        }

        public Uri StylesheetUri
        {
            get
            {
                return this.chat.StylesheetUri;
            }
            set
            {
                this.chat.StylesheetUri = value;
            }
        }

        public void AddChat(params ChatNode[] nodes)
        {
            IEnumerable<ChatNode> nodeList = nodes;
            chat.AddChat(nodeList);
        }

        public void AddChat(IEnumerable<ChatNode> nodes)
        {
            chat.AddChat(nodes);
        }
    }
}
