using RateCard.ApiCore.Crud;
using RateCard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RateCard.Api.Core
{
    public class RateCardManagement
    {
        private static RateCardManagement _instance;     
        public List<RateCardRevised> List_Rate_Card { get; set; }
        private RateCardRevisedCrud _Crud_Rate_Card;
        private MiscellaneousCrud _Crud_Misc;
        private TSSOfferPriceCrud _Crud_TSSOffer;
        private Miscellaneous MOU_DISC_SERVICE_ONLY;
        private Miscellaneous MOU_DISC_SAW_SAAS;
        private Miscellaneous BSM_PLATFORM_COST;
        public List<TSSOfferPrice> List_OfferPrice;
        private const string DbDetailMOUSawSaas = "MOU_DISC_SAW_SAAS";
        private const string DbDetailMOUServOnly = "MOU_DISC_SERVICE_ONLY";
        private const string DbDetailBSMPlatformCost = "BSM_PLATFORM_COST";

        public static RateCardManagement GetInstance()
        {
            return _instance ?? (_instance = new RateCardManagement());
        }
        private RateCardManagement()
        {
            _Crud_Rate_Card = new RateCardRevisedCrud();
            _Crud_Misc = new MiscellaneousCrud();
            _Crud_TSSOffer = new TSSOfferPriceCrud();
            LoadAllRateCards();
            Initialize_Miscellaneous();
            Initialize_TSSOfferPrice();
            Get_Annual_Cost_TSS_OSS_Pricing_H8J01A4_102();
           
        }

        private void Initialize_Miscellaneous()
        {
            MOU_DISC_SERVICE_ONLY = new Miscellaneous();
            MOU_DISC_SERVICE_ONLY.Detail = DbDetailMOUServOnly;
            MOU_DISC_SAW_SAAS = new Miscellaneous();
            MOU_DISC_SAW_SAAS.Detail = DbDetailMOUSawSaas;
            BSM_PLATFORM_COST = new Miscellaneous();
            BSM_PLATFORM_COST.Detail = DbDetailBSMPlatformCost;

            MOU_DISC_SERVICE_ONLY = _Crud_Misc.Retrieve<Miscellaneous>(MOU_DISC_SERVICE_ONLY);
            MOU_DISC_SAW_SAAS = _Crud_Misc.Retrieve<Miscellaneous>(MOU_DISC_SAW_SAAS);
            BSM_PLATFORM_COST = _Crud_Misc.Retrieve<Miscellaneous>(BSM_PLATFORM_COST);
            
        }

        private void Initialize_TSSOfferPrice()
        {
            List_OfferPrice = new List<TSSOfferPrice>();
            List<BaseEntity> Retrieved_List = new List<BaseEntity>();
            Retrieved_List = _Crud_TSSOffer.RetrieveAll<BaseEntity>();
            

            foreach(var offer in Retrieved_List)
            {
                List_OfferPrice.Add((TSSOfferPrice)offer);
               
            }

        }
        public double GetDiscountedRatePerFYMOU(RateCardRevised p_rate_card)
        {
            double Discounted_Rate = 0;

            if (p_rate_card.Mou_Disc_Rate.Equals(DbDetailMOUSawSaas))
            {
                Discounted_Rate = p_rate_card.List_Price * (1 - MOU_DISC_SAW_SAAS.ValueAmount);
            }
            else
            {
                if (p_rate_card.Mou_Disc_Rate.Equals(DbDetailMOUServOnly))
                {
                    Discounted_Rate = p_rate_card.List_Price * (1 - MOU_DISC_SERVICE_ONLY.ValueAmount);
                }

            }
            
            return Discounted_Rate;
        }

        private void LoadAllRateCards()
        {
            List_Rate_Card = new List<RateCardRevised>();
            List<BaseEntity> listsRetrieved = new List<BaseEntity>();
            try
            {
                listsRetrieved = _Crud_Rate_Card.RetrieveAll<BaseEntity>();
                if (listsRetrieved == null)
                {
                    Console.WriteLine("List is empty");
                }
                else
                {
                    foreach (BaseEntity baseEntity in listsRetrieved)
                    {
                      
                        List_Rate_Card.Add((RateCardRevised)baseEntity);
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Management" + e.Message);
            }

        }

        public double Get_Quantity_Network_Tech()
        {
            double NetWorkCount = 0;
            var deal_manager = DealManagement.GetInstance();
            foreach (var selectedTech in deal_manager.ListOfSelectedTechnologies)
            {
                foreach (var Tech in deal_manager.List_Technologies)
                {
                    if (Tech.Description.Equals(selectedTech.Description) &&
                        Tech.Type.Equals("Network (Switch)"))
                    {
                        NetWorkCount += selectedTech.Quantity;
                    }

                }
            }

            return NetWorkCount;
        }

        public double Get_Quantity_Storage_Tech()
        {
            double Storage_Count = 0;
            var deal_manager = DealManagement.GetInstance();
            foreach (var selectedTech in deal_manager.ListOfSelectedTechnologies)
            {
                foreach (var Tech in deal_manager.List_Technologies)
                {
                    if (Tech.Description.Equals(selectedTech.Description) &&
                        Tech.Type.Equals("Storage/Servers"))
                    {
                        Storage_Count += selectedTech.Quantity;
                    }
                }
            }

            return Storage_Count;
        }
        public double Get_Annual_List_Cost_H8J01A4_102() {
            double annual_cost = 0;
            double Network_Qty = Get_Quantity_Network_Tech();

            Network_Qty = Network_Qty / 50;
            Network_Qty = Math.Ceiling(Network_Qty);

            foreach (var Rate_Card in List_Rate_Card)
            {
                if (Rate_Card.Part_Number.Equals("H8J01A4#102"))
                {
                    annual_cost = (Rate_Card.List_Price / Rate_Card.Years) * Network_Qty;
                }

            }
            return annual_cost;
        }
        public double Get_Annual_List_Cost_H9S50AS()
        {
            double Storage_Count = Get_Quantity_Storage_Tech();
            Storage_Count = Storage_Count / 50;
            Storage_Count = Math.Ceiling(Storage_Count);

            double annual_cost = 0;
            var deal_manager = DealManagement.GetInstance();


            foreach (var Rate_Card in List_Rate_Card)
            {
                if (Rate_Card.Part_Number.Equals("H9S50AS"))
                {
                    annual_cost = (Rate_Card.List_Price / Rate_Card.Years) * Storage_Count;
                }

            }
            return annual_cost;
        }
        public double Get_Annual_List_Cost_HP745A3_105()
        {
           
            int Count = 5;
            double annual_cost = 0;

            foreach (var Rate_Card in List_Rate_Card)
            {
                if (Rate_Card.Part_Number.Equals("HP745A3#105"))
                {
                    annual_cost = (Rate_Card.List_Price / Rate_Card.Years) * Count;
                }

            }
            return annual_cost;
        }
        public double Get_Annual_List_Cost_HL859A3_101()
        {
           
            int Count = 0;
            double annual_cost = 0;

            foreach (var Rate_Card in List_Rate_Card)
            {
                if (Rate_Card.Part_Number.Equals("HP745A3#105"))
                {
                    annual_cost = (Rate_Card.List_Price / Rate_Card.Years) * Count;
                }

            }
            return annual_cost;
        }
        public double Get_Annual_List_Cost_Platform()
        {
           
            int Count = 0;
            double annual_cost = 0;
            var deal_manager = DealManagement.GetInstance();
            foreach (var selectedTech in deal_manager.ListOfSelectedTechnologies)
            {
                foreach (var Tech in deal_manager.List_Technologies)
                {
                    if (selectedTech.Quantity > 0 &&
                        (Tech.Type.Equals("Network (Switch)") || Tech.Type.Equals("Storage/Servers")) &&
                        selectedTech.Description.Equals(Tech.Description))
                    {
                        Count += selectedTech.Quantity;
                    }

                }
            }
              
            annual_cost = BSM_PLATFORM_COST.ValueAmount * Count;
                  
            return annual_cost;
        }

        public double Get_Total_Annual_List_Cost()
        {
            return Get_Annual_List_Cost_H8J01A4_102() + Get_Annual_List_Cost_H9S50AS() +
                Get_Annual_List_Cost_HP745A3_105() + Get_Annual_List_Cost_HL859A3_101() +
                Get_Annual_List_Cost_Platform();
                }

        public double Get_Annual_FY_MOU_Cost_H8J01A4_102()
        {
            
            int NetWorkCount = 0;
            double annual_cost = 0;
            var deal_manager = DealManagement.GetInstance();
            foreach (var selectedTech in deal_manager.ListOfSelectedTechnologies)
            {
                foreach (var Tech in deal_manager.List_Technologies)
                {
                    if (selectedTech.Quantity > 0 && Tech.Type.Equals("Network (Switch)")
                        && selectedTech.Description.Equals(Tech.Description))
                    {
                        NetWorkCount += selectedTech.Quantity;
                    }

                }
                

            }
            foreach (var Rate_Card in List_Rate_Card)
            {
                if (Rate_Card.Part_Number.Equals("H8J01A4#102"))
                {
                    annual_cost = (GetDiscountedRatePerFYMOU(Rate_Card) / Rate_Card.Years) * NetWorkCount;
                }

            }
            return annual_cost;
        }
        public double Get_Annual_FY_MOU_Cost_H9S50AS()
        {
           
            int StorageCount = 0;
            double annual_cost = 0;
            var deal_manager = DealManagement.GetInstance();
            foreach (var selectedTech in deal_manager.ListOfSelectedTechnologies)
            {

                foreach (var Tech in deal_manager.List_Technologies)
                {
                    if (selectedTech.Quantity > 0 && Tech.Type.Equals("Storage/Servers")
                        && selectedTech.Description.Equals(Tech.Description))
                    {
                        StorageCount += selectedTech.Quantity;
                    }

                }
            }

            foreach (var Rate_Card in List_Rate_Card)
            {
                if (Rate_Card.Part_Number.Equals("H9S50AS"))
                {
                    annual_cost = ((GetDiscountedRatePerFYMOU(Rate_Card) / Rate_Card.Years) * StorageCount);
                }

            }
            return annual_cost;
        }
        public double Get_Annual_FY_MOU_Cost_HP745A3_105()
        {
            
            int Count = 5;
            double annual_cost = 0;

            foreach (var Rate_Card in List_Rate_Card)
            {
                if (Rate_Card.Part_Number.Equals("HP745A3#105"))
                {
                    annual_cost = (GetDiscountedRatePerFYMOU(Rate_Card) / Rate_Card.Years) * Count;
                }

            }
            return annual_cost;
        }
        public double Get_Annual_FY_MOU_Cost_HL859A3_101()
        {
           
            int Count = 0;
            double annual_cost = 0;

            foreach (var Rate_Card in List_Rate_Card)
            {
                if (Rate_Card.Part_Number.Equals("HP745A3#105"))
                {
                    annual_cost = ((GetDiscountedRatePerFYMOU(Rate_Card) / Rate_Card.Years) * Count);
                }

            }
            return annual_cost;
        }

        public double Get_Total_Annual_FYMOU_Cost()
        {
            return Get_Annual_FY_MOU_Cost_H8J01A4_102() + Get_Annual_FY_MOU_Cost_H9S50AS() +
                Get_Annual_FY_MOU_Cost_HP745A3_105() + Get_Annual_FY_MOU_Cost_HL859A3_101() +
                Get_Annual_List_Cost_Platform();
        }

        public double Get_Annual_Cost_TSS_OSS_Pricing_H8J01A4_102()
        {
           
            List<TSSOfferPrice> OrderedListByAnnualListCost = 
                List_OfferPrice.OrderBy(o => o.ANNUAL_TOTAL_LIST_COST_MIN).ToList();
           
            double annual_cost = 0;
            
            for (int i= (OrderedListByAnnualListCost.Count - 1); i>=0 ;i--)
            {
                
                if (Get_Total_Annual_List_Cost() >= 
                    OrderedListByAnnualListCost[i].ANNUAL_TOTAL_LIST_COST_MIN)
                {
                    annual_cost = Get_Annual_List_Cost_H8J01A4_102 ()* (1 - OrderedListByAnnualListCost[i].SAAS);
                    i = -1;
                }
            }
            
            return annual_cost;
        }
        public double Get_Annual_Cost_TSS_OSS_Pricing_H9S50AS()
        {
            List<TSSOfferPrice> OrderedListByAnnualListCost =
                List_OfferPrice.OrderBy(o => o.ANNUAL_TOTAL_LIST_COST_MIN).ToList();
           
            double annual_cost = 0;

            for (int i = (OrderedListByAnnualListCost.Count - 1); i >= 0; i--)
            {                           
                if (Get_Total_Annual_List_Cost() >=
                    OrderedListByAnnualListCost[i].ANNUAL_TOTAL_LIST_COST_MIN)
                {                   
                    annual_cost = Get_Annual_List_Cost_H9S50AS() * (1 - OrderedListByAnnualListCost[i].SAAS);
                    i = -1;
                }
            }

            return annual_cost;
        }
        public double Get_Annual_Cost_TSS_OSS_Pricing_HP745A3_105()
        {
            List<TSSOfferPrice> OrderedListByAnnualListCost =
                List_OfferPrice.OrderBy(o => o.ANNUAL_TOTAL_LIST_COST_MIN).ToList();

            var Deal_manager = DealManagement.GetInstance();
            double annual_cost = 0;

            for (int i = (OrderedListByAnnualListCost.Count - 1); i >= 0; i--)
            {
                
                if (Get_Total_Annual_List_Cost() >=
                    OrderedListByAnnualListCost[i].ANNUAL_TOTAL_LIST_COST_MIN)
                {
                   
                    annual_cost = Get_Annual_List_Cost_HP745A3_105() * 
                        (1 - OrderedListByAnnualListCost[i].SAW);
                    
                    i = -1;
                }
            }

           

            return annual_cost;
        }
        public double Get_Annual_Cost_TSS_OSS_Pricing_HL859A3_101()
        {
            List<TSSOfferPrice> OrderedListByAnnualListCost =
                List_OfferPrice.OrderBy(o => o.ANNUAL_TOTAL_LIST_COST_MIN).ToList();          
            double annual_cost = 0;

            for (int i = (OrderedListByAnnualListCost.Count - 1); i >= 0; i--)
            {
                if (Get_Total_Annual_List_Cost() >=
                    OrderedListByAnnualListCost[i].ANNUAL_TOTAL_LIST_COST_MIN)
                {
                    annual_cost = Get_Annual_List_Cost_HL859A3_101() * (1 - OrderedListByAnnualListCost[i].SAW);
                    i = -1;
                }
            }

            return annual_cost;
        }
    }
   
}
