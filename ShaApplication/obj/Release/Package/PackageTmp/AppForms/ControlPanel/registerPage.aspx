<%@ Page Title="Sha-Register" Language="C#" MasterPageFile="~/Master/LoginLayout.Master" AutoEventWireup="true" CodeBehind="registerPage.aspx.cs" Inherits="ShaApplication.AppForms.ControlPanel.registerPage" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="login" runat="server">
    <div class="container register_padding">
        <div class="bg-light register_div">
            <h2 class="mb-4 register_head">Registration</h2>
            <div class="row center-align">
                <div class="col-lg-12 col-md-12 row center-align">
                    <div class="form-group row col-12 col-sm-6 col-md-6 col-lg-6">
                        <label for="company" class="col col-sm-6 col-md-6 col-lg-6">Company<span class="mandatory">*</span></label>
                        <div class="col col-sm-6 col-md-6 col-lg-6">
                            <asp:DropDownList runat="server" class="form-control" ID="companyDropDownList" required="required"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group row col-12 col-sm-6 col-md-6 col-lg-6">
                        <label for="passNo" class="col col-sm-6 col-md-6 col-lg-6">Passport No<span class="mandatory">*</span></label>
                        <div class="col col-sm-6 col-md-6 col-lg-6">
                            <asp:TextBox runat="server" title="Passport Number" autocomplete="off" class="form-control" ID="passPortNo" MaxLength="25" pattern="[A-Za-z0-9]+" required="required"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 col-md-12 row center-align">
                    <div class="form-group row col-12 col-sm-6 col-md-6 col-lg-6">
                        <label for="aName" class="col col-sm-6 col-md-6 col-lg-6">Admin Name<span class="mandatory">*</span></label>
                        <div class="col col-sm-6 col-md-6 col-lg-6">
                            <asp:TextBox runat="server" title="Admin Name" autocomplete="off" class="form-control" ID="adminName" MaxLength="250" pattern="[A-Za-z0-9\s]+" required="required"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row col-12 col-sm-6 col-md-6 col-lg-6">
                        <label for="icNo" class="col col-sm-6 col-md-6 col-lg-6">IC No<span class="mandatory">*</span></label>
                        <div class="col col-sm-6 col-md-6 col-lg-6">
                            <asp:TextBox runat="server" title="Ic Number" autocomplete="off" class="form-control" ID="icNo" MaxLength="25" pattern="[A-Za-z0-9]+" required="required"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 col-md-12 row center-align">
                    <div class="form-group row col-12 col-sm-6 col-md-6 col-lg-6">
                        <label for="phNo" class="col col-sm-6 col-md-6 col-lg-6">Phone No<span class="mandatory">*</span></label>
                        <div class="col col-sm-6 col-md-6 col-lg-6">
                            <asp:TextBox runat="server" title="Phone Number" autocomplete="off" class="form-control" ID="phNo" MaxLength="10" pattern="[0-9]+" required="required"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row col-12 col-sm-6 col-md-6 col-lg-6">
                        <label for="password" class="col col-sm-6 col-md-6 col-lg-6">Password<span class="mandatory">*</span></label>
                        <div class="col col-sm-6 col-md-6 col-lg-6">
                            <asp:TextBox runat="server" TextMode="Password" title="Password" autocomplete="off" class="form-control" ID="password" MaxLength="250" pattern="^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).+$" required="required"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 col-md-12 row center-align">
                    <div class="form-group row col-12 col-sm-6 col-md-6 col-lg-6">
                        <label for="address" class="col col-sm-6 col-md-6 col-lg-6">Address<span class="mandatory">*</span></label>
                        <div class="col col-sm-6 col-md-6 col-lg-6">
                            <textarea runat="server" title="Address" autocomplete="off" class="form-control" id="address" maxlength="250" pattern="[A-Za-z0-9]+" required="required"></textarea>
                        </div>
                    </div>
                    <div class="form-group row col-12 col-sm-6 col-md-6 col-lg-6">
                        <label for="cPassword" class="col col-sm-6 col-md-6 col-lg-6">Confirm Password<span class="mandatory">*</span></label>
                        <div class="col col-sm-6 col-md-6 col-lg-6">
                            <asp:TextBox runat="server" TextMode="Password" title="Confirm Password" autocomplete="off" class="form-control" ID="cPassword" MaxLength="250" required="required"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 col-md-12 row center-align">
                    <div class="form-group row col-12 col-sm-6 col-md-6 col-lg-6">
                        <label for="eMail" class="col col-sm-6 col-md-6 col-lg-6">Email Address<span class="mandatory">*</span></label>
                        <div class="col col-sm-6 col-md-6 col-lg-6">
                            <asp:TextBox runat="server" type="email" title="Email Address" pattern="^[a-z\d.]+@[a-z\d]+\.[a-z]+$" autocomplete="off" class="form-control" ID="emailAddress" MaxLength="150" required="required"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row col-12 col-sm-6 col-md-9 col-lg-6">
                        <label for="isAdmin" class="col col-sm-6 col-md-6 col-lg-6">Is Admin</label>
                        <div class="col col-sm-6 col-md-6 col-lg-6">
                            <asp:CheckBox runat="server" title="Is Admin" ID="isAdmin" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="text-align: right">
                <div class="col-lg-12 col-md-12 center-align register-div-btns">
                    <asp:Button runat="server" ID="register" Text="  Submit  " OnClick="SaveRegisterDetails" class="btn btn-primary px-3" />
                    <asp:Button runat="server" ID="clear" Text="  Clear  " OnClick="ClearRegisterDetails" class="btn btn-primary px-3" />
                </div>
                <div class="row error_msg_div center-align">
                    <asp:Label runat="server" ID="viewMessage" Visible="false" />
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $('#reg-btn').hide();
            $('#back-btn').show();
        });
    </script>
</asp:Content>



