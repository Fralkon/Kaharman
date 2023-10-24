namespace Kaharman
{
    partial class Clubs
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
            dataGridView1 = new DataGridView();
            okButton = new Button();
            deleteButton = new Button();
            editButton = new Button();
            textBox1 = new TextBox();
            addButton = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.BackgroundColor = SystemColors.Window;
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(8, 7);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(229, 360);
            dataGridView1.TabIndex = 12;
            // 
            // okButton
            // 
            okButton.Location = new Point(353, 344);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 11;
            okButton.Text = "Ок";
            okButton.UseVisualStyleBackColor = true;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(243, 98);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(185, 23);
            deleteButton.TabIndex = 10;
            deleteButton.Text = "Удалить";
            deleteButton.UseVisualStyleBackColor = true;
            // 
            // editButton
            // 
            editButton.Location = new Point(243, 69);
            editButton.Name = "editButton";
            editButton.Size = new Size(185, 23);
            editButton.TabIndex = 9;
            editButton.Text = "Изменить";
            editButton.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(243, 11);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(185, 23);
            textBox1.TabIndex = 8;
            // 
            // addButton
            // 
            addButton.Location = new Point(243, 40);
            addButton.Name = "addButton";
            addButton.Size = new Size(185, 23);
            addButton.TabIndex = 7;
            addButton.Text = "Добавить";
            addButton.UseVisualStyleBackColor = true;
            // 
            // Clubs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(435, 375);
            Controls.Add(dataGridView1);
            Controls.Add(okButton);
            Controls.Add(deleteButton);
            Controls.Add(editButton);
            Controls.Add(textBox1);
            Controls.Add(addButton);
            Name = "Clubs";
            Text = "Клубы";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button okButton;
        private Button deleteButton;
        private Button editButton;
        private TextBox textBox1;
        private Button addButton;
    }
}