﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using BNSharp.Net;
using JinxBot.Views;
using JinxBot.Wizards;
using JinxBot.Configuration;
using System.Reflection;
using JinxBot.Plugins;
using JinxBot.Reliability;
using JinxBot.Plugins.UI;
using BNSharp.BattleNet;
using JinxBot.Plugins.BNSharp;

namespace JinxBot
{
    public partial class MainWindow : Form
    {
        private Dictionary<ClientProfile, JinxBotClient> m_activeClients;

        public MainWindow()
        {
            InitializeComponent();

            m_activeClients = new Dictionary<ClientProfile, JinxBotClient>();

            JinxBotConfiguration.Instance.ProfileAdded += new EventHandler(Instance_ProfileAdded);
            JinxBotConfiguration.Instance.ProfileRemoved += new EventHandler(Instance_ProfileRemoved);

            // show and hide again to ensure that web browser catches up
            GlobalErrorHandler.ErrorLog.Show(dock);
            //GlobalErrorHandler.ErrorLog.Hide();

            //this.menuStrip1.Renderer = new JinxBotMenuRenderer();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GlobalErrorHandler.ErrorLog.Hide();
        }

        void Instance_ProfileRemoved(object sender, EventArgs e)
        {
            RebindProfiles();
        }

        private void RebindProfiles()
        {
            ClearProfilesList();
            foreach (ClientProfile p in JinxBotConfiguration.Instance.Profiles)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(p.ProfileName);
                tsmi.Tag = p;
                tsmi.Click += new EventHandler(tsmi_Click);
                this.profilesToolStripMenuItem.DropDownItems.Add(tsmi);
            }
        }

        private void ClearProfilesList()
        {
            while (profilesToolStripMenuItem.DropDownItems.Count > 2)
            {
                profilesToolStripMenuItem.DropDownItems.RemoveAt(2);
            }
        }

        void tsmi_Click(object sender, EventArgs e)
        {
            ClientProfile cp = (sender as ToolStripMenuItem).Tag as ClientProfile;
            if (cp != null)
            {
                if (m_activeClients.ContainsKey(cp))
                {
                    m_activeClients[cp].ProfileDocument.Show();
                }
                else
                {
                    JinxBotClient client = new JinxBotClient(cp);
                    client.Client.Connected += client_Connected;
                    client.Client.Disconnected += client_Disconnected;
                    m_activeClients.Add(cp, client);
                    client.ProfileDocument.Show(this.dock);
                }
            }
        }

        void Instance_ProfileAdded(object sender, EventArgs e)
        {
            RebindProfiles();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProfileDocument pd = this.dock.ActiveDocument as ProfileDocument;
            if (pd != null)
            {
                pd.BeginConnecting();

                this.connectToolStripMenuItem1.Enabled = false;
                this.disconnectToolStripMenuItem.Enabled = true;
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            RebindProfiles();

            if (!JinxBotConfiguration.ConfigurationFileExists)
            {
                FirstRunWizard wizard = new FirstRunWizard();
                wizard.ShowDialog(this);
            }

            if (JinxBotConfiguration.Instance.Profiles.Length == 0)
            {
                CreateProfileWizard cpw = new CreateProfileWizard();
                cpw.ShowDialog(this);
            }
        }

        private delegate void SyncDel();

        private void client_Connected(object sender, EventArgs e)
        {
            BattleNetClient client = sender as BattleNetClient;
            if (client == null) return;
            ProfileDocument profileDoc = this.dock.ActiveDocument as ProfileDocument;
            if (profileDoc == null) return;
            if (profileDoc.Client == client)
            {
                SyncDel go = delegate
                {
                    this.connectToolStripMenuItem1.Enabled = false;
                    this.disconnectToolStripMenuItem.Enabled = true;
                };
                if (InvokeRequired)
                    BeginInvoke(go);
                else
                    go();
            }
        }

        private void client_Disconnected(object sender, EventArgs e)
        {
            BattleNetClient client = sender as BattleNetClient;
            if (client == null) return;
            ProfileDocument profileDoc = this.dock.ActiveDocument as ProfileDocument;
            if (profileDoc == null) return;
            if (profileDoc.Client == client)
            {
                SyncDel go = delegate
                {
                    this.connectToolStripMenuItem1.Enabled = true;
                    this.disconnectToolStripMenuItem.Enabled = false;
                };
                if (InvokeRequired)
                    BeginInvoke(go);
                else
                    go();
            }
        }

        private void newProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateProfileWizard cpw = new CreateProfileWizard();
            cpw.ShowDialog(this);
        }

        private void dock_ActiveDocumentChanged(object sender, EventArgs e)
        {
            ProfileDocument profileDoc = this.dock.ActiveDocument as ProfileDocument;
            if (profileDoc != null)
            {
                this.currentProfileNoneToolStripMenuItem.Enabled = true;
                this.enableVoidViewToolStripMenuItem.Checked = profileDoc.VoidView;
                this.connectToolStripMenuItem1.Enabled = !profileDoc.Client.IsConnected;
                this.disconnectToolStripMenuItem.Enabled = !connectToolStripMenuItem1.Enabled;
            }
            else
            {
                this.currentProfileNoneToolStripMenuItem.Enabled = false;
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProfileDocument pd = this.dock.ActiveDocument as ProfileDocument;
            if (pd != null)
            {
                if (pd.Client.IsConnected)
                    pd.Client.Close();

                ClientProfile profile = pd.Client.Settings as ClientProfile;
                pd.Close();

                JinxBotClient client = m_activeClients[profile];
                client.Client.Disconnected -= client_Disconnected;
                client.Client.Connected -= client_Connected;

                m_activeClients.Remove(profile);
            }
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProfileDocument pd = this.dock.ActiveDocument as ProfileDocument;
            if (pd != null)
            {
                pd.Disconnect();

                this.disconnectToolStripMenuItem.Enabled = false;
                this.connectToolStripMenuItem1.Enabled = true;
            }
        }

        private void displayErrorLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (displayErrorLogToolStripMenuItem.Checked)
            {
                GlobalErrorHandler.ErrorLog.Hide();
                displayErrorLogToolStripMenuItem.Checked = false;
            }
            else
            {
                GlobalErrorHandler.ErrorLog.Show(this.dock);
                displayErrorLogToolStripMenuItem.Checked = true;
            }
        }

        private void defaultStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IChatTab tab = this.dock.ActiveDocument as IChatTab;
            if (tab != null)
            {
                string jbControlsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JinxBot.Controls");
                string defaultPath = Path.Combine(jbControlsPath, "DefaultStyles.css");
                Uri styleUri = new Uri(string.Concat("file:///", defaultPath));
                tab.StylesheetUri = styleUri;
            }
        }

        private void blizzardStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IChatTab tab = this.dock.ActiveDocument as IChatTab;
            if (tab != null)
            {
                string jbControlsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JinxBot.Controls");
                string defaultPath = Path.Combine(jbControlsPath, "BlizzStyles.css");
                Uri styleUri = new Uri(string.Concat("file:///", defaultPath));
                tab.StylesheetUri = styleUri;
            }
        }

        private void enableVoidViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProfileDocument tab = this.dock.ActiveDocument as ProfileDocument;
            if (tab != null)
            {
                tab.VoidView = enableVoidViewToolStripMenuItem.Checked;
            }
        }
    }
}
