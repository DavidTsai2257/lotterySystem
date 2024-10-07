using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lotterySystem.Models.DataModels
{
    /// <summary>
    /// 同仁清單
    /// </summary>
    public class StaffListModel
    {
        /// <summary>
        /// 單位(處)
        /// </summary>
        public string Office { get; set; }
        /// <summary>
        /// 單位(科)
        /// </summary>
        public string Section { get; set;}
        /// <summary>
        /// 員工編號
        /// </summary>
        public string StaffNumber { get; set; }
        /// <summary>
        /// 員工姓名
        /// </summary>
        public string StaffName { get; set; }
        /// <summary>
        /// 獎項
        /// </summary>
        public string Awards { get; set; }
    }
}