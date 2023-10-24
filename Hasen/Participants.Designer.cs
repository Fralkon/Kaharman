namespace Hasen
{
    partial class Participants
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
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.AllowDrop = true;
            textBox1.Location = new Point(170, 61);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(183, 23);
            textBox1.TabIndex = 0;
            textBox1.DragDrop += Participants_DragDrop;
            textBox1.DragOver += Participants_DragOver;
            textBox1.DragLeave += Participants_DragLeave;
            // 
            // Participants
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(404, 390);
            Controls.Add(textBox1);
            Name = "Participants";
            Text = "Participants";
            DragDrop += Participants_DragDrop;
            DragOver += Participants_DragOver;
            DragLeave += Participants_DragLeave;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
    }
}