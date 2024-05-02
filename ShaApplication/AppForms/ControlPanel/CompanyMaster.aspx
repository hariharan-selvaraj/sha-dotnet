<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainLayout.Master" AutoEventWireup="true" CodeBehind="CompanyMaster.aspx.cs" Inherits="ShaApplication.AppForms.ControlPanel.CompanyMaster" EnableViewState="true" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function companyRowClick(row) {
            let rows = document.querySelectorAll(".clickable_row");
            rows.forEach(function (row) {
                row.classList.remove("selected_row");
            });
            row.classList.add("selected_row");
            let rowId = row.getAttribute("data-row-id");
            document.getElementById('<%= SelectedRowIdHiddenField.ClientID %>').value = rowId;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="custom_container">
        <div class="custom_inner_container">
            <div class="pagename_header_Container">
                <h1 class="page_head">Company Master
                </h1>
            </div>
        </div>
        <div class="div_container" style="width: 100%;">
            <div class="grid_container">
                <div class="tbl_head_div">
                    <div class="action_btn_container col col-sm-12">
                        <div class="grid_btn_headrow row">
                            <button runat="server" class="actions_btn grid_haction_btn btn_add" onserverclick="AddCompany_Click">
                                <i class="fa fa-plus grid_haction_icon"></i>Add
                            </button>
                            <button runat="server" class="actions_btn grid_haction_btn btn_edit" id="editBtn" onserverclick="EditCompany_Click">
                                <i class="fa fa-edit grid_haction_icon"></i>Edit
                            </button>
                            <button runat="server" class="actions_btn grid_haction_btn btn_delete" onserverclick="DeleteCompany_Click">
                                <i class="fa fa-archive  grid_haction_icon"></i>Delete
                            </button>
                        </div>
                    </div>
                </div>
                <div class="grid_head">
                    <asp:GridView ID="CompanyGridView" CssClass="grid_view" runat="server" OnRowDataBound="SetDataRowId" OnSelectedIndexChanged="CompanyGridView_SelectedIndexChanged" AllowPaging="true" OnPageIndexChanging ="CompanyGridView_PageIndexChanging" PageSize="10" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="CompanyId" Visible="false" />
                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                            <asp:BoundField DataField="CompanyCode" HeaderText="Company Code" />
                            <asp:BoundField DataField="PhoneNo" HeaderText="Phone Number" />
                            <asp:BoundField DataField="EmailAddress" HeaderText="Email Address" />
                        </Columns>
                    </asp:GridView>
                    <asp:HiddenField ID="SelectedRowIdHiddenField" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <div runat="server" id="popup_container" class="popup_outer_container">
        <div class="popup_inner_container">
            <div class="popup_header">
                <p class="popup_header_title">Add Company</p>
                <asp:LinkButton ID="lnkClose" runat="server" OnClick="btnCancel_Click" CssClass="popup_header_close_icon">×</asp:LinkButton>
            </div>
            <div class="popup_body">
                <div class="div_container">
                    <div class="row">
                        <asp:HiddenField runat="server" ID="companyId" />
                        <div class="col-lg-12 col-md-12 row center-align" style="margin: 0px;">
                            <div class="form-group row col-12 col-sm-6 col-md-6 col-lg-6">
                                <label for="cName" class="col col-sm-6 col-md-6 col-lg-6">Company Name<span class="mandatory">*</span></label>
                                <div class="col col-sm-6 col-md-6 col-lg-6">
                                    <asp:TextBox runat="server" title="Company Name" autocomplete="off" class="form-control" ID="companyName" MaxLength="250" pattern="[A-Za-z0-9]+" required="required"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row col-12 col-sm-6 col-md-6 col-lg-6">
                                <label for="phNo" class="col col-sm-6 col-md-6 col-lg-6">Phone Number<span class="mandatory">*</span></label>
                                <div class="col col-sm-6 col-md-6 col-lg-6">
                                    <asp:TextBox runat="server" title="Phone Number" autocomplete="off" class="form-control" ID="phNo" MaxLength="10" pattern="[0-9]+" required="required"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12 col-md-12 row center-align" style="margin: 0px;">
                            <div class="form-group row col-12 col-sm-6 col-md-6 col-lg-6">
                                <label for="cCode" class="col col-sm-6 col-md-6 col-lg-6">Company Code<span class="mandatory">*</span></label>
                                <div class="col col-sm-6 col-md-6 col-lg-6">
                                    <asp:TextBox runat="server" title="Company Code" autocomplete="off" class="form-control" ID="companyCode" MaxLength="25" pattern="[A-Za-z]+" required="required"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row col-12 col-sm-6 col-md-6 col-lg-6">
                                <label for="address" class="col col-sm-6 col-md-6 col-lg-6">Address<span class="mandatory">*</span></label>
                                <div class="col col-sm-6 col-md-6 col-lg-6">
                                    <textarea runat="server" title="Address" autocomplete="off" class="form-control" id="address" maxlength="250" pattern="[A-Za-z0-9]+" required="required"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12 col-md-12 row center-align" style="margin: 0px;">
                            <div class="form-group row col-12 col-sm-6 col-md-6 col-lg-6">
                                <label for="cType" class="col col-sm-6 col-md-6 col-lg-6">Company Type<span class="mandatory">*</span></label>
                                <div class="col col-sm-6 col-md-6 col-lg-6">
                                    <asp:DropDownList runat="server" class="form-control" ID="companyTypeDropDownList" required="required"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row col-12 col-sm-6 col-md-6 col-lg-6">
                                <label for="eMail" class="col col-sm-6 col-md-6 col-lg-6">Email Address<span class="mandatory">*</span></label>
                                <div class="col col-sm-6 col-md-6 col-lg-6">
                                    <asp:TextBox runat="server" type="email" title="Email Address" pattern="^[a-z\d.]+@[a-z\d]+\.[a-z]+$" autocomplete="off" class="form-control" ID="emailAddress" MaxLength="150" required="required"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12 col-md-12 row center-align" style="margin: 0px;">
                            <div class="form-group row col-12 col-sm-6 col-md-6 col-lg-6">
                                <label for="cUen" class="col col-sm-6 col-md-6 col-lg-6">Company UEN</label>
                                <div class="col col-sm-6 col-md-6 col-lg-6">
                                    <asp:TextBox runat="server" title="Company UEN" autocomplete="off" class="form-control" ID="companyUen" MaxLength="250" pattern="[A-Za-z0-9\s]+" required="required"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row col-12 col-sm-6 col-md-6 col-lg-6">
                                <label for="pCode" class="col col-sm-6 col-md-6 col-lg-6">Postal Code<span class="mandatory">*</span></label>
                                <div class="col col-sm-6 col-md-6 col-lg-6">
                                    <asp:TextBox runat="server" title="Postal Code" autocomplete="off" class="form-control" ID="postalCode" MaxLength="25" pattern="[A-Za-z0-9]+" required="required"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="popup_footer">
                <asp:Button runat="server" ID="save" CssClass="popup-btn popup-btn-save" Text="Save" OnClick="SaveCompanyDetails" class="btn btn-primary px-4" />
                <asp:Button runat="server" ID="cancel" CssClass="popup-btn popup-btn-cancel" Text="Cancel" OnClick="btnCancel_Click" class="btn btn-primary px-4" UseSubmitBehavior="false" />
            </div>
        </div>
    </div>
    <div runat="server" id="popup_confirm_container" class="popup_outer_container">
        <div class="confirm_popup_inner_container">
            <p>Are You Sure You Want to Delete this Company ? </p>
            <div class="confirm_popup_action_container">
                <asp:Button runat="server" ID="OkBtn" CssClass="btn_ok" Text="Ok" OnClick="ConfirmDeleteCompany_Click" />
                <asp:Button runat="server" ID="CancelBtn" CssClass="btn_cancel" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
    </div>
</asp:Content>
