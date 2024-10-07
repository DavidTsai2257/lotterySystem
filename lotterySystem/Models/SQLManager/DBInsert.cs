using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using lotterySystem.Models.DataModels;
using lotterySystem.Models.SQLManager;

namespace lotterySystem.Models.SQLManager
{
    public class DBInsert
    {
        /// <summary>
        /// 寫入獎項table
        /// </summary>
        /// <param name="prizeList"></param>
        /// <param name="Reset"></param>
        /// <returns></returns>
        public bool PrizeList(List<PrizeListModel> prizeList,bool Reset)
        {
            bool result = true;
            String str1 = @"DELETE FROM tb_PrizeList;";
            String str2 = @"INSERT INTO tb_PrizeList 
                            ([Category],[Awards],[CompanyName],[Lottery])
                           VALUES
                            (@Category,@Awards,@CompanyName,@Lottery);";
            try
            {
                if (Reset == true)
                    result = new SQLImplement().SqlExecuteNonQuery(str1);              
                foreach (var item in prizeList)
                {
                    result = new SQLImplement().SqlExecuteNonQuery(str2, new SqlParameter[]
                    {
                        new SqlParameter("@Category", item.Category),
                        new SqlParameter("@Awards", item.Awards),
                        new SqlParameter("@CompanyName", item.CompanyName),
                        new SqlParameter("@Lottery", item.Lottery)
                    });
                };
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 寫入同仁table
        /// </summary>
        /// <param name="prizeList"></param>
        /// <param name="Reset"></param>
        /// <returns></returns>
        public bool StaffList(List<StaffListModel> staffList, bool Reset)
        {
            bool result = true;
            String str1 = @"DELETE FROM tb_StaffList;";
            String str2 = @"INSERT INTO tb_StaffList 
                            ([Office],[Section],[StaffNumber],[StaffName])
                           VALUES
                            (@Office,@Section,@StaffNumber,@StaffName);";
            try
            {
                if (Reset == true)
                    result = new SQLImplement().SqlExecuteNonQuery(str1);
                foreach (var item in staffList)
                {
                    result = new SQLImplement().SqlExecuteNonQuery(str2, new SqlParameter[]
                    {
                        new SqlParameter("@Office", item.Office),
                        new SqlParameter("@Section", item.Section),
                        new SqlParameter("@StaffNumber", item.StaffNumber),
                        new SqlParameter("@StaffName", item.StaffName)
                    });
                };
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                result = false;
            }
            return result;
        }
        public bool AwardedList(AwardedListModel awardedList)
        {
            bool result = true;
            String str2 = @"INSERT INTO tb_AwardedList 
                            ([Category],[Awards],[Office],[Section],[StaffNumber],[StaffName],[CreateTime],[DeleteMark])
                           VALUES
                            (@Category,@Awards,@Office,@Section,@StaffNumber,@StaffName,@CreateTime,@DeleteMark);";
            try
            {
                result = new SQLImplement().SqlExecuteNonQuery(str2, new SqlParameter[]
                {
                        new SqlParameter("@Category", awardedList.Category),
                        new SqlParameter("@Awards", awardedList.Awards),
                        new SqlParameter("@Office", awardedList.Office),
                        new SqlParameter("@Section", awardedList.Section),
                        new SqlParameter("@StaffNumber", awardedList.StaffNumber),
                        new SqlParameter("@StaffName", awardedList.StaffName),
                        new SqlParameter("@CreateTime", DateTime.Now),
                        new SqlParameter("@DeleteMark", "N"),
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