using System;
using System.Collections.Generic;
using lotterySystem.Models.DataModels;
using System.Data.SqlClient;
using System.Configuration;

namespace lotterySystem.Models.SQLManager
{
    public class DBSelecte
    {
        /// <summary>
        /// DB連線資訊
        /// </summary>
        private readonly string Connstr = ConfigurationManager.ConnectionStrings["lotterySystemConnectionString"].ConnectionString;
        /// <summary>
        /// 讀取獎項資料
        /// </summary>
        /// <returns></returns>
        public List<PrizeListModel> PrizeList()
        {
            List<PrizeListModel> PrizeList = new List<PrizeListModel>();

            try
            {
                SqlConnection sqlConnection = new SqlConnection(Connstr);
                SqlCommand sqlCommand = new SqlCommand(@"SELECT ID,Category,Awards,CompanyName,Lottery
                                                           FROM tb_PrizeList
                                                          ORDER BY [Category];");
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    PrizeListModel prizeList = new PrizeListModel
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Category = Convert.ToInt32(reader["Category"]),
                        Awards = reader["Awards"].ToString(),
                        CompanyName = reader["CompanyName"].ToString(),
                        Lottery = reader["Lottery"].ToString()
                    };
                    PrizeList.Add(prizeList);
                }
                sqlConnection.Close();
                return PrizeList;
            }
            catch(Exception ex)
            {
                string errmsg = ex.Message;
                throw ex;
            }
        }
        public List<StaffListModel> StaffList()
        {
            List<StaffListModel> StaffList = new List<StaffListModel>();

            try
            {
                SqlConnection sqlConnection = new SqlConnection(Connstr);
                SqlCommand sqlCommand = new SqlCommand(@"SELECT Office,Section,StaffNumber,StaffName,Awards
                                                           FROM tb_StaffList
                                                          ORDER BY [Office],[Section],[StaffNumber];");
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    StaffListModel staffList = new StaffListModel
                    {
                        Office = reader["Office"].ToString(),
                        Section = reader["Section"].ToString(),
                        StaffNumber = reader["StaffNumber"].ToString(),
                        StaffName = reader["StaffName"].ToString(),
                        Awards = reader["Awards"].ToString()
                    };
                    StaffList.Add(staffList);
                }
                sqlConnection.Close();
                return StaffList;
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                throw ex;
            }
        }
        public List<AwardedListModel> AwardedList()
        {
            List<AwardedListModel> AwardedList = new List<AwardedListModel>();

            try
            {
                SqlConnection sqlConnection = new SqlConnection(Connstr);
                SqlCommand sqlCommand = new SqlCommand(@"SELECT ID,Category,Awards,Office,Section,StaffNumber,StaffName,CreateTime
                                                           FROM tb_AwardedList
                                                          WHERE DeleteMark = 'N'
                                                          ORDER BY [Category],[Awards],[Office],[Section],[StaffNumber],[StaffName]");
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    AwardedListModel awardedList = new AwardedListModel
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Category = Convert.ToInt32(reader["Category"]),
                        Awards = reader["Awards"].ToString(),
                        Office = reader["Office"].ToString(),
                        Section = reader["Section"].ToString(),
                        StaffNumber = reader["StaffNumber"].ToString(),
                        StaffName = reader["StaffName"].ToString(),
                        CreateTime = reader["CreateTime"].ToString()
                    };
                    AwardedList.Add(awardedList);
                }
                sqlConnection.Close();
                return AwardedList;
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                throw ex;
            }
        }
    }
}