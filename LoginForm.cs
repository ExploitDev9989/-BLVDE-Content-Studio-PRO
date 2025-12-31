using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace BLVDEContentStudio
{
    public partial class LoginForm : Form
    {
        private const string USERS_FILE = "users.dat";

        public LoginForm()
        {
            InitializeComponent();
            EnsureAdminExists();
        }

        private void EnsureAdminExists()
        {
            // If user file doesn't exist, create a default admin
            if (!File.Exists(USERS_FILE))
            {
                // Format: username:password
                File.WriteAllLines(USERS_FILE, new string[] { "admin:password" });
                MessageBox.Show("First run detected.\nDefault credentials created:\n\nUser: admin\nPass: password", "SYSTEM_INIT");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string inputUser = txtUser.Text.Trim();
            string inputPass = txtPass.Text.Trim();

            if (Authenticate(inputUser, inputPass))
            {
                // Login Success
                this.Hide();
                MainForm mainForm = new MainForm();
                mainForm.ShowDialog(); // Show main form modally
                this.Close(); // Close login when main form finishes
            }
            else
            {
                MessageBox.Show("ACCESS_DENIED // INVALID_CREDENTIALS", "SECURITY_ALERT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPass.Clear();
            }
        }

        private bool Authenticate(string user, string pass)
        {
            try
            {
                string[] lines = File.ReadAllLines(USERS_FILE);
                foreach (string line in lines)
                {
                    var parts = line.Split(':');
                    if (parts.Length == 2)
                    {
                        if (parts[0] == user && parts[1] == pass)
                        {
                            return true;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        // Helper to add user (can be called from Main Form later)
        public static void AddUser(string user, string pass)
        {
            File.AppendAllText(USERS_FILE, $"{user}:{pass}\n");
        }
    }
}
