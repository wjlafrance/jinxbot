﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JinxBot.Plugins;
using BNSharp.BattleNet;
using JinxBot.Plugins.UI;
using JinxBot.Plugins.Data;
using JinxBot.Views;
using JinxBot.Plugins.BNSharp;
using JinxBot.Configuration;
using BNSharp.Plugins;

namespace JinxBot
{
    internal sealed class JinxBotClient : IJinxBotClient
    {
        private BattleNetClient m_client;
        private ProfileDocument m_window;
        private ProfileResourceProvider m_resourceProvider;
        private IJinxBotDatabase m_database;
        private ClientProfile m_profile;
        private Dictionary<ProfilePluginConfiguration, IJinxBotPlugin> m_activePlugins;
        private CommandTranslator m_cmdTranslator;

        private List<ICommandHandler> m_commandHandlers;

        public JinxBotClient(ClientProfile profile)
        {
            m_activePlugins = new Dictionary<ProfilePluginConfiguration, IJinxBotPlugin>();

            m_client = new BattleNetClient(profile);
            m_profile = profile;
            m_resourceProvider = ProfileResourceProvider.RegisterProvider(m_client);
            m_window = new ProfileDocument(m_client);

            m_cmdTranslator = new CommandTranslator(this);

            bool hasSetCommandQueue = false;
            
            // initialize plugins
            m_commandHandlers = new List<ICommandHandler>();
            foreach (ProfilePluginConfiguration pluginConfig in profile.PluginSettings)
            {
                hasSetCommandQueue = ProcessPlugin(hasSetCommandQueue, pluginConfig);
            }

            if (!hasSetCommandQueue)
            {
                m_client.CommandQueue = new TimedMessageQueue();
            }

            if (m_database == null)
                m_database = new JinxBotDefaultDatabase();
        }

        private bool ProcessPlugin(bool hasSetCommandQueue, ProfilePluginConfiguration pluginConfig)
        {
            IJinxBotPlugin plugin = PluginFactory.CreatePlugin(pluginConfig);
            m_activePlugins.Add(pluginConfig, plugin);

            // test if the plugin is a command queue
            ICommandQueue commandQueuePlugin = plugin as ICommandQueue;
            if (!hasSetCommandQueue && commandQueuePlugin != null)
            {
                m_client.CommandQueue = commandQueuePlugin;
                hasSetCommandQueue = true;
            }

            // test if the plugin is a database plugin
            IJinxBotDatabase databasePlugin = plugin as IJinxBotDatabase;
            if (databasePlugin != null)
                m_database = databasePlugin;

            // test if the plugin is a command handler
            ICommandHandler handler = plugin as ICommandHandler;
            if (handler != null)
                m_commandHandlers.Add(handler);

            // test if the plugin is multi-client
            IMultiClientPlugin mcp = plugin as IMultiClientPlugin;
            if (mcp != null)
                mcp.AddClient(this);

            return hasSetCommandQueue;
        }

        public void Close()
        {
            m_client.Close();
            
            m_cmdTranslator.Dispose();
            m_cmdTranslator = null;

            foreach (ProfilePluginConfiguration config in m_activePlugins.Keys)
            {
                IJinxBotPlugin plugin = m_activePlugins[config];
                IMultiClientPlugin mcp = plugin as IMultiClientPlugin;
                if (mcp != null)
                    mcp.RemoveClient(this);

                PluginFactory.ClosePluginInstance(config, plugin);
            }
        }

        #region IJinxBotClient Members

        public BattleNetClient Client
        {
            get { return m_client; }
        }

        public ProfileDocument ProfileDocument
        {
            get { return m_window; }
        }

        public IChatTab MainWindow
        {
            get { return m_window; }
        }

        public IJinxBotDatabase Database
        {
            get { return m_database; }
        }

        public void SendMessage(string message)
        {
            m_client.SendMessage(message);
        }

        internal IEnumerable<ICommandHandler> CommandHandlers
        {
            get
            {
                foreach (ICommandHandler handler in m_commandHandlers)
                    yield return handler;
            }
        }
        #endregion
    }
}