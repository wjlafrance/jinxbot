﻿namespace JinxBot.Views
{
    partial class FriendsList
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbFriends = new JinxBot.Views.Chat.CustomDrawnListBox();
            this.SuspendLayout();
            // 
            // lbFriends
            // 
            this.lbFriends.BackColor = System.Drawing.Color.Black;
            this.lbFriends.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFriends.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbFriends.ForeColor = System.Drawing.Color.White;
            this.lbFriends.FormattingEnabled = true;
            this.lbFriends.Location = new System.Drawing.Point(0, 0);
            this.lbFriends.Name = "lbFriends";
            this.lbFriends.Size = new System.Drawing.Size(292, 486);
            this.lbFriends.TabIndex = 1;
            // 
            // FriendsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 486);
            this.Controls.Add(this.lbFriends);
            this.Name = "FriendsList";
            this.TabText = "Friends";
            this.Text = "Friends";
            this.ResumeLayout(false);

        }

        #endregion

        private JinxBot.Views.Chat.CustomDrawnListBox lbFriends;




    }
}