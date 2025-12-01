using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMasterHelper
{
    interface IToolsHelper
    {
        List<AdminMenu> GetAdminMenuList(GetResponse modal);
        List<AdminModule> GetModuleListWithMenu(GetResponse modal);
        List<ErrorLog> ErrorLogList();
        List<EmailTemplate> GetEmailTemplateList(long ID);
        int CheckEmailTemplateNameExist(string ID, string TemplateName);
        List<UserRole> GetRoleList(long RoleID);
        List<UserMan.List> GetLoginUserList(GetResponse modal);
        UserMan.Add GetLoginUser(GetResponse modal);
     
        List<ConfigSetting> GetConfigSettingList(long ID);
        List<User_Task.Task.List> GetUser_TaskList(long TaskID, long TableID = 0, string TableName = "");
        List<User_Task.Notes.List> GetUser_NotesList(long TaskID);
        List<User_Task.StatusList> GetUser_Task_StatusList(long TaskID, long StatusID, string Type, string IsActive);
        List<Menu.List> GetLoginMenuList(GetResponse modal);

        Menu.Add GetLoginMenu(GetResponse modal);
        PostResponse fnSetLoginMenu(Menu.Add model);
        List<Menu.ChildMenu> GetMenuChildList(GetResponse modal);
        UserMan.Add GetLoginNonmiteUser(GetResponse modal);
        List<UserMan.List> GetLoginNonMiteUserList(GetResponse modal);
        List<UserMan.NonMitrList> GetNonMitrList();
    }
}