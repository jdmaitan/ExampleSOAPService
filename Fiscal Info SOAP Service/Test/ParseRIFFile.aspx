<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParseRIFFile.aspx.cs" Inherits="FiscalInfoWebService.UI.ParseRIFFileTest" MaintainScrollPositionOnPostback="true" %>

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

            <legend><strong>GetFiscalInfoFromFile</strong></legend>

            <em>Parse the fiscal info of a taxpayer from a RIF file produced by SENIAT</em>

            <div class="inputBox">
                <label for="EncodedFileTextbox">Base64 encoded PDF RIF file</label>
                <asp:TextBox ID="EncodedFileTextbox" runat="server"></asp:TextBox>
            </div>

            <div style="text-align: center">
                <asp:FileUpload ID="RIFFileUpload" runat="server" accept=".pdf" />
                <asp:Button ID="ConvertButton" runat="server" Text="Encode to base64" Font-Bold="true" OnClick="ConvertButton_Click" />
            </div>

            <br />

            <div style="text-align: center">
                <asp:Button ID="SubmitButton" runat="server" Text="Get taxpayer info" Font-Bold="true" OnClick="SubmitButton_Click" />
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