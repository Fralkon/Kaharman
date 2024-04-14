namespace Kaharman
{
    partial class TournamentGridForm
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
            button1 = new Button();
            button2 = new Button();
            dateTimePicker1 = new DateTimePicker();
            menuStrip1 = new MenuStrip();
            nameTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            textBox2 = new TextBox();
            label3 = new Label();
            dataGridView1 = new DataGridView();
            dataGridView2 = new DataGridView();
            label4 = new Label();
            label5 = new Label();
            button3 = new Button();
            button4 = new Button();
            panel1 = new Panel();
            progressBar1 = new ProgressBar();
            panel2 = new Panel();
            label6 = new Label();
            numberProtocol = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.Location = new Point(425, 519);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Создать";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button2.Location = new Point(326, 519);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 1;
            button2.Text = "Отмена";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dateTimePicker1.Location = new Point(130, 57);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(206, 23);
            dateTimePicker1.TabIndex = 2;
            // 
            // menuStrip1
            // 
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1104, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // nameTextBox
            // 
            nameTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            nameTextBox.Location = new Point(113, 57);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(406, 23);
            nameTextBox.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 60);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 5;
            label1.Text = "Наименование";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 31);
            label2.Name = "label2";
            label2.Size = new Size(87, 15);
            label2.TabIndex = 7;
            label2.Text = "Соревнование";
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox2.Enabled = false;
            textBox2.Location = new Point(113, 28);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(406, 23);
            textBox2.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 60);
            label3.Name = "label3";
            label3.Size = new Size(100, 15);
            label3.TabIndex = 8;
            label3.Text = "Дата проведения";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.BackgroundColor = SystemColors.Window;
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.Location = new Point(17, 119);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(504, 369);
            dataGridView1.TabIndex = 15;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            // 
            // dataGridView2
            // 
            dataGridView2.AllowDrop = true;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.AllowUserToResizeColumns = false;
            dataGridView2.AllowUserToResizeRows = false;
            dataGridView2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.BackgroundColor = SystemColors.Window;
            dataGridView2.BorderStyle = BorderStyle.Fixed3D;
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(15, 119);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.ReadOnly = true;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.RowTemplate.Height = 25;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.Size = new Size(484, 369);
            dataGridView2.TabIndex = 19;
            dataGridView2.CellDoubleClick += dataGridView2_CellDoubleClick;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 101);
            label4.Name = "label4";
            label4.Size = new Size(98, 15);
            label4.TabIndex = 20;
            label4.Text = "Участники сетки";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(14, 101);
            label5.Name = "label5";
            label5.Size = new Size(86, 15);
            label5.TabIndex = 21;
            label5.Text = "Все участники";
            // 
            // button3
            // 
            button3.Location = new Point(540, 275);
            button3.Name = "button3";
            button3.Size = new Size(46, 25);
            button3.TabIndex = 22;
            button3.Text = ">";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(540, 304);
            button4.Name = "button4";
            button4.Size = new Size(46, 25);
            button4.TabIndex = 23;
            button4.Text = "<";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(progressBar1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(nameTextBox);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(dataGridView1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 24);
            panel1.Name = "panel1";
            panel1.Size = new Size(534, 557);
            panel1.TabIndex = 24;
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            progressBar1.Location = new Point(282, 90);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(239, 23);
            progressBar1.TabIndex = 22;
            progressBar1.Visible = false;
            // 
            // panel2
            // 
            panel2.AutoScroll = true;
            panel2.Controls.Add(numberProtocol);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(dateTimePicker1);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(dataGridView2);
            panel2.Controls.Add(label4);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(589, 24);
            panel2.Name = "panel2";
            panel2.Size = new Size(515, 557);
            panel2.TabIndex = 25;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(15, 31);
            label6.Name = "label6";
            label6.Size = new Size(107, 15);
            label6.TabIndex = 21;
            label6.Text = "Номер протокола";
            // 
            // numberProtocol
            // 
            numberProtocol.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            numberProtocol.Location = new Point(130, 28);
            numberProtocol.Name = "numberProtocol";
            numberProtocol.Size = new Size(206, 23);
            numberProtocol.TabIndex = 23;
            // 
            // TournamentGridForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1104, 581);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "TournamentGridForm";
            Text = "Создание сетки";
            Load += TournamentGridForm_Load;
            Resize += TournamentGridForm_Resize;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private DateTimePicker dateTimePicker1;
        private MenuStrip menuStrip1;
        private TextBox nameTextBox;
        private Label label1;
        private Label label2;
        private TextBox textBox2;
        private Label label3;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private Label label4;
        private Label label5;
        private Button button3;
        private Button button4;
        private Panel panel1;
        private Panel panel2;
        private ProgressBar progressBar1;
        private Label label6;
        private TextBox numberProtocol;
    }
}