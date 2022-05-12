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
    public partial class PublisherInfoForm : Form
    {
        public static List<string> SetValuesForAdditionalInfo;
        public PublisherInfoForm()
        {
            InitializeComponent();
        }

        private void AddtionalInfoForm_Load(object sender, EventArgs e)
        {

            using (var db = new BooksDBEntities4())
            {
                var list = from x in db.Publishers
                           orderby x.PublishersId
                           select x;
                foreach (var item in list)
                {
                    comboBoxAddtionalInfo.Items.Add(item.PublishersId + "|" + item.PublisherName);
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
                MessageBox.Show("Please enter the publisher info", "Invalid Publisher info");
                return;
            }


            using (var db = new BooksDBEntities4())
            {
                Publisher pub = new Publisher();
                pub = (db.Publishers.Where(x => x.PublishersId == itemId)).FirstOrDefault();

                if (pub != null)
                {
                    MessageBox.Show("Publisher already exists!");
                }
                else
                {
                    Publisher pub1 = new Publisher();
                    pub1.PublisherName = textBoxInfo.Text;
                    pub1.WebAddress = textBoxWebAddress.Text;
                    db.Publishers.Add(pub1);
                    db.SaveChanges();
                    MessageBox.Show("Publisher saved!");
                    textBoxInfo.Text = "";
                    textBoxId.Text = "";
                    textBoxWebAddress.Text = "";
                }

                comboBoxAddtionalInfo.Items.Clear();
               
                    var list = from x in db.Publishers
                               orderby x.PublishersId
                               select x;
                    foreach (var item in list)
                    {
                        comboBoxAddtionalInfo.Items.Add(item.PublishersId + "|" + item.PublisherName);
                    }
                
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            using (var db = new BooksDBEntities4())
            {
                //UPDATE
                int itemId = Convert.ToInt32(textBoxId.Text);
                Publisher pub = new Publisher();
                pub = (db.Publishers.Where(x => x.PublishersId == itemId)).FirstOrDefault();
                pub.PublishersId = Convert.ToInt32(textBoxId.Text);
                pub.PublisherName = textBoxInfo.Text;
                pub.WebAddress = textBoxWebAddress.Text;

                db.SaveChanges();
                MessageBox.Show("Publisher updated!");
                comboBoxAddtionalInfo.Items.Clear();

                var list = from x in db.Publishers
                           orderby x.PublishersId
                           select x;
                foreach (var item in list)
                {
                    comboBoxAddtionalInfo.Items.Add(item.PublishersId + "|" + item.PublisherName);
                }

            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int itemId = Convert.ToInt32(textBoxId.Text);
                using (var db = new BooksDBEntities4())
                {
                    var deleteItem =
                    from details in db.Publishers
                    where details.PublishersId == itemId
                    select details;

                    if (deleteItem != null)
                    {
                        foreach (var detail in deleteItem)
                        {
                            db.Publishers.Remove(detail);


                        }
                        db.SaveChanges();
                        MessageBox.Show("Publisher deleted!");
                        textBoxId.Text = "";
                        textBoxInfo.Text = "";
                        textBoxWebAddress.Text = "";

                    }
                    else
                    {
                        MessageBox.Show("Publisher not found!");
                    }

                    comboBoxAddtionalInfo.Items.Clear();

                    var list = from x in db.Publishers
                               orderby x.PublishersId
                               select x;
                    foreach (var item in list)
                    {
                        comboBoxAddtionalInfo.Items.Add(item.PublishersId + "|" + item.PublisherName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error ocurred and item could not be deleted!" + ex.Message + ex.InnerException.Source, "Error");
            }




        }
    }
}
