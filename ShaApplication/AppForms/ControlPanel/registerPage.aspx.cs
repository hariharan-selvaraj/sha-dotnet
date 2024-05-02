using SHA.BLL.Service;
using SHA.Data.Models;
using System;
using System.Collections.Generic;
using System.Web.UI;
using SHA.Data.Utility;
using Newtonsoft.Json;


namespace ShaApplication.AppForms.ControlPanel
{
    public partial class registerPage : System.Web.UI.Page
    {
        #region Common Variables
        private IUserService userService = null;
        private ILogFileService logFileService
        {
            get
            {
                if (__logFileService == null) { __logFileService = new LogFileService(); }
                return __logFileService;
            }
        }
        private ILogFileService __logFileService = null;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateCompaniesDropDownList();
            }
        }
        private void PopulateCompaniesDropDownList()
        {
            List<DropDownItem> companyList = new List<DropDownItem>();
            string modelJson = "";
            try
            {
                this.userService = new UserService();
                companyList = userService.GetCompanyDrpoDownData();
                companyList.Insert(0, new DropDownItem { Key = 0, Value = "-Select Company-" });
                companyDropDownList.DataSource = companyList;
                companyDropDownList.DataValueField = "Key";
                companyDropDownList.DataTextField = "Value";
                companyDropDownList.DataBind();
            }
            catch (Exception ex)
            {
                modelJson = JsonConvert.SerializeObject(companyList);
                this.logFileService.LogError(0, "REGISTER", "registerPage.aspx.cs", ex, modelJson);
            }
            finally { companyList = null; }
        }
        public void SaveRegisterDetails(object sender, EventArgs e)
        {
            string msg = "";
            string modelJson = "";
            long flag;
            UserDetails model = new UserDetails();
            try
            {
                model = GetUserDetails();
                msg = ValidateModel(model);
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    viewMessage.Text = msg;
                    viewMessage.Visible = true;
                    return;
                }
                userService = new UserService();
                flag = userService.RegisterUser(model);
                viewMessage.Text = (flag >= 1 ? "Registered Successfully" : ((flag == -1 ? "An error occurred while processing your request." : "IC No already exist.")));
                viewMessage.CssClass = flag > 0 ? "text-success" : "text-danger";
                viewMessage.Visible = true;
                if (flag > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = 'loginPage.aspx'; }, 1000);", true);
                }
            }
            catch (Exception ex)
            {
                viewMessage.Text = "An error occurred while processing your request.";
                viewMessage.Visible = true;
                modelJson = JsonConvert.SerializeObject(model);
                this.logFileService.LogError(0, "REGISTER", "registerPage.aspx.cs", ex, modelJson);
            }
            finally { model = null; msg = null; this.userService = null; this.__logFileService = null; }
        }

        private UserDetails GetUserDetails()
        {
            UserDetails model;
            try
            {
                model = new UserDetails();
                model.Company = long.Parse(companyDropDownList.SelectedItem.Value);
                model.AdminName = adminName.Text.Trim();
                model.PhoneNumber = phNo.Text.Trim();
                model.Address = address.Value.Trim();
                model.EmailAddress = emailAddress.Text.Trim();
                model.PassportNumber = passPortNo.Text.Trim();
                model.IcNumber = icNo.Text.Trim();
                model.IsAdmin = isAdmin.Checked;
                model.RoleId = isAdmin.Checked ? 1 : 2;
                model.Password = password.Text.Trim();
                model.ConfirmPassword = cPassword.Text.Trim();
                return model;
            }
            finally { model = null; }
        }
        private string ValidateModel(UserDetails model)
        {
            if (model.Company <= 0) { return "Please select a company."; }
            if (string.IsNullOrEmpty(model.AdminName)) { return "Please fill Admin Name."; }
            if (string.IsNullOrEmpty(model.PhoneNumber)) { return "Please fill Phone Number."; }
            if (string.IsNullOrEmpty(model.Address)) { return "Please fill Address."; }
            if (string.IsNullOrEmpty(model.EmailAddress)) { return "Please fill Email Address."; }
            if (string.IsNullOrEmpty(model.PassportNumber)) { return "Please fill Password."; }
            if (string.IsNullOrEmpty(model.Password)) { return "Please fill Confirm password."; }
            if (string.IsNullOrEmpty(model.ConfirmPassword)) { return "Please fill Confirm password."; }
            if (model.Password != model.ConfirmPassword) { return "Password is Mismatching. Please Enter Correct password."; }
            return "";
        }

        protected void ClearRegisterDetails(object sender, EventArgs e)
        {
            companyDropDownList.SelectedIndex = 0;
            adminName.Text = "";
            phNo.Text = "";
            address.Value = "";
            emailAddress.Text = "";
            passPortNo.Text = "";
            icNo.Text = "";
            password.Text = "";
            cPassword.Text = "";
        }
    }
}