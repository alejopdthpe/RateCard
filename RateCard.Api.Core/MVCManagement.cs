using RateCard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateCard.Api.Core
{
    public class MVCManagement
    {
        private static MVCManagement _instance;
        private MVCManagement()
        {

        }
        public static MVCManagement GetInstance()
        {
            return _instance ?? (_instance = new MVCManagement());
        }

        public void UPD_DealInputs(Deal deal)
        {
            var _Deal_Manager = DealManagement.GetInstance();
            _Deal_Manager.SetSLA(deal.SLA);
            _Deal_Manager.SetLocationCost(deal.Location);
            _Deal_Manager.SetRegionAOH(deal.RegionAOH);
            _Deal_Manager.SetTargetMarging(deal.TargetMargin);
        }

        public void UPD_ListOfSelectedTechnologies(List<SelectedTechnology> list)
        {
            var _Deal_Manager = DealManagement.GetInstance();
            _Deal_Manager.ListOfSelectedTechnologies.Clear();
            foreach (SelectedTechnology tech in list)
            {
                if (tech.Quantity > 0) {
                    _Deal_Manager.ListOfSelectedTechnologies.Add(tech);
                }                                                 
            }
        }

        public List<TableRow> RET_TBLGeneralNetPriceData()
        {
            //double[] HCPriceGridCost4Years = LoadHCPricePricePerYear();
            //double[] LicensesAndPlatformPricePerYear = LoadLicensesAndPlatformPricePerYear();
            //double[] PMToolSustenancePricePerYear = LoadPMToolSustenancePricePerYear();
            //double[] TransitionDeploymentPricePerYear = LoadTransitionDeploymentPricePerYear();
            //double[] TotalPricePerYear = LoadTotalPricePerYear();

            List<TableRow> list = new List<TableRow>();
            var t1 = new TableRow {
                Detail= "HC Cost",
                ValueAmounts= LoadHCPricePricePerYear()
            };
            var t2 = new TableRow
            {
                Detail = "Licenses & Platform",
                ValueAmounts = LoadLicensesAndPlatformPricePerYear()
            };
            var t3 = new TableRow
            {
                Detail = "PM & Tool Sustenance",
                ValueAmounts = LoadPMToolSustenancePricePerYear()
            };
            var t4 = new TableRow
            {
                Detail = "Transition / Deployment",
                ValueAmounts = LoadTransitionDeploymentPricePerYear()
            };
            var t5 = new TableRow
            {
                Detail = "Total Net Price",
                ValueAmounts = LoadTotalPricePerYear()
            };
            list.Add(t1);
            list.Add(t2);
            list.Add(t3);
            list.Add(t4);
            list.Add(t5);

            return list;
        }
        private double[] LoadHCPricePricePerYear()
        {
            var _CostSummary_Manager = CostSummaryManagement.GetInstance();
            double[] amounts = _CostSummary_Manager.GetHCPriceGridCost4Years();

            return amounts;
        }
        private double[] LoadLicensesAndPlatformPricePerYear()
        {

            var _CostSummary_Manager = CostSummaryManagement.GetInstance();
            double amount = GetLicensesPlatformCost();
            double[] amounts4Year = new double[4];
            amounts4Year[0] = amount;
            amounts4Year[1] = amount;
            amounts4Year[2] = amount;
            amounts4Year[3] = amount;
            amounts4Year = _CostSummary_Manager.GetLicensesPlatformPrice4Year(amounts4Year);

            return amounts4Year;
          
        }
        private double GetLicensesPlatformCost()
        {
            double amount = 0;

            var _CostSummary_Manager = CostSummaryManagement.GetInstance();
            amount = _CostSummary_Manager.GetLicensesAndPlatformCostPerYear();

            return amount;
        }
        private double[] LoadPMToolSustenancePricePerYear()
        {

            var _CostSummary_Manager = CostSummaryManagement.GetInstance();
            double[] amounts = _CostSummary_Manager.GetPMToolSustenanceGridPrice4Year();

            return amounts;
        }
        private double[] LoadTransitionDeploymentPricePerYear()
        {
            var _Deal_Manager = DealManagement.GetInstance();
            var _CostSummary_Manager = CostSummaryManagement.GetInstance();
            double amountY1 = _Deal_Manager.GetTransitionDeploymentCost();
            double[] amounts4Year = _CostSummary_Manager.GetTransitionDeploymentPrice(amountY1);

            return amounts4Year;
        }

        private double[] LoadTotalPricePerYear()
        {
            var _Deal_Manager = DealManagement.GetInstance();
            var _CostSummary_Manager = CostSummaryManagement.GetInstance();
            double[] PlatFormPriceAmounts4Year = new double[4];
            double PlatFormCostAmount = GetLicensesPlatformCost();
            double[] TotalPricePerYear = new double[4];
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
                              _Deal_Manager.Selected_SLA.Value_Amnt) + (HCPrice4Year[0] + PlatFormPriceAmounts4Year[0] + PMToolSust4year[0] + TransDepPrice4Year[0]);
            double totalY2 = (HCPrice4Year[1] + PlatFormPriceAmounts4Year[1] + PMToolSust4year[1] + TransDepPrice4Year[1]);
            double totalY3 = (HCPrice4Year[2] + PlatFormPriceAmounts4Year[2] + PMToolSust4year[2] + TransDepPrice4Year[2]);
            double totalY4 = (HCPrice4Year[3] + PlatFormPriceAmounts4Year[3] + PMToolSust4year[3] + TransDepPrice4Year[3]);

            TotalPricePerYear[0] = totalY1;
            TotalPricePerYear[1] = totalY2;
            TotalPricePerYear[2] = totalY3;
            TotalPricePerYear[3] = totalY4;

            return TotalPricePerYear;
        }

        public List<TableRow> RET_TBLFixedPriceData()
        {
            List<TableRow> list = new List<TableRow>();
            var _Deal_Manager = DealManagement.GetInstance();
            var _CostSummary_Manager = CostSummaryManagement.GetInstance();

            double amountY1 = _Deal_Manager.GetTransitionDeploymentCost();
            double[] amounts4Year = _CostSummary_Manager.GetTransitionDeploymentPrice(amountY1);

            double nullAmount = 0;

            var t1 = new TableRow
            {
                Detail = "Transition / Deployment",
                ValueAmounts = new double [4] { amountY1,0,0,0 }
            };

            double[] amountsTooling4Year = _CostSummary_Manager.GetToolingPrice();
            var t2 = new TableRow
            {
                Detail = "Tooling Price",
                ValueAmounts = amountsTooling4Year
            };

            list.Add(t1);
            list.Add(t2);

            return list;
        }

        public List<TableRow> RET_TBLTechnologyPriceData()
        {
            List<TableRow> list = new List<TableRow>();
            var _Deal_Manager = DealManagement.GetInstance();
            var _CostSummary_Manager = CostSummaryManagement.GetInstance();

            foreach (var selectedTech in _Deal_Manager.ListOfSelectedTechnologies)
            {
                double[] amounts4Years = _CostSummary_Manager.GetTotalUnitCostPerYear(selectedTech);
                amounts4Years = _CostSummary_Manager.GetTotalUnitPricePerYear(amounts4Years);
                var t = new TableRow
                {
                    Detail = selectedTech.Description,
                    ValueAmounts = amounts4Years
                };

                list.Add(t);
            }

            return list;
        }
        public List<TableRow> RET_TBLCasperCostData()
        {
            double HCCostAvg=0;
            double PMSustanceAvg=0;
            double LicensePlatCostAvg=0;
            double TransitionDeployCost = 0;

            List<TableRow> list = new List<TableRow>();
            var _Deal_Manager = DealManagement.GetInstance();
            var _CostSummary_Manager = CostSummaryManagement.GetInstance();

            double[] HCCostAmounts = _CostSummary_Manager.GetHCCostGridCost4Years();

            for (int i = 0; i < HCCostAmounts.Length; i++)
            {
                HCCostAvg += HCCostAmounts[i];
            }
            HCCostAvg = (HCCostAvg / HCCostAmounts.Length);


            double[] PMToolSustAmounts = _CostSummary_Manager.GetPMToolSustenanceGridCost4Year();


            for (int i = 0; i < PMToolSustAmounts.Length; i++)
            {
                PMSustanceAvg += PMToolSustAmounts[i];
            }
            PMSustanceAvg = (PMSustanceAvg / PMToolSustAmounts.Length);


            LicensePlatCostAvg = _CostSummary_Manager.GetLicensesAndPlatformCostPerYear();
            double CasperRecurringCosts = HCCostAvg + PMSustanceAvg + LicensePlatCostAvg;

            var t1 = new TableRow
            {
                Detail = "CASPER Recurring Costs",
                ValueAmounts = new double[4] { CasperRecurringCosts , 0, 0,0 }
            };
            list.Add(t1);

            TransitionDeployCost = _Deal_Manager.GetTransitionDeploymentCost();
            var t2 = new TableRow
            {
                Detail = "CASPER Upfront Costs",
                ValueAmounts = new double[4] { TransitionDeployCost, 0, 0, 0 }
            };
            list.Add(t2);
            return list;
        }

    }
}
