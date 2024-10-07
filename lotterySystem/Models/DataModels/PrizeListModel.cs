namespace lotterySystem.Models.DataModels
{
    /// <summary>
    /// 獎品清單
    /// </summary>
    public class PrizeListModel
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
        /// 廠商名稱
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 是否抽過
        /// </summary>
        public string Lottery { get; set; }
    }
}