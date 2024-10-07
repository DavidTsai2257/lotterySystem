using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lotterySystem.Models.DataModels
{
    /// <summary>
    /// 資料寫入DB結果
    /// </summary>
    public class DataResultModel
    {
        /// <summary>
        /// 結果
        /// </summary>
        public string result { get; set; }
        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string msg { get; set; }
    }
}