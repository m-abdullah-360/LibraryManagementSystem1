using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LibraryManagementSystem1
{
    public partial class Form1 : Form
    {
        string filePath = "LibraryData.txt";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dtpIssue.Value = DateTime.Now;
            dtpReturn.Value = DateTime.Now.AddDays(7);

            listBox1.Font = new Font("Courier New", 10, FontStyle.Bold);
            string header = "ID".PadRight(10) + "Book Name".PadRight(25) + "Author".PadRight(20);
            listBox1.Items.Add(header);
            listBox1.Items.Add("----------------------------------------------------------");

            if (LoginForm.UserRole == "Member")
            {
                btnAdd.Visible = false;
                btnRemove.Visible = false;
                btnSave.Visible = false;
                btnUpdate.Visible = false;
                btnIssue.Visible = false;
                btnReturnBook.Visible = false;

                this.Text = "Library System (Member Mode)";
            }
            else
            {
                this.Text = "Library System (Librarian Mode)";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBookID.Text) || string.IsNullOrWhiteSpace(txtBookName.Text))
            {
                MessageBox.Show("Please fill in the Book details first.");
                return;
            }

            string idCol = txtBookID.Text.PadRight(10);
            string nameCol = txtBookName.Text.PadRight(25);
            string authorCol = txtAuthor.Text.PadRight(20);

            string fullRow = idCol + nameCol + authorCol;
            listBox1.Items.Add(fullRow);

            txtBookID.Clear();
            txtBookName.Clear();
            txtAuthor.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter sw = new StreamWriter(filePath);
                foreach (var item in listBox1.Items)
                {
                    sw.WriteLine(item.ToString());
                }
                sw.Close();
                MessageBox.Show("All book records have been saved to the file successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving file: " + ex.Message);
            }
        }

        private void btnSearchGrid_Click(object sender, EventArgs e)
        {
            string find = txtSearch.Text.ToLower();
            bool found = false;

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.Items[i].ToString().ToLower().Contains(find))
                {
                    listBox1.SelectedIndex = i;
                    found = true;
                    break;
                }
            }

            if (!found) MessageBox.Show("Book not found!");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtBookID.Clear();
            txtBookName.Clear();
            txtAuthor.Clear();
            txtSearch.Clear();
            listBox1.SelectedIndex = -1;

            MessageBox.Show("All fields have been reset.");
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > 1) 
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                MessageBox.Show("Book record removed from the list.");
            }
            else
            {
                MessageBox.Show("Please select a book from the list to remove.");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > 1)
            {
                string updatedRecord = txtBookID.Text.PadRight(10) +
                                       txtBookName.Text.PadRight(25) +
                                       txtAuthor.Text.PadRight(20);

                listBox1.Items[listBox1.SelectedIndex] = updatedRecord;
                MessageBox.Show("Record updated successfully!");
            }
            else
            {
                MessageBox.Show("Please select a book from the list to update.");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > 1)
            {
                string selectedRow = listBox1.SelectedItem.ToString();
                txtBookID.Text = selectedRow.Substring(0, 10).Trim();
                txtBookName.Text = selectedRow.Substring(10, 25).Trim();
                txtAuthor.Text = selectedRow.Substring(35).Trim();
            }
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > 1)
            {
                string selectedBook = listBox1.SelectedItem.ToString();
                string issueDate = dtpIssue.Value.ToShortDateString();
                string returnDate = dtpReturn.Value.ToShortDateString();

                string borrowRecord = "BOOK: " + selectedBook.Trim() +
                                      " | ISSUED: " + issueDate +
                                      " | DUE: " + returnDate;

                File.AppendAllText("BorrowedBooks.txt", borrowRecord + Environment.NewLine);
                MessageBox.Show("Book issued successfully!\nDue Date: " + returnDate);
            }
            else
            {
                MessageBox.Show("Please select a book to issue.");
            }
        }

        private void btnReturnBook_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > 1)
            {
                string bookName = listBox1.SelectedItem.ToString().Trim();
                MessageBox.Show("Book: " + bookName + "\nHas been returned successfully. Record updated!");
                listBox1.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Please select the returning book from the list.");
            }
        }
    }
}