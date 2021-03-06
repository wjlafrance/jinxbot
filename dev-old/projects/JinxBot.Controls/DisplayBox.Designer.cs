﻿using System.Security.Permissions;
namespace JinxBot.Controls
{
    partial class DisplayBox
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ctx = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyAsPlainTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsHTMLTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsUBBCForumTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wpfHost = new System.Windows.Forms.Integration.ElementHost();
            this.display = new JinxBot.Controls.WpfDisplay();
            this.ctx.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctx
            // 
            this.ctx.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyAsPlainTextToolStripMenuItem,
            this.copyAsHTMLTextToolStripMenuItem,
            this.copyAsUBBCForumTextToolStripMenuItem});
            this.ctx.Name = "ctx";
            this.ctx.Size = new System.Drawing.Size(295, 92);
            // 
            // copyAsPlainTextToolStripMenuItem
            // 
            this.copyAsPlainTextToolStripMenuItem.Name = "copyAsPlainTextToolStripMenuItem";
            this.copyAsPlainTextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
                        | System.Windows.Forms.Keys.C)));
            this.copyAsPlainTextToolStripMenuItem.Size = new System.Drawing.Size(294, 22);
            this.copyAsPlainTextToolStripMenuItem.Text = "Copy as Plain &Text";
            this.copyAsPlainTextToolStripMenuItem.Click += new System.EventHandler(this.copyAsPlainTextToolStripMenuItem_Click);
            // 
            // copyAsHTMLTextToolStripMenuItem
            // 
            this.copyAsHTMLTextToolStripMenuItem.Name = "copyAsHTMLTextToolStripMenuItem";
            this.copyAsHTMLTextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyAsHTMLTextToolStripMenuItem.Size = new System.Drawing.Size(294, 22);
            this.copyAsHTMLTextToolStripMenuItem.Text = "&Copy as HTML Text";
            this.copyAsHTMLTextToolStripMenuItem.Click += new System.EventHandler(this.copyAsHTMLTextToolStripMenuItem_Click);
            // 
            // copyAsUBBCForumTextToolStripMenuItem
            // 
            this.copyAsUBBCForumTextToolStripMenuItem.Name = "copyAsUBBCForumTextToolStripMenuItem";
            this.copyAsUBBCForumTextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.C)));
            this.copyAsUBBCForumTextToolStripMenuItem.Size = new System.Drawing.Size(294, 22);
            this.copyAsUBBCForumTextToolStripMenuItem.Text = "Copy as &UBBC (Forum) Text";
            this.copyAsUBBCForumTextToolStripMenuItem.Click += new System.EventHandler(this.copyAsUBBCForumTextToolStripMenuItem_Click);
            // 
            // wpfHost
            // 
            this.wpfHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpfHost.Location = new System.Drawing.Point(0, 0);
            this.wpfHost.Name = "wpfHost";
            this.wpfHost.Size = new System.Drawing.Size(464, 229);
            this.wpfHost.TabIndex = 1;
            this.wpfHost.Text = "elementHost1";
            this.wpfHost.Child = this.display;
            // 
            // DisplayBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Controls.Add(this.wpfHost);
            this.Name = "DisplayBox";
            this.Size = new System.Drawing.Size(464, 229);
            this.ctx.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip ctx;
        private System.Windows.Forms.ToolStripMenuItem copyAsPlainTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAsHTMLTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAsUBBCForumTextToolStripMenuItem;
        private System.Windows.Forms.Integration.ElementHost wpfHost;
        private WpfDisplay display;
    }
}
