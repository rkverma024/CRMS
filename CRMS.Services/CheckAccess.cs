using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using CRMS.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CRMS.Services
{
    public class CheckAccess 
    {        
        public static bool ChechAccessPermission(string formAccessCode, string action)
        {
            bool RoleCode = (bool)HttpContext.Current.Session["RoleCode"];
            if (RoleCode == false)
            {
                List<FormRoleMapping> formRole = HttpContext.Current.Session["Permission"] as List<FormRoleMapping>;
                if (formRole != null)
                {
                    using (DataContext db = new DataContext())
                    {
                        Guid FormId = db.FormMsts.Where(x => x.FormAccessCode == formAccessCode).Select(x => x.Id).FirstOrDefault();
                        FormRoleMapping mapping = formRole.Where(x => x.FormId == FormId).FirstOrDefault();

                        if (formRole != null)
                        {
                            if (mapping.AllowView == true)
                            {
                                CheckRoleRights.View = mapping.AllowView;
                                CheckRoleRights.Insert = mapping.AllowInsert;
                                CheckRoleRights.Edit = mapping.AllowEdit;
                                CheckRoleRights.Delete = mapping.AllowDelete;
                                if (action == CheckRoleRights.FormAccessCode.IsView.ToString())
                                {
                                    return mapping.AllowView;
                                }
                                else if (action == CheckRoleRights.FormAccessCode.IsInsert.ToString())
                                {
                                    return mapping.AllowInsert;
                                }
                                else if (action == CheckRoleRights.FormAccessCode.IsEdit.ToString())
                                {
                                    return mapping.AllowEdit;
                                }
                                else if (action == CheckRoleRights.FormAccessCode.IsDelete.ToString())
                                {
                                    return mapping.AllowDelete;
                                }
                            }
                            else
                            {
                                CheckRoleRights.View = false;
                                CheckRoleRights.Insert = false;
                                CheckRoleRights.Edit = false;
                                CheckRoleRights.Delete = false;
                                return false;
                            }
                            return false;
                        }
                        else
                        {
                            CheckRoleRights.View = false;
                            CheckRoleRights.Insert = false;
                            CheckRoleRights.Edit = false;
                            CheckRoleRights.Delete = false;
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                CheckRoleRights.View = true;
                CheckRoleRights.Insert = true;
                CheckRoleRights.Edit = true;
                CheckRoleRights.Delete = true;
                return true;
            }
        }
    }
}




