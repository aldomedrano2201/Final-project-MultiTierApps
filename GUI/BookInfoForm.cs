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
    public partial class BookInfoForm : Form
    {
        public static List<string> SetValuesForAdditionalInfo;
        public BookInfoForm()
        {
            InitializeComponent();
        }

        private void BookInfoForm_Load(object sender, EventArgs e)
        {
            using (var db = new BooksDBEntities4())
            {
                var list = from x in db.Books
                           orderby x.ISBN
                           select x;
                foreach (var item in list)
                {
                    comboBoxBooks.Items.Add(item.ISBN + "|" + item.Title);
                }
            }
        }




        private void buttonClose_Click(object sender, EventArgs e)
        {
            
            this.Close();

        }

        private void comboBoxBooks_SelectedIndexChanged(object sender, EventArgs e)
        {

            using (var db = new BooksDBEntities4())
            {
                string[] values = comboBoxBooks.Text.Split('|');
                string valueBook = values[0];
                var itemBook = (from x in db.Books
                                where x.ISBN == valueBook
                                select new
                                {
                                    BookId = x.ISBN,
                                    UnitPrice = x.UnitPrice,
                                    QOH = x.QOH

                                });
                if (itemBook != null)
                {
                    foreach (var item in itemBook)
                    {
                        SetValuesForAdditionalInfo = new List<string>();
                        textBoxBookDescription.Text = "Unit price: " + item.UnitPrice.ToString() + ", Quantity available: " + item.QOH.ToString();
                        SetValuesForAdditionalInfo.Add(item.BookId.ToString());
                        SetValuesForAdditionalInfo.Add(item.UnitPrice.ToString());
                        SetValuesForAdditionalInfo.Add(item.QOH.ToString());
                    }
                }
            }

        }
    }
}
