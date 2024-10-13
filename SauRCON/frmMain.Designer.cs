namespace SauRCON
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnConnect = new System.Windows.Forms.Button();
            this.lvPlayers = new System.Windows.Forms.ListView();
            this.chId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chNetworkId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsPlayer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCopyNetworkId = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiKick = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAddAdmin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRemoveAdmin = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.tbCommand = new System.Windows.Forms.TextBox();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.pbFilter = new System.Windows.Forms.PictureBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lbLinkIn = new System.Windows.Forms.Label();
            this.lbLinkOut = new System.Windows.Forms.Label();
            this.cbShowBots = new System.Windows.Forms.CheckBox();
            this.cbShowSpectators = new System.Windows.Forms.CheckBox();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.tbHost = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.lbHostPort = new System.Windows.Forms.Label();
            this.lbPassword = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.ilLink = new System.Windows.Forms.ImageList(this.components);
            this.tmrLink = new System.Windows.Forms.Timer(this.components);
            this.bwLink = new System.ComponentModel.BackgroundWorker();
            this.lbSummary = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.cmsPlayer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFilter)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Image = ((System.Drawing.Image)(resources.GetObject("btnConnect.Image")));
            this.btnConnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConnect.Location = new System.Drawing.Point(14, 12);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.btnConnect.Size = new System.Drawing.Size(286, 60);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.toolTip.SetToolTip(this.btnConnect, "Connect to server using RCON");
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lvPlayers
            // 
            this.lvPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPlayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chId,
            this.chName,
            this.chNetworkId});
            this.lvPlayers.ContextMenuStrip = this.cmsPlayer;
            this.lvPlayers.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvPlayers.FullRowSelect = true;
            this.lvPlayers.HideSelection = false;
            this.lvPlayers.Location = new System.Drawing.Point(14, 123);
            this.lvPlayers.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lvPlayers.MultiSelect = false;
            this.lvPlayers.Name = "lvPlayers";
            this.lvPlayers.Size = new System.Drawing.Size(899, 207);
            this.lvPlayers.TabIndex = 1;
            this.toolTip.SetToolTip(this.lvPlayers, "Server Players - Double click entry to open information and copy network Id to cl" +
        "ipboard");
            this.lvPlayers.UseCompatibleStateImageBehavior = false;
            this.lvPlayers.View = System.Windows.Forms.View.Details;
            this.lvPlayers.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvPlayers_ColumnClick);
            this.lvPlayers.SelectedIndexChanged += new System.EventHandler(this.lvPlayers_SelectedIndexChanged);
            this.lvPlayers.DoubleClick += new System.EventHandler(this.lvPlayers_DoubleClick);
            // 
            // chId
            // 
            this.chId.Text = "Id #";
            // 
            // chName
            // 
            this.chName.Text = "Player Name";
            this.chName.Width = 400;
            // 
            // chNetworkId
            // 
            this.chNetworkId.Text = "Network Id";
            this.chNetworkId.Width = 200;
            // 
            // cmsPlayer
            // 
            this.cmsPlayer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopyNetworkId,
            this.tsmiSep2,
            this.tsmiKick,
            this.tsmiBan,
            this.tsmiSep1,
            this.tsmiAddAdmin,
            this.tsmiRemoveAdmin});
            this.cmsPlayer.Name = "cmsPlayer";
            this.cmsPlayer.Size = new System.Drawing.Size(233, 126);
            this.cmsPlayer.Opening += new System.ComponentModel.CancelEventHandler(this.cmsPlayer_Opening);
            // 
            // tsmiCopyNetworkId
            // 
            this.tsmiCopyNetworkId.Name = "tsmiCopyNetworkId";
            this.tsmiCopyNetworkId.Size = new System.Drawing.Size(232, 22);
            this.tsmiCopyNetworkId.Text = "Copy Network Id to Clipboard";
            this.tsmiCopyNetworkId.Click += new System.EventHandler(this.tsmiCopyNetworkId_Click);
            // 
            // tsmiSep2
            // 
            this.tsmiSep2.Name = "tsmiSep2";
            this.tsmiSep2.Size = new System.Drawing.Size(229, 6);
            // 
            // tsmiKick
            // 
            this.tsmiKick.Name = "tsmiKick";
            this.tsmiKick.Size = new System.Drawing.Size(232, 22);
            this.tsmiKick.Text = "Kick";
            this.tsmiKick.Click += new System.EventHandler(this.tsmiKick_Click);
            // 
            // tsmiBan
            // 
            this.tsmiBan.Name = "tsmiBan";
            this.tsmiBan.Size = new System.Drawing.Size(232, 22);
            this.tsmiBan.Text = "Ban";
            this.tsmiBan.Click += new System.EventHandler(this.tsmiBan_Click);
            // 
            // tsmiSep1
            // 
            this.tsmiSep1.Name = "tsmiSep1";
            this.tsmiSep1.Size = new System.Drawing.Size(229, 6);
            // 
            // tsmiAddAdmin
            // 
            this.tsmiAddAdmin.Name = "tsmiAddAdmin";
            this.tsmiAddAdmin.Size = new System.Drawing.Size(232, 22);
            this.tsmiAddAdmin.Text = "Add Admin";
            this.tsmiAddAdmin.Click += new System.EventHandler(this.tsmiAddAdmin_Click);
            // 
            // tsmiRemoveAdmin
            // 
            this.tsmiRemoveAdmin.Name = "tsmiRemoveAdmin";
            this.tsmiRemoveAdmin.Size = new System.Drawing.Size(232, 22);
            this.tsmiRemoveAdmin.Text = "Remove Admin";
            this.tsmiRemoveAdmin.Click += new System.EventHandler(this.tsmiRemoveAdmin_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnect.Image = ((System.Drawing.Image)(resources.GetObject("btnDisconnect.Image")));
            this.btnDisconnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDisconnect.Location = new System.Drawing.Point(308, 12);
            this.btnDisconnect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.btnDisconnect.Size = new System.Drawing.Size(287, 60);
            this.btnDisconnect.TabIndex = 2;
            this.btnDisconnect.Text = "Disconnect";
            this.toolTip.SetToolTip(this.btnDisconnect, "Disconnect from server");
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // tbOutput
            // 
            this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutput.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOutput.Location = new System.Drawing.Point(15, 378);
            this.tbOutput.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ReadOnly = true;
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOutput.Size = new System.Drawing.Size(898, 164);
            this.tbOutput.TabIndex = 3;
            this.toolTip.SetToolTip(this.tbOutput, "RCON output");
            // 
            // tbCommand
            // 
            this.tbCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCommand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbCommand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbCommand.Enabled = false;
            this.tbCommand.Location = new System.Drawing.Point(15, 544);
            this.tbCommand.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbCommand.Name = "tbCommand";
            this.tbCommand.Size = new System.Drawing.Size(898, 21);
            this.tbCommand.TabIndex = 4;
            this.toolTip.SetToolTip(this.tbCommand, "RCON command line");
            this.tbCommand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCommand_KeyDown);
            // 
            // tbFilter
            // 
            this.tbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilter.Location = new System.Drawing.Point(728, 96);
            this.tbFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(185, 21);
            this.tbFilter.TabIndex = 5;
            this.toolTip.SetToolTip(this.tbFilter, "Filter by Id, partial or full name or Network Id");
            this.tbFilter.TextChanged += new System.EventHandler(this.tbFilter_TextChanged);
            // 
            // pbFilter
            // 
            this.pbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbFilter.Image = ((System.Drawing.Image)(resources.GetObject("pbFilter.Image")));
            this.pbFilter.InitialImage = null;
            this.pbFilter.Location = new System.Drawing.Point(690, 96);
            this.pbFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pbFilter.Name = "pbFilter";
            this.pbFilter.Size = new System.Drawing.Size(24, 24);
            this.pbFilter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbFilter.TabIndex = 6;
            this.pbFilter.TabStop = false;
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 250;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Enabled = false;
            this.btnRefresh.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(789, 336);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Padding = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.btnRefresh.Size = new System.Drawing.Size(125, 36);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.btnRefresh, "Refresh using \"status\"");
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lbLinkIn
            // 
            this.lbLinkIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLinkIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLinkIn.Enabled = false;
            this.lbLinkIn.Image = ((System.Drawing.Image)(resources.GetObject("lbLinkIn.Image")));
            this.lbLinkIn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbLinkIn.Location = new System.Drawing.Point(446, 336);
            this.lbLinkIn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbLinkIn.Name = "lbLinkIn";
            this.lbLinkIn.Padding = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.lbLinkIn.Size = new System.Drawing.Size(165, 36);
            this.lbLinkIn.TabIndex = 15;
            this.lbLinkIn.Text = "In Link Inactive";
            this.lbLinkIn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.lbLinkIn, "Is the game to tool link active");
            // 
            // lbLinkOut
            // 
            this.lbLinkOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLinkOut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLinkOut.Enabled = false;
            this.lbLinkOut.Image = ((System.Drawing.Image)(resources.GetObject("lbLinkOut.Image")));
            this.lbLinkOut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbLinkOut.Location = new System.Drawing.Point(617, 336);
            this.lbLinkOut.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbLinkOut.Name = "lbLinkOut";
            this.lbLinkOut.Padding = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.lbLinkOut.Size = new System.Drawing.Size(165, 36);
            this.lbLinkOut.TabIndex = 16;
            this.lbLinkOut.Text = "Out Link Inactive";
            this.lbLinkOut.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.lbLinkOut, "Is the tool to game link active");
            // 
            // cbShowBots
            // 
            this.cbShowBots.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbShowBots.AutoSize = true;
            this.cbShowBots.Location = new System.Drawing.Point(465, 100);
            this.cbShowBots.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbShowBots.Name = "cbShowBots";
            this.cbShowBots.Size = new System.Drawing.Size(86, 17);
            this.cbShowBots.TabIndex = 7;
            this.cbShowBots.Text = "Show Bots";
            this.cbShowBots.UseVisualStyleBackColor = true;
            this.cbShowBots.CheckedChanged += new System.EventHandler(this.cbShowBots_CheckedChanged);
            // 
            // cbShowSpectators
            // 
            this.cbShowSpectators.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbShowSpectators.AutoSize = true;
            this.cbShowSpectators.Checked = true;
            this.cbShowSpectators.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowSpectators.Location = new System.Drawing.Point(561, 100);
            this.cbShowSpectators.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbShowSpectators.Name = "cbShowSpectators";
            this.cbShowSpectators.Size = new System.Drawing.Size(122, 17);
            this.cbShowSpectators.TabIndex = 8;
            this.cbShowSpectators.Text = "Show Spectators";
            this.cbShowSpectators.UseVisualStyleBackColor = true;
            this.cbShowSpectators.CheckedChanged += new System.EventHandler(this.cbShowSpectators_CheckedChanged);
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Interval = 60000;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // tbHost
            // 
            this.tbHost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHost.Location = new System.Drawing.Point(728, 12);
            this.tbHost.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbHost.Name = "tbHost";
            this.tbHost.Size = new System.Drawing.Size(125, 21);
            this.tbHost.TabIndex = 10;
            this.tbHost.Text = "localhost";
            // 
            // tbPort
            // 
            this.tbPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPort.Location = new System.Drawing.Point(859, 12);
            this.tbPort.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(54, 21);
            this.tbPort.TabIndex = 11;
            this.tbPort.Text = "7778";
            // 
            // lbHostPort
            // 
            this.lbHostPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbHostPort.Location = new System.Drawing.Point(646, 15);
            this.lbHostPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbHostPort.Name = "lbHostPort";
            this.lbHostPort.Size = new System.Drawing.Size(76, 13);
            this.lbHostPort.TabIndex = 12;
            this.lbHostPort.Text = "Host && Port:";
            // 
            // lbPassword
            // 
            this.lbPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPassword.Location = new System.Drawing.Point(646, 42);
            this.lbPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(76, 13);
            this.lbPassword.TabIndex = 14;
            this.lbPassword.Text = "Password:";
            this.lbPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbPassword
            // 
            this.tbPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPassword.Location = new System.Drawing.Point(728, 39);
            this.tbPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(125, 21);
            this.tbPassword.TabIndex = 13;
            this.tbPassword.Text = "lol";
            // 
            // ilLink
            // 
            this.ilLink.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilLink.ImageStream")));
            this.ilLink.TransparentColor = System.Drawing.Color.Transparent;
            this.ilLink.Images.SetKeyName(0, "window-icon-24.png");
            this.ilLink.Images.SetKeyName(1, "window-refresh-icon-24.png");
            // 
            // tmrLink
            // 
            this.tmrLink.Interval = 2500;
            this.tmrLink.Tick += new System.EventHandler(this.tmrLink_Tick);
            // 
            // bwLink
            // 
            this.bwLink.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLink_DoWork);
            // 
            // lbSummary
            // 
            this.lbSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbSummary.Location = new System.Drawing.Point(12, 333);
            this.lbSummary.Name = "lbSummary";
            this.lbSummary.Size = new System.Drawing.Size(416, 25);
            this.lbSummary.TabIndex = 17;
            // 
            // lbTitle
            // 
            this.lbTitle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.Location = new System.Drawing.Point(12, 95);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(416, 25);
            this.lbTitle.TabIndex = 18;
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 576);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.lbSummary);
            this.Controls.Add(this.lbLinkOut);
            this.Controls.Add(this.lbLinkIn);
            this.Controls.Add(this.lbPassword);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.lbHostPort);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.tbHost);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.cbShowSpectators);
            this.Controls.Add(this.cbShowBots);
            this.Controls.Add(this.pbFilter);
            this.Controls.Add(this.tbFilter);
            this.Controls.Add(this.tbCommand);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.lvPlayers);
            this.Controls.Add(this.btnConnect);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SauRCON";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.cmsPlayer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbFilter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ListView lvPlayers;
        private System.Windows.Forms.ColumnHeader chId;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chNetworkId;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.TextBox tbCommand;
        private System.Windows.Forms.TextBox tbFilter;
        private System.Windows.Forms.PictureBox pbFilter;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ContextMenuStrip cmsPlayer;
        private System.Windows.Forms.ToolStripMenuItem tsmiKick;
        private System.Windows.Forms.ToolStripMenuItem tsmiBan;
        private System.Windows.Forms.ToolStripSeparator tsmiSep1;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddAdmin;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoveAdmin;
        private System.Windows.Forms.CheckBox cbShowBots;
        private System.Windows.Forms.CheckBox cbShowSpectators;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.TextBox tbHost;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label lbHostPort;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyNetworkId;
        private System.Windows.Forms.ToolStripSeparator tsmiSep2;
        private System.Windows.Forms.Label lbLinkIn;
        private System.Windows.Forms.ImageList ilLink;
        private System.Windows.Forms.Timer tmrLink;
        private System.ComponentModel.BackgroundWorker bwLink;
        private System.Windows.Forms.Label lbLinkOut;
        private System.Windows.Forms.Label lbSummary;
        private System.Windows.Forms.Label lbTitle;
    }
}

