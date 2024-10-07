using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using lotterySystem.Enums;
using lotterySystem.Models;
using lotterySystem.Models.DataModels;
using lotterySystem.Models.SQLManager;

namespace lotterySystem.Controllers
{      
    public class HomeController : Controller
    {
        DataResultModel data = new DataResultModel();
        DBSelecte dbSelecte = new DBSelecte();
        DBInsert dBInsert = new DBInsert();
        DBUpdate dBUpdate = new DBUpdate();
        DropDownListModel DropDownList = new DropDownListModel();
        /// <summary>
        /// 抽獎首頁
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var PrizeList = DropDownList.SelectListItems();
            Session["PrizeOption"] = PrizeList;
            var CategoryList = DropDownList.CategoryList();
            Session["CategoryOption"] = CategoryList;
            return View();
        }
        [HttpPost]
        /// <summary>
        /// 開始抽獎
        /// </summary>
        /// <param name="Category">獎品類別</param>
        /// <param name="Prize">獎品名稱</param>
        /// <param name="PeopleNumber">中獎人數</param>
        /// <param name="YN">是否可以重複抽</param>
        /// <returns></returns>
        public JsonResult LotteryMethod(int Category,int Prize,int PeopleNumber,int YN)
        {
            data.result = "success";
            var PrizeList = dbSelecte.PrizeList().Where
                (x => x.Lottery == "N" && x.Category == Category && x.ID == Prize).FirstOrDefault();
            if (PrizeList == null)
            {
                data.result = "fail";
                data.msg = "該獎項已經被抽過了";
                return Json(data);
            }
            //獎項標記為以抽過
            PrizeListModel prizeList = new PrizeListModel()
            {
                ID = PrizeList.ID,
                Lottery = "Y"
            };
            //讀取同仁清單
            var PeopleList = dbSelecte.StaffList();
            //判斷是否可以重複抽
            if (YN == 0 )
                PeopleList = PeopleList.Where(x => x.Awards == "").ToList();
            if (PeopleNumber > PeopleList.Count)
            {
                data.result = "fail";
                data.msg = "超出可抽人數!";
                return Json(data);
            }
            if (!dBUpdate.PrizeList(prizeList))
            {
                data.result = "fail";
                data.msg = "PrizeList data update fail!";
                return Json(data);
            }
            //隨機亂數
            var rnd = new Random();
            PeopleList = PeopleList.OrderBy(x => rnd.Next()).ToList();
            for (int i = 0; i < PeopleNumber; i++)
            {
                string Awards = "";
                if (PeopleList[i].Awards != "")
                    Awards = PeopleList[i].Awards + "、" + PrizeList.Awards;
                else
                    Awards = PrizeList.Awards;
                //人員清單標記以得獎
                if (!dBUpdate.StaffList(Awards, PeopleList[i].StaffNumber))
                {
                    data.result = "fail";
                    data.msg = "StaffList data update fail!";
                    return Json(data);
                }
                //寫入得獎清單
                AwardedListModel awardedList = new AwardedListModel()
                {
                    Category = Category,
                    Awards = PrizeList.Awards,
                    Office = PeopleList[i].Office,
                    Section = PeopleList[i].Section,
                    StaffNumber = PeopleList[i].StaffNumber,
                    StaffName = PeopleList[i].StaffName,
                };
                if (!dBInsert.AwardedList(awardedList))
                {
                    data.result = "fail";
                    data.msg = "AwardedList data insert fail!";
                    return Json(data);
                }
                //回傳字串給前端
                if (PeopleList[i].Section == "")
                    data.msg += "單位(處):" + PeopleList[i].Office + "  工號:" + PeopleList[i].StaffNumber +
                        "  姓名:" + PeopleList[i].StaffName + "\n";
                else
                    data.msg += "單位(處):" + PeopleList[i].Office + "  單位(科):" + PeopleList[i].Section +
                        "  工號:" + PeopleList[i].StaffNumber + "  姓名:" + PeopleList[i].StaffName + "\n";
            }
            return Json(data);
        }
        /// <summary>
        /// 撈取獎項
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        public JsonResult GetPrizeList(int Category)
        {
            var PrizeList = dbSelecte.PrizeList().Where(x => x.Category == Category && x.Lottery == "N").ToList();
            //if (Category == (int)CategoryEnum.CompanyAwards)
            //{
            //    //判斷是否有抽過
            //    PrizeList = PrizeList.Where(x => x.Lottery == "N");
            //}
            var items = DropDownList.PrizeList(PrizeList);
            return Json(items, JsonRequestBehavior.AllowGet);
        }
    }
}