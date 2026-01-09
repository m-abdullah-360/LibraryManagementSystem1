using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem1
{
    public partial class LoginForm : Form
    {
        public static string UserRole = "";
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            {
                cmbRole.Items.Add("Librarian");
                cmbRole.Items.Add("Member");   
                cmbRole.SelectedIndex = 0;  
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            {
                UserRole = cmbRole.Text;

                if (cmbRole.Text == "Librarian")
                {
                    if (txtUser.Text == "admin" && txtPass.Text == "123") // Username: admin, Pass: 123
                    {
                        OpenMainForm();
                    }
                    else { MessageBox.Show("wrong Librarian Password!"); }
                }
                else if (cmbRole.Text == "Member")
                {
                    if (txtUser.Text == "user" && txtPass.Text == "123") // Username: user, Pass: 123
                    {
                        OpenMainForm();
                    }
                    else { MessageBox.Show("wrong Member Password!"); }
                }
            }
           
        }
        private void OpenMainForm()
        {
            Form1 mainForm = new Form1(); // Main Form ka object
            mainForm.Show();              // Form1 ko dikhayein
            this.Hide();                  // Login Form ko chupa dein
        }
    }
}
