using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecurePasswordStorage
{
    public partial class Form1 : Form
    {
        PasswordManager pm = new PasswordManager();

        public Form1()
        {
            InitializeComponent();
        }

        private void Signup_Click(object sender, EventArgs e)
        {
            HashedPassword hashedUserPassword = pm.HashPassword(textBoxPassword.Text);

            //I use a local file as a "datebase" and use the different lines to store the user
            string[] lines = { textBoxUsername.Text, hashedUserPassword.Hash, Convert.ToBase64String(hashedUserPassword.Salt)};
            using (StreamWriter outputFile = new StreamWriter("User.txt"))
            {
                foreach (string line in lines) { 
                    outputFile.WriteLine(line);
                }
            }

        }

        private void Login_Click(object sender, EventArgs e)
        {
            var userFile = File.ReadAllLines("User.txt");
            var userDetails = new List<string>(userFile);

            HashedPassword ValidateUserPassword = new HashedPassword(Convert.FromBase64String(userDetails[2]), userDetails[1]);

            if(pm.IsValid(textBoxPassword.Text, ValidateUserPassword))
            {
                MessageBox.Show("Status", "Login is correct");
            }
            else
            {
                MessageBox.Show("Status", "Login is wrong");
            }
        }
    }
}
