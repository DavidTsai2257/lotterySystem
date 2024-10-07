using lotterySystem.Enums;
using lotterySystem.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lotterySystem.Models
{
    public class DropDownListModel
    {
        /// <summary>
        /// 下拉式選單的頭
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> SelectListItems()
        {
            return new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Text = "請選擇",
                    Value = "0",
                    Selected = true
                }
            };
        }
        /// <summary>
        /// 獎品類別清單
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> CategoryList()
        {
            var CategoryList = SelectListItems();
            CategoryList.AddRange(Enum.GetValues(typeof(CategoryEnum)).
                Cast<CategoryEnum>().Select(v => new SelectListItem
                {
                    Text = GetTransfer.GetDescription(v),
                    Value = ((int)v).ToString()
                }).ToList());
            return CategoryList;

        }
        /// <summary>
        /// 獎品下拉式選單
        /// </summary>
        /// <param name="prizeList"></param>
        /// <returns></returns>
        public List<SelectListItem> PrizeList(IEnumerable<PrizeListModel> prizeList)
        {
            List<SelectListItem> items = SelectListItems();

            foreach (var item in prizeList)
            {
                items.Add(new SelectListItem()
                {
                    Text = item.Awards,
                    Value = item.ID.ToString()
                });
            }
            return items;
        }
    }
}