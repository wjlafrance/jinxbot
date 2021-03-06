﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JinxBot.Controls.Design;
using System.ComponentModel;

namespace JinxBot.Plugins
{
    /// <summary>
    /// When implemented, allows a plugin to handle startup and shutdown, and persist settings.
    /// </summary>
    /// <remarks>
    /// <para>All JinxBot plugins are required to implement this plugin at minimum.  It provides no functionality
    /// other than </para>
    /// </remarks>
    public interface IJinxBotPlugin : IDisposable
    {
        /// <summary>
        /// Causes a plugin to load its settings from a dictionary.
        /// </summary>
        /// <param name="settings">A dictionary containing the settings persisted from previous loads.</param>
        void Startup(IDictionary<string, string> settings);

        /// <summary>
        /// Causes a plugin to save its settings to a dictionary.
        /// </summary>
        /// <param name="settings"></param>
        void Shutdown(IDictionary<string, string> settings);

        /// <summary>
        /// Causes a plugin to present a settings object.
        /// </summary>
        /// <remarks>
        /// <para>This object is displayed to the user
        /// via a PropertyGrid.  Consequently, it may use any normal PropertyGrid descriptors, 
        /// such as a <see>DescriptionAttribute</see>, <see>BrowsableAttribute</see> or 
        /// <see>CategoryAttribute</see>, as well as <see>NameAttribute</see> from within 
        /// JinxBot.Controls.dll.</para>
        /// </remarks>
        /// <returns></returns>
        object GetSettingsObject();

        /// <summary>
        /// Causes a plugin to check whether it should be updated.
        /// </summary>
        /// <remarks>
        /// <para>During initialization and plugin discovery, JinxBot prompts all located plugins to determine whether they should be updated.  A plugin
        /// that does not support this capability should simply return <see langword="null" /> in all cases.</para>
        /// <para>JinxBot Beta 1 will not support automatic updates.  For this release, all plugins should 
        /// return <see langword="null" />.</para>
        /// </remarks>
        /// <returns>An object reference implementing <see>IPluginUpdateManifest</see> if updates are required;
        /// otherwise <see langword="null" />.</returns>
        IPluginUpdateManifest CheckForUpdates();
    }
}
