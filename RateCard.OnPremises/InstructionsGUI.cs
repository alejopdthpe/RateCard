using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RateCard.OnPremises
{
    public partial class InstructionsGUI : MetroFramework.Forms.MetroForm
    {
        private static InstructionsGUI _instanceInstructionsGUI;

        public static InstructionsGUI GetInstance()
        {
            if (_instanceInstructionsGUI == null)
            {
                _instanceInstructionsGUI = new InstructionsGUI();
            }

            return _instanceInstructionsGUI;
        }
        private InstructionsGUI()
        {
            InitializeComponent();
        }
        protected override void Dispose(bool disposing)
        {
            //if (disposing && (components != null))
            //{
            //    InstructionsGUI.GetInstance().Hide();
            //}
           
            base.Dispose(disposing);
            _instanceInstructionsGUI = null;
        }

        private void InstructionsGUI_Load(object sender, EventArgs e)
        {
            GridCase1.Rows.Add("Windows OS", "0");
            GridCase1.Rows.Add("Physical Servers", "10");
            GridCase1.Rows.Add("Unix OS", "0");
            GridCase1.Rows.Add("Other Unix Type", "0");
            GridCase1.Rows.Add("Hypervisor enviroment", "5");
            GridCase1.Rows.Add("VM Guest", "50");

            GridCase2.Rows.Add("Windows OS", "6");
            GridCase2.Rows.Add("Physical Servers", "0");
            GridCase2.Rows.Add("Unix OS", "4");
            GridCase2.Rows.Add("Other Unix Type", "0");
            GridCase2.Rows.Add("Hypervisor enviroment", "5");
            GridCase2.Rows.Add("VM Guest", "50");

            GridCase3.Rows.Add("Storage", "2");
            GridCase3.Rows.Add("Storage 3Par TB QTY", "0");
            GridCase3.Rows.Add("Storage XP TB QTY", "0");
            GridCase3.Rows.Add("Storage Entry TB QTY", "0");

            GridCase4.Rows.Add("Storage", "0");
            GridCase4.Rows.Add("Storage 3Par TB QTY", "400");
            GridCase4.Rows.Add("Storage XP TB QTY", "0");
            GridCase4.Rows.Add("Storage Entry TB QTY", "0");

            GridCase5.Rows.Add("Physical servers", 30,true,false);
        }

        private void metroLabel2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void metroLabel4_Click(object sender, EventArgs e)
        {

        }
    }
}
