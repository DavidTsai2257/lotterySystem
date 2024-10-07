using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using lotterySystem.Enums;
using lotterySystem.Models;
using lotterySystem.Models.DataModels;
using lotterySystem.Models.SQLManager;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;

namespace lotterySystem.Controllers
{
    public class AwardedController : Controller
    {
        DataResultModel data = new DataResultModel();
        DBSelecte dbSelecte = new DBSelecte();
        DBInsert dBInsert = new DBInsert();
        DBUpdate dBUpdate = new DBUpdate();
        DropDownListModel DropDownList = new DropDownListModel();
        /// <summary>
        /// 得獎清單
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var AwardedList = dbSelecte.AwardedList();
            ViewBag.AwardedList = AwardedList;
            return View();
        }
        /// <summary>
        /// 得獎清單匯出
        /// </summary>
        /// <returns></returns>
        public ActionResult AwardedList_Export()
        {
            var AwardedList = dbSelecte.AwardedList().ToList();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var ep = new ExcelPackage())
            {
                var sheet = ep.Workbook.Worksheets.Add("得獎清單");
                int col = 1;
                sheet.Cells["A1:G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                sheet.Cells["A1:G1"].Style.Fill.BackgroundColor.SetColor(Color.CornflowerBlue);
                ///表頭
                sheet.Cells[col, 1].Value = "獎項類別";
                sheet.Cells[col, 2].Value = "獎項";
                sheet.Cells[col, 3].Value = "得獎人單位(處)";
                sheet.Cells[col, 4].Value = "得獎人單位(科)";
                sheet.Cells[col, 5].Value = "得獎人工號";
                sheet.Cells[col, 6].Value = "得獎人姓名";
                sheet.Cells[col, 7].Value = "得獎時間";
                int row = 2; ///資料由第二列開始寫入
                foreach (var item in AwardedList) ///寫值進入報表內
                {
                    col = 1;
                    sheet.Cells[row, col++].Value = GetTransfer.GetDescription((CategoryEnum)item.Category);
                    sheet.Cells[row, col++].Value = item.Awards;
                    sheet.Cells[row, col++].Value = item.Office;
                    sheet.Cells[row, col++].Value = item.Section;
                    sheet.Cells[row, col++].Value = item.StaffNumber;
                    sheet.Cells[row, col++].Value = item.StaffName;
                    sheet.Cells[row, col++].Value = item.CreateTime;
                    row++; ///下一列
                }
                sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                ///匯出檔案及檔名
                return File(ep.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "得獎清單.xlsx");
            }
        }
        /// <summary>
        /// 清除得獎清單
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Awarded_Clear()
        {
            data.result = "success";
            if (!dBUpdate.AwardedList())
            {
                data.result = "fail";
                data.msg = "清除列表失敗";
                return Json(data);
            }
            return Json(data);
        }
        /// <summary>
        /// 刪除指定得獎資料
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult Awarded_Delete(int ID)
        {
            //同仁清單得獎欄位修改
            var awardedList = dbSelecte.AwardedList().Where(x => x.ID == ID).FirstOrDefault(); //在得獎清單讀取對應資料
            var staffList = dbSelecte.StaffList().Where(x => x.StaffNumber == awardedList.StaffNumber).FirstOrDefault(); //讀取得獎者資料
            string[] awardedList2 = staffList.Awards.Split('、'); //將該得獎者得獎項目做切割
            awardedList2 = awardedList2.Where(x => x != awardedList.Awards).ToArray(); //排除掉要刪除的獎項
            string awarded = "";
            //如果該得獎者還有其他獎項，將該名得獎者的獎項欄位字串做重組
            if (awardedList2.Count() != 0)
            {
                foreach (var item in awardedList2)
                {
                    awarded += item + "、";
                }
                awarded = awarded.Substring(0, awarded.Length - 1);
            }
            dBUpdate.StaffList(awarded, staffList.StaffNumber);//寫入DB
            //註銷指定得獎資料
            dBUpdate.AwardedList(ID);
            var AwardedList = dbSelecte.AwardedList();
            ViewBag.AwardedList = AwardedList;
            return View("Index");
        }
    }
}