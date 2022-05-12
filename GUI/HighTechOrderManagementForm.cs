using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Final_Project.BLL;
using Final_Project.DAL;
using Final_Project.VALIDATOR;
using Final_Project.Entity;
using System.Configuration;


namespace Final_Project.GUI
{
    public partial class HighTechOrderManagementForm : Form
    {
        DataSet dsBooksDB = new DataSet();
        DataTable dtCustomers = new DataTable("Customers");
        public int remainingUnits = 0;
        public int unitsToShip = 0;
        public double valueBeforeTax;
        public double gstValue;
        public double qstValue;


        SqlDataAdapter da;

        public HighTechOrderManagementForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            string input = string.Empty;
            input = (textBoxEmployeeId.Text);
            
            if (input != "")
            {
                if (emp.IsDuplicateEmpId(input))
                {
                    MessageBox.Show("This employee Id already exists", "Invalid employee ID",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxEmployeeId.Clear();
                    return;
                }

               

            }

            

            
            input = textBoxFirstName.Text.Trim();

            //check first name
            if (!Validator.IsValidName(input))
            {
                MessageBox.Show("First name contains only letters and space" +
                    " to separate first name components", "Invalid First Name",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxFirstName.Clear();
                textBoxFirstName.Focus();
                return;
            }

            input = "";
            input = textBoxLastName.Text.Trim();

            //check last name
            if (!Validator.IsValidName(input))
            {
                MessageBox.Show("Last name contains only letters and space" +
                    " to separate last name components", "Invalid last Name",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLastName.Clear();
                textBoxLastName.Focus();
                return;
            }


            input = textBoxEmail.Text.Trim();
            if (input == "")
            {
                MessageBox.Show("Email field cannot be empty", "Invalid email",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEmail.Clear();
                return;
            }

            

            //check employee email
            if (!Validator.IsValidEmail(input))
            {
                MessageBox.Show("Email must contain the @ sign", "Invalid Email",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEmail.Clear();
                textBoxEmail.Focus();
                return;
            }

           

            if (emp.IsDuplicateEmpEmail(textBoxEmail.Text.Trim().ToLower()))
            {
                MessageBox.Show("This employee email already exists", "Invalid email",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEmail.Clear();
                return;
            }

            input = maskedTextBoxPhoneNumber.Text.Trim();
            input = Regex.Replace(input, @"[.\D+]", "");
            string error = "";

            //check telephone employee
            if (!Validator.IsValidId(input, 10, ref error, maskedTextBoxPhoneNumber.Text))
            {
                MessageBox.Show("Phone number is invalid", "Invalid phone number",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBoxPhoneNumber.Clear();
                maskedTextBoxPhoneNumber.Focus();
                return;
            }

            if (comboBoxJobTitle.Text == "")
            {
                MessageBox.Show("Please select a job title", "Invalid Job Title");
                return;
            }            
            else 
            {
                string[] jobValues = comboBoxJobTitle.Text.Split('-');
                emp.JobId = Convert.ToInt32( jobValues[0]);
            }

            if (comboBoxStatus.Text == "")
            {
                MessageBox.Show("Please select a employee status", "Invalid Employee Status");
                return;
            }
            else
            {
                string[] statusValues = comboBoxStatus.Text.Split('-');
                emp.StatusId = Convert.ToInt32(statusValues[0]);
            }


            emp.FirstName = textBoxFirstName.Text.Trim();
            emp.LastName = textBoxLastName.Text.Trim();
            emp.PhoneNumber = maskedTextBoxPhoneNumber.Text.Trim();
            emp.Email = textBoxEmail.Text.Trim().ToLower();
            emp.SaveEmployee(emp);
            MessageBox.Show("Employee saved");
            textBoxEmployeeId.Text = "";
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxEmail.Text = "";
            maskedTextBoxPhoneNumber.Text = "";
            comboBoxJobTitle.SelectedIndex = -1;
            comboBoxStatus.SelectedIndex = -1;

        }

        private void buttonList_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            List<Employee> listEmp = emp.GetAllEmployees();
            listViewEmployees.Items.Clear();
            if (listEmp.Count != 0)
            {
                foreach (Employee e_item in listEmp)
                {
                    ListViewItem item = new ListViewItem(e_item.EmployeeId.ToString());
                    item.SubItems.Add(e_item.FirstName);
                    item.SubItems.Add(e_item.LastName);
                    item.SubItems.Add(e_item.Email);
                    item.SubItems.Add(e_item.PhoneNumber);
                    listViewEmployees.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Employee list is empty!!\nPlease enter employee data", "No employee data",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string input = this.textBoxSearchEmployee.Text.Trim();
            string error = "";
            textBoxEmployeeId.Enabled = true;
            if (this.textBoxSearchEmployee.Text == "")
            {
                MessageBox.Show("Please enter an employee Id value");
            }
            else
            {
                if (!(Validator.IsValidId(input, 4, ref error, textBoxSearchEmployee.Text)))
                {
                    MessageBox.Show(error, "Invalid employee ID",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxSearchEmployee.Clear();
                    textBoxSearchEmployee.Focus();
                    return;
                }



                Employee emp = new Employee();
                emp = emp.SearchEmployee(input);
                if (emp != null)
                {
                    textBoxEmployeeId.Text = emp.EmployeeId.ToString();
                    textBoxFirstName.Text = emp.FirstName.ToString();
                    textBoxLastName.Text = emp.LastName.ToString();
                    maskedTextBoxPhoneNumber.Text = emp.PhoneNumber.ToString();
                    textBoxEmail.Text = emp.Email.ToString();
                    Status sta = new Status();
                    sta = sta.SearchStatus(emp.StatusId);
                    if (sta != null)
                        comboBoxStatus.Text = sta.StatusId + "-" + sta.State;
                    else
                    {
                        MessageBox.Show("Status information not found for employee!", "No status data",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    JobPosition job = new JobPosition();
                    job = job.SearchJobPosition(emp.JobId);
                    if (job != null)
                        comboBoxJobTitle.Text = job.JobId + "-" + job.JobTitle;
                    else
                    {
                        MessageBox.Show("Job position information not found for employee!", "No job position data",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    textBoxEmployeeId.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Employee not found!!", "Invalid employee ",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);

                    textBoxSearchEmployee.Focus();

                }
            }

            textBoxSearchEmployee.Clear();
        }

        private void HighTechOrderManagementForm_Load(object sender, EventArgs e)
        {
           
            //retreive job position id to enable the user access to the correspondant tab
            string jobPositionId = LoginForm.SetValueForJobPosition;

            
            foreach (Control ctl in this.tabControl1.Controls)
            {
                ctl.Enabled = false;
                if (jobPositionId == "1" && ctl.Name == "tabEmployeeUser")
                    ctl.Enabled = true;
                else if (jobPositionId == "2" && ctl.Name == "tabCustomer")
                    ctl.Enabled = true;
                else if (jobPositionId == "3" && ctl.Name == "tabBookInventory")
                    ctl.Enabled = true;
                else if (jobPositionId == "3" && ctl.Name == "tabBookAuthors")
                    ctl.Enabled = true;
                else if (jobPositionId == "3" && ctl.Name == "tabPageAuthors")
                    ctl.Enabled = true;
                else if (jobPositionId == "4" && ctl.Name == "tabPageOrders")
                    ctl.Enabled = true;
                else if (jobPositionId == "5" && ctl.Name == "tabPageInvoices")
                    ctl.Enabled = true;
                
            }


            //Employee - User Tab loading validation (Connected Mode)
            Status sta = new Status();
            List<Status> listSta = sta.GetAllStatus();
            comboBoxStatus.Items.Clear();
            if (listSta.Count != 0)
            {
                foreach (Status s_item in listSta)
                {
                    comboBoxStatus.Items.Add(s_item.StatusId + "-" + s_item.State);
                }
            }
            else
            {
                MessageBox.Show("Status information is empty!", "No status data",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            JobPosition job = new JobPosition();
            List<JobPosition> listJob = job.GetAllJobPositions();
            comboBoxJobTitle.Items.Clear();
            if (listJob.Count != 0)
            {
                foreach (JobPosition s_item in listJob)
                {
                    comboBoxJobTitle.Items.Add(s_item.JobId + "-" + s_item.JobTitle);
                }
            }
            else
            {
                MessageBox.Show("Job Position information is empty!", "No job position data",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //Customer tab  - loading validation (Disconnected mode)
            dsBooksDB.Tables.Add(dtCustomers);
            dtCustomers.Columns.Add("CustomerId", typeof(string));
            dtCustomers.PrimaryKey = new DataColumn[] { dtCustomers.Columns["CustomerId"] };
            dtCustomers.Columns["CustomerId"].AutoIncrement = true;
            dtCustomers.Columns["CustomerId"].AutoIncrementSeed = 1000;
            dtCustomers.Columns["CustomerId"].AutoIncrementStep = 1;
            dtCustomers.Columns.Add("CustomerName", typeof(string));
            dtCustomers.Columns.Add("StreetAddress", typeof(string));
            dtCustomers.Columns.Add("City", typeof(string));
            dtCustomers.Columns.Add("PostalCode", typeof(string));
            dtCustomers.Columns.Add("PhoneNumber", typeof(string));
            dtCustomers.Columns.Add("FaxNumber", typeof(string));
            dtCustomers.Columns.Add("CreditLimit", typeof(float));
            dtCustomers.Columns.Add("Email", typeof(string));
            da = new SqlDataAdapter("SELECT * FROM Customers", UtilityDB.ConnectDB());
            da.Fill(dsBooksDB.Tables["Customers"]);

            //Books Inventory tab - loading publisher and category information from DB (Entity frameowrk mode)
            comboBoxPublisher.Items.Clear();
            using (var db = new BooksDBEntities4())
            {
                var list = from x in db.Publishers
                           orderby x.PublishersId
                           select x;
                foreach (var item in list)
                {
                    comboBoxPublisher.Items.Add(item.PublishersId + "|" + item.PublisherName);
                }
            }


            comboBoxCategory.Items.Clear();

            

            using (var db = new BooksDBEntities4())
            {
                var list = from x in db.Categories
                           orderby x.CategoryId
                           select x;
                foreach (var item in list)
                {
                    comboBoxCategory.Items.Add(item.CategoryId + "|" + item.CategoryName);
                }
            }

            //Authors page tab loading author column names table (Entity framework mode)
            using (var db = new BooksDBEntities4())
            {
                var columnNames = db.Database.SqlQuery<string>("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Authors');").ToList();
                foreach (var item in columnNames)
                {
                    comboBoxSearchBy.Items.Add(item);
                }

                         


            }

            //Books Authors tab - loading authors and books information from DB (Entity frameowrk mode)
            comboBoxAuthors.Items.Clear();
            using (var db = new BooksDBEntities4())
            {
                var list = from x in db.Authors
                           orderby x.AuthorId
                           select x;
                foreach (var item in list)
                {
                    comboBoxAuthors.Items.Add(item.AuthorId);
                }
            }


            comboBoxBooks.Items.Clear();



            using (var db = new BooksDBEntities4())
            {
                var list = from x in db.Books
                           orderby x.ISBN
                           select x;
                foreach (var item in list)
                {
                    comboBoxBooks.Items.Add(item.ISBN);
                }
            }

            // Orders Tab loading information (Entity framework mode)

            using (var db = new BooksDBEntities4())
            {
                var list = from x in db.StatusOrders
                           orderby x.StatusOrderId
                           select x;
                foreach (var item in list)
                {
                    comboBoxStatusOrder.Items.Add(item.StatusOrderId + "|" + item.Status);
                }
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm frm = new LoginForm();
            frm.ShowDialog();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            string input = string.Empty;
            input = (textBoxEmployeeId.Text);

            

            input = textBoxEmployeeId.Text.Trim();

            if (input == "")
            {
                MessageBox.Show("Employee Id invalid", "Invalid Employee Id",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEmployeeId.Clear();
                return;
            }



            //check first name
            input = textBoxFirstName.Text.Trim();
            if (!Validator.IsValidName(input))
            {
                MessageBox.Show("First name contains only letters and space" +
                    " to separate first name components", "Invalid First Name",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxFirstName.Clear();
                textBoxFirstName.Focus();
                return;
            }

            input = "";
            input = textBoxLastName.Text.Trim();

            //check last name
            if (!Validator.IsValidName(input))
            {
                MessageBox.Show("Last name contains only letters and space" +
                    " to separate last name components", "Invalid last Name",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLastName.Clear();
                textBoxLastName.Focus();
                return;
            }

            input = textBoxEmail.Text.Trim();
            if (input == "")
            {
                MessageBox.Show("Email field cannot be empty", "Invalid email",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEmail.Clear();
                return;
            }
            //check email
            if (!Validator.IsValidEmail(input))
            {
                MessageBox.Show("Email must contain the @ sign", "Invalid Email",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEmail.Clear();
                textBoxEmail.Focus();
                return;
            }

            

            //if (emp.IsDuplicateEmpEmail(textBoxEmail.Text.Trim().ToLower()))
            //{
            //    MessageBox.Show("This employee email already exists", "Invalid email",
            //       MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    textBoxEmail.Clear();
            //    return;
            //}

            input = maskedTextBoxPhoneNumber.Text.Trim();
            input = Regex.Replace(input, @"[.\D+]", "");
            string error = "";
            //check last name
            if (!Validator.IsValidId(input, 10, ref error, textBoxSearchEmployee.Text))
            {
                MessageBox.Show("Phone number is invalid", "Invalid phone number",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBoxPhoneNumber.Clear();
                maskedTextBoxPhoneNumber.Focus();
                return;
            }

            if (comboBoxJobTitle.Text == "")
            {
                MessageBox.Show("Please select a job title", "Invalid Job Title");
                return;
            }
            else
            {
                string[] jobValues = comboBoxJobTitle.Text.Split('-');
                emp.JobId = Convert.ToInt32(jobValues[0]);
            }

            if (comboBoxStatus.Text == "")
            {
                MessageBox.Show("Please select a employee status", "Invalid Employee Status");
                return;
            }
            else
            {
                string[] statusValues = comboBoxStatus.Text.Split('-');
                emp.StatusId = Convert.ToInt32(statusValues[0]);
            }

            emp.EmployeeId = Convert.ToInt32( textBoxEmployeeId.Text.Trim());
            emp.FirstName = textBoxFirstName.Text.Trim();
            emp.LastName = textBoxLastName.Text.Trim();
            emp.PhoneNumber = maskedTextBoxPhoneNumber.Text.Trim();
            emp.Email = textBoxEmail.Text.Trim().ToLower();
            emp.UpdateEmployee(emp);
            MessageBox.Show("Employee updated");


        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            emp.DeleteEmployee(Convert.ToInt32(textBoxEmployeeId.Text));
            MessageBox.Show("Employee deleted");
            textBoxEmployeeId.Text = "";
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxEmail.Text = "";
            maskedTextBoxPhoneNumber.Text = "";
            comboBoxStatus.SelectedIndex = -1;
            comboBoxJobTitle.SelectedIndex = -1;



        }

        private void button2_Click(object sender, EventArgs e)
        {
            User user = new User();
            List<User> listUsers = user.GetAllUsers();
            listViewUsers.Items.Clear();
            if (listUsers.Count != 0)
            {
                foreach (User e_item in listUsers)
                {
                    ListViewItem item = new ListViewItem(e_item.UserId.ToString());
                    item.SubItems.Add(e_item.EmployeeId.ToString());
                    listViewUsers.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Users list is empty!!\nPlease enter users data", "No Users data",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            textBoxEmployeeIdUser.Text = textBoxEmployeeId.Text;
        }

        private void buttonSaveUser_Click(object sender, EventArgs e)
        {
            User user = new User();
            
            
            if (textBoxEmployeeIdUser.Text == "")
            {
                MessageBox.Show("Please assign an employee to create its user account");
                return;
            }
            else
            {
                int input = Convert.ToInt32(textBoxEmployeeIdUser.Text);
                if (user.IsDuplicateUserEmployeeId(input))
                {
                    MessageBox.Show("This user account already exists", "Invalid employee ID",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxEmployeeId.Clear();
                    return;
                }

                user.EmployeeId = Convert.ToInt32(textBoxEmployeeIdUser.Text);
                user.SaveUser(user);
                MessageBox.Show("User Account Saved and assigned to employee ID: " + textBoxEmployeeIdUser.Text);
                textBoxEmployeeIdUser.Text = "";
                textBoxUserId.Text = "";
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = this.textBoxSearchUser.Text.Trim();
            string error = "";
            if (this.textBoxSearchUser.Text == "")
            {
                MessageBox.Show("Employee Id is empty!");
            }
            else
            {
                if (!(Validator.IsValidId(input, 4, ref error, textBoxSearchUser.Text)))
                {
                    MessageBox.Show(error, "Invalid employee ID",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxSearchEmployee.Clear();
                    textBoxSearchEmployee.Focus();
                    return;
                }


                User user = new User();
                user = user.SearchUser(Convert.ToInt32( input));
                if (user != null)
                {
                    textBoxEmployeeIdUser.Text = user.EmployeeId.ToString();
                    textBoxUserId.Text = user.UserId.ToString();
                    
                }
                else
                {
                    MessageBox.Show("User not found!!", "Invalid user ",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);

                    textBoxSearchEmployee.Focus();

                }


            }
        }

        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            User user = new User();

            if (textBoxUserId.Text!="")
                user.DeleteUser(Convert.ToInt32(textBoxUserId.Text));
            else
            {
                MessageBox.Show("Please search for an user before to delete it");
                return;
            }

            MessageBox.Show("User deleted");
            textBoxEmployeeIdUser.Text = "";
            textBoxUserId.Text = "";
            
        }

      

        private void buttonSearchCustomer_Click(object sender, EventArgs e)
        {
            int searchId;

            if (textBoxSearchCustomer.Text != "")
            {
                searchId = Convert.ToInt32(textBoxSearchCustomer.Text);
                DataRow dr = dtCustomers.Rows.Find(searchId);
                if (dr != null)
                {
                    textBoxCustomerId.Text = dr["CustomerId"].ToString();
                    textBoxCustomerName.Text = dr["CustomerName"].ToString();
                    textBoxCity.Text = dr["City"].ToString();
                    maskedTextBoxCreditLimit.Text = dr["CreditLimit"].ToString();
                    textBoxStreetAddress.Text = dr["StreetAddress"].ToString();
                    textBoxPostalCode.Text = dr["PostalCode"].ToString();
                    maskedTextBoxCustomerPhoneNumber.Text = dr["PhoneNumber"].ToString();
                    maskedTextBoxFaxNumber.Text = dr["FaxNumber"].ToString();
                    textBoxCustomerEmail .Text = dr["Email"].ToString();
                }
                else
                {
                    MessageBox.Show("Customer info not found!", "Invalid customer id");
                }

            }
            else
            {
                MessageBox.Show("Searching text empty", "Invalid student id");
            }





        }

        private void buttonSaveCustomer_Click_1(object sender, EventArgs e)
        {
            DataRow dr = dtCustomers.NewRow();

            string input = textBoxCustomerId.Text.Trim();

            if (input == "")
                input = "0";
                
                dr = dtCustomers.Rows.Find(input);
                if (dr != null)
                {
                    MessageBox.Show("Customer info already exists!", "Invalid customer id");
                return;
                }
                else
                {
                    input = textBoxCustomerName.Text.Trim();

                    //check first name
                    if (!Validator.IsValidName(input))
                    {
                        MessageBox.Show("Customer name must contain only letters and space" , "Invalid Customer Name",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxCustomerName.Clear();
                        textBoxCustomerName.Focus();
                        return;
                    }

                    input = "";
                    input = textBoxCity.Text.Trim();

                    //check last name
                    if (!Validator.IsValidName(input))
                    {
                        MessageBox.Show("City name must contain only letters and space", "Invalid City Name",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                        textBoxCity.Clear();
                        textBoxCity.Focus();
                        return;
                    }

                    input = textBoxCustomerEmail.Text.Trim();

                    //check last name
                    if (!Validator.IsValidEmail(input))
                    {
                        MessageBox.Show("Email must contain the @ sign", "Invalid Email",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxCustomerEmail.Clear();
                        textBoxCustomerEmail.Focus();
                        return;
                    }

                    input = maskedTextBoxCustomerPhoneNumber.Text.Trim();
                    input = Regex.Replace(input, @"[.\D+]", "");
                    string error = "";
                    //check last name
                    if (!Validator.IsValidId(input, 10, ref error, maskedTextBoxCustomerPhoneNumber.Text))
                    {
                        MessageBox.Show("Phone number is invalid", "Invalid phone number",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                        maskedTextBoxCustomerPhoneNumber.Clear();
                        maskedTextBoxCustomerPhoneNumber.Focus();
                        return;
                    }

                    input = maskedTextBoxFaxNumber.Text.Trim();
                    input = Regex.Replace(input, @"[.\D+]", "");
                    error = "";
                    //check last name
                    if (!Validator.IsValidId(input, 10, ref error, maskedTextBoxFaxNumber.Text))
                    {
                        MessageBox.Show("Fax number is invalid", "Invalid fax number",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                        maskedTextBoxFaxNumber.Clear();
                        maskedTextBoxFaxNumber.Focus();
                        return;
                    }




                dr = dtCustomers.NewRow();
                // store data in the row

                dr["CustomerName"] = textBoxCustomerName.Text.Trim();
                    dr["StreetAddress"] = textBoxStreetAddress.Text.Trim();
                    dr["City"] = textBoxCity.Text.Trim();
                    dr["PostalCode"] = textBoxPostalCode.Text.Trim();
                    dr["PhoneNumber"] = maskedTextBoxCustomerPhoneNumber.Text.Trim();
                    dr["FaxNumber"] = maskedTextBoxFaxNumber.Text.Trim();
                input = Regex.Replace(maskedTextBoxCreditLimit.Text.Trim(), @"[.\D+]", "");
                dr["CreditLimit"] = float.Parse( input);
                    dr["Email"] = textBoxCustomerEmail.Text.Trim();
                    // add the row to the datable
                    dtCustomers.Rows.Add(dr);
                    MessageBox.Show(dr.RowState.ToString());

                

                


                }



            textBoxCustomerId.Text = "";
            textBoxCustomerName.Text = "";
            textBoxStreetAddress.Text = "";
            textBoxCity.Text = "";
            textBoxPostalCode.Text = "";
            maskedTextBoxCustomerPhoneNumber.Text = "";
            maskedTextBoxFaxNumber.Text = "";
            maskedTextBoxCreditLimit.Text = "";
            textBoxCustomerEmail.Text = "";



           


        }

        private void buttonUpdateCustomer_Click(object sender, EventArgs e)
        {
            if (textBoxCustomerId.Text == "")
            {
                MessageBox.Show("Please search for the customer to get his/her customer id", "Invalid customer Id");
                return;
            }
            int searchId = Convert.ToInt32(textBoxCustomerId.Text);
            
                
            DataRow dr = dtCustomers.Rows.Find(searchId);
            string input = textBoxCustomerName.Text.Trim();

            //check first name
            if (!Validator.IsValidName(input))
            {
                MessageBox.Show("Customer name must contain only letters and space", "Invalid Customer Name",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxFirstName.Clear();
                textBoxFirstName.Focus();
                return;
            }

            input = "";
            input = textBoxCity.Text.Trim();

            //check last name
            if (!Validator.IsValidName(input))
            {
                MessageBox.Show("City name must contain only letters and space", "Invalid City Name",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);

                textBoxLastName.Clear();
                textBoxLastName.Focus();
                return;
            }

            input = textBoxCustomerEmail.Text.Trim();

            //check last name
            if (!Validator.IsValidEmail(input))
            {
                MessageBox.Show("Email must contain the @ sign", "Invalid Email",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEmail.Clear();
                textBoxEmail.Focus();
                return;
            }

            input = maskedTextBoxCustomerPhoneNumber.Text.Trim();
            input = Regex.Replace(input, @"[.\D+]", "");
            string error = "";
            //check last name
            if (!Validator.IsValidId(input, 10, ref error, maskedTextBoxCustomerPhoneNumber.Text))
            {
                MessageBox.Show("Phone number is invalid", "Invalid phone number",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBoxCustomerPhoneNumber.Clear();
                maskedTextBoxCustomerPhoneNumber.Focus();
                return;
            }

            input = maskedTextBoxFaxNumber.Text.Trim();
            input = Regex.Replace(input, @"[.\D+]", "");
            error = "";
            //check last name
            if (!Validator.IsValidId(input, 10, ref error, maskedTextBoxFaxNumber.Text))
            {
                MessageBox.Show("Fax number is invalid", "Invalid fax number",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBoxFaxNumber.Clear();
                maskedTextBoxFaxNumber.Focus();
                return;
            }

           



            // store data in the row
            dr["CustomerName"] = textBoxCustomerName.Text.Trim();
            dr["StreetAddress"] = textBoxStreetAddress.Text.Trim();
            dr["City"] = textBoxCity.Text.Trim();
            dr["PostalCode"] = textBoxPostalCode.Text.Trim();
            dr["PhoneNumber"] = maskedTextBoxCustomerPhoneNumber.Text.Trim();
            dr["FaxNumber"] = maskedTextBoxFaxNumber.Text.Trim();
            input = Regex.Replace(maskedTextBoxCreditLimit.Text.Trim(), @"[.\D+]", "");
            dr["CreditLimit"] = float.Parse(input);
            dr["Email"] = textBoxCustomerEmail.Text.Trim();
            
            MessageBox.Show(dr.RowState.ToString());
        }

        private void buttonDeleteCustomer_Click(object sender, EventArgs e)
        {
            int searchId = Convert.ToInt32(textBoxCustomerId.Text);
            DataRow dr = dtCustomers.Rows.Find(searchId);
            dr.Delete();
            MessageBox.Show(dr.RowState.ToString());
            listViewInventoryBooks.Items.Clear();
            maskedTextBoxISBN.Text = "";
            textBoxBookTitle.Text = "";
            maskedTextBoxYearPublished.Text = "";
            maskedTextBoxQOH.Text = "";
            comboBoxPublisher.Text = "";
            comboBoxCategory.Text = "";
            

        }

        private void buttonListCustomers_Click(object sender, EventArgs e)
        {
            da.Fill(dsBooksDB.Tables["Customers"]);
            dataGridViewCustomersDS.DataSource = dsBooksDB.Tables["Customers"];
        }

        private void buttonUpdateDatabse_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder builder = new SqlCommandBuilder(da);

            // add rows to dataset

            builder.GetInsertCommand();

            da.Update(dsBooksDB.Tables["Customers"]);
            MessageBox.Show("Database updated", "Database Update");
        }

        private void buttonAssignAuthor_Click(object sender, EventArgs e)
        {
            if (comboBoxAuthors.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an author from the list!", "Author");
                return;
            }
            else if (comboBoxBooks.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a book from the list!", "Books");
                return;
            }


            int authorId = Convert.ToInt32(comboBoxAuthors.Text);
            using (var db = new BooksDBEntities4())
            {
                var bookCheck = (from authA in db.AuthorsBooks
                                where authA.AurthorId == authorId && authA.ISBN == comboBoxBooks.Text.Trim()
                                select authA).FirstOrDefault();

                if (bookCheck != null)
                {
                    MessageBox.Show("Book already assigned to the author!", "Book Author");
                }
                else
                {
                    AuthorsBook bookAssign = new AuthorsBook();
                    bookAssign.AurthorId = Convert.ToInt32(comboBoxAuthors.Text);
                    bookAssign.ISBN = comboBoxBooks.Text.Trim();
                    bookAssign.YearPublished = Convert.ToInt32(textBoxYearPublished.Text);
                    bookAssign.Edition = textBoxEdition.Text;
                    db.AuthorsBooks.Add(bookAssign);
                    db.SaveChanges();
                    MessageBox.Show("Book assigned to the author!", "Book Author");
                }


            }


        }

        private void linkLabelEditCategory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            comboBoxCategory.Items.Clear();
            
            CategoryInfoForm frmAddInfoForm = new CategoryInfoForm();
            frmAddInfoForm.ShowDialog();

            using (var db = new BooksDBEntities4())
            {
                var list = from x in db.Categories
                           orderby x.CategoryId
                           select x;
                foreach (var item in list)
                {
                    comboBoxCategory.Items.Add(item.CategoryId + "|" + item.CategoryName);
                }
            }

        }

        private void buttonListBooks_Click(object sender, EventArgs e)
        {
            listViewInventoryBooks.Items.Clear();
            using (var db = new BooksDBEntities4())
            {
                // display the list of employees
                var listBooks = from book in db.Books
                              orderby book.ISBN
                              select book;
                foreach (var book in listBooks)
                {
                    ListViewItem item = new ListViewItem(book.ISBN.ToString());
                    item.SubItems.Add(book.Title);
                    item.SubItems.Add(book.UnitPrice.ToString());
                    item.SubItems.Add(book.YearPublished.ToString());
                    item.SubItems.Add(book.QOH.ToString());
                    listViewInventoryBooks.Items.Add(item);
                }



            }
        }

        private void buttonSaveBooks_Click(object sender, EventArgs e)
        {
            string bookId = maskedTextBoxISBN.Text.Trim();

            if(comboBoxPublisher.Text == "")
            {
                MessageBox.Show("Please select a publisher", "Invalid publisher");
                return;
            }
            if(comboBoxCategory.Text == "")
            {
                MessageBox.Show("Please select a category", "Invalid category");
                return;
            }
            
            if (maskedTextBoxISBN.Text == "")
            {
                MessageBox.Show("Please enter a ISBN value", "Invalid ISBN");
                return;
            }

            if (textBoxBookTitle.Text == "")
            {
                MessageBox.Show("Please enter a title value", "Invalid title");
                return;
            }

            if (textBoxUnitPrice.Text == "")
            {
                MessageBox.Show("Please enter a unit price value", "Invalid unit price value");
                return;
            }

            if (maskedTextBoxYearPublished.Text == "")
            {
                MessageBox.Show("Please enter a year published value", "Invalid year published");
                return;
            }

            if (maskedTextBoxQOH.Text == "")
            {
                MessageBox.Show("Please enter a QOH value", "Invalid QOH");
                return;
            }
            using (var db = new BooksDBEntities4())
            {
                Book book = new Book();
                book = db.Books.Find(maskedTextBoxISBN.Text);

                if (book != null)
                {
                    MessageBox.Show("Book already exists!");
                }
                else
                {
                    Book book1 = new Book();
                    book1.ISBN = maskedTextBoxISBN.Text;
                    book1.Title = textBoxBookTitle.Text;
                    //bookId = Regex.Replace(textBoxUnitPrice.Text, @"[.\D+]", "");
                    book1.UnitPrice = Convert.ToDouble(textBoxUnitPrice.Text);
                    bookId = Regex.Replace(maskedTextBoxYearPublished.Text, @"[.\D+]", "");
                    book1.YearPublished = Convert.ToInt32(bookId);
                    bookId = Regex.Replace(maskedTextBoxQOH.Text, @"[.\D+]", "");
                    book1.QOH = Convert.ToInt32(bookId);
                    string[] values = comboBoxCategory.Text.Split('|');
                    book1.CategoryId = Convert.ToInt32( values[0]);
                    values = comboBoxPublisher.Text.Split('|');
                    book1.PublisherId = Convert.ToInt32(values[0]);
                    db.Books.Add(book1);
                    db.SaveChanges();
                    MessageBox.Show("Book saved!");
                    listViewInventoryBooks.Items.Clear();
                    maskedTextBoxISBN.Text = "";
                    textBoxBookTitle.Text = "";
                    maskedTextBoxYearPublished.Text = "";
                    maskedTextBoxQOH.Text = "";
                    comboBoxPublisher.Text = "";
                    comboBoxCategory.Text = "";
                    textBoxUnitPrice.Text = "";
                    textBoxSerachBook.Clear();

                    comboBoxBooks.Items.Clear();




                    var list2 = from x in db.Books
                                orderby x.ISBN
                                select x;
                    foreach (var item in list2)
                    {
                        comboBoxBooks.Items.Add(item.ISBN);
                    }

                }


            }
        }

        private void buttonUpdateBooks_Click(object sender, EventArgs e)
        {

            if (comboBoxPublisher.Text == "")
            {
                MessageBox.Show("Please select a publisher", "Invalid publisher");
                return;
            }
            if (comboBoxCategory.Text == "")
            {
                MessageBox.Show("Please select a category", "Invalid category");
                return;
            }

            if (maskedTextBoxISBN.Text == "")
            {
                MessageBox.Show("Please enter a ISBN value", "Invalid ISBN");
                return;
            }

            if (textBoxBookTitle.Text == "")
            {
                MessageBox.Show("Please enter a title value", "Invalid title");
                return;
            }

            if (textBoxUnitPrice.Text == "")
            {
                MessageBox.Show("Please enter a unit price value", "Invalid unit price value");
                return;
            }

            if (maskedTextBoxYearPublished.Text == "")
            {
                MessageBox.Show("Please enter a year published value", "Invalid year published");
                return;
            }

            if (maskedTextBoxQOH.Text == "")
            {
                MessageBox.Show("Please enter a QOH value", "Invalid QOH");
                return;
            }

            string bookId = maskedTextBoxISBN.Text;
            using (var db = new BooksDBEntities4())
            {
                Book book1 = new Book();
                book1 = (db.Books.Where(x => x.ISBN == bookId)).FirstOrDefault();

                if (book1 != null)
                {
                    book1.ISBN = maskedTextBoxISBN.Text;
                    book1.Title = textBoxBookTitle.Text;
                    //bookId = Regex.Replace(textBoxUnitPrice.Text, @"[.\D+]", "");
                    book1.UnitPrice = Convert.ToDouble(textBoxUnitPrice.Text);
                    bookId = Regex.Replace(maskedTextBoxYearPublished.Text, @"[.\D+]", "");
                    book1.YearPublished = Convert.ToInt32(bookId);
                    bookId = Regex.Replace(maskedTextBoxQOH.Text, @"[.\D+]", "");
                    book1.QOH = Convert.ToInt32(bookId);
                    string[] values = comboBoxCategory.Text.Split('|');
                    book1.CategoryId = Convert.ToInt32(values[0]);
                    values = comboBoxPublisher.Text.Split('|');
                    book1.PublisherId = Convert.ToInt32(values[0]);
                    //db.Books.Add(book1);
                    db.SaveChanges();
                    MessageBox.Show("Book updated!");
                    listViewBooks.Items.Clear();
                    textBoxSerachBook.Clear();

                    comboBoxBooks.Items.Clear();




                    var list2 = from x in db.Books
                                orderby x.ISBN
                                select x;
                    foreach (var item in list2)
                    {
                        comboBoxBooks.Items.Add(item.ISBN);
                    }
                }
                else
                {
                    MessageBox.Show("Book id does not exist", "Invalid Book");
                }

                
            }

        }

        private void buttonDeleteBooks_Click(object sender, EventArgs e)
        {
            try
            {
                if (maskedTextBoxISBN.Text.Any(char.IsDigit))
                {
                    using (var db = new BooksDBEntities4())
                    {
                        string bookId = maskedTextBoxISBN.Text;
                        var deleteBook =
                        from details in db.Books
                        where details.ISBN.Equals(bookId)
                        select details;

                        if (deleteBook != null)
                        {
                            foreach (var detail in deleteBook)
                            {
                                db.Books.Remove(detail);


                            }
                            db.SaveChanges();
                            MessageBox.Show("Book deleted!");
                            listViewInventoryBooks.Items.Clear();
                            maskedTextBoxISBN.Text = "";
                            textBoxBookTitle.Text = "";
                            maskedTextBoxYearPublished.Text = "";
                            maskedTextBoxQOH.Text = "";
                            comboBoxPublisher.Text = "";
                            comboBoxCategory.Text = "";
                            textBoxSerachBook.Clear();
                            textBoxUnitPrice.Text = "";
                            comboBoxBooks.Items.Clear();




                            var list2 = from x in db.Books
                                        orderby x.ISBN
                                        select x;
                            foreach (var item in list2)
                            {
                                comboBoxBooks.Items.Add(item.ISBN);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Book not found!");
                        }


                    }

                }
                else
                {
                    MessageBox.Show("Please search for a book to delete", "Invalid Book Id");
                }

            }catch(Exception ex)
            {
                MessageBox.Show("An error ocurred and item could not be deleted!" + ex.Message + ex.InnerException.Source, "Error");
            }




        }

        private void buttonSearchBook_Click(object sender, EventArgs e)
        {
            string stringInput = textBoxSerachBook.Text;


            using (var db = new BooksDBEntities4())
            {
                Book book = new Book();


                book = (db.Books.Where(x => x.ISBN.Equals(stringInput))).FirstOrDefault();

               


                if (book != null)
                {

                    maskedTextBoxISBN.Text = book.ISBN.ToString();
                    textBoxUnitPrice.Text = book.UnitPrice.ToString();
                    textBoxBookTitle.Text = book.Title;
                    maskedTextBoxYearPublished.Text = book.YearPublished.ToString();
                    maskedTextBoxQOH.Text = book.QOH.ToString();
                    Category cat = new Category();
                    cat = (db.Categories.Where(x => x.CategoryId.Equals(book.CategoryId))).FirstOrDefault();
                    comboBoxCategory.Text = cat.CategoryId.ToString() + "|" + cat.CategoryName.ToString();
                    Publisher pub = new Publisher();
                    pub = (db.Publishers.Where(x => x.PublishersId.Equals(book.PublisherId))).FirstOrDefault();
                    comboBoxPublisher.Text = pub.PublishersId.ToString() + "|" + pub.PublisherName.ToString();



                }
                else
                {
                    textBoxSerachBook.Focus();
                    textBoxSerachBook.Clear();
                    MessageBox.Show("Book not found!");
                }


            }
        }

        private void linkLabelEditPublisher_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            comboBoxPublisher.Items.Clear();

            PublisherInfoForm frmAddInfoForm = new PublisherInfoForm();
            frmAddInfoForm.ShowDialog();

            using (var db = new BooksDBEntities4())
            {
                var list = from x in db.Publishers
                           orderby x.PublishersId
                           select x;
                foreach (var item in list)
                {
                    comboBoxPublisher.Items.Add(item.PublishersId + "|" + item.PublisherName);
                }
            }
        }

        private void buttonSaveAuthor_Click(object sender, EventArgs e)
        {
            int authorId;
            string input = string.Empty;
            if (textBoxAuthorId.Text == "")
            {
                authorId = 0;
            }
            else
            {
                authorId = Convert.ToInt32(textBoxAuthorId.Text);
            }

            


            input = textBoxFirstNameAuthor.Text.Trim();

            //check first name
            if (!Validator.IsValidName(input))
            {
                MessageBox.Show("First name contains only letters and space" +
                    " to separate first name components", "Invalid First Name",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxFirstNameAuthor.Clear();
                textBoxFirstNameAuthor.Focus();
                return;
            }

            input = "";
            input = textBoxLastNameAuthor.Text.Trim();

            //check last name
            if (!Validator.IsValidName(input))
            {
                MessageBox.Show("Last name contains only letters and space" +
                    " to separate last name components", "Invalid last Name",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLastNameAuthor.Clear();
                textBoxLastNameAuthor.Focus();
                return;
            }


            input = textBoxEmailAuthor.Text.Trim();
            if (input == "")
            {
                MessageBox.Show("Email field cannot be empty", "Invalid email",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEmailAuthor.Clear();
                return;
            }



            //check employee email
            if (!Validator.IsValidEmail(input))
            {
                MessageBox.Show("Email must contain the @ sign", "Invalid Email",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEmailAuthor.Clear();
                textBoxEmailAuthor.Focus();
                return;
            }


            using (var db = new BooksDBEntities4())
            {
                Author auth = new Author();
                auth = (db.Authors.Where(x => x.AuthorId == authorId)).FirstOrDefault();

                if (auth != null)
                {
                    MessageBox.Show("Author already exists!");
                }
                else
                {
                    Author auth1 = new Author();
                    //auth1.AuthorId = Convert.ToInt32(textBoxAuthorId.Text);
                    auth1.FirstName = textBoxFirstNameAuthor.Text.ToLower();
                    auth1.LastName = textBoxLastNameAuthor.Text.ToLower();

                    auth1.Email = textBoxEmailAuthor.Text;
                    db.Authors.Add(auth1);
                    db.SaveChanges();
                    MessageBox.Show("Author saved!");
                    listViewAuthors.Items.Clear();
                    textBoxAuthorId.Text = "";
                    textBoxFirstNameAuthor.Text = "";
                    textBoxLastNameAuthor.Text = "";
                    textBoxEmailAuthor.Text = "";
                    comboBoxAuthors.Items.Clear();
                    
                        var list = from x in db.Authors
                                   orderby x.AuthorId
                                   select x;
                        foreach (var item in list)
                        {
                            comboBoxAuthors.Items.Add(item.AuthorId);
                        }
                   


                    
                   

                }


            }
        }

        private void buttonListAuthors_Click(object sender, EventArgs e)
        {
            listViewAuthor.Items.Clear();
            using (var db = new BooksDBEntities4())
            {
                // display the list of authors
                var listAuth = from auth in db.Authors
                              orderby auth.FirstName
                              select auth;
                foreach (var author in listAuth)
                {
                    ListViewItem item = new ListViewItem(author.AuthorId.ToString());
                    item.SubItems.Add(author.FirstName);
                    item.SubItems.Add(author.LastName);
                    item.SubItems.Add(author.Email);
                    listViewAuthor.Items.Add(item);
                }



            }
        }

        private void buttonUpdateAuthor_Click(object sender, EventArgs e)
        {
            //UPDATE
            string input = string.Empty;
            int authorId = 0;
            if (textBoxAuthorId.Text == "")
            {
                MessageBox.Show("Please search for an author","Invalid Info");
            }
            else
            {
                authorId = Convert.ToInt32( textBoxAuthorId.Text);
                input = textBoxFirstNameAuthor.Text.Trim();

                //check first name
                if (!Validator.IsValidName(input))
                {
                    MessageBox.Show("First name contains only letters and space" +
                        " to separate first name components", "Invalid First Name",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxFirstNameAuthor.Clear();
                    textBoxFirstNameAuthor.Focus();
                    return;
                }

                input = "";
                input = textBoxLastNameAuthor.Text.Trim();

                //check last name
                if (!Validator.IsValidName(input))
                {
                    MessageBox.Show("Last name contains only letters and space" +
                        " to separate last name components", "Invalid last Name",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxLastNameAuthor.Clear();
                    textBoxLastNameAuthor.Focus();
                    return;
                }


                input = textBoxEmailAuthor.Text.Trim();
                if (input == "")
                {
                    MessageBox.Show("Email field cannot be empty", "Invalid email",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxEmailAuthor.Clear();
                    return;
                }



                //check employee email
                if (!Validator.IsValidEmail(input))
                {
                    MessageBox.Show("Email must contain the @ sign", "Invalid Email",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxEmailAuthor.Clear();
                    textBoxEmailAuthor.Focus();
                    return;
                }




                using (var db = new BooksDBEntities4())
                {
                    Author auth1 = new Author();
                    auth1 = (db.Authors.Where(x => x.AuthorId == authorId)).FirstOrDefault();
                    auth1.AuthorId = Convert.ToInt32(textBoxAuthorId.Text);
                    auth1.FirstName = textBoxFirstNameAuthor.Text.ToLowerInvariant();
                    auth1.LastName = textBoxLastNameAuthor.Text.ToLowerInvariant();

                    auth1.Email = textBoxEmailAuthor.Text.ToLower();
                   
                    db.SaveChanges();
                    MessageBox.Show("Author updated!");
                    listViewEmployees.Items.Clear();

                    comboBoxAuthors.Items.Clear();

                    var list = from x in db.Authors
                               orderby x.AuthorId
                               select x;
                    foreach (var item in list)
                    {
                        comboBoxAuthors.Items.Add(item.AuthorId);
                    }



                  

                }

            }
            
           
        }

        private void buttonSearchAuthor_Click(object sender, EventArgs e)
        {
            string stringInput = textBoxSearchAuthor.Text.ToLower();


            using (var db = new BooksDBEntities4())
            {
                Author auth = new Author();
                if (comboBoxSearchBy.Text == "AuthorId" && textBoxSearchAuthor.Text != "")
                {
                    int authorVal = Convert.ToInt32(stringInput);
                    auth = (db.Authors.Where(x => x.AuthorId == authorVal)).FirstOrDefault();
                }

                else if (comboBoxSearchBy.Text == "FirstName")
                    auth = (db.Authors.Where(x => x.FirstName == stringInput)).FirstOrDefault();
                else if (comboBoxSearchBy.Text == "LastName")
                    auth = (db.Authors.Where(x => x.LastName == stringInput)).FirstOrDefault();
                
                else if (comboBoxSearchBy.Text == "Email")
                    auth = (db.Authors.Where(x => x.Email == stringInput)).FirstOrDefault();
                else
                    MessageBox.Show("Search option not selected or search field empty!");

                if (auth != null)
                {

                    textBoxAuthorId.Text = auth.AuthorId.ToString();
                    textBoxFirstNameAuthor.Text = auth.FirstName;
                    textBoxLastNameAuthor.Text = auth.LastName;
                    
                    textBoxEmailAuthor.Text = auth.Email;




                }
                else
                {
                    textBoxSearchAuthor.Focus();
                    textBoxSearchAuthor.Clear();
                    MessageBox.Show("Author not found!");
                }


            }
        }

        private void buttonDeleteAuthor_Click(object sender, EventArgs e)
        {
            int authorId;
            if (textBoxAuthorId.Text == "")
            {
                MessageBox.Show("Please search for an author to delete", "Invalid Info");
            }
            else
            {
                authorId = Convert.ToInt32(textBoxAuthorId.Text);
                using (var db = new BooksDBEntities4())
                {
                    var deleteAuthor =
                    from details in db.Authors
                    where details.AuthorId == authorId
                    select details;

                    if (deleteAuthor != null)
                    {
                        foreach (var detail in deleteAuthor)
                        {
                            db.Authors.Remove(detail);


                        }
                        db.SaveChanges();
                        MessageBox.Show("Author deleted!");
                        listViewEmployees.Items.Clear();
                        listViewAuthors.Items.Clear();
                        textBoxAuthorId.Text = "";
                        textBoxFirstNameAuthor.Text = "";
                        textBoxLastNameAuthor.Text = "";
                        textBoxEmailAuthor.Text = "";

                        comboBoxAuthors.Items.Clear();

                        var list = from x in db.Authors
                                   orderby x.AuthorId
                                   select x;
                        foreach (var item in list)
                        {
                            comboBoxAuthors.Items.Add(item.AuthorId);
                        }





                    }
                    else
                    {
                        MessageBox.Show("Author not found!");
                    }


                }
            }
            
        }

        private void comboBoxBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            string bookId = comboBoxBooks.Text;
            using (var db = new BooksDBEntities4())
            {
                Book book = new Book();
                book = (db.Books.Where(x => x.ISBN == bookId)).FirstOrDefault();
                if (book != null)
                {
                    textBoxBooks.Text = "Book Title:" + book.Title + "\n Publisher name: " +
                       book.Publisher.PublisherName + "\n Category: " + book.Category.CategoryName + " Year Published: " + book.YearPublished;
                    textBoxYearPublished.Text =  book.YearPublished.ToString();
                }
                   
                else
                    MessageBox.Show("Book not found!");
            }
        }

        private void comboBoxAuthors_SelectedIndexChanged(object sender, EventArgs e)
        {
            int authorId = Convert.ToInt32( comboBoxAuthors.Text);
            using (var db = new BooksDBEntities4())
            {
                Author author = new Author();
                author = (db.Authors.Where(x => x.AuthorId == authorId)).FirstOrDefault();
                if (author != null)
                    textBoxAuthors.Text = "Author Name:" + author.FirstName +  author.LastName + "Email: " + author.Email;
                else
                    MessageBox.Show("Author not found!");
            }
        }

        private void buttonListBook_Click(object sender, EventArgs e)
        {
            if (comboBoxAuthors.SelectedIndex != -1)
            {
                listViewBooks.Items.Clear();
                int authId = Convert.ToInt32( comboBoxAuthors.Text.Trim());



                using (var db = new BooksDBEntities4())
                {
                    // display the list of employees
                    var listBookAsignment = (from ba in db.AuthorsBooks
                                            join bo in db.Books
                                            on ba.ISBN equals bo.ISBN
                                            where ba.AurthorId == authId
                                            select new
                                            {
                                                ISBN = bo.ISBN,
                                                Title = bo.Title,
                                                
                                            });

                    if (listBookAsignment.Count() > 0)
                    {
                        foreach (var itemB in listBookAsignment)
                        {

                            ListViewItem item = new ListViewItem(itemB.ISBN.ToString());
                            item.SubItems.Add(itemB.Title.ToString());

                            listViewBooks.Items.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No book asociated to the author", "Book Authors");
                    }

                    



                }
                
            }
            else
            {
                MessageBox.Show("Please select an author!", "Author");
                return;
            }

        }

        private void buttonListAuthor_Click(object sender, EventArgs e)
        {
            if (comboBoxBooks.SelectedIndex != -1)
            {
                listViewAuthors.Items.Clear();
                string bookId = comboBoxBooks.Text.Trim();



                using (var db = new BooksDBEntities4())
                {
                    // display the list of employees
                    var listAuthorAsignment = (from ab in db.AuthorsBooks
                                             join au in db.Authors
                                             on ab.AurthorId equals au.AuthorId
                                             where ab.ISBN == bookId
                                             select new
                                             {
                                                 FirstName = au.FirstName,
                                                 LastName = au.LastName,
                                                 Email = au.Email

                                             });
                    if (listAuthorAsignment.Count() > 0)
                    {
                        foreach (var itemA in listAuthorAsignment)
                        {

                            ListViewItem item = new ListViewItem(itemA.FirstName.ToString());
                            item.SubItems.Add(itemA.LastName.ToString());
                            item.SubItems.Add(itemA.Email.ToString());
                            listViewAuthors.Items.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No author asociated to the book!", "Book Authors");
                    }
                    



                }
                
            }
            else
            {
                MessageBox.Show("Please select a book!", "Book");
                
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            using (var db = new BooksDBEntities4())
            {
                string[] values = comboBoxStatusOrder.Text.Split('|');
                int valueId = Convert.ToInt32(values[0]);
                var itemDesc = (from x in db.StatusOrders
                           orderby x.StatusOrderId
                           where x.StatusOrderId == valueId
                               select new
                               {
                                   Description = x.Description
                                  
                               });
                if (itemDesc != null)
                {
                    foreach(var item in itemDesc)
                        textBoxStatusOrderDescription.Text = item.Description.ToString();
                }
            }

                
        }

        private void buttonSaveOrder_Click(object sender, EventArgs e)
        {
            
            

        }

        private void buttonSearchorder_Click(object sender, EventArgs e)
        {
            int input = 0; 

            if (textBoxOrderSearch.Text == "")
            {
                MessageBox.Show("Search order field is empty!", "Invalid order");
                return;
            }
            else
            {
                input = Convert.ToInt32(textBoxOrderSearch.Text);
                using (var db = new BooksDBEntities4())
                {
                    Order order = new Order();
                    order = (db.Orders.Where(x => x.OrderId.Equals(input))).FirstOrDefault();
                    if (order == null)
                    {
                        MessageBox.Show("Order id does not exist", "Invalid order");
                        textBoxOrderSearch.Clear();
                        textBoxOrderSearch.Focus();
                    }
                    else
                    {
                        textBoxOrderIdInfo.Text = order.OrderId.ToString();
                        textBoxCustomerOrderId.Text = order.CustomerId.ToString();
                        textBoxOrderType.Text = order.OrderType.ToString();
                        dateTimePickerRequiredDate.Text = order.RequiredDate.ToString();
                        dateTimePickerShippingDate.Text = order.ShippingDate.ToString();
                        comboBoxStatusOrder.Text = order.StatusOrder.StatusOrderId + "|" + order.StatusOrder.Status;
                        maskedTextBoxQuantityOrder.Text = order.QuantityOrdered;
                        textBoxPayment.Text = order.Payment.ToString();
                        textBoxCustomerId.Text = order.CustomerId.ToString();
                        listViewCustomerDetails.Items.Clear();
                        ListViewItem item = new ListViewItem(order.CustomerId.ToString());
                        item.SubItems.Add(order.Customer.CustomerName);
                        item.SubItems.Add(order.Customer.PhoneNumber);
                        item.SubItems.Add(order.Customer.Email);
                        listViewCustomerDetails.Items.Add(item);
                    }
                    
                           
                    
                    

                }
            }

            
        }

        private void textBoxOrderSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelOrderSearch_Click(object sender, EventArgs e)
        {

        }

        private void labelOrderType_Click(object sender, EventArgs e)
        {

        }

        private void textBoxOrderType_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BookInfoForm frmBookInfoForm = new BookInfoForm();
            frmBookInfoForm.ShowDialog();

            textBoxUnitAvailable.Text = BookInfoForm.SetValuesForAdditionalInfo[2];

        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            if (maskedTextBoxQuantityOrder.Text == "")
            {
                MessageBox.Show("Please input a quantity order");
                return;
            }
            else if (textBoxUnitAvailable.Text == "")
            {
                MessageBox.Show("Please input unity available");
                return;
            }
           
            if (Convert.ToInt32(maskedTextBoxQuantityOrder.Text) > Convert.ToInt32(textBoxUnitAvailable.Text))
            {
                MessageBox.Show("There are not enough units to comply with the quantity order");
                DialogResult dialogResult = MessageBox.Show("Do you want to place the order with the available units?", "Some Title", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    unitsToShip = Convert.ToInt32( textBoxUnitAvailable.Text);
                    remainingUnits = 0;
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            } else if (textBoxUnitAvailable.Text == "0")
            {
                MessageBox.Show("There are not enough units to comply with the quantity order");
                return;
            }
            else
            {
                unitsToShip = Convert.ToInt32(maskedTextBoxQuantityOrder.Text);
                remainingUnits = Convert.ToInt32(textBoxUnitAvailable.Text) -  Convert.ToInt32( maskedTextBoxQuantityOrder.Text);
                
            }
            valueBeforeTax = unitsToShip * Convert.ToDouble( BookInfoForm.SetValuesForAdditionalInfo[1]);
            gstValue = (valueBeforeTax * (double.Parse( ConfigurationManager.AppSettings["GST"]))/100);
            qstValue = (valueBeforeTax * (double.Parse(ConfigurationManager.AppSettings["QST"])) / 100);

            textBoxPayment.Text =  (gstValue + qstValue + valueBeforeTax).ToString("#.##");

        }

        private void buttonPlaceOrder_Click(object sender, EventArgs e)
        {
            
            
            if(textBoxCustomerOrderId.Text != "")
            {
                //int orderId = Convert.ToInt32(textBoxOrderIdInfo.Text);
                if (dateTimePickerShippingDate.Value <= DateTime.Now)
                {
                    MessageBox.Show("Shipping date cannot be less than the current date");
                    return;
                }
                if (dateTimePickerRequiredDate.Value <= dateTimePickerShippingDate.Value)
                {
                    MessageBox.Show("Required date cannot be less than the shipping date");
                    return;
                }

                if (textBoxOrderType.Text == "")
                {
                    MessageBox.Show("Order type cannot be empty", "Invalid order");
                    return;
                }

                if (comboBoxStatusOrder.Text != "")
                {

                    using (var db = new BooksDBEntities4())
                    {
                        Order order = new Order();
                        if (textBoxOrderIdInfo.Text == "")
                            textBoxOrderIdInfo.Text = "0";
                        order = db.Orders.Find(Convert.ToInt32(textBoxOrderIdInfo.Text));
                        if (order == null)
                        {
                            if (maskedTextBoxQuantityOrder.Text == "")
                            {
                                MessageBox.Show("Quantity order cannot be empty", "Invalid order");
                                return;
                            }
                            else if (textBoxPayment.Text == "")
                            {
                                MessageBox.Show("Please calculate the payment", "Invalid order");
                                return;
                            }
                            else
                            {



                                
                                Order order1 = new Order();
                                order1.OrderType = textBoxOrderType.Text;
                                order1.ShippingDate = dateTimePickerShippingDate.Value;
                                order1.StatusOrderId = 2;
                                order1.QuantityOrdered = unitsToShip.ToString();
                                order1.Payment = double.Parse(textBoxPayment.Text);
                                order1.RequiredDate = dateTimePickerRequiredDate.Value;
                                order1.ISBN = BookInfoForm.SetValuesForAdditionalInfo[0];
                                order1.CustomerId = Convert.ToInt32( textBoxCustomerOrderId.Text);
                                order1.EmployeeId = Convert.ToInt32(LoginForm.userEmpId);
                                order1.OrderDate = DateTime.Now;
                                string bookId = BookInfoForm.SetValuesForAdditionalInfo[0];
                                Book book = new Book();
                                book = (db.Books.Where(x => x.ISBN == bookId)).FirstOrDefault();
                                book.QOH = Convert.ToInt32(remainingUnits);
                                //db.Books.Add(book);
                               
                                //
                                db.Orders.Add(order1);
                                db.SaveChanges();
                                MessageBox.Show("Order placed!");
                                textBoxOrderType.Text = "";
                                textBoxOrderCsutomerId.Text = "";
                                textBoxOrderIdInfo.Text = "";
                                textBoxSearchCustomerOrder.Text = "";
                                textBoxOrderSearch.Text = "";
                                textBoxSearchCustomerOrder.Text = "";
                                textBoxStatusOrderDescription.Text = "";
                                comboBoxStatusOrder.SelectedItem = -1;
                                maskedTextBoxQuantityOrder.Text = "";
                                textBoxPayment.Text = "";
                                textBoxUnitAvailable.Text = "";
                                dateTimePickerRequiredDate.Value = DateTime.Now;
                                dateTimePickerShippingDate.Value = DateTime.Now;


                            }

                        }
                        else
                        {
                            MessageBox.Show("Order already exists!", "Invalid order");
                            return;
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Please select a status for the order", "Invalid order");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please search for a customer to place its order");
                return;
            }
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Value before tax: " + valueBeforeTax + "\n" +
                "Value GST tax: " + gstValue + "\n" +
                "Value QST tax: " + qstValue + "\n" +
                "Total after tax: " + textBoxPayment.Text);
        }

        private void buttonAssignCustomer_Click(object sender, EventArgs e)
        {
            if (textBoxOrderCsutomerId.Text != "")
                textBoxOrderCsutomerId.Text = textBoxOrderIdInfo.Text;
            else
                MessageBox.Show("Please search an order to associate to the customer");
        }

        private void buttonListCustomersOrder_Click(object sender, EventArgs e)
        {
            listViewCustomerDetails.Items.Clear();
            using (var db = new BooksDBEntities4())
            {
                // display the list of employees
                var listCustomers = from cus in db.Customers
                                orderby cus.CustomerId
                                select cus;
                foreach (var customer in listCustomers)
                {
                    ListViewItem item = new ListViewItem(customer.CustomerId.ToString());
                    item.SubItems.Add(customer.CustomerName);
                    item.SubItems.Add(customer.PhoneNumber.ToString());
                    item.SubItems.Add(customer.Email.ToString());
                    listViewCustomerDetails.Items.Add(item);
                }



            }
        }

        private void buttonSearchCustomerOrder_Click(object sender, EventArgs e)
        {
            int customerId;

            if (textBoxSearchCustomerOrder.Text == "")
            {
                MessageBox.Show("Please enter a customer id");
                return;
            }
            else
            {
                int cutomerId = Convert.ToInt32(textBoxSearchCustomerOrder.Text);
                using (var db = new BooksDBEntities4())
                {
                    Customer cus = new Customer();


                    cus = (db.Customers.Where(x => x.CustomerId == cutomerId).FirstOrDefault());
                    



                    if (cus != null)
                    {
                        textBoxCustomerOrderId.Text = cus.CustomerId.ToString();
                        listViewCustomerDetails.Items.Clear();
                        ListViewItem item = new ListViewItem(cus.CustomerId.ToString());
                        item.SubItems.Add(cus.CustomerName);
                        item.SubItems.Add(cus.PhoneNumber);
                        item.SubItems.Add(cus.Email);
                        listViewCustomerDetails.Items.Add(item);


                    }
                    else
                    {
                        textBoxSearchCustomerOrder.Focus();
                        textBoxSearchCustomerOrder.Clear();
                        MessageBox.Show("Customer not found!");
                    }


                }
            }

            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            listViewCustomersOrders.Items.Clear();
            using (var db = new BooksDBEntities4())
            {
                // display the list of employees
                var listOrders = from ord in db.Orders
                                    orderby ord.OrderId
                                    select ord;
                foreach (var order in listOrders)
                {
                    ListViewItem item = new ListViewItem(order.OrderId.ToString());
                    item.SubItems.Add(order.RequiredDate.ToString());
                    item.SubItems.Add(order.ShippingDate.ToString());
                    item.SubItems.Add(order.StatusOrder.Status. ToString());
                    item.SubItems.Add(order.Customer.CustomerName.ToString());
                    item.SubItems.Add(order.Employee.FirstName + ", " + order.Employee.LastName);
                    item.SubItems.Add(order.QuantityOrdered.ToString());
                    item.SubItems.Add(order.StatusOrder.Status.ToString());
                    item.SubItems.Add(order.Payment.ToString());
                    listViewCustomersOrders.Items.Add(item);
                }



            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listViewOrderInvoice.Items.Clear();
            using (var db = new BooksDBEntities4())
            {
                // display the list of employees
                var listOrders = from ord in db.Orders
                                 orderby ord.OrderId
                                 select ord;
                foreach (var order in listOrders)
                {
                    ListViewItem item = new ListViewItem(order.OrderId.ToString());
                    item.SubItems.Add(order.RequiredDate.ToString());
                    item.SubItems.Add(order.ShippingDate.ToString());
                    item.SubItems.Add(order.StatusOrder.Status.ToString());
                    item.SubItems.Add(order.Customer.CustomerName.ToString());
                    item.SubItems.Add(order.Employee.FirstName + ", " + order.Employee.LastName);
                    item.SubItems.Add(order.QuantityOrdered.ToString());
                    item.SubItems.Add(order.StatusOrder.Status.ToString());
                    item.SubItems.Add(order.Payment.ToString());
                    listViewOrderInvoice.Items.Add(item);
                }



            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int input;
            if (textBoxSearcOrderInvoice.Text == "")
            {
                MessageBox.Show("Please input an order id", "Invalid order");
                return;
            }
            else
            {
                input = Convert.ToInt32(textBoxSearcOrderInvoice.Text);
                using (var db = new BooksDBEntities4())
                {
                    Order order = new Order();
                    order = (db.Orders.Where(x => x.OrderId.Equals(input))).FirstOrDefault();
                    textBoxInvoice.Text = "************************ INVOICE ***************************\r\n";
                    textBoxInvoice.Text = textBoxInvoice.Text + "\r\n";
                    textBoxCustomerId.Text = textBoxInvoice.Text + "Customer Name: " + order.Customer.CustomerName.ToString();
                    textBoxInvoice.Text = textBoxInvoice.Text + "Order id: " + order.OrderId.ToString() + "\r\n";
                    textBoxInvoice.Text = textBoxInvoice.Text + "Required date: " + order.RequiredDate.ToString() + "\r\n";
                    textBoxInvoice.Text = textBoxInvoice.Text + "Shipping date: " + order.ShippingDate.ToString() + "\r\n";
                    textBoxInvoice.Text = textBoxInvoice.Text + "Quantity ordered: " + order.QuantityOrdered.ToString() + "\r\n";
                    valueBeforeTax = double.Parse( order.QuantityOrdered) * order.Book.UnitPrice;
                    textBoxInvoice.Text = textBoxInvoice.Text + "Value before tax: " + valueBeforeTax.ToString() + "\r\n";
                    gstValue = (valueBeforeTax * (double.Parse(ConfigurationManager.AppSettings["GST"])) / 100);
                    textBoxInvoice.Text = textBoxInvoice.Text + "GST: " + gstValue.ToString() + "\r\n";
                    qstValue = (valueBeforeTax * (double.Parse(ConfigurationManager.AppSettings["QST"])) / 100);
                    textBoxInvoice.Text = textBoxInvoice.Text + "QST: " + qstValue.ToString() + "\r\n";
                    textBoxInvoice.Text = textBoxInvoice.Text + "Total Payment: " + order.Payment.ToString();
                    
                    



                }
            }
        }

        private void buttonDeleteOrder_Click(object sender, EventArgs e)
        {
            int orderId;
            if (textBoxOrderIdInfo.Text == "")
            {
                MessageBox.Show("Please search for an order to delete", "Invalid Info");
            }
            else
            {
                orderId = Convert.ToInt32(textBoxOrderIdInfo.Text);
                using (var db = new BooksDBEntities4())
                {
                    Order order = new Order();
                    order = (db.Orders.Where(x => x.OrderId.Equals(orderId))).FirstOrDefault();

                    if (order != null)
                    {
                        
                          if (order.StatusOrderId == 3 || order.StatusOrderId == 4)
                            {
                                MessageBox.Show("Order status is either delivered or shipped. It cannot be deleted","Inavlid order");
                                return;
                            }
                            else
                            {
                               
                                    db.Orders.Remove(order);
                                    
                               
                               
                            }

                            db.SaveChanges();
                            MessageBox.Show("Order deleted!");
                            textBoxOrderType.Text = "";
                            textBoxOrderCsutomerId.Text = "";
                            textBoxOrderIdInfo.Text = "";
                            textBoxSearchCustomerOrder.Text = "";
                            textBoxOrderSearch.Text = "";
                            textBoxSearchCustomerOrder.Text = "";
                            textBoxStatusOrderDescription.Text = "";
                            comboBoxStatusOrder.SelectedItem = -1;
                            maskedTextBoxQuantityOrder.Text = "";
                            textBoxPayment.Text = "";
                            textBoxUnitAvailable.Text = "";
                            dateTimePickerRequiredDate.Value = DateTime.Now;
                            dateTimePickerShippingDate.Value = DateTime.Now;


                       
                        
                    }
                    else
                    {
                        MessageBox.Show("Author not found!");
                    }


                }
            }
        }

        private void buttonUpdateOrder_Click(object sender, EventArgs e)
        {
            if (textBoxOrderIdInfo.Text != "")
            {
                //int orderId = Convert.ToInt32(textBoxOrderIdInfo.Text);
                if (dateTimePickerShippingDate.Value <= DateTime.Now)
                {
                    MessageBox.Show("Shipping date cannot be less than the current date");
                    return;
                }
                if (dateTimePickerRequiredDate.Value <= dateTimePickerShippingDate.Value)
                {
                    MessageBox.Show("Required date cannot be less than the shipping date");
                    return;
                }

                if (textBoxOrderType.Text == "")
                {
                    MessageBox.Show("Order type cannot be empty", "Invalid order");
                    return;
                }

                if (comboBoxStatusOrder.Text != "")
                {

                    using (var db = new BooksDBEntities4())
                    {
                        Order order = new Order();
                        if (textBoxOrderIdInfo.Text == "")
                            textBoxOrderIdInfo.Text = "0";
                        order = db.Orders.Find(Convert.ToInt32(textBoxOrderIdInfo.Text));
                        if (order != null)
                        {
                            if (maskedTextBoxQuantityOrder.Text == "")
                            {
                                MessageBox.Show("Quantity order cannot be empty", "Invalid order");
                                return;
                            }
                            else if (textBoxPayment.Text == "")
                            {
                                MessageBox.Show("Please calculate the payment", "Invalid order");
                                return;
                            }
                            else
                            {
                                int orderId = Convert.ToInt32( textBoxOrderIdInfo.Text);
                                Order order1 = new Order();
                                order1 = (db.Orders.Where(x => x.OrderId == orderId)).FirstOrDefault();
                                string statusOrder = order1.StatusOrderId.ToString();
                                if (statusOrder == "2" || statusOrder == "4")
                                {

                                    order1.OrderType = textBoxOrderType.Text;
                                    order1.ShippingDate = dateTimePickerShippingDate.Value;
                                    string[] status = comboBoxStatusOrder.Text.Split('|');
                                    order1.StatusOrderId = Convert.ToInt32( status[0]);
                                    order1.QuantityOrdered = unitsToShip.ToString();
                                    order1.Payment = double.Parse(textBoxPayment.Text);
                                    order1.RequiredDate = dateTimePickerRequiredDate.Value;
                                    //order1.ISBN = BookInfoForm.SetValuesForAdditionalInfo[0];
                                    order1.CustomerId = Convert.ToInt32(textBoxCustomerOrderId.Text);
                                    order1.EmployeeId = Convert.ToInt32(LoginForm.userEmpId);
                                    //order1.OrderDate = DateTime.Now;
                                    //string bookId = BookInfoForm.SetValuesForAdditionalInfo[0];
                                    //Book book = new Book();
                                    //book = (db.Books.Where(x => x.ISBN == bookId)).FirstOrDefault();
                                    //order1.Book.QOH = Convert.ToInt32(remainingUnits);
                                    //db.Books.Add(book);

                                    //
                                    //db.Orders.Add(order1);
                                    db.SaveChanges();
                                    MessageBox.Show("Order updated!");
                                    

                                }
                                else
                                {
                                    MessageBox.Show("Order either Shipped or delivered cannot be updated", "Invalid Order");
                                    return;
                                }


                                

                            }

                        }
                        else
                        {
                            MessageBox.Show("Order does not exist!", "Invalid order");
                            return;
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Please select a status for the order", "Invalid order");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please search for an order to update");
                return;
            }
        }

        private void textBoxUnitPrice_KeyPress(object sender, KeyPressEventArgs args)
        {
            const int BACKSPACE = 8;
            const int DECIMAL_POINT = 46;
            const int ZERO = 48;
            const int NINE = 57;
            const int NOT_FOUND = -1;

            int keyvalue = (int)args.KeyChar; // not really necessary to cast to int

            if ((keyvalue == BACKSPACE) ||
            ((keyvalue >= ZERO) && (keyvalue <= NINE))) return;
            // Allow the first (but only the first) decimal point
            if ((keyvalue == DECIMAL_POINT) &&
            (textBoxUnitPrice.Text.IndexOf(".") == NOT_FOUND)) return;
            // Allow nothing else
            args.Handled = true;
        }

        
    }
}
