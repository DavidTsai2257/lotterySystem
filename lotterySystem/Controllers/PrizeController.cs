using lotterySystem.Models.SQLManager;
using lotterySystem.Models.DataModels;
using System.Web.Mvc;
using System.Web;
using OfficeOpenXml;
using System.IO;
using System;
using System.Collections.Generic;
using lotterySystem.Models;

namespace lotterySystem.Controllers
{
    public class PrizeController : Controller
    {
        DataResultModel data = new DataResultModel();
        DBSelecte dbSelecte = new DBSelecte();
        DBInsert dBInsert = new DBInsert();
        DropDownListModel DropDownList = new DropDownListModel();
        /// <summary>
        /// 獎品清單
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var CategoryList = DropDownList.CategoryList();
            Session["CategoryOption"] = CategoryList;

            var PrizeList = dbSelecte.PrizeList();
            ViewBag.PrizeList = PrizeList;
            return View();
        }
        /// <summary>
        /// 獎品新增
        /// </summary>
        /// <param name="Category">獎品類型</param>
        /// <param name="CompanyName">廠商名稱</param>
        /// <param name="Awards">獎品名稱</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Create(int Category,string CompanyName,string Awards)
        {
            data.result = "success";
            List<PrizeListModel> prizeList = new List<PrizeListModel>();
            prizeList.Add(new PrizeListModel() 
            {
                Category = Category,
                Awards = Awards,
                CompanyName = CompanyName,
                Lottery = "N"
            });
            if (!dBInsert.PrizeList(prizeList, false))
            {
                data.result = "fail";
                data.msg = "PrizeList insert fail";
                return Json(data);
            }
            data.msg = "新獎項建立成功";
            return Json(data);
        }
        /// <summary>
        /// 獎品清單匯入
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Import()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        // 另存檔案
                        HttpPostedFileBase file = files[i];
                        string fname;
                        fname = Path.Combine(Server.MapPath("~/File/"), file.FileName);
                        file.SaveAs(fname);
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        using (FileStream fs = new FileStream(fname, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            //載入Excel檔案
                            using (ExcelPackage ep = new ExcelPackage(fs))
                            {
                                ExcelWorksheet sheet = ep.Workbook.Worksheets[0];//取得Sheet1
                                List<PrizeListModel> prizeList = new List<PrizeListModel>();

                                bool isLastRow = false;
                                int RowId = 2;   // 因為有標題列，所以從第2列開始讀起
                                do  // 讀取資料，直到讀到空白列為止
                                {
                                    string cellValue = sheet.Cells[RowId, 1].Text;
                                    if (string.IsNullOrEmpty(cellValue))
                                    {
                                        isLastRow = true;
                                    }
                                    else
                                    {
                                        // 將資料放入UserListRowData中
                                        prizeList.Add(new PrizeListModel()
                                        {
                                            Category = Convert.ToInt32(cellValue),
                                            Awards = sheet.Cells[RowId, 2].Text,
                                            CompanyName = sheet.Cells[RowId, 3].Text,
                                            Lottery = "N"
                                            //UserName = sheet.Cells[RowId, 2].Text
                                        });
                                        RowId += 1;
                                    }
                                } while (!isLastRow);
                                if (!dBInsert.PrizeList(prizeList,true))
                                {
                                    return Json("獎品清單匯入失敗!");
                                }
                            }
                        }
                        System.IO.File.Delete(fname);
                    }
                    return Json("獎品清單匯入成功!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
    }
}