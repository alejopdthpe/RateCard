using Microsoft.SharePoint.Client;
using RateCard.Api.Core;
using RateCard.ApiCore.Dao;
using RateCard.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RateCard.OnPremises
{
    public partial class MainGUI : MetroFramework.Forms.MetroForm
    {
        private double HCCostAvg;
        private double LicensePlatCostAvg;
        private double PMSustanceAvg;
        private double TransitionDeployCost;
        private string CustomerName;
        private string SAName;
        private string LocationCountry;
        private string SLA;
        private string RegionAOH;


        private string userName = "";
        private string userPassword = "";

        public MainGUI()
        {
            InitializeComponent();

        }

        private void txtCustName_MouseHover(object sender, EventArgs e)
        {
            //txtCustName.CustomButton.FlatAppearance.BorderColor = Color.FromArgb(1,169,130);
            txtCustName.CustomButton.FlatAppearance.BorderColor = Color.Gold;
        }

        private void metroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void metroTabPage1_Click(object sender, EventArgs e)
        {

        }

        private void GridCostPerYear_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void MainGUI_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture =
            new System.Globalization.CultureInfo("en-US", false);
            LoadCmbBoxLocation();
            LoadCmbBoxAOH();
            LoadCmbBoxSLA();
            FillGridTechnologies();
            TabControl.DisableTab(costTab);
            TabControl.DisableTab(pricingTab);
            TabControl.DisableTab(TabCasperCostInput);

        }
        private void FillGridTechnologies()
        {
            var _Deal_Manager = DealManagement.GetInstance();
            for (int i = 0; i < _Deal_Manager.List_Technologies.Count; i++)
            {
                GridTecnologies.Rows.Add(_Deal_Manager.List_Technologies[i].Description, 0, true, true);
            }
        }

        private void LoadCmbBoxLocation()
        {
            var _Deal_Manager = DealManagement.GetInstance();
            CmbBoxLocation.Items.Add("-- Select a country --");
            for (int i = 0; i < _Deal_Manager.List_Location_Costs.Count; i++)
            {
                this.CmbBoxLocation.Items.Add(_Deal_Manager.List_Location_Costs[i].Country);
            }
            CmbBoxLocation.Text = "-- Select a country --";
        }
        private void LoadCmbBoxAOH()
        {
            var _Deal_Manager = DealManagement.GetInstance();
            CmbBoxRegionAOH.Items.Add("-- Select an AOH --");
            for (int i = 0; i < _Deal_Manager.List_AOH.Count; i++)
            {
                string clean = Regex.Replace(_Deal_Manager.List_AOH[i].Detail, "AOH_", "");
                this.CmbBoxRegionAOH.Items.Add(clean);
            }
            CmbBoxRegionAOH.Text = "-- Select an AOH --";
        }
        private void LoadCmbBoxSLA()
        {
            var _Deal_Manager = DealManagement.GetInstance();
            CmbBoxSLA.Items.Add("-- Select SLA --");
            for (int i = 0; i < _Deal_Manager.List_SLAs.Count; i++)
            {
                this.CmbBoxSLA.Items.Add(_Deal_Manager.List_SLAs[i].Description);
            }
            CmbBoxSLA.Text = "-- Select SLA --";

        }

        private void ValidateUpdateTargetMargin()
        {
            var _Deal_Manager = DealManagement.GetInstance();
            double targetMargin;
            try
            {
                targetMargin = Convert.ToDouble(txtTargetMargin.Text);
                targetMargin = targetMargin / 100;
                if (targetMargin != _Deal_Manager.TARGET_MARGIN.ValueAmount)
                {
                    DialogResult dialog = MessageBox.Show(
                        "The Target Margin value was changed, do you want to update the value?",
                        "Rate Card", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        _Deal_Manager.TARGET_MARGIN.ValueAmount = targetMargin;
                    }

                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(
                        "The Target Margin value is not valid");
            }


        }
        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (AreCmbBoxesValid())
            {
                ValidateUpdateTargetMargin();
                var _Deal_Manager = DealManagement.GetInstance();

                if (AreEnteredUnitsValid())
                {
                    lblErrorMssg.Visible = false;
                    lblErrorMssgUnits.Visible = false;
                    CalculateOutputs();
                    TabControl.EnableTab(costTab);
                    TabControl.EnableTab(pricingTab);
                    TabControl.EnableTab(TabCasperCostInput);
                    TabControl.SelectTab(costTab);
                }
                else
                {
                    GridTecnologies.CurrentCell = GridTecnologies.Rows[0].Cells[1];
                    GridTecnologies.BeginEdit(true);
                }
            }
        }
        private void CalculateOutputs()
        {
            FillListOfSelectedTechnologies();
            LoadGridCostPerYear();
            LoadGridCostPerUnit();
            LoadGridPricePerYear();
            LoadGridTechUnitPrice();
            LoadGridFixedPrice();
            LoadGridCasperCost();
        }
        private void RecalculateOutputs()
        {
            if (AreCmbBoxesValid() && AreEnteredUnitsValid())
            {
                CalculateOutputs();
            }

        }
        private bool AreCmbBoxesValid()
        {
            bool validData = false;

            if (!(CmbBoxLocation.SelectedItem.Equals("-- Select a country --")) &&
                !(CmbBoxSLA.SelectedItem.Equals("-- Select SLA --")) &&
                !(CmbBoxRegionAOH.SelectedItem.Equals("-- Select an AOH --")))
            {
                CustomerName = txtCustName.Text.ToString();
                SAName = txtSAName.Text.ToString();
                validData = true;
                lblErrorMssg.Visible = false;
            }
            else
            {
                validData = false;
                lblErrorMssg.Visible = true;
            }
            return validData;

        }
        private void SetSLAOnDealManager()
        {
            var _Deal_Manager = DealManagement.GetInstance();
            foreach (var Sla in _Deal_Manager.List_SLAs)
            {
                if (!(CmbBoxSLA.SelectedItem.ToString().Equals("-- Select SLA --")))
                {
                    if (Sla.Description.Equals(CmbBoxSLA.SelectedItem.ToString()))
                    {
                        _Deal_Manager.Selected_SLA = Sla;
                        RecalculateOutputs();
                    }
                }

            }
        }
        private void SetLocationCostOnDealManager()
        {
            var _Deal_Manager = DealManagement.GetInstance();
            foreach (var location in _Deal_Manager.List_Location_Costs)
            {
                if (!(CmbBoxLocation.SelectedItem.ToString().Equals("-- Select a country --")))
                {
                    if (location.Country.Equals(CmbBoxLocation.SelectedItem.ToString()))
                    {
                        _Deal_Manager.Selected_Country = location;
                        RecalculateOutputs();
                    }
                }

            }

        }
        private bool AreEnteredUnitsValid()
        {
            bool valid = true;
            int sumUnits = 0;
            GridTecnologies.EndEdit();
            foreach (DataGridViewRow row in GridTecnologies.Rows)
            {
                if (Convert.ToInt32(row.Cells[1].Value) > 0)
                {
                    row.Cells[1].Value = Convert.ToInt32(row.Cells[1].Value);
                }
                sumUnits += Convert.ToInt32(row.Cells[1].Value);
            }

            if (sumUnits == 0)
            {
                lblErrorMssgUnits.Visible = true;
                valid = false;
            }
            else
            {
                valid = true;
            }
            return valid;
        }

        private void FillListOfSelectedTechnologies()
        {

            var _Deal_Manager = DealManagement.GetInstance();
            _Deal_Manager.ListOfSelectedTechnologies.Clear();
            foreach (DataGridViewRow row in GridTecnologies.Rows)
            {
                try
                {
                    if (Convert.ToInt32(row.Cells[1].Value) > 0)
                    {
                        var selectedTech = new SelectedTechnology();
                        selectedTech.Description = row.Cells[0].Value.ToString();
                        selectedTech.Quantity = Convert.ToInt32(row.Cells[1].Value.ToString());
                        selectedTech.HasMonitoring = Convert.ToBoolean(row.Cells[2].Value);
                        selectedTech.HasOperAndAdmin = Convert.ToBoolean(row.Cells[3].Value);
                        _Deal_Manager.ListOfSelectedTechnologies.Add(selectedTech);
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Please select a valid amount for units");
                }

            }

        }
        private void GridTecnologies_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadGridCostPerYear()
        {
            GridCostPerYear.Rows.Clear();
            LoadHCCostGridCostPerYear();
            LoadLicensesAndPlatformGridCostPerYear();
            LoadPMToolSustenanceGridCostPerYear();
            LoadTransitionDeploymentGridCostPerYear();
            LoadTotalsCostPerYear();

        }
        private void LoadHCCostGridCostPerYear()
        {
            var _CostSummary_Manager = CostSummaryManagement.GetInstance();
            double[] amounts = _CostSummary_Manager.GetHCCostGridCost4Years();
            this.HCCostAvg = 0;

            for (int i = 0; i < amounts.Length; i++)
            {
                this.HCCostAvg += amounts[i];
            }
            this.HCCostAvg = (HCCostAvg / amounts.Length);
            GridCostPerYear.Rows.Add("HC Cost", amounts[0].ToString("C", CultureInfo.CurrentCulture),
                                    amounts[1].ToString("C", CultureInfo.CurrentCulture),
                                    amounts[2].ToString("C", CultureInfo.CurrentCulture),
                                    amounts[3].ToString("C", CultureInfo.CurrentCulture));
        }

        private void LoadLicensesAndPlatformGridCostPerYear()
        {
            double amount = GetLicensesPlatformCost();

            this.LicensePlatCostAvg = amount;

            GridCostPerYear.Rows.Add("Licenses & Platform", amount.ToString("C", CultureInfo.CurrentCulture),
                                    amount.ToString("C", CultureInfo.CurrentCulture),
                                    amount.ToString("C", CultureInfo.CurrentCulture),
                                    amount.ToString("C", CultureInfo.CurrentCulture));
        }
        private double GetLicensesPlatformCost()
        {
            double amount = 0;

            var _CostSummary_Manager = CostSummaryManagement.GetInstance();
            amount = _CostSummary_Manager.GetLicensesAndPlatformCostPerYear();

            return amount;
        }
        private void LoadPMToolSustenanceGridCostPerYear()
        {
            var _CostSummary_Manager = CostSummaryManagement.GetInstance();
            double[] amounts = _CostSummary_Manager.GetPMToolSustenanceGridCost4Year();


            for (int i = 0; i < amounts.Length; i++)
            {
                this.PMSustanceAvg += amounts[i];
            }
            this.PMSustanceAvg = (PMSustanceAvg / amounts.Length);

            GridCostPerYear.Rows.Add("PM & Tool Sustenance", amounts[0].ToString("C", CultureInfo.CurrentCulture),
                                    amounts[1].ToString("C", CultureInfo.CurrentCulture),
                                    amounts[2].ToString("C", CultureInfo.CurrentCulture),
                                    amounts[3].ToString("C", CultureInfo.CurrentCulture));
        }

        private void LoadTransitionDeploymentGridCostPerYear()
        {
            var _Deal_Manager = DealManagement.GetInstance();
            var _CostSummary_Manager = CostSummaryManagement.GetInstance();
            double amountY1 = _Deal_Manager.GetTransitionDeploymentCost();
            this.TransitionDeployCost = amountY1;
            double nullAmount = 0;
            GridCostPerYear.Rows.Add("Transition / Deployment", amountY1.ToString("C", CultureInfo.CurrentCulture),
                                    nullAmount.ToString("C", CultureInfo.CurrentCulture),
                                    nullAmount.ToString("C", CultureInfo.CurrentCulture),
                                    nullAmount.ToString("C", CultureInfo.CurrentCulture));
        }

        private void LoadTotalsCostPerYear()
        {
            var _CostSummary_Manager = CostSummaryManagement.GetInstance();
            var _Deal_Manager = DealManagement.GetInstance();
            double[] HCCost4Year = _CostSummary_Manager.GetHCCostGridCost4Years();
            double PlatFormCostY1 = GetLicensesPlatformCost();
            double[] PMToolSust4year = _CostSummary_Manager.GetPMToolSustenanceGridCost4Year();
            double TransDepCostY1 = _Deal_Manager.GetTransitionDeploymentCost();
            double[] TransDepCost4Year = new double[4];
            TransDepCost4Year[0] = TransDepCostY1;
            TransDepCost4Year[1] = 0;
            TransDepCost4Year[2] = 0;
            TransDepCost4Year[3] = 0;
            double totalY1 = ((HCCost4Year[0] + PlatFormCostY1 + PMToolSust4year[0] + TransDepCost4Year[0]) *
                              GetSelectedSLA().Value_Amnt) + (HCCost4Year[0] + PlatFormCostY1 + PMToolSust4year[0] + TransDepCost4Year[0]);
            double totalY2 = (HCCost4Year[1] + PlatFormCostY1 + PMToolSust4year[1] + TransDepCost4Year[1]);
            double totalY3 = (HCCost4Year[2] + PlatFormCostY1 + PMToolSust4year[2] + TransDepCost4Year[2]);
            double totalY4 = (HCCost4Year[3] + PlatFormCostY1 + PMToolSust4year[3] + TransDepCost4Year[3]);

            GridCostPerYear.Rows.Add("Total Cost", totalY1.ToString("C", CultureInfo.CurrentCulture),
                                    totalY2.ToString("C", CultureInfo.CurrentCulture),
                                    totalY3.ToString("C", CultureInfo.CurrentCulture),
                                    totalY4.ToString("C", CultureInfo.CurrentCulture));
        }

        private SLA GetSelectedSLA()
        {
            var _Deal_Manager = DealManagement.GetInstance();

            return _Deal_Manager.Selected_SLA;

        }
        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in GridTecnologies.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[2];
                if (ChkBxMonit.Checked == false)
                {
                    chk.Value = chk.FalseValue;
                }
                else
                {
                    chk.Value = true;
                }
            }

        }

        private void ChkBxOpAdmin_CheckStateChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in GridTecnologies.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[3];
                if (ChkBxOpAdmin.Checked == false)
                {
                    chk.Value = chk.FalseValue;
                }
                else
                {
                    chk.Value = true;
                }
            }

        }

        private void LoadGridCostPerUnit()
        {
            GridCostPerUnit.Rows.Clear();
            var _Deal_Manager = DealManagement.GetInstance();
            var _CostSummary_Manager = CostSummaryManagement.GetInstance();
            foreach (var selectedTech in _Deal_Manager.ListOfSelectedTechnologies)
            {
                double[] amounts4Years = _CostSummary_Manager.GetTotalUnitCostPerYear(selectedTech);
                GridCostPerUnit.Rows.Add(selectedTech.Description,
                                      amounts4Years[0].ToString("C", CultureInfo.CurrentCulture),
                                      amounts4Years[1].ToString("C", CultureInfo.CurrentCulture),
                                      amounts4Years[2].ToString("C", CultureInfo.CurrentCulture),
                                      amounts4Years[3].ToString("C", CultureInfo.CurrentCulture));
            }
            GridCostPerUnit.Height = 52;
            int gridHeight = GridCostPerUnit.Height;
            foreach (var row in GridCostPerUnit.Rows)
            {
                gridHeight += 26;
            }
            GridCostPerUnit.Height = gridHeight;
        }


        // Pricing
        private void LoadGridPricePerYear()
        {
            GridNetPrice.Rows.Clear();
            LoadHCPriceGridPricePerYear();
            LoadLicensesAndPlatformGridPricePerYear();
            LoadPMToolSustenanceGridPricePerYear();
            LoadTransitionDeploymentGridPricePerYear();
            LoadTotalPricePerYear();

        }

        private void LoadHCPriceGridPricePerYear()
        {
            var _CostSummary_Manager = CostSummaryManagement.GetInstance();
            double[] amounts = _CostSummary_Manager.GetHCPriceGridCost4Years();
            GridNetPrice.Rows.Add("HC Cost", amounts[0].ToString("C", CultureInfo.CurrentCulture),
                                    amounts[1].ToString("C", CultureInfo.CurrentCulture),
                                    amounts[2].ToString("C", CultureInfo.CurrentCulture),
                                    amounts[3].ToString("C", CultureInfo.CurrentCulture));
        }

        private void LoadLicensesAndPlatformGridPricePerYear()
        {

            var _CostSummary_Manager = CostSummaryManagement.GetInstance();
            double amount = GetLicensesPlatformCost();
            double[] amounts4Year = new double[4];
            amounts4Year[0] = amount;
            amounts4Year[1] = amount;
            amounts4Year[2] = amount;
            amounts4Year[3] = amount;
            amounts4Year = _CostSummary_Manager.GetLicensesPlatformPrice4Year(amounts4Year);
            GridNetPrice.Rows.Add("Licenses & Platform", amounts4Year[0].ToString("C", CultureInfo.CurrentCulture),
                                    amounts4Year[1].ToString("C", CultureInfo.CurrentCulture),
                                    amounts4Year[2].ToString("C", CultureInfo.CurrentCulture),
                                    amounts4Year[3].ToString("C", CultureInfo.CurrentCulture));
        }

        private void LoadPMToolSustenanceGridPricePerYear()
        {

            var _CostSummary_Manager = CostSummaryManagement.GetInstance();
            double[] amounts = _CostSummary_Manager.GetPMToolSustenanceGridPrice4Year();

            GridNetPrice.Rows.Add("PM & Tool Sustenance", amounts[0].ToString("C", CultureInfo.CurrentCulture),
                                    amounts[1].ToString("C", CultureInfo.CurrentCulture),
                                    amounts[2].ToString("C", CultureInfo.CurrentCulture),
                                    amounts[3].ToString("C", CultureInfo.CurrentCulture));
        }

        private void LoadTransitionDeploymentGridPricePerYear()
        {
            var _Deal_Manager = DealManagement.GetInstance();
            var _CostSummary_Manager = CostSummaryManagement.GetInstance();
            double amountY1 = _Deal_Manager.GetTransitionDeploymentCost();
            double[] amounts4Year = _CostSummary_Manager.GetTransitionDeploymentPrice(amountY1);

            double nullAmount = 0;
            GridNetPrice.Rows.Add("Transition / Deployment", amountY1.ToString("C", CultureInfo.CurrentCulture),
                                    nullAmount.ToString("C", CultureInfo.CurrentCulture),
                                    nullAmount.ToString("C", CultureInfo.CurrentCulture),
                                    nullAmount.ToString("C", CultureInfo.CurrentCulture));
        }

        private void LoadTotalPricePerYear()
        {
            var _Deal_Manager = DealManagement.GetInstance();
            var _CostSummary_Manager = CostSummaryManagement.GetInstance();
            double PlatFormCostAmount = GetLicensesPlatformCost();
            double[] PlatFormPriceAmounts4Year = new double[4];
            PlatFormPriceAmounts4Year[0] = PlatFormCostAmount;
            PlatFormPriceAmounts4Year[1] = PlatFormCostAmount;
            PlatFormPriceAmounts4Year[2] = PlatFormCostAmount;
            PlatFormPriceAmounts4Year[3] = PlatFormCostAmount;

            double[] HCPrice4Year = _CostSummary_Manager.GetHCPriceGridCost4Years();
            double[] PlatFormCost4Year = _CostSummary_Manager.GetLicensesPlatformPrice4Year(PlatFormPriceAmounts4Year);
            double[] PMToolSust4year = _CostSummary_Manager.GetPMToolSustenanceGridPrice4Year();


            double TransDepCostY1 = _Deal_Manager.GetTransitionDeploymentCost();
            double[] TransDepPrice4Year = _CostSummary_Manager.GetTransitionDeploymentPrice(TransDepCostY1);



            double totalY1 = ((HCPrice4Year[0] + PlatFormPriceAmounts4Year[0] + PMToolSust4year[0] + TransDepPrice4Year[0]) *
                              GetSelectedSLA().Value_Amnt) + (HCPrice4Year[0] + PlatFormPriceAmounts4Year[0] + PMToolSust4year[0] + TransDepPrice4Year[0]);
            double totalY2 = (HCPrice4Year[1] + PlatFormPriceAmounts4Year[1] + PMToolSust4year[1] + TransDepPrice4Year[1]);
            double totalY3 = (HCPrice4Year[2] + PlatFormPriceAmounts4Year[2] + PMToolSust4year[2] + TransDepPrice4Year[2]);
            double totalY4 = (HCPrice4Year[3] + PlatFormPriceAmounts4Year[3] + PMToolSust4year[3] + TransDepPrice4Year[3]);

            GridNetPrice.Rows.Add("Total Net Price", totalY1.ToString("C", CultureInfo.CurrentCulture),
                                   totalY2.ToString("C", CultureInfo.CurrentCulture),
                                   totalY3.ToString("C", CultureInfo.CurrentCulture),
                                   totalY4.ToString("C", CultureInfo.CurrentCulture));
        }

        private void LoadGridTechUnitPrice()
        {
            GridTechUnitPrice.Rows.Clear();
            GridTechUnitPrice.Height = 53;
            int gridHeight = GridTechUnitPrice.Height;
            var _Deal_Manager = DealManagement.GetInstance();
            var _CostSummary_Manager = CostSummaryManagement.GetInstance();
            foreach (var selectedTech in _Deal_Manager.ListOfSelectedTechnologies)
            {
                double[] amounts4Years = _CostSummary_Manager.GetTotalUnitCostPerYear(selectedTech);
                amounts4Years = _CostSummary_Manager.GetTotalUnitPricePerYear(amounts4Years);
                GridTechUnitPrice.Rows.Add(selectedTech.Description,
                                      amounts4Years[0].ToString("C", CultureInfo.CurrentCulture),
                                      amounts4Years[1].ToString("C", CultureInfo.CurrentCulture),
                                      amounts4Years[2].ToString("C", CultureInfo.CurrentCulture),
                                      amounts4Years[3].ToString("C", CultureInfo.CurrentCulture));
            }


            foreach (var row in GridTechUnitPrice.Rows)
            {
                gridHeight += 26;
            }
            GridTechUnitPrice.Height = gridHeight;
        }

        private void LoadGridFixedPrice()
        {
            GridFixedPrice.Rows.Clear();
            var _Deal_Manager = DealManagement.GetInstance();
            var _CostSummary_Manager = CostSummaryManagement.GetInstance();
            double amountY1 = _Deal_Manager.GetTransitionDeploymentCost();
            double[] amounts4Year = _CostSummary_Manager.GetTransitionDeploymentPrice(amountY1);

            double nullAmount = 0;
            GridFixedPrice.Rows.Add("Transition / Deployment", amountY1.ToString("C", CultureInfo.CurrentCulture),
                                    nullAmount.ToString("C", CultureInfo.CurrentCulture),
                                    nullAmount.ToString("C", CultureInfo.CurrentCulture),
                                    nullAmount.ToString("C", CultureInfo.CurrentCulture));

            double[] amountsTooling4Year = _CostSummary_Manager.GetToolingPrice();
            GridFixedPrice.Rows.Add("Tooling Price", amountsTooling4Year[0].ToString("C", CultureInfo.CurrentCulture),
                        amountsTooling4Year[1].ToString("C", CultureInfo.CurrentCulture),
                        amountsTooling4Year[2].ToString("C", CultureInfo.CurrentCulture),
                        amountsTooling4Year[3].ToString("C", CultureInfo.CurrentCulture));

            GridFixedPrice.Height = 52;
            int gridHeight = GridFixedPrice.Height;
            foreach (var row in GridFixedPrice.Rows)
            {
                gridHeight += 28;
            }
            GridFixedPrice.Height = gridHeight;
        }

        private void GridTecnologies_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (GridTecnologies.CurrentCell.ColumnIndex == 1) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }

        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void CmbBoxLocation_SelectedValueChanged(object sender, EventArgs e)
        {

            SetLocationCostOnDealManager();
            LocationCountry = CmbBoxLocation.SelectedItem.ToString();
        }

        private void CmbBoxSLA_SelectedValueChanged(object sender, EventArgs e)
        {

            SetSLAOnDealManager();
            SLA = CmbBoxSLA.SelectedItem.ToString();
        }

        private void ExportToExcel()
        {
            // Creating a Excel object. 
            Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            try
            {

                worksheet = workbook.ActiveSheet;
                worksheet.Name = "DC OSS Rate Card";

                worksheet.Cells[1, 2] = "DC OSS Rate Card Outputs Report";
                worksheet.Cells[1, 2].Font.Size = 20;
                worksheet.Cells[1, 2].Font.Name = "MetricHPE Medium";


                worksheet.Cells[3, 1] = "Customer Name";
                worksheet.Cells[3, 1].Font.Size = 14;
                worksheet.Cells[3, 1].Font.Name = "MetricHPE Light";
                worksheet.Cells[3, 2] = CustomerName;
                worksheet.Cells[3, 2].Font.Name = "MetricHPE Light";
                worksheet.Cells[3, 2].Font.Size = 14;
                worksheet.Cells[3, 2].Font.Bold = true;

                worksheet.Cells[3, 3] = "Location Cost";
                worksheet.Cells[3, 3].Font.Size = 14;
                worksheet.Cells[3, 3].Font.Name = "MetricHPE Light";
                worksheet.Cells[3, 4] = LocationCountry;
                worksheet.Cells[3, 4].Font.Name = "MetricHPE Light";
                worksheet.Cells[3, 4].Font.Size = 14;
                worksheet.Cells[3, 4].Font.Bold = true;


                worksheet.Cells[3, 5] = "Region AOH";
                worksheet.Cells[3, 5].Font.Size = 14;
                worksheet.Cells[3, 5].Font.Name = "MetricHPE Light";
                worksheet.Cells[3, 6] = RegionAOH;
                worksheet.Cells[3, 6].Font.Name = "MetricHPE Light";
                worksheet.Cells[3, 6].Font.Size = 14;
                worksheet.Cells[3, 6].Font.Bold = true;

                worksheet.Cells[4, 1] = "SA Name";
                worksheet.Cells[4, 1].Font.Size = 14;
                worksheet.Cells[4, 1].Font.Name = "MetricHPE Light";
                worksheet.Cells[4, 2] = SAName;
                worksheet.Cells[4, 2].Font.Name = "MetricHPE Light";
                worksheet.Cells[4, 2].Font.Size = 14;
                worksheet.Cells[4, 2].Font.Bold = true;

                worksheet.Cells[4, 3] = "SLA";
                worksheet.Cells[4, 3].Font.Size = 14;
                worksheet.Cells[4, 3].Font.Name = "MetricHPE Light";
                worksheet.Cells[4, 4] = SLA;
                worksheet.Cells[4, 4].Font.Name = "MetricHPE Light";
                worksheet.Cells[4, 4].Font.Size = 14;
                worksheet.Cells[4, 4].Font.Bold = true;


                //Net price per year
                worksheet.Cells[6, 1] = "General Net Price Per Year";
                worksheet.Cells[6, 1].Font.Size = 16;
                worksheet.Cells[6, 1].Font.Name = "MetricHPE Medium";

                int cellRowIndex = 7;
                int cellColumnIndex = 1;
                int totalRowSum = 7;

                for (int i = 0; i < GridNetPrice.Rows.Count + 1; i++)
                {

                    for (int j = 0; j < GridNetPrice.Columns.Count; j++)
                    {
                        // Excel index starts from 1,1. As first Row would have the Column headers, adding a condition check. 
                        if (cellRowIndex == totalRowSum)
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = GridNetPrice.Columns[j].HeaderText;
                            worksheet.Cells[cellRowIndex, cellColumnIndex].Font.Size = 14;
                            worksheet.Cells[cellRowIndex, cellColumnIndex].Font.Name = "MetricHPE Light";
                        }
                        else
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = GridNetPrice.Rows[i - 1].Cells[j].Value.ToString();
                            worksheet.Cells[cellRowIndex, cellColumnIndex].Font.Size = 14;
                            worksheet.Cells[cellRowIndex, cellColumnIndex].Font.Name = "MetricHPE Light";
                        }
                        cellColumnIndex++;
                    }
                    cellColumnIndex = 1;
                    cellRowIndex++;

                }
                totalRowSum += GridNetPrice.Rows.Count;
                totalRowSum += 2;

                worksheet.Cells[totalRowSum, 1] = "General Fixed Price Per Year";
                worksheet.Cells[totalRowSum, 1].Font.Size = 16;
                worksheet.Cells[totalRowSum, 1].Font.Name = "MetricHPE Medium";
                totalRowSum += 1;

                cellRowIndex = totalRowSum;
                cellColumnIndex = 1;
                //Fixed price grid


                for (int i = 0; i < GridFixedPrice.Rows.Count + 1; i++)
                {

                    for (int j = 0; j < GridFixedPrice.Columns.Count; j++)
                    {
                        // Excel index starts from 1,1. As first Row would have the Column headers, adding a condition check. 
                        if (cellRowIndex == totalRowSum)
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = GridFixedPrice.Columns[j].HeaderText;
                            worksheet.Cells[cellRowIndex, cellColumnIndex].Font.Size = 14;
                            worksheet.Cells[cellRowIndex, cellColumnIndex].Font.Name = "MetricHPE Light";

                        }
                        else
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = GridFixedPrice.Rows[i - 1].Cells[j].Value.ToString();
                            worksheet.Cells[cellRowIndex, cellColumnIndex].Font.Size = 14;
                            worksheet.Cells[cellRowIndex, cellColumnIndex].Font.Name = "MetricHPE Light";
                        }
                        cellColumnIndex++;
                    }
                    cellColumnIndex = 1;
                    cellRowIndex++;

                }

                totalRowSum += GridFixedPrice.Rows.Count;
                totalRowSum += 2;

                worksheet.Cells[totalRowSum, 1] = "Technology Unit Price Per Year";
                worksheet.Cells[totalRowSum, 1].Font.Size = 16;
                worksheet.Cells[totalRowSum, 1].Font.Name = "MetricHPE Medium";
                totalRowSum += 1;

                cellRowIndex = totalRowSum;
                cellColumnIndex = 1;

                //Tech unit price grid

                for (int i = 0; i < GridTechUnitPrice.Rows.Count + 1; i++)
                {

                    for (int j = 0; j < GridTechUnitPrice.Columns.Count; j++)
                    {
                        // Excel index starts from 1,1. As first Row would have the Column headers, adding a condition check. 
                        if (cellRowIndex == totalRowSum)
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = GridTechUnitPrice.Columns[j].HeaderText;
                            worksheet.Cells[cellRowIndex, cellColumnIndex].Font.Size = 14;
                            worksheet.Cells[cellRowIndex, cellColumnIndex].Font.Name = "MetricHPE Light";

                        }
                        else
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = GridTechUnitPrice.Rows[i - 1].Cells[j].Value.ToString();
                            worksheet.Cells[cellRowIndex, cellColumnIndex].Font.Size = 14;
                            worksheet.Cells[cellRowIndex, cellColumnIndex].Font.Name = "MetricHPE Light";
                        }
                        cellColumnIndex++;
                    }
                    cellColumnIndex = 1;
                    cellRowIndex++;

                }
                totalRowSum += GridTechUnitPrice.Rows.Count;
                totalRowSum += 2;

                //Casper cost 
                worksheet.Cells[totalRowSum, 1] = "CASPER Cost Input";
                worksheet.Cells[totalRowSum, 1].Font.Size = 16;
                worksheet.Cells[totalRowSum, 1].Font.Name = "MetricHPE Medium";
                totalRowSum++;
                cellRowIndex = totalRowSum;
                cellColumnIndex = 1;
                for (int i = 0; i < GridCasperCost.Rows.Count + 1; i++)
                {

                    for (int j = 0; j < GridCasperCost.Columns.Count; j++)
                    {
                        // Excel index starts from 1,1. As first Row would have the Column headers, adding a condition check. 
                        if (cellRowIndex == totalRowSum)
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = GridCasperCost.Columns[j].HeaderText;
                            worksheet.Cells[cellRowIndex, cellColumnIndex].Font.Size = 14;
                            worksheet.Cells[cellRowIndex, cellColumnIndex].Font.Name = "MetricHPE Light";

                        }
                        else
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = GridCasperCost.Rows[i - 1].Cells[j].Value.ToString();
                            worksheet.Cells[cellRowIndex, cellColumnIndex].Font.Size = 14;
                            worksheet.Cells[cellRowIndex, cellColumnIndex].Font.Name = "MetricHPE Light";
                        }
                        cellColumnIndex++;
                    }
                    cellColumnIndex = 1;
                    cellRowIndex++;

                }
                totalRowSum += GridCasperCost.Rows.Count;
                totalRowSum += 3;
                worksheet.Cells[totalRowSum, 1] = "Report generated by DC-OSS Rate Card Tool";
                worksheet.Cells[totalRowSum, 1].Font.Name = "MetricHPE Medium";
                worksheet.Cells[totalRowSum, 1].Font.Size = 12;
                worksheet.Cells[totalRowSum, 1].Font.Bold = true;
                totalRowSum++;
                worksheet.Cells[totalRowSum, 1] = "HPE Confidential © 2017 Hewlett Packard Enterprise ";
                worksheet.Cells[totalRowSum, 1].Font.Name = "MetricHPE Medium";
                worksheet.Cells[totalRowSum, 1].Font.Size = 12;

                worksheet.Columns["A:E"].ColumnWidth = 25;

                //worksheet.Protect(Password: "rc2017", AllowFormattingCells: false);
                //Getting the location and file name of the excel to save from user. 
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveDialog.FilterIndex = 2;

                if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    workbook.SaveAs(saveDialog.FileName);
                    MessageBox.Show("Export Successful");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                excel.Quit();
                workbook = null;
                excel = null;
            }

        }

        private void BtnExportGridPriceYear_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        private void LoadGridCasperCost()
        {
            GridCasperCost.Rows.Clear();
            double CasperRecurringCosts = this.HCCostAvg + this.PMSustanceAvg + this.LicensePlatCostAvg;
            GridCasperCost.Rows.Add("CASPER Recurring Costs", CasperRecurringCosts.ToString("C", CultureInfo.CurrentCulture));
            GridCasperCost.Rows.Add("CASPER Upfront Costs", TransitionDeployCost.ToString("C", CultureInfo.CurrentCulture));

        }

        private void lblHelp_Click(object sender, EventArgs e)
        {
            HelpMenu.Show(lblHelp, 0, lblHelp.Height);
        }
        private void instructionsItem_Click(object sender, System.EventArgs e)
        {
            InstructionsGUI.GetInstance().Show();

        }

        private void ModelAssumptions_Click_1(object sender, EventArgs e)
        {
            ModelAssumptionsGUI.GetInstance().Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutGUI.GetInstance().Show();
        }

        private void dealTab_Click(object sender, EventArgs e)
        {

        }

        private void txtTargetMargin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 110)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }

        }

        private void CmbBoxRegionAOH_SelectedValueChanged(object sender, EventArgs e)
        {

            var _Deal_Manager = DealManagement.GetInstance();
            foreach (var Aoh in _Deal_Manager.List_AOH)
            {
                if (!(CmbBoxRegionAOH.SelectedItem.ToString().Equals("-- Select an AOH --")))
                {
                    string aohConcat = "AOH_" + CmbBoxRegionAOH.SelectedItem.ToString();
                    if (Aoh.Detail.Equals(aohConcat))
                    {
                        RegionAOH = CmbBoxRegionAOH.SelectedItem.ToString();
                        _Deal_Manager.REGION_AOH = Aoh;
                        RecalculateOutputs();
                    }
                }

            }

        }
        private void btnUpdateDB_Click(object sender, EventArgs e)
        {

            UpdateDBtoSP();
        }
        private void UpdateDBtoSP()
        {
            string fileToUpload = @"C:\Program Files (x86)\Hewlett Packard Enterprise\DC OSS RateCard\RATECARD_DB.accdb";
            string sharePointSite = "https://hpe.sharepoint.com/teams/PDT/";
            string documentLibraryName = "RateCard";

            using (ClientContext clientContext = new ClientContext(sharePointSite))
            {
                SecureString passWord = new SecureString();
                foreach (char c in userPassword.ToCharArray()) passWord.AppendChar(c);
                clientContext.Credentials = new SharePointOnlineCredentials(userName, passWord);
                Web web = clientContext.Web;
                FileCreationInformation newFile = new FileCreationInformation();
                newFile.Content = System.IO.File.ReadAllBytes(fileToUpload);
                newFile.Url = "https://hpe.sharepoint.com/teams/PDT/RateCard/RATECARD_DB.accdb";
                newFile.Overwrite = true;
                Boolean replaceExistingFiles = true;

                List docs = web.Lists.GetByTitle(documentLibraryName);
                
                Microsoft.SharePoint.Client.File uploadFile = docs.RootFolder.Files.Add(newFile);

                clientContext.ExecuteQuery();
            }

        }   
        private void DownloadDBToSystem()
        {
            string pathToDownload = @"C:\Program Files (x86)\Hewlett Packard Enterprise\DC OSS RateCard\";
            string sharePointSite = "https://hpe.sharepoint.com/teams/PDT/";
            string documentLibraryName = "RateCard/";

            DownloadFilesFromSharePoint(sharePointSite, documentLibraryName, pathToDownload);
            //using (var clientContext = new ClientContext(sharePointSite))
            //{
            //    SecureString passWord = new SecureString();
            //    foreach (char c in userPassword.ToCharArray()) passWord.AppendChar(c);
            //    clientContext.Credentials = new SharePointOnlineCredentials(userName, passWord);

            //    var list = clientContext.Web.Lists.GetByTitle(documentLibraryName);
            //    var listItem = list.GetItemById("RATECARD_DB");
            //    clientContext.Load(list);
            //    clientContext.Load(listItem, i => i.File);
            //    clientContext.ExecuteQuery();

            //    var fileRef = listItem.File.ServerRelativeUrl;
            //    var fileInfo = Microsoft.SharePoint.Client.File.OpenBinaryDirect(clientContext, fileRef);
            //    var fileName = Path.Combine(pathToDownload, (string)listItem.File.Name);
            //    using (var fileStream = System.IO.File.Create(fileName))
            //    {
            //        fileInfo.Stream.CopyTo(fileStream);
            //    }
            //}

            /////////////2nda
            //ClientContext clientContext = new ClientContext(sharePointSite);
            //SecureString passWord = new SecureString();
            //foreach (char c in userPassword.ToCharArray()) passWord.AppendChar(c);
            //clientContext.Credentials = new SharePointOnlineCredentials(userName, passWord);

            //clientContext.Load(clientContext.Web);
            //List list = clientContext.Web.Lists.GetByTitle(documentLibraryName);
            //clientContext.Load(list);
            //clientContext.ExecuteQuery();
            //CamlQuery query = new CamlQuery();
            //query.ViewXml = "<view/>";
            //ListItemCollection licoll = list.GetItems(query);
            //clientContext.Load(licoll);
            //clientContext.ExecuteQuery();
            //foreach (ListItem li in licoll)
            //{
            //    Microsoft.SharePoint.Client.File file = li.File;
            //    if (file != null)
            //    {
            //        //how to code this block to download the file?
            //    }
            //}


        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            DownloadDBToSystem();
        }
        private void DownloadFilesFromSharePoint(string siteUrl, string folderPath, string tempLocation)
        {
            ClientContext ctx = new ClientContext(siteUrl);
            SecureString passWord = new SecureString();
            foreach (char c in userPassword.ToCharArray()) passWord.AppendChar(c);
            ctx.Credentials = new SharePointOnlineCredentials(userName, passWord);

            FileCollection files = ctx.Web.GetFolderByServerRelativeUrl(folderPath).Files;

            ctx.Load(files);
            ctx.ExecuteQuery();

            foreach (Microsoft.SharePoint.Client.File file in files)
            {
                FileInformation fileInfo = Microsoft.SharePoint.Client.File.OpenBinaryDirect(ctx, file.ServerRelativeUrl);
                ctx.ExecuteQuery();

                var filePath = tempLocation + file.Name;
                using (var fileStream = new FileStream(filePath, System.IO.FileMode.Create))
                {
                    fileInfo.Stream.CopyTo(fileStream);
                }
            }
        }

        private void lblAdministration_Click(object sender, EventArgs e)
        {
            try
            {
                new frmCredentials().Show();
                this.Hide();
            }
            catch(System.ObjectDisposedException ex)
            {
                new frmCredentials().Show();
            }

        }

        private void lblAdministration_MouseHover(object sender, EventArgs e)
        {
            lblAdministration.Font = 
                new System.Drawing.Font("MetricHPE Semibold", 21F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | 
                System.Drawing.FontStyle.Underline))), 
                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void lblAdministration_MouseLeave(object sender, EventArgs e)
        {
            lblAdministration.Font = new System.Drawing.Font("MetricHPE Semibold", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void lblHelp_MouseHover(object sender, EventArgs e)
        {
            lblHelp.Font =
                new System.Drawing.Font("MetricHPE Semibold", 21F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold |
                System.Drawing.FontStyle.Underline))),
                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void lblHelp_MouseLeave(object sender, EventArgs e)
        {
            lblHelp.Font = new System.Drawing.Font("MetricHPE Semibold", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        }

        private void MainGUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
