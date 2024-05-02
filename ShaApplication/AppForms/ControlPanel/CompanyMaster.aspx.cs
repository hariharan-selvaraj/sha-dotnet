using Newtonsoft.Json;
using SHA.BLL.Service;
using SHA.Data.Models;
using SHA.Data.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShaApplication.AppForms.ControlPanel
{
    public partial class CompanyMaster : System.Web.UI.Page
    {
        #region Common Variables
        private ICompanyService companyService = null;
        private ILogFileService logFileService
        {
            get
            {
                if (__logFileService == null) { __logFileService = new LogFileService(); }
                return __logFileService;
            }
        }
        private ILogFileService __logFileService = null;
        private string selectedRowId = null;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateCompanyTypeDropDownList();
                //CompanyGridView.PageIndexChanging += new GridViewPageEventHandler(CompanyGridView_PageIndexChanging);
                //CompanyGridView.EnableViewState = true;
                BindCompanyGrid();
                popup_container.Visible = false;
                popup_confirm_container.Visible = false;
            }
        }
        private void PopulateCompanyTypeDropDownList()
        {
            List<DropDownItem> companyList = new List<DropDownItem>();
            string modelJson = "";
            try
            {
                this.companyService = new CompanyService();
                companyList = companyService.GetCompanyTypeDrpoDownData();
                companyList.Insert(0, new DropDownItem { Key = 0, Value = "-Select Company-" });
                companyTypeDropDownList.DataSource = companyList;
                companyTypeDropDownList.DataValueField = "Key";
                companyTypeDropDownList.DataTextField = "Value";
                companyTypeDropDownList.DataBind();
            }
            catch (Exception ex)
            {
                modelJson = JsonConvert.SerializeObject(companyList);
                this.logFileService.LogError(0, "COMPANY MASTER", "CompanyMaster.aspx.cs", ex, modelJson);
            }
            finally { companyList = null; }
        }
        public void SaveCompanyDetails(object sender, EventArgs e)
        {
            string msg = "";
            string modelJson = "";
            long flag;
            companyDetails model = new companyDetails();
            try
            {
                model = GetCompanyDetails();
                msg = ValidateModel(model);
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    return;
                }
                companyService = new CompanyService();
                if (model.CompanyId == 0)
                {
                    flag = companyService.AddCompany(model);
                }
                else
                {
                    flag = companyService.EditCompany(model);
                }
                if (flag > 0)
                {
                    BindCompanyGrid();
                    popup_container.Visible = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Problem In Application.Please Contact Admin');", true);
                }
            }
            catch (Exception ex)
            {
                modelJson = JsonConvert.SerializeObject(model);
                this.logFileService.LogError(0, "COMPANY MASTER", "CompanyMaster.aspx.cs", ex, modelJson);
            }
            finally { model = null; msg = null; this.companyService = null; this.__logFileService = null; }
        }
        private companyDetails GetCompanyDetails()
        {
            companyDetails model;
            long companyIdValue;
            try
            {
                model = new companyDetails();
                model.CompanyId = long.TryParse(companyId.Value, out companyIdValue) ? companyIdValue : 0;
                model.CompanyName = companyName.Text.Trim();
                model.CompanyCode = companyCode.Text.Trim();
                model.CompanyTypeId = int.Parse(companyTypeDropDownList.SelectedItem.Value);
                model.CompanyUen = companyUen.Text.Trim();
                model.PhoneNo = phNo.Text.Trim();
                model.Address = address.Value.Trim();
                model.EmailAddress = emailAddress.Text.Trim();
                model.PostalCode = postalCode.Text.Trim();
                return model;
            }
            finally { model = null; }
        }
        private string ValidateModel(companyDetails model)
        {

            if (string.IsNullOrEmpty(model.CompanyName)) { return "Please fill Company Name."; }
            if (string.IsNullOrEmpty(model.CompanyCode)) { return "Please fill Company Code."; }
            if (model.CompanyTypeId <= 0) { return "Please select a Company Type."; }
            if (string.IsNullOrEmpty(model.PhoneNo)) { return "Please fill Phone Number."; }
            if (string.IsNullOrEmpty(model.Address)) { return "Please fill Address."; }
            if (string.IsNullOrEmpty(model.EmailAddress)) { return "Please fill Email Address."; }
            if (string.IsNullOrEmpty(model.PostalCode)) { return "Please fill Postal Code."; }
            return "";
        }
        private void BindCompanyGrid()
        {
            List<companyGridModel> companiesGridData;
            try
            {
                companyService = new CompanyService();
                companiesGridData = companyService.GetCompanyGridData();
                CompanyGridView.DataSource = companiesGridData;
                CompanyGridView.DataBind();
            }
            finally { }
        }
        protected void CompanyGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CompanyGridView.PageIndex = e.NewPageIndex;
            BindCompanyGrid();
        }
        protected void SetDataRowId(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    companyGridModel company = e.Row.DataItem as companyGridModel;
                    if (company != null && company.CompanyId > 0)
                    {
                        e.Row.Attributes["data-row-id"] = company.CompanyId.ToString();
                        //e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(CompanyGridView, "Select$" + e.Row.RowIndex);
                        //e.Row.Attributes["style"] = "cursor: pointer;";
                        e.Row.CssClass = "clickable_row";
                        e.Row.Attributes["onclick"] = "companyRowClick(this);";
                    }
                }
            }
            finally { }
        }
        protected void CompanyGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in CompanyGridView.Rows)
                {
                    if (row.RowIndex == CompanyGridView.SelectedIndex)
                    {
                        row.CssClass = "selected_row";
                        selectedRowId = row.Attributes["data-row-id"];
                        editBtn.EnableViewState = !string.IsNullOrEmpty(selectedRowId) ? true : false;
                        SelectedRowIdHiddenField.Value = selectedRowId;
                    }else
                    {
                        row.CssClass = "";
                    }
                }
            }
            finally { }
        }
        protected void AddCompany_Click(object sender, EventArgs e)
        {
            companyId.Value = "0";
            companyName.Text = "";
            companyCode.Text = "";
            companyTypeDropDownList.SelectedIndex = 0;
            companyUen.Text = "";
            phNo.Text = "";
            address.Value = "";
            emailAddress.Text = "";
            postalCode.Text = "";
            popup_container.Visible = true;
        }
        protected void EditCompany_Click(object sender, EventArgs e)
        {
            try
            {
                selectedRowId = SelectedRowIdHiddenField.Value;
                if (!string.IsNullOrEmpty(selectedRowId))
                {
                    companyService = new CompanyService();
                    companyDetails selCompanyRowData = companyService.FetchCompanyDetails(selectedRowId);
                    companyId.Value = selCompanyRowData.CompanyId.ToString();
                    companyName.Text = selCompanyRowData.CompanyName;
                    companyCode.Text = selCompanyRowData.CompanyCode;
                    companyTypeDropDownList.SelectedIndex = selCompanyRowData.CompanyTypeId;
                    companyUen.Text = selCompanyRowData.CompanyUen;
                    phNo.Text = selCompanyRowData.PhoneNo;
                    address.Value = selCompanyRowData.Address;
                    emailAddress.Text = selCompanyRowData.EmailAddress;
                    postalCode.Text = selCompanyRowData.PostalCode;
                    popup_container.Visible = true;
                    SelectedRowIdHiddenField.Value = "";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please Select the Record to Edit.');", true);
                }
            }
            catch (Exception ex)
            {
                this.logFileService.LogError(0, "COMPANY MASTER", "CompanyMaster.aspx.cs", ex, "");
            }
        }
        protected void DeleteCompany_Click(object sender, EventArgs e)
        {
            selectedRowId = SelectedRowIdHiddenField.Value;
            if (!string.IsNullOrEmpty(selectedRowId))
            {
                popup_confirm_container.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please Select the Record to Delete.');", true);
            }
        }
        protected void ConfirmDeleteCompany_Click(object sender, EventArgs e)
        {
            long flag;
            try
            {
                selectedRowId = SelectedRowIdHiddenField.Value;
                companyService = new CompanyService();
                flag = companyService.DeleteCompany(selectedRowId);
                if (flag > 0)
                {
                    BindCompanyGrid();
                    popup_confirm_container.Visible = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Problem In Application.Please Contact Admin');", true);
                }
            }
            catch (Exception ex)
            {
                this.logFileService.LogError(0, "COMPANY MASTER", "CompanyMaster.aspx.cs", ex, "");
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //popup_container.Style["display"] = "none";
            //string script = @"<script type='text/javascript'>document.getElementById('popup_container').style.display = 'none';</script>";
            popup_container.Visible = false;
            popup_confirm_container.Visible = false;
        }
    }
}