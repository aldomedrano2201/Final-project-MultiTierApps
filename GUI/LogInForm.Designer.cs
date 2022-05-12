
namespace Final_Project.GUI
{
    partial class LoginForm
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
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxpassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelUser = new System.Windows.Forms.Label();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.labelReEnterPassword = new System.Windows.Forms.Label();
            this.textBoxReEnterPassword = new System.Windows.Forms.TextBox();
            this.buttonUpdateUser = new System.Windows.Forms.Button();
            this.linkLabelAssignPassword = new System.Windows.Forms.LinkLabel();
            this.buttonExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(515, 199);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(109, 49);
            this.buttonLogin.TabIndex = 4;
            this.buttonLogin.TabStop = false;
            this.buttonLogin.Text = "Sign In";
            this.buttonLogin.UseVisualStyleBackColor = false;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(400, 199);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(109, 49);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.TabStop = false;
            this.buttonCancel.Text = "Clear";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxpassword
            // 
            this.textBoxpassword.Location = new System.Drawing.Point(207, 104);
            this.textBoxpassword.Name = "textBoxpassword";
            this.textBoxpassword.PasswordChar = '*';
            this.textBoxpassword.Size = new System.Drawing.Size(417, 22);
            this.textBoxpassword.TabIndex = 2;
            this.textBoxpassword.TabStop = false;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPassword.Location = new System.Drawing.Point(86, 104);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(106, 25);
            this.labelPassword.TabIndex = 3;
            this.labelPassword.Text = "Password";
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUser.Location = new System.Drawing.Point(135, 61);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(57, 25);
            this.labelUser.TabIndex = 5;
            this.labelUser.Text = "User";
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(207, 61);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(417, 22);
            this.textBoxUser.TabIndex = 1;
            this.textBoxUser.TabStop = false;
            this.textBoxUser.Text = "Enter your email";
            // 
            // labelReEnterPassword
            // 
            this.labelReEnterPassword.AutoSize = true;
            this.labelReEnterPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReEnterPassword.Location = new System.Drawing.Point(7, 142);
            this.labelReEnterPassword.Name = "labelReEnterPassword";
            this.labelReEnterPassword.Size = new System.Drawing.Size(185, 25);
            this.labelReEnterPassword.TabIndex = 7;
            this.labelReEnterPassword.Text = "Confirm password";
            this.labelReEnterPassword.Visible = false;
            // 
            // textBoxReEnterPassword
            // 
            this.textBoxReEnterPassword.Location = new System.Drawing.Point(207, 146);
            this.textBoxReEnterPassword.Name = "textBoxReEnterPassword";
            this.textBoxReEnterPassword.PasswordChar = '*';
            this.textBoxReEnterPassword.Size = new System.Drawing.Size(417, 22);
            this.textBoxReEnterPassword.TabIndex = 3;
            this.textBoxReEnterPassword.TabStop = false;
            this.textBoxReEnterPassword.Visible = false;
            // 
            // buttonUpdateUser
            // 
            this.buttonUpdateUser.Location = new System.Drawing.Point(515, 199);
            this.buttonUpdateUser.Name = "buttonUpdateUser";
            this.buttonUpdateUser.Size = new System.Drawing.Size(109, 49);
            this.buttonUpdateUser.TabIndex = 6;
            this.buttonUpdateUser.TabStop = false;
            this.buttonUpdateUser.Text = "Update User Info";
            this.buttonUpdateUser.UseVisualStyleBackColor = false;
            this.buttonUpdateUser.Visible = false;
            this.buttonUpdateUser.Click += new System.EventHandler(this.buttonUpdateUser_Click);
            // 
            // linkLabelAssignPassword
            // 
            this.linkLabelAssignPassword.AutoSize = true;
            this.linkLabelAssignPassword.Location = new System.Drawing.Point(414, 265);
            this.linkLabelAssignPassword.Name = "linkLabelAssignPassword";
            this.linkLabelAssignPassword.Size = new System.Drawing.Size(210, 17);
            this.linkLabelAssignPassword.TabIndex = 7;
            this.linkLabelAssignPassword.TabStop = true;
            this.linkLabelAssignPassword.Text = "Assign/update password to user";
            this.linkLabelAssignPassword.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAssignPassword_LinkClicked);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(285, 199);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(109, 49);
            this.buttonExit.TabIndex = 4;
            this.buttonExit.TabStop = false;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 386);
            this.ControlBox = false;
            this.Controls.Add(this.buttonUpdateUser);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.linkLabelAssignPassword);
            this.Controls.Add(this.labelReEnterPassword);
            this.Controls.Add(this.textBoxReEnterPassword);
            this.Controls.Add(this.labelUser);
            this.Controls.Add(this.textBoxUser);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.textBoxpassword);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login Form";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxpassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.Label labelReEnterPassword;
        private System.Windows.Forms.TextBox textBoxReEnterPassword;
        private System.Windows.Forms.Button buttonUpdateUser;
        private System.Windows.Forms.LinkLabel linkLabelAssignPassword;
        private System.Windows.Forms.Button buttonExit;
    }
}