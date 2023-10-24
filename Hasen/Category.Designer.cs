namespace Kaharman
{
    partial class Category
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
            genderComboBox = new ComboBox();
            okButton = new Button();
            addButton = new Button();
            editButton = new Button();
            deleteButton = new Button();
            minTextBox = new TextBox();
            maxTextBox = new TextBox();
            label1 = new Label();
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
            dataGridView1.Location = new Point(12, 42);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(229, 360);
            dataGridView1.TabIndex = 13;
            // 
            // genderComboBox
            // 
            genderComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            genderComboBox.FormattingEnabled = true;
            genderComboBox.Location = new Point(12, 13);
            genderComboBox.Name = "genderComboBox";
            genderComboBox.Size = new Size(229, 23);
            genderComboBox.TabIndex = 14;
            genderComboBox.SelectedIndexChanged += genderComboBox_SelectedIndexChanged;
            // 
            // okButton
            // 
            okButton.Location = new Point(335, 379);
            okButton.Name = "okButton";
            okButton.Size = new Size(81, 23);
            okButton.TabIndex = 15;
            okButton.Text = "Ок";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // addButton
            // 
            addButton.Location = new Point(258, 100);
            addButton.Name = "addButton";
            addButton.Size = new Size(158, 23);
            addButton.TabIndex = 16;
            addButton.Text = "Добавить";
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += addButton_Click;
            // 
            // editButton
            // 
            editButton.Location = new Point(258, 129);
            editButton.Name = "editButton";
            editButton.Size = new Size(158, 23);
            editButton.TabIndex = 17;
            editButton.Text = "Изменить";
            editButton.UseVisualStyleBackColor = true;
            editButton.Click += editButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(258, 158);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(158, 23);
            deleteButton.TabIndex = 18;
            deleteButton.Text = "Удалить";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click;
            // 
            // minTextBox
            // 
            minTextBox.Location = new Point(258, 71);
            minTextBox.Name = "minTextBox";
            minTextBox.Size = new Size(66, 23);
            minTextBox.TabIndex = 19;
            // 
            // maxTextBox
            // 
            maxTextBox.Location = new Point(348, 71);
            maxTextBox.Name = "maxTextBox";
            maxTextBox.Size = new Size(68, 23);
            maxTextBox.TabIndex = 20;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(330, 74);
            label1.Name = "label1";
            label1.Size = new Size(12, 15);
            label1.TabIndex = 21;
            label1.Text = "-";
            // 
            // Category
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(430, 412);
            Controls.Add(label1);
            Controls.Add(maxTextBox);
            Controls.Add(minTextBox);
            Controls.Add(deleteButton);
            Controls.Add(editButton);
            Controls.Add(addButton);
            Controls.Add(okButton);
            Controls.Add(genderComboBox);
            Controls.Add(dataGridView1);
            Name = "Category";
            Text = "Весовые категории";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private ComboBox genderComboBox;
        private Button okButton;
        private Button addButton;
        private Button editButton;
        private Button deleteButton;
        private TextBox minTextBox;
        private TextBox maxTextBox;
        private Label label1;
    }
}