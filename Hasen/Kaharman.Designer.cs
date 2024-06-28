namespace Hasen
{
    partial class Kaharman
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Kaharman));
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            выходToolStripMenuItem = new ToolStripMenuItem();
            турнирыToolStripMenuItem = new ToolStripMenuItem();
            создатьToolStripMenuItem = new ToolStripMenuItem();
            историяТурнировToolStripMenuItem = new ToolStripMenuItem();
            участникиToolStripMenuItem = new ToolStripMenuItem();
            создатьToolStripMenuItem1 = new ToolStripMenuItem();
            базаДанныхToolStripMenuItem = new ToolStripMenuItem();
            ParticipantView = new DataGridView();
            tournamentContextMenuStrip = new ContextMenuStrip(components);
            создатьТурнирToolStripMenuItem = new ToolStripMenuItem();
            удалитьToolStripMenuItem = new ToolStripMenuItem();
            TournamentView = new DataGridView();
            DataparticipantContextMenuStrip = new ContextMenuStrip(components);
            создатьToolStripMenuItem2 = new ToolStripMenuItem();
            удалитьToolStripMenuItem1 = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ParticipantView).BeginInit();
            tournamentContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TournamentView).BeginInit();
            DataparticipantContextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, турнирыToolStripMenuItem, участникиToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(834, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { выходToolStripMenuItem });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(48, 20);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // выходToolStripMenuItem
            // 
            выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            выходToolStripMenuItem.Size = new Size(109, 22);
            выходToolStripMenuItem.Text = "Выход";
            выходToolStripMenuItem.Click += выходToolStripMenuItem_Click;
            // 
            // турнирыToolStripMenuItem
            // 
            турнирыToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { создатьToolStripMenuItem, историяТурнировToolStripMenuItem });
            турнирыToolStripMenuItem.Name = "турнирыToolStripMenuItem";
            турнирыToolStripMenuItem.Size = new Size(59, 20);
            турнирыToolStripMenuItem.Text = "Турнир";
            // 
            // создатьToolStripMenuItem
            // 
            создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            создатьToolStripMenuItem.Size = new Size(176, 22);
            создатьToolStripMenuItem.Text = "Создать";
            создатьToolStripMenuItem.Click += создатьToolStripMenuItem_Click;
            // 
            // историяТурнировToolStripMenuItem
            // 
            историяТурнировToolStripMenuItem.Name = "историяТурнировToolStripMenuItem";
            историяТурнировToolStripMenuItem.Size = new Size(176, 22);
            историяТурнировToolStripMenuItem.Text = "История турниров";
            историяТурнировToolStripMenuItem.Click += историяТурнировToolStripMenuItem_Click;
            // 
            // участникиToolStripMenuItem
            // 
            участникиToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { создатьToolStripMenuItem1, базаДанныхToolStripMenuItem });
            участникиToolStripMenuItem.Name = "участникиToolStripMenuItem";
            участникиToolStripMenuItem.Size = new Size(77, 20);
            участникиToolStripMenuItem.Text = "Участники";
            // 
            // создатьToolStripMenuItem1
            // 
            создатьToolStripMenuItem1.Name = "создатьToolStripMenuItem1";
            создатьToolStripMenuItem1.Size = new Size(180, 22);
            создатьToolStripMenuItem1.Text = "Создать";
            создатьToolStripMenuItem1.Click += создатьToolStripMenuItem1_Click;
            // 
            // базаДанныхToolStripMenuItem
            // 
            базаДанныхToolStripMenuItem.Name = "базаДанныхToolStripMenuItem";
            базаДанныхToolStripMenuItem.Size = new Size(180, 22);
            базаДанныхToolStripMenuItem.Text = "База данных";
            базаДанныхToolStripMenuItem.Click += базаДанныхToolStripMenuItem_Click_1;
            // 
            // ParticipantView
            // 
            ParticipantView.AllowDrop = true;
            ParticipantView.AllowUserToAddRows = false;
            ParticipantView.AllowUserToDeleteRows = false;
            ParticipantView.AllowUserToResizeColumns = false;
            ParticipantView.AllowUserToResizeRows = false;
            ParticipantView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ParticipantView.BackgroundColor = SystemColors.Window;
            ParticipantView.BorderStyle = BorderStyle.Fixed3D;
            ParticipantView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            ParticipantView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ParticipantView.Dock = DockStyle.Fill;
            ParticipantView.Location = new Point(0, 24);
            ParticipantView.Name = "ParticipantView";
            ParticipantView.ReadOnly = true;
            ParticipantView.RowHeadersVisible = false;
            ParticipantView.RowTemplate.Height = 25;
            ParticipantView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ParticipantView.Size = new Size(834, 494);
            ParticipantView.TabIndex = 1;
            // 
            // tournamentContextMenuStrip
            // 
            tournamentContextMenuStrip.Items.AddRange(new ToolStripItem[] { создатьТурнирToolStripMenuItem, удалитьToolStripMenuItem });
            tournamentContextMenuStrip.Name = "contextMenuStrip1";
            tournamentContextMenuStrip.Size = new Size(119, 48);
            // 
            // создатьТурнирToolStripMenuItem
            // 
            создатьТурнирToolStripMenuItem.Name = "создатьТурнирToolStripMenuItem";
            создатьТурнирToolStripMenuItem.Size = new Size(118, 22);
            создатьТурнирToolStripMenuItem.Text = "Создать";
            создатьТурнирToolStripMenuItem.Click += создатьToolStripMenuItem_Click;
            // 
            // удалитьToolStripMenuItem
            // 
            удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            удалитьToolStripMenuItem.Size = new Size(118, 22);
            удалитьToolStripMenuItem.Text = "Удалить";
            удалитьToolStripMenuItem.Click += удалитьToolStripMenuItem_Click;
            // 
            // TournamentView
            // 
            TournamentView.AllowDrop = true;
            TournamentView.AllowUserToAddRows = false;
            TournamentView.AllowUserToDeleteRows = false;
            TournamentView.AllowUserToResizeColumns = false;
            TournamentView.AllowUserToResizeRows = false;
            TournamentView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            TournamentView.BackgroundColor = SystemColors.Window;
            TournamentView.BorderStyle = BorderStyle.Fixed3D;
            TournamentView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            TournamentView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            TournamentView.ContextMenuStrip = DataparticipantContextMenuStrip;
            TournamentView.Dock = DockStyle.Fill;
            TournamentView.Location = new Point(0, 24);
            TournamentView.Name = "TournamentView";
            TournamentView.ReadOnly = true;
            TournamentView.RowHeadersVisible = false;
            TournamentView.RowTemplate.Height = 25;
            TournamentView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            TournamentView.Size = new Size(834, 494);
            TournamentView.TabIndex = 2;
            TournamentView.CellMouseDoubleClick += TournamentView_CellMouseDoubleClick;
            TournamentView.VisibleChanged += TournamentView_VisibleChanged;
            // 
            // DataparticipantContextMenuStrip
            // 
            DataparticipantContextMenuStrip.Items.AddRange(new ToolStripItem[] { создатьToolStripMenuItem2, удалитьToolStripMenuItem1 });
            DataparticipantContextMenuStrip.Name = "contextMenuStrip2";
            DataparticipantContextMenuStrip.Size = new Size(119, 48);
            // 
            // создатьToolStripMenuItem2
            // 
            создатьToolStripMenuItem2.Name = "создатьToolStripMenuItem2";
            создатьToolStripMenuItem2.Size = new Size(118, 22);
            создатьToolStripMenuItem2.Text = "Создать";
            создатьToolStripMenuItem2.Click += создатьToolStripMenuItem_Click;
            // 
            // удалитьToolStripMenuItem1
            // 
            удалитьToolStripMenuItem1.Name = "удалитьToolStripMenuItem1";
            удалитьToolStripMenuItem1.Size = new Size(118, 22);
            удалитьToolStripMenuItem1.Text = "Удалить";
            удалитьToolStripMenuItem1.Click += удалитьToolStripMenuItem1_Click;
            // 
            // Kaharman
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(834, 518);
            Controls.Add(TournamentView);
            Controls.Add(ParticipantView);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "Kaharman";
            Text = "Kaharman";
            Activated += Kaharman_Activated;
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ParticipantView).EndInit();
            tournamentContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)TournamentView).EndInit();
            DataparticipantContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem турнирыToolStripMenuItem;
        private ToolStripMenuItem участникиToolStripMenuItem;
        private DataGridView ParticipantView;
        private ToolStripMenuItem выходToolStripMenuItem;
        private ToolStripMenuItem историяТурнировToolStripMenuItem;
        private ToolStripMenuItem базаДанныхToolStripMenuItem;
        private ContextMenuStrip tournamentContextMenuStrip;
        private ToolStripMenuItem создатьТурнирToolStripMenuItem;
        private ToolStripMenuItem удалитьToolStripMenuItem;
        private ToolStripMenuItem создатьToolStripMenuItem1;
        private ToolStripMenuItem создатьToolStripMenuItem;
        private DataGridView TournamentView;
        private ContextMenuStrip DataparticipantContextMenuStrip;
        private ToolStripMenuItem создатьToolStripMenuItem2;
        private ToolStripMenuItem удалитьToolStripMenuItem1;
    }
}