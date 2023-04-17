using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.ViewModel
{
    public static class CheckRoleRights
    {
        public static bool View { get; set; }
        public static bool Insert { get; set; }
        public static bool Edit { get; set; }
        public static bool Delete { get; set; }


        public static string IsView = "IsView";
        public static string IsInsert = "IsInsert";
        public static string IsEdit = "IsEdit";
        public static string IsDelete = "IsDelete";

        public enum FormAccessCode
        {
            IsView = 1,
            IsInsert = 2,
            IsEdit = 3,
            IsDelete = 4
        }
    }
}
