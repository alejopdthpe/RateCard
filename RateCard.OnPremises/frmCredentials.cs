using RateCard.Api.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RateCard.OnPremises
{
    public partial class frmCredentials : MetroFramework.Forms.MetroForm
    {


 
        public frmCredentials()
        {
            InitializeComponent();
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
       
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            new MainGUI().Show();
        }

        private void frmCredentials_FormClosed(object sender, FormClosedEventArgs e)
        {
            new MainGUI().Show();
        }

        private void frmCredentials_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            bool validEmail = false;
            bool validPassword = false;
            if (txtEmail.Text.Equals(""))
            {
                txtEmail.WithError = true;
                validEmail = false;
            }
            else {
                validEmail = true;
                txtEmail.WithError = false;
            }
            if (txtPassword.Text.Equals(""))
            {
                txtPassword.WithError = true;
                validPassword = false;
            }else
            {
                txtPassword.WithError = false;
                validPassword = true;
            }
            if (validEmail && validPassword) {
                AdministrationManagement.GetInstance().SetUserCredentials(txtEmail.Text, txtPassword.Text);
                this.Hide();
                new frmAdministration().Show();
            }
        }
    }
}
