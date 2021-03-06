﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JinxBot.Controls.Docking;
using BNSharp.Net;
using BNSharp.BattleNet.Clans;
using System.Threading;
using System.Globalization;
using BNSharp.BattleNet;

namespace JinxBot.Views
{
    public partial class ClanList : DockableToolWindow
    {
        private BattleNetClient m_client;
        private ProfileResourceProvider m_prp;

        private ICollection<ClanMember> m_members;

        protected ClanList()
        {
            InitializeComponent();
        }

        public ClanList(BattleNetClient client)
            : this()
        {
            m_client = client;

            m_prp = ProfileResourceProvider.GetForClient(client);

            client.ClanMemberListReceived += new ClanMemberListEventHandler(client_ClanMemberListReceived);
            client.ClanMembershipReceived += new ClanMembershipEventHandler(client_ClanMembershipReceived);
        }

        void client_ClanMembershipReceived(object sender, ClanMembershipEventArgs e)
        {
            ThreadStart update = delegate
            {
                lbClanMembers.Items.Clear();
                this.TabText = string.Format(CultureInfo.CurrentCulture, "Clan List: {0}", e.Tag);
            };
            if (InvokeRequired)
                BeginInvoke(update);
            else
                update();
        }

        void client_ClanMemberListReceived(object sender, ClanMemberListEventArgs e)
        {
            m_members = e.Members;

            ThreadStart update = delegate
            {
                this.lbClanMembers.BeginUpdate();

                foreach (ClanMember member in m_members)
                {
                    this.lbClanMembers.Items.Add(member);
                }

                this.lbClanMembers.EndUpdate();
            };

            if (InvokeRequired)
                BeginInvoke(update);
            else
                update();
        }
    }
}
