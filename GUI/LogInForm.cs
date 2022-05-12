using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Final_Project.BLL;

namespace Final_Project.GUI
{
    public partial class LoginForm : Form
    {
        public static string SetValueForJobPosition = "";
        public static int userEmpId;
        public int tries = 1;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            textBoxUser.Focus();
            this.AcceptButton = buttonLogin;
            textBoxUser.GotFocus += new EventHandler(this.TextGotFocus);
            textBoxUser.LostFocus += new EventHandler(this.TextLostFocus);
        }

        public void TextGotFocus(object sender, EventArgs e)
        {

            if (textBoxUser.Text == "Enter your email")
            {
                textBoxUser.Text = "";
                textBoxUser.ForeColor = Color.Black;
            }
        }

        public void TextLostFocus(object sender, EventArgs e)
        {

            if (textBoxUser.Text == "")
            {
                textBoxUser.Text = "Enter your email";
                textBoxUser.ForeColor = Color.LightGray;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            textBoxUser.Text = "";
            textBoxpassword.Text = "";
            textBoxReEnterPassword.Text = "";
            
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            
            if (textBoxpassword.Text == "" && textBoxUser.Text == "")
            {
                MessageBox.Show("Please enter a valid user and password", "Invalid user/password");
            }
            else if (textBoxpassword.Text == "" || textBoxUser.Text == "")
            {
                MessageBox.Show("User or password fields are empty", "Invalid user/password");
            }

            else if (textBoxUser.Text != "" && textBoxpassword.Text != "")
            {
                List<string> userEmp = new List<string>();
                User user = new User();
                userEmp = user.CheckUser(textBoxUser.Text, textBoxpassword.Text, 0);

                if (userEmp == null)
                {
                    MessageBox.Show("User/Password invalid", "Invalid user/password");
                    tries = tries + 1;
                }
                else
                {
                    if (userEmp[2] == "2")
                    {
                        MessageBox.Show("User is inactive", "Invalid user");
                        return;
                    }
                    else
                    {
                        //set job position id for validate user acces on the hightech management form
                        userEmpId = Convert.ToInt32( userEmp[0]);
                        SetValueForJobPosition = userEmp[1];
                        this.Hide();
                        HighTechOrderManagementForm frmHighTechForm = new HighTechOrderManagementForm();
                        frmHighTechForm.ShowDialog();

                        

                    }
                }

            }
            if (tries >= Convert.ToInt32( ConfigurationManager.AppSettings["Attempts"]))
            {
                MessageBox.Show("Number of attempts reached", "Invalid user/password");
                Application.Exit();
            }


        }



        private void linkLabelAssignPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            textBoxReEnterPassword.Visible = true;
            labelReEnterPassword.Visible = true;
            buttonUpdateUser.Visible = true;
            linkLabelAssignPassword.Visible = false;

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            AboutBoxHighTech frm = new AboutBoxHighTech();
            frm.ShowDialog();
            
        }

        private void buttonUpdateUser_Click(object sender, EventArgs e)
        {
            if (textBoxpassword.Text == "" && textBoxReEnterPassword.Text == "" && textBoxUser.Text == "Enter your email")
            {
                MessageBox.Show("User name/password invalid", "Invalid user/password");
            }else if (textBoxpassword.Text != textBoxReEnterPassword.Text)
            {
                MessageBox.Show("Password and Confirm password fields do not match", "Invalid password");
            }else if (textBoxpassword.Text == "" || textBoxReEnterPassword.Text == "")
            {
                MessageBox.Show("Password or Confirm password fields are empty", "Invalid user/password");
            }
            else
            {
                int updateRecordFlag = 1;
                User user = new User();
                if (user.UpdateUserOK(textBoxUser.Text.ToLower(),textBoxpassword.Text, updateRecordFlag))
                {
                    MessageBox.Show("User Account updated!", "User Account");
                    labelReEnterPassword.Visible = false;
                    textBoxReEnterPassword.Visible = false;
                    buttonUpdateUser.Visible = false;
                    linkLabelAssignPassword.Visible = true;
                }
                else 
                {
                    MessageBox.Show("User Account not found!","Invalid User Account");
                }

            }
        }
    }
}
