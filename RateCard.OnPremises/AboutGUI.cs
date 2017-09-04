using RateCard.Api.Core;
using RateCard.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using System.Runtime.InteropServices;

namespace RateCard.OnPremises
{
    public partial class AboutGUI : MetroFramework.Forms.MetroForm
    {
        private static AboutGUI _instaceAboutGUI;
        private Miscellaneous _Tool_Version;
        private AboutGUI()
        {
            InitializeComponent();
            _Tool_Version = DealManagement.GetInstance().GetToolVersion();
            lblVersion.Text = Convert.ToString(_Tool_Version.ValueAmount);
        }
        public static AboutGUI GetInstance()
        {
            if (_instaceAboutGUI == null)
            {
                _instaceAboutGUI = new AboutGUI();
            }

            return _instaceAboutGUI;
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _instaceAboutGUI = null;
            
        }
        private void AboutGUI_Load(object sender, EventArgs e)
        {
           
        }

        private void metroPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnContactSupport_Click(object sender, EventArgs e)
        {
            try
            {
            
            Microsoft.Office.Interop.Outlook.Application app =
                Marshal.GetActiveObject("Outlook.Application") as Microsoft.Office.Interop.Outlook.Application;
            Microsoft.Office.Interop.Outlook.MailItem mailItem =
                app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

            mailItem.Subject = "DC-OSS Rate Card Contact Support";
            mailItem.To = "pdt@hpe.com";

            mailItem.Display(true);
        } catch (System.Runtime.InteropServices.COMException ex) {                

                Microsoft.Office.Interop.Outlook.Application app =
                new Microsoft.Office.Interop.Outlook.Application();
                Microsoft.Office.Interop.Outlook.MailItem mailItem =
                    app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

                mailItem.Subject = "DC-OSS Rate Card Contact Support";
                mailItem.To = "pdt@hpe.com";

                mailItem.Display(true);
            }

        }  
    }
}
