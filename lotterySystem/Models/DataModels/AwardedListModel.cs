using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lotterySystem.Models.DataModels
{
    /// <summary>
    /// 中獎清單
    /// </summary>
    public class AwardedListModel
    {
        /// <summary>
        /// 流水號
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 獎項類別
        /// </summary>
        public int Category { get; set; }
        /// <summary>
        /// 獎項
        /// </summary>
        public string Awards { get; set; }
        /// <summary>
        /// 得獎人單位(處)
        /// </summary>
        public string Office { get; set; }
        /// <summary>
        /// 得獎人單位(科)
        /// </summary>
        public string Section { get; set; }
        /// <summary>
        /// 得獎人工號
        /// </summary>
        public string StaffNumber { get; set; }
        /// <summary>
        /// 得獎人姓名
        /// </summary>
        public string StaffName { get; set; }
        /// <summary>
        /// 得獎時間
        /// </summary>
        public string CreateTime { get; set; }
    }
}