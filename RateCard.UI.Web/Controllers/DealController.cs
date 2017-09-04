using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RateCard.Api.Core;
using RateCard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RateCard.UI.Web.Controllers
{
    public class DealController : Controller
    {       
        // GET: Deal
        public ActionResult Deal()
        {
            var listOfTechnologies = DealManagement.GetInstance().List_Technologies;

            return View(listOfTechnologies);
        }
        [HttpPost]
        public void UPDSelectedTechnologies(List<SelectedTechnology> list)
        {
            MVCManagement.GetInstance().UPD_ListOfSelectedTechnologies(list);
          
        }
        [HttpPost]
        public JsonResult UPDDealInputs(Deal deal)
        {
            MVCManagement.GetInstance().UPD_DealInputs(deal);

            return Json(new { });

        }

        [HttpGet]
        public JsonResult RETTBLGeneralNetPriceData()
        {
            List<TableRow> list = MVCManagement.GetInstance().RET_TBLGeneralNetPriceData();


            return Json(new {
                            list = list
            }, JsonRequestBehavior.AllowGet
                    );
        }
        
        [HttpGet]
        public JsonResult RETTBLFixedPriceData()
        {
            List<TableRow> list = MVCManagement.GetInstance().RET_TBLFixedPriceData();

            return Json(new
            {
                list = list
            }, JsonRequestBehavior.AllowGet
                    );
        }
        [HttpGet]
        public JsonResult RETTBLTechnologyPriceData()
        {
            List<TableRow> list = MVCManagement.GetInstance().RET_TBLTechnologyPriceData();

            return Json(new
            {
                list = list
            }, JsonRequestBehavior.AllowGet
                    );
        }
        [HttpGet]
        public JsonResult RETTBLCasperCostData()
        {
            List<TableRow> list = MVCManagement.GetInstance().RET_TBLCasperCostData();

            return Json(new
            {
                list = list
            }, JsonRequestBehavior.AllowGet
                    );
        }
    }
}