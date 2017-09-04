using RateCard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RateCard.Api.Core
{
    public class CostSummaryManagement
    {
        private static CostSummaryManagement _instance;
        public static CostSummaryManagement GetInstance()
        {
            return _instance ?? (_instance = new CostSummaryManagement());
        }
        private CostSummaryManagement()
        {
            
        }
        public double GetTotalLicenseCost()
        {
            double Total_Cost = 0;
            var RateCard_Manager = RateCardManagement.GetInstance();
            Total_Cost= RateCard_Manager.Get_Annual_Cost_TSS_OSS_Pricing_H8J01A4_102() +
                RateCard_Manager.Get_Annual_Cost_TSS_OSS_Pricing_H9S50AS() +
                RateCard_Manager.Get_Annual_Cost_TSS_OSS_Pricing_HP745A3_105();
            return Total_Cost;
        }
        public double GetToolSusteance()
        {
            var Deal_Manager = DealManagement.GetInstance();
            double percentage = 0.07;
            double Total_Cost = 0;
            double Total_Units = Convert.ToDouble(Deal_Manager.GetTotalUnits());

            Total_Cost = ((Total_Units/100)* percentage* Deal_Manager.Selected_Country.L4);


            return Total_Cost;
        }
        public double GetToolSusteanceUnitsTotal()
        {

            var Deal_Manager = DealManagement.GetInstance();
            double BackUpJobsTotal = 0;
           foreach(var selectedTech in Deal_Manager.ListOfSelectedTechnologies)
            {
                if(selectedTech.Description.Equals("Backup Jobs"))
                {
                    BackUpJobsTotal = selectedTech.Quantity;
                }
            }
            double Total_Cost = GetToolSusteance()/(Deal_Manager.GetTotalUnits()- BackUpJobsTotal);
           
            return Total_Cost;
        }
        public double GetPlaformCost()
        {
            var RateCard_Manager = RateCardManagement.GetInstance();
            return RateCard_Manager.Get_Annual_List_Cost_Platform();
        }
        public double GetSawLicenses()
        {
            var RateCard_Manager = RateCardManagement.GetInstance();
            return RateCard_Manager.Get_Annual_Cost_TSS_OSS_Pricing_HP745A3_105();
        }
        public double GetNNMiTotalCost()
        {
            var RateCard_Manager = RateCardManagement.GetInstance();
            return RateCard_Manager.Get_Annual_Cost_TSS_OSS_Pricing_H8J01A4_102();
        }
        public double GetNNMiCostDevice()
        {
            var RateCard_Manager = RateCardManagement.GetInstance();
            return RateCard_Manager.Get_Annual_Cost_TSS_OSS_Pricing_H8J01A4_102() /
                RateCard_Manager.Get_Quantity_Network_Tech();
        }
        public double GetSisTotalCost()
        {
            var RateCard_Manager = RateCardManagement.GetInstance();
            return RateCard_Manager.Get_Annual_Cost_TSS_OSS_Pricing_H9S50AS();
        }
        public double GetSisCostPerDevice()
        {
            var RateCard_Manager = RateCardManagement.GetInstance();

            return RateCard_Manager.Get_Annual_Cost_TSS_OSS_Pricing_H9S50AS() /
                RateCard_Manager.Get_Quantity_Storage_Tech();
        }
        public double GetGeneralCostPerYear(SelectedTechnology p_technology)
        {
            return GetTotalMonitCostPerYear(p_technology) + GetTotalOperAndAdminCostPerYear(p_technology);

        }
        public double GetGeneralCostPerYearPerUnit(SelectedTechnology p_technology)
        {
            return GetMonitCostPerYearPerUnit(p_technology) + GetOperAndAdminCostPerYearPerUnit(p_technology);

        }
        public double GetAllTotalMonitCostPerYear()
        {
            double total = 0;

            var Deal_Manager = DealManagement.GetInstance();
            foreach (var selectedTech in Deal_Manager.ListOfSelectedTechnologies)
            {
                if (selectedTech.HasMonitoring == true) {
                    total += GetTotalMonitCostPerYear(selectedTech);
                }
                
            }
            return total;
        }
        public double GetTotalMonitCostPerYear(SelectedTechnology p_technology)
        {
            return GetMonitCostPerYearPerUnit(p_technology) * p_technology.Quantity;
        }
        public double GetMonitCostPerYearPerUnit(SelectedTechnology p_technology)
        {
            return GetMonitCostPerDay(p_technology) * 12 * 30;
        }//column AD Tech
        private double GetMonitCostPerDay(SelectedTechnology p_technology)//column AB TECH
        {
            double Cost_Per_Day = 0;

            Technology technology = new Technology();
            var Deal_Manager = DealManagement.GetInstance();
            foreach (var tech in Deal_Manager.List_Technologies)
            {
                if (p_technology.Description.Equals(tech.Description))
                {
                    technology = tech;
                }
            }

            if (technology.Unit_Of_Measure.Equals("Unit")) 
            {
                
               Cost_Per_Day = (Deal_Manager.Selected_Country.L1 / 12 / 30 / 24 / 60) * GetMonitoringEffortPerTech(technology);
                    
            } else
            {
                if (technology.Unit_Of_Measure.Equals("Terabyte")) // column AF tech
                {
                    Cost_Per_Day = GetMonitoringCostPerTB(technology);
                }

            }
            return Cost_Per_Day;
        }

        private double GetMonitoringCostPerTB(Technology p_tech)
        {
            double result = 0;

            var Deal_Manager = DealManagement.GetInstance();
            result = (GetMonitoringMinCostPerTB(p_tech) * (Deal_Manager.Selected_Country.L1 / 12 / 30 / 24 / 60));

            return result;
        }
        private double GetMonitoringMinCostPerTB(Technology p_tech)
        {

            var Deal_Manager = DealManagement.GetInstance();
            double result = 0;
            foreach (var tech in Deal_Manager.List_Technologies)
            {
                if (tech.Description.Equals("Storage"))
                {              
                    result = GetMonitoringEffortPerTech(tech) / p_tech.Avg_TBs;
                }
            }
            return result;
        }

        private double GetMonitoringEffortPerTech(Technology p_tech)
        {

            var Deal_Manager = DealManagement.GetInstance();
            if (p_tech.Description.Equals("Network Switch")|| p_tech.Description.Equals("Network Routers"))
            {
                return p_tech.Monitoring;
            }else
            {
                return (p_tech.Monitoring + (p_tech.Monitoring * Deal_Manager.Selected_Country.Efficiency));

            }         
        }

        private double GetIncidentEffortPerTech(Technology p_tech)
        {

            var Deal_Manager = DealManagement.GetInstance();
            if (p_tech.Description.Equals("Network Switch") || p_tech.Description.Equals("Network Routers"))
            {
                return p_tech.Incidents;
            }
            else
            {
                return (p_tech.Incidents + (p_tech.Incidents * Deal_Manager.Selected_Country.Efficiency));

            }
        }
        private double GetServicesEffortPerTech(Technology p_tech)
        {

            var Deal_Manager = DealManagement.GetInstance();
            if (p_tech.Description.Equals("Network Switch") || p_tech.Description.Equals("Network Routers"))
            {
                return p_tech.Services;
            }
            else
            {
                return (p_tech.Services + (p_tech.Services * Deal_Manager.Selected_Country.Efficiency));

            }
        }

        public double GetAllTotalOperAndAdminCostPerYear()
        {

            var Deal_Manager = DealManagement.GetInstance();
            double total = 0;
            foreach (var selectedTech in Deal_Manager.ListOfSelectedTechnologies)
            {
                if (selectedTech.HasOperAndAdmin == true) {
                    total += GetTotalOperAndAdminCostPerYear(selectedTech);
                }
                
            }

            return total;
        }
        public double GetTotalOperAndAdminCostPerYear(SelectedTechnology p_technology)
        {

            return GetOperAndAdminCostPerYearPerUnit(p_technology) * p_technology.Quantity;
        }
        public double GetOperAndAdminCostPerYearPerUnit(SelectedTechnology p_technology)
        {
            return GetOperAndAdminCostPerDay(p_technology) * 12 * 30;
        }
        private double GetOperAndAdminCostPerDay(SelectedTechnology p_technology) // column AC Tech
        {
            double Cost_Per_Day = 0;
            double Oper_And_Admin_Effort = 0;
            Technology technology = new Technology();
            var Deal_Manager = DealManagement.GetInstance();
            foreach (var tech in Deal_Manager.List_Technologies)
            {
                if (p_technology.Description.Equals(tech.Description))
                {
                    technology = tech;
                }

            }
            Oper_And_Admin_Effort = GetIncidentEffortPerTech(technology) +
                                    GetServicesEffortPerTech(technology);

            if (technology.Unit_Of_Measure.Equals("Unit"))
            {              
                Cost_Per_Day = (Deal_Manager.Selected_Country.L2 / 12 / 30 / 24 / 60) * Oper_And_Admin_Effort;                    
            }
            else
            {
                if (technology.Unit_Of_Measure.Equals("Terabyte"))
                {
                    Cost_Per_Day = GetMonitoringCostPerTB(technology);
                }

            }
            return Cost_Per_Day;
        }
        private double GetOpAndAdminCostPerTB(Technology p_tech) //column AG Tech
        {

            var Deal_Manager = DealManagement.GetInstance();
            double result = 0;
            result = (GetOpAndAdminMinCostPerTB(p_tech) * (Deal_Manager.Selected_Country.L2 / 12 / 30 / 24 / 60));

            return result;
        }

        private double GetOpAndAdminMinCostPerTB(Technology p_tech)
        {
            double result = 0;
            var Deal_Manager = DealManagement.GetInstance();
            foreach (var tech in Deal_Manager.List_Technologies)
            {
                if (tech.Description.Equals("Storage"))
                {
                    result = (GetIncidentEffortPerTech(tech) +
                                GetServicesEffortPerTech(tech)) / p_tech.Avg_TBs;
                }
            }
            return result;
        }

        public double GetLicensesAndPlatformCostPerYear()
        {
            var Deal_Manager = DealManagement.GetInstance();
            double amount = 0;
            bool anyTechHasOpsAdmin = false;

            foreach (var selectedTech in Deal_Manager.ListOfSelectedTechnologies) {

            if (selectedTech.HasOperAndAdmin == true) {
                    anyTechHasOpsAdmin = true;
                }
            }

            if (anyTechHasOpsAdmin) {

                amount = GetLicensesAndPlatformSawGridCostPerYear();
            }else
            {
                amount = GetLicensesAndPlatformNOSawGridCostPerYear();
            }
            return amount;

        }
        private double GetLicensesAndPlatformSawGridCostPerYear()
        {
            double amount = 0;

            amount = GetSisTotalCost() +
                         GetNNMiTotalCost() +
                         GetSawLicenses() +
                         GetPlaformCost();

            return amount;
        }
        private double GetLicensesAndPlatformNOSawGridCostPerYear()
        {
            double amount = 0;
            
                amount = GetSisTotalCost() +
                        GetNNMiTotalCost() +
                        GetPlaformCost();
            return amount;

        }

        public double[] GetHCCostGridCost4Years()
        {

            var Deal_Manager = DealManagement.GetInstance();
            double[] amounts = new double[4];
           
            double amountY1 = GetAllTotalMonitCostPerYear() +
                GetAllTotalOperAndAdminCostPerYear();
            amounts[0] = amountY1;

            double amountY2 = amountY1 + (amountY1 * Deal_Manager.Selected_Country.Inflation_Rate) -
                               (amountY1 * Deal_Manager.Selected_Country.Decrease_Rate);
            amounts[1] = amountY2;

            double amountY3 = amountY2 + (amountY2 * Deal_Manager.Selected_Country.Inflation_Rate) -
                   (amountY2 * Deal_Manager.Selected_Country.Decrease_Rate);
            amounts[2] = amountY3;

            double amountY4 = amountY3 + (amountY3 * Deal_Manager.Selected_Country.Inflation_Rate) -
                   (amountY3 * Deal_Manager.Selected_Country.Decrease_Rate);

            amounts[3] = amountY4;

            return amounts;
        }

        public double[] GetPMToolSustenanceGridCost4Year()
        {

            var Deal_Manager = DealManagement.GetInstance();
            double[] amounts = new double[4];
            double amountY1 = GetToolSusteance();

            amounts[0] = amountY1;
            double amountY2 = amountY1 + (amountY1 * Deal_Manager.Selected_Country.Inflation_Rate) -
                               (amountY1 * Deal_Manager.Selected_Country.Decrease_Rate);
            amounts[1] = amountY2;
            double amountY3 = amountY2 + (amountY2 * Deal_Manager.Selected_Country.Inflation_Rate) -
                   (amountY2 * Deal_Manager.Selected_Country.Decrease_Rate);
            amounts[2] = amountY3;
            double amountY4 = amountY3 + (amountY3 * Deal_Manager.Selected_Country.Inflation_Rate) -
                   (amountY3 * Deal_Manager.Selected_Country.Decrease_Rate);

            amounts[3] = amountY4;

            return amounts;

        }

        public double[] GetTotalUnitCostPerYear(BaseEntity p_tech)
        {

            var Deal_Manager = DealManagement.GetInstance();
            double[] amount4Year = new double[4];           

            double amountY1 = GetToolSusteanceUnitsTotal() +
                GetGeneralCostPerYearPerUnit((SelectedTechnology)p_tech) +
                GetSisCostPerDevice();
            amount4Year[0] = amountY1;
            amount4Year[1] = amountY1 + (amountY1 * Deal_Manager.Selected_Country.Inflation_Rate) -
                                (amountY1 * Deal_Manager.Selected_Country.Decrease_Rate);
            amount4Year[2] = amount4Year[1] + (amount4Year[1] * Deal_Manager.Selected_Country.Inflation_Rate) -
                            (amount4Year[1] * Deal_Manager.Selected_Country.Decrease_Rate);
            amount4Year[3] = amount4Year[2] + (amount4Year[2] * Deal_Manager.Selected_Country.Inflation_Rate) -
                                (amount4Year[2] * Deal_Manager.Selected_Country.Decrease_Rate);
            return amount4Year;
        }

        public double[] GetHCPriceGridCost4Years() {
            double[] amounts = GetHCCostGridCost4Years();
            amounts = GetPriceFromCost(amounts);
            return amounts;
        }

        public double[] GetLicensesPlatformPrice4Year(double[] p_amounts) {

            p_amounts = GetPriceFromCost(p_amounts);

            return p_amounts;
        }
        private double[] GetPriceFromCost(double[] p_amounts) {

            var Deal_Manager = DealManagement.GetInstance();

            p_amounts[0] = p_amounts[0] / 
                (1 - (Deal_Manager.TARGET_MARGIN.ValueAmount+ 
                Deal_Manager.REGION_AOH.ValueAmount))*
                (1+Deal_Manager.SLA_PRICE_ADJUSTMENT.ValueAmount);
            p_amounts[1] = p_amounts[1] /
                (1 - (Deal_Manager.TARGET_MARGIN.ValueAmount +
                Deal_Manager.REGION_AOH.ValueAmount)) *
                (1 + Deal_Manager.SLA_PRICE_ADJUSTMENT.ValueAmount);
            p_amounts[2] = p_amounts[2] /
                (1 - (Deal_Manager.TARGET_MARGIN.ValueAmount +
                Deal_Manager.REGION_AOH.ValueAmount)) *
                (1 + Deal_Manager.SLA_PRICE_ADJUSTMENT.ValueAmount);
            p_amounts[3] = p_amounts[3] /
                (1 - (Deal_Manager.TARGET_MARGIN.ValueAmount +
                Deal_Manager.REGION_AOH.ValueAmount)) *
                (1 + Deal_Manager.SLA_PRICE_ADJUSTMENT.ValueAmount);

            return p_amounts;
        }

        public double[] GetPMToolSustenanceGridPrice4Year() {

            double[] amounts = GetPMToolSustenanceGridCost4Year();
            amounts = GetPriceFromCost(amounts);

            return amounts;
        }

        public double[] GetTransitionDeploymentPrice(double p_amountY1) {
            double[] amounts = new double[4];

            amounts[0] = p_amountY1;
            amounts[1] = p_amountY1;
            amounts[2] = p_amountY1;
            amounts[3] = p_amountY1;
            amounts = GetPriceFromCost(amounts);
            amounts[1] = 0;
            amounts[2] = 0;
            amounts[3] = 0;

            return amounts;
            
        }

        public double[] GetTotalUnitPricePerYear(double[] p_amounts)
        {
            p_amounts = GetPriceFromCost(p_amounts);

            return p_amounts;
        }

        public double[] GetToolingPrice()
        {
            var RateCard_Manager = RateCardManagement.GetInstance();

            double[] amounts = new double[4];
            double sum = GetPlaformCost() + RateCard_Manager.Get_Annual_Cost_TSS_OSS_Pricing_HP745A3_105();
            amounts[0] = sum;
            amounts[1] = sum;
            amounts[2] = sum;
            amounts[3] = sum;
            amounts = GetPriceFromCost(amounts);
            return amounts;

        }

        public double GetCasperRecurringCosts()
        {
            double cost = 0;
            double[] HCCost = GetHCCostGridCost4Years();

            return cost;

        }
    }

}
