
namespace Final_Project.GUI
{
    partial class BookInfoForm
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
            this.comboBoxBooks = new System.Windows.Forms.ComboBox();
            this.textBoxBookDescription = new System.Windows.Forms.TextBox();
            this.labelItems = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxBooks
            // 
            this.comboBoxBooks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBooks.FormattingEnabled = true;
            this.comboBoxBooks.Location = new System.Drawing.Point(45, 72);
            this.comboBoxBooks.Name = "comboBoxBooks";
            this.comboBoxBooks.Size = new System.Drawing.Size(359, 24);
            this.comboBoxBooks.TabIndex = 0;
            this.comboBoxBooks.SelectedIndexChanged += new System.EventHandler(this.comboBoxBooks_SelectedIndexChanged);
            // 
            // textBoxBookDescription
            // 
            this.textBoxBookDescription.Location = new System.Drawing.Point(45, 120);
            this.textBoxBookDescription.Multiline = true;
            this.textBoxBookDescription.Name = "textBoxBookDescription";
            this.textBoxBookDescription.Size = new System.Drawing.Size(359, 161);
            this.textBoxBookDescription.TabIndex = 1;
            // 
            // labelItems
            // 
            this.labelItems.AutoSize = true;
            this.labelItems.Location = new System.Drawing.Point(42, 39);
            this.labelItems.Name = "labelItems";
            this.labelItems.Size = new System.Drawing.Size(107, 17);
            this.labelItems.TabIndex = 13;
            this.labelItems.Text = "Select the Book";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(297, 287);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(106, 34);
            this.buttonClose.TabIndex = 14;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // BookInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 340);
            this.ControlBox = false;
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.labelItems);
            this.Controls.Add(this.textBoxBookDescription);
            this.Controls.Add(this.comboBoxBooks);
            this.Name = "BookInfoForm";
            this.Text = "Book Info Form";
            this.Load += new System.EventHandler(this.BookInfoForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxBooks;
        private System.Windows.Forms.TextBox textBoxBookDescription;
        private System.Windows.Forms.Label labelItems;
        private System.Windows.Forms.Button buttonClose;
    }
}