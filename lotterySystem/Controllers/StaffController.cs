using lotterySystem.Models.SQLManager;
using lotterySystem.Models.DataModels;
using System.Web.Mvc;
using System.Web;
using OfficeOpenXml;
using System.IO;
using System;
using System.Collections.Generic;

namespace lotterySystem.Controllers
{
    public class StaffController : Controller
    {
        DBSelecte dbSelecte = new DBSelecte();
        DBInsert dBInsert = new DBInsert();
        /// <summary>
        /// 同仁清單
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var StaffList = dbSelecte.StaffList();
            ViewBag.StaffList = StaffList;
            return View();
        }
        /// <summary>
        /// 同仁清單匯入
        /// </summary>
        /// <returns></returns>
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
                                List<StaffListModel> staffList = new List<StaffListModel>();

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
                                        staffList.Add(new StaffListModel()
                                        {
                                            Office = cellValue,
                                            Section = sheet.Cells[RowId, 2].Text,
                                            StaffNumber = sheet.Cells[RowId, 3].Text,
                                            StaffName = sheet.Cells[RowId, 4].Text,
                                        });
                                        RowId += 1;
                                    }
                                } while (!isLastRow);
                                if (!dBInsert.StaffList(staffList, true))
                                {
                                    return Json("同仁清單匯入失敗!");
                                }
                            }
                        }
                        System.IO.File.Delete(fname);
                    }
                    return Json("同仁清單匯入成功!");
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