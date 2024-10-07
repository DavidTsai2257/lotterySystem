using System.ComponentModel;

namespace lotterySystem.Enums
{
    public enum CategoryEnum
    {
        [Description("公司獎項")]
        CompanyAwards = 1,
        [Description("公司加碼")]
        CompanyOverweight = 2,
        [Description("廠商加碼")]
        VendorOverweight = 3
    }

}