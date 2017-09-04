using RateCard.ApiCore.Crud;
using RateCard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RateCard.Api.Core
{
    public class DealManagement
    {
        private static DealManagement _instance;
        private MiscellaneousCrud _Crud_Misc;
        private Miscellaneous TOOLS_SME;
        private Miscellaneous TECH_SME;
        private Miscellaneous TRANS_MANAGER;
        private Miscellaneous TOOL_VERSION;
        public Miscellaneous REGION_AOH;
        public Miscellaneous TARGET_MARGIN { get; set; }
        public List<Miscellaneous> List_AOH { get; set; }
        public Miscellaneous SLA_PRICE_ADJUSTMENT { get; set; }
        private const string DbDetailToolsSME = "TOOLS_SME";
        private const string DbDetailTechSME = "TECH_SME";
        private const string DbDetailTransitionManager = "TRANSITION_MANAGER";
        private const string DbDetailTargetMargin = "TARGET_MARGIN";
        private const string DbDetailEMEA_AOH = "AOH_EMEA";
        private const string DbDetailAMS_AOH = "AOH_AMS";
        private const string DbDetailLAC_AOH = "AOH_LAC";
        private const string DbDetailAPJ_AOH = "AOH_APJ";
        private const string DbDetailSLAPriceAdjust = "SLA_PRICE_ADJUSTMENT";
        private const string DbDetailVersion = "VERSION";
        public List<LocationCost> List_Location_Costs { get; set; }
        public List<SLA> List_SLAs { get; set; }
        public List<Technology> List_Technologies { get; set; }
        public List<SelectedTechnology> ListOfSelectedTechnologies { get; set; }
        private DealCrud _Crud_Deal;
        private SLACrud _Crud_Sla;
        private TechnologyCrud _Crud_Tech;
        public LocationCost Selected_Country { get; set; }
        public SLA Selected_SLA { get; set; }

        public static DealManagement GetInstance()
        {
            return _instance ?? (_instance = new DealManagement());
        }
        private DealManagement()
        {
            _Crud_Deal = new DealCrud();
            _Crud_Sla = new SLACrud();
            _Crud_Tech = new TechnologyCrud();
            ListOfSelectedTechnologies = new List<SelectedTechnology>();
            List_AOH = new List<Miscellaneous>();
            LoadAllLocationCosts();
            LoadAllSLAs();
            LoadTechnologies();
            LoadPricingVariables();
        }
        public Miscellaneous GetToolVersion()
        {
            TOOL_VERSION = new Miscellaneous();
            TOOL_VERSION.Detail = DbDetailVersion;
            _Crud_Misc = new MiscellaneousCrud();
            TOOL_VERSION = _Crud_Deal.Retrieve<Miscellaneous>(TOOL_VERSION);

            return TOOL_VERSION;
        }
        public void SetSLA(string p_description) {

            foreach (var sl in List_SLAs) {
                if (sl.Description.Equals(p_description)) {
                    Selected_SLA = sl;
                }
            }
        }
        public void SetTargetMarging (double p_targetMargin)
        {
            p_targetMargin = p_targetMargin / 100;
            
            TARGET_MARGIN.ValueAmount = p_targetMargin;
           
        }
        public void SetLocationCost(string p_country_name)
        {
            foreach (var location in List_Location_Costs)
            {
                if (location.Country.Equals(p_country_name))
                {
                    this.Selected_Country = location;
                }
            }
        }
        public void SetRegionAOH(string p_region)
        {
            foreach (var Aoh in List_AOH)
            {
                string aohConcat = "AOH_" + p_region;
                if (Aoh.Detail.Equals(aohConcat))
                {
                    REGION_AOH = Aoh;
                }
            }
        }
        private void LoadAllLocationCosts() {
            List_Location_Costs = new List<LocationCost>();
            List<BaseEntity> listsRetrieved = new List<BaseEntity>();         
            try
            {
                listsRetrieved = _Crud_Deal.RetrieveAll<BaseEntity>();
                if (listsRetrieved == null)
                {
                    Console.WriteLine("List is empty");
                }
                else
                {
                    foreach(BaseEntity baseEntity in listsRetrieved)
                    {

                        List_Location_Costs.Add((LocationCost)baseEntity);
                    }
                }
            }
            catch(NullReferenceException e)
            {
                Console.WriteLine("Management" + e.Message);
            }
           
        }

        private void LoadAllSLAs()
        {
            List_SLAs = new List<SLA>();
            List<BaseEntity> listsRetrieved = new List<BaseEntity>();         
            try
            {
                listsRetrieved = _Crud_Sla.RetrieveAll<BaseEntity>();
                if (listsRetrieved == null)
                {
                    Console.WriteLine("List is empty");
                }
                else
                {
                    foreach (BaseEntity baseEntity in listsRetrieved)
                    {
                        
                        List_SLAs.Add((SLA)baseEntity);
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Management" + e.Message);
            }
            
        }

        private void LoadTechnologies()
        {
            List_Technologies = new List<Technology>();          
            List<BaseEntity> listsRetrieved = new List<BaseEntity>();
            try
            {
                listsRetrieved = _Crud_Tech.RetrieveAll<BaseEntity>();
                if (listsRetrieved == null)
                {
                    Console.WriteLine("List is empty");
                }
                else
                {
                    foreach (BaseEntity baseEntity in listsRetrieved)
                    {
                        
                        List_Technologies.Add((Technology)baseEntity);
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Management" + e.Message);
            }
        }

        public int GetTotalUnits() {
            int total = 0;
            foreach (SelectedTechnology selectedTech in ListOfSelectedTechnologies)
            {

                total += selectedTech.Quantity;
            }
            return total;
        }

        private void LoadTransitionDeploymentVariables()
        {
            TOOLS_SME = new Miscellaneous();
            TOOLS_SME.Detail = DbDetailToolsSME;
            TECH_SME = new Miscellaneous();
            TECH_SME.Detail = DbDetailTechSME;
            TRANS_MANAGER = new Miscellaneous();
            TRANS_MANAGER.Detail = DbDetailTransitionManager;

            _Crud_Misc = new MiscellaneousCrud();

            TOOLS_SME = _Crud_Deal.Retrieve<Miscellaneous>(TOOLS_SME);
            TECH_SME = _Crud_Deal.Retrieve<Miscellaneous>(TECH_SME);
            TRANS_MANAGER = _Crud_Deal.Retrieve<Miscellaneous>(TRANS_MANAGER);
        }

        public double GetTransitionDeploymentCost()
        {
            double amount = 0;
            LoadTransitionDeploymentVariables();
            amount = GetToolsSMECost() + GetTechSMECost() + GetTransitionManagerCost();
            return amount;

        }
        private double GetToolsSMECost()
        {
            double amount = 0;
            amount = (Selected_Country.L3 / 12 / 22)*TOOLS_SME.ValueAmount;
            return amount;
        }
        private double GetTechSMECost()
        {
            double amount = 0;
            amount = (Selected_Country.L2 / 12 / 22) * TECH_SME.ValueAmount;
            return amount;
        }
        private double GetTransitionManagerCost()
        {
            double amount = 0;
            amount = (Selected_Country.L4 / 12 / 22) * TRANS_MANAGER.ValueAmount;
            return amount;
        }

        public void LoadPricingVariables()
        {

            TARGET_MARGIN = new Miscellaneous();
            TARGET_MARGIN.Detail = DbDetailTargetMargin;

            SLA_PRICE_ADJUSTMENT = new Miscellaneous();
            SLA_PRICE_ADJUSTMENT.Detail = DbDetailSLAPriceAdjust;

            REGION_AOH = new Miscellaneous();
            Miscellaneous AOH_EMEA = new Miscellaneous();
            AOH_EMEA.Detail = DbDetailEMEA_AOH;
            Miscellaneous AOH_AMS = new Miscellaneous();
            AOH_AMS.Detail = DbDetailAMS_AOH;
            Miscellaneous AOH_LAC = new Miscellaneous();
            AOH_LAC.Detail = DbDetailLAC_AOH;
            Miscellaneous AOH_APJ = new Miscellaneous();
            AOH_APJ.Detail = DbDetailAPJ_AOH;

            _Crud_Misc = new MiscellaneousCrud();

            SLA_PRICE_ADJUSTMENT = _Crud_Deal.Retrieve<Miscellaneous>(SLA_PRICE_ADJUSTMENT);
            TARGET_MARGIN = _Crud_Deal.Retrieve<Miscellaneous>(TARGET_MARGIN);

            AOH_EMEA = _Crud_Deal.Retrieve<Miscellaneous>(AOH_EMEA);
            AOH_AMS = _Crud_Deal.Retrieve<Miscellaneous>(AOH_AMS);
            AOH_LAC = _Crud_Deal.Retrieve<Miscellaneous>(AOH_LAC);
            AOH_APJ = _Crud_Deal.Retrieve<Miscellaneous>(AOH_APJ);

            List_AOH.Add(AOH_EMEA);
            List_AOH.Add(AOH_AMS);
            List_AOH.Add(AOH_LAC);
            List_AOH.Add(AOH_APJ);

        }

    }
}
