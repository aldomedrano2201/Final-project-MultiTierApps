using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Final_Project.Entity;

namespace Final_Project.GUI
{
    public partial class CategoryInfoForm : Form
    {
        public static List<string> SetValuesForAdditionalInfo;
        public CategoryInfoForm()
        {
            InitializeComponent();
        }

        private void AddtionalInfoForm_Load(object sender, EventArgs e)
        {

            using (var db = new BooksDBEntities4())
            {
                var list = from x in db.Categories
                           orderby x.CategoryId
                           select x;
                foreach (var item in list)
                {
                    comboBoxAddtionalInfo.Items.Add(item.CategoryId + "|" + item.CategoryName);
                }
            }


        }

        private void comboBoxAddtionalInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] values = comboBoxAddtionalInfo.Text.Split('|');
            if (values != null)
            {
                textBoxId.Text = values[0];
                textBoxInfo.Text = values[1];
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetValuesForAdditionalInfo = new List<string>();
            foreach (var item in comboBoxAddtionalInfo.Items)
            {
                SetValuesForAdditionalInfo.Add((string)item);
            }

            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            int itemId;
            if (textBoxId.Text == "")
                itemId = 0;
            else
                itemId = Convert.ToInt32(textBoxId.Text);

            if (textBoxInfo.Text == "")
            {
                MessageBox.Show("Please enter the category info", "Invalid Category info");
                return;
            }


            using (var db = new BooksDBEntities4())
            {
                Category cat = new Category();
                cat = (db.Categories.Where(x => x.CategoryId == itemId)).FirstOrDefault();

                if (cat != null)
                {
                    MessageBox.Show("Category already exists!");
                }
                else
                {
                    Category cat1 = new Category();
                    cat1.CategoryName = textBoxInfo.Text;
                    db.Categories.Add(cat1);
                    db.SaveChanges();
                    MessageBox.Show("Category saved!");
                    textBoxInfo.Text = "";
                    textBoxId.Text = "";
                }

                comboBoxAddtionalInfo.Items.Clear();

                var list = from x in db.Categories
                               orderby x.CategoryId
                               select x;
                    foreach (var item in list)
                    {
                        comboBoxAddtionalInfo.Items.Add(item.CategoryId + "|" + item.CategoryName);
                    }
               

            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            using (var db = new BooksDBEntities4())
            {
                //UPDATE
                int itemId = Convert.ToInt32(textBoxId.Text);
                Category cat = new Category();
                cat = (db.Categories.Where(x => x.CategoryId == itemId)).FirstOrDefault();
                cat.CategoryId = Convert.ToInt32(textBoxId.Text);
                cat.CategoryName = textBoxInfo.Text;

                db.SaveChanges();
                MessageBox.Show("Category updated!");
                comboBoxAddtionalInfo.Items.Clear();

                var list = from x in db.Categories
                           orderby x.CategoryId
                           select x;
                foreach (var item in list)
                {
                    comboBoxAddtionalInfo.Items.Add(item.CategoryId + "|" + item.CategoryName);
                }

            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int itemId = Convert.ToInt32(textBoxId.Text);





            using (var db = new BooksDBEntities4())
            {

                try
                {
                    var deleteItem =
                from details in db.Categories
                where details.CategoryId == itemId
                select details;

                    if (deleteItem != null)
                    {
                        foreach (var detail in deleteItem)
                        {
                            db.Categories.Remove(detail);


                        }
                        db.SaveChanges();
                        MessageBox.Show("Item deleted!");
                        textBoxInfo.Text = "";
                        textBoxId.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Item not found!");
                    }

                    comboBoxAddtionalInfo.Items.Clear();
                    var list = from x in db.Categories
                               orderby x.CategoryId
                               select x;
                    foreach (var item in list)
                    {
                        comboBoxAddtionalInfo.Items.Add(item.CategoryId + "|" + item.CategoryName);
                    }
                }
                catch(Exception ex)
                {
                   
                    MessageBox.Show("An error ocurred and item could not be deleted!" + ex.Message + ex.InnerException.Source, "Error");
                }
                

            }
        }
    }
}
