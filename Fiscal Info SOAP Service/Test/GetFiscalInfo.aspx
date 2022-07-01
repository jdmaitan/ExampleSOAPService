<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetFiscalInfo.aspx.cs" Inherits="FiscalInfoWebService.UI.GetFiscalInfoTest" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <link rel="stylesheet" href="../css/styles.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Fiscal Information Service</title>
</head>

<body>
    <div class="navbar">
        <a href="../index.html">Home</a>
        <div class="dropdown">
            <button class="activeDropButton">Test▼</button>
            <div class="dropdown-content">
                <a href="GetFiscalInfo.aspx">GetFiscalInfo</a>
                <a href="PostFiscalInfo.aspx">PostFiscalInfo</a>
                <a href="ParseRIFFile.aspx">ParseRIFFile</a>
                <a href="ValidateRIF.aspx">ValidateRIF</a>
            </div>
        </div>
        <a href="../FiscalInfoService.svc?wsdl" target="_blank">WSDL</a>
    </div>

    <form id="form1" style="margin-top: 10px" runat="server">

        <fieldset>

            <legend><strong>GetRif</strong></legend>

            <em>Verification of existence of taxpayer in database</em>

            <div class="inputBox">
                <label for="RIFTextBox">RIF</label>
                <asp:TextBox ID="RIFTextBox" runat="server"></asp:TextBox>
            </div>

            <div style="text-align: center">
                <asp:Button ID="SubmitButton" runat="server" Text="Verify" Font-Bold="true" OnClick="SubmitButton_Click" />
            </div>

            <br />

            <div style="text-align: center">
                <div>
                    <asp:Label ID="ResponseLabel" runat="server"></asp:Label>
                </div>
            </div>

        </fieldset>

    </form>

</body>

</html>