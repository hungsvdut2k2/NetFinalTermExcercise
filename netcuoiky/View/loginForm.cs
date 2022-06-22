﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using netcuoiky.BLL;
using netcuoiky.DTO;
using netcuoiky.View;

namespace netcuoiky
{
    public partial class loginForm : Form
    {
        public string userId;
        public static loginForm instance;
        public loginForm()
        {
            InitializeComponent();
            instance = this;
        }

        private void signInButton_Click(object sender, EventArgs e)
        {
            string Username = userNameTextBox.Text;
            string Password = passwordTextBox.Text;
            Account loginAccount = Account_BLL.Instance.Login(Username, Password);
            if (loginAccount != null)
            {
                if (loginAccount.role == "Admin")
                {
                    this.Hide();
                    new adminForm().Show();
                }
                else if(loginAccount.role == "Student")
                {
                    userId = loginAccount.userId;
                    this.Hide();
                    new userForm().Show();
                }
                else
                {
                    userId = loginAccount.userId;
                    this.Hide();
                    new View.teacherForm().Show();
                }
            }
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            new forgotPasswordForm().ShowDialog();
        }
    }
}
