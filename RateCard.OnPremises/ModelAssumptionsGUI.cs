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
    public partial class ModelAssumptionsGUI : MetroFramework.Forms.MetroForm
    {

        private static ModelAssumptionsGUI _instaceModelAssumpGUI;

        public static ModelAssumptionsGUI GetInstance()
        {
            if (_instaceModelAssumpGUI == null)
            {
                _instaceModelAssumpGUI = new ModelAssumptionsGUI();
            }

            return _instaceModelAssumpGUI;
        }
        private ModelAssumptionsGUI()
        {
            InitializeComponent();
        }
        protected override void Dispose(bool disposing)
        {
            
            base.Dispose(disposing);
            _instaceModelAssumpGUI = null;
        }
        private void ModelAssumptionsGUI_Load(object sender, EventArgs e)
        {

        }
    }
}
