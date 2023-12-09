namespace Kaharman
{
    partial class GridForm
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
            panel1 = new Panel();
            dateStart = new Label();
            nameTournamet = new Label();
            nameGrid = new Label();
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            печатьToolStripMenuItem = new ToolStripMenuItem();
            протоколСоревнованияToolStripMenuItem = new ToolStripMenuItem();
            выходToolStripMenuItem = new ToolStripMenuItem();
            panel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top;
            panel1.AutoScroll = true;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(dateStart);
            panel1.Controls.Add(nameTournamet);
            panel1.Controls.Add(nameGrid);
            panel1.Location = new Point(128, 30);
            panel1.Name = "panel1";
            panel1.Size = new Size(1155, 425);
            panel1.TabIndex = 0;
            // 
            // dateStart
            // 
            dateStart.AutoSize = true;
            dateStart.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dateStart.Location = new Point(550, 60);
            dateStart.Name = "dateStart";
            dateStart.Size = new Size(43, 17);
            dateStart.TabIndex = 5;
            dateStart.Text = "label2";
            // 
            // nameTournamet
            // 
            nameTournamet.AutoSize = true;
            nameTournamet.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            nameTournamet.Location = new Point(550, 10);
            nameTournamet.Name = "nameTournamet";
            nameTournamet.Size = new Size(52, 21);
            nameTournamet.TabIndex = 4;
            nameTournamet.Text = "label2";
            // 
            // nameGrid
            // 
            nameGrid.AutoSize = true;
            nameGrid.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            nameGrid.Location = new Point(550, 34);
            nameGrid.Name = "nameGrid";
            nameGrid.Size = new Size(52, 21);
            nameGrid.TabIndex = 3;
            nameGrid.Text = "label2";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1408, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { печатьToolStripMenuItem, протоколСоревнованияToolStripMenuItem, выходToolStripMenuItem });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(48, 20);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // печатьToolStripMenuItem
            // 
            печатьToolStripMenuItem.Name = "печатьToolStripMenuItem";
            печатьToolStripMenuItem.Size = new Size(210, 22);
            печатьToolStripMenuItem.Text = "Печать сетки";
            печатьToolStripMenuItem.Click += печатьToolStripMenuItem_Click;
            // 
            // протоколСоревнованияToolStripMenuItem
            // 
            протоколСоревнованияToolStripMenuItem.Name = "протоколСоревнованияToolStripMenuItem";
            протоколСоревнованияToolStripMenuItem.Size = new Size(210, 22);
            протоколСоревнованияToolStripMenuItem.Text = "Протокол соревнования";
            протоколСоревнованияToolStripMenuItem.Click += протоколСоревнованияToolStripMenuItem_Click;
            // 
            // выходToolStripMenuItem
            // 
            выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            выходToolStripMenuItem.Size = new Size(210, 22);
            выходToolStripMenuItem.Text = "Выход";
            выходToolStripMenuItem.Click += выходToolStripMenuItem_Click;
            // 
            // GridForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(1408, 563);
            Controls.Add(panel1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "GridForm";
            Text = "Турнирная сетка";
            FormClosing += TournamentGrid_FormClosing;
            Resize += GridForm_Resize;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem выходToolStripMenuItem;
        private ToolStripMenuItem печатьToolStripMenuItem;
        private Label nameGrid;
        private Label nameTournamet;
        private ToolStripMenuItem протоколСоревнованияToolStripMenuItem;
        private Label dateStart;
    }
}