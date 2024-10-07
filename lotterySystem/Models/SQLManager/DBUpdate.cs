using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using lotterySystem.Models.DataModels;
using lotterySystem.Models.SQLManager;

namespace lotterySystem.Models.SQLManager
{
    public class DBUpdate
    {
        /// <summary>
        /// 獎項標記為已抽過
        /// </summary>
        /// <param name="prizeList"></param>
        /// <returns></returns>
        public bool PrizeList(PrizeListModel prizeList)
        {
            bool result = true;

            String str = @"UPDATE tb_PrizeList SET 
                                   [Lottery] = @Lottery
                             WHERE [ID] = @ID;";
            try
            {
                result = new SQLImplement().SqlExecuteNonQuery(str, new SqlParameter[]
                {
                        new SqlParameter("@ID", prizeList.ID),
                        new SqlParameter("@Lottery", prizeList.Lottery)
                });
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 人員清單標記得獎
        /// </summary>
        /// <param name="staffList"></param>
        /// <returns></returns>
        public bool StaffList(string Awards,string StaffNumber)
        {
            bool result = true;
            String str = @"UPDATE tb_StaffList SET 
                                   [Awards] = @Awards
                             WHERE [StaffNumber] = @StaffNumber;";
            try
            {
                result = new SQLImplement().SqlExecuteNonQuery(str, new SqlParameter[]
                {
                        new SqlParameter("@Awards", Awards),
                        new SqlParameter("@StaffNumber", StaffNumber)
                });
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 刪除單筆獲獎資料
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool AwardedList(int ID)
        {
            bool result = true;
            String str = @"UPDATE tb_AwardedList SET 
                                   [DeleteMark] = @DeleteMark
                             WHERE [ID] = @ID;";
            try
            {
                result = new SQLImplement().SqlExecuteNonQuery(str, new SqlParameter[]
                {
                        new SqlParameter("@DeleteMark", "Y"),
                        new SqlParameter("@ID", ID)
                });
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 清除所有獲獎資料
        /// </summary>
        /// <returns></returns>
        public bool AwardedList()
        {
            bool result = true;
            String str = @"UPDATE tb_AwardedList SET [DeleteMark] = @DeleteMark;
                           UPDATE tb_PrizeList SET [Lottery] = @Lottery;
                           UPDATE tb_StaffList SET [Awards] = null;";
            try
            {
                result = new SQLImplement().SqlExecuteNonQuery(str, new SqlParameter[]
                {
                        new SqlParameter("@DeleteMark", "Y"),
                        new SqlParameter("@Lottery", "N")
                });
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                result = false;
            }
            return result;
        }
    }
}