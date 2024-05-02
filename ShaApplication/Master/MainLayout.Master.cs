using Newtonsoft.Json;
using SHA.BLL.Service;
using SHA.Data.Models;
using ShaApplication.Utility;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;

namespace ShaApplication.Master
{
    public partial class MainLayout : System.Web.UI.MasterPage
    {
        private ILogFileService logFileService
        {
            get
            {
                if (__logFileService == null) { __logFileService = new LogFileService(); }
                return __logFileService;
            }
        }
        private ILogFileService __logFileService = null;
        private IUserService userService = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            List<MenuDetails> menuList;
            string jsonMenu;
            try
            {
                if (SessionManager.UserId <= 0)
                {
                    //FormsAuthentication.RedirectToLoginPage();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please Login');", true);
                    Response.Redirect("loginPage.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    return;
                }
                else
                {
                    if (!IsPostBack) {
                        menuList = GetMenuData();
                        jsonMenu = JsonConvert.SerializeObject(menuList);
                        SessionManager.MenuData = jsonMenu;
                    }
                    else
                    {
                        jsonMenu = SessionManager.MenuData;
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "fnGenerateListBody", $"fnGenerateListBody('{jsonMenu}');", true);
                }
            }
            catch (Exception ex)
            {
                this.logFileService.LogError(SessionManager.UserId, "MAIN LAYOUT", "MainLayout.Master.cs", ex, "");
                ScriptManager.RegisterStartupScript(this, GetType(), "errorAlert", "alert('An error occurred. Please try again later.');", true);
            }
            finally { menuList = null; jsonMenu = null; }
        }

        //[WebMethod]
        public List<MenuDetails> GetMenuData()
        {
            userService = new UserService();
            try
            {
                return userService.GetMenuData(SessionManager.UserId, SessionManager.RoleId);
            }
            catch(Exception ex)
            {
                this.logFileService.LogError(SessionManager.UserId, "MAIN LAYOUT", "MainLayout.Master.cs", ex, "");
                return new List<MenuDetails>();
            }
            finally
            {
                userService = null;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("loginPage.aspx", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}