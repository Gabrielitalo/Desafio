<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Desafio.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <title>Login</title>
  <%--Bootstrap--%>
  <link rel="stylesheet" href="~/Content/bootstrap.min.css" />

  <%--JavaScriptCustom--%>
  <script src='/Scripts/Customizado.js'></script>


    <%--Importando ícones--%>
  <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.0/css/all.css" />
</head>
<body>
  <form id="form1" runat="server">
    <asp:ScriptManager runat="server">
      <Scripts>

        <asp:ScriptReference Name="MsAjaxBundle" />
        <asp:ScriptReference Name="jquery" />
        <asp:ScriptReference Name="bootstrap" />
        <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
        <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
        <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
        <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
        <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
        <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
        <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
        <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
        <asp:ScriptReference Name="WebFormsBundle" />
      </Scripts>
    </asp:ScriptManager>
    <div class="d-flex justify-content-center">
      <div class="col-12">
        <div class="mt-4 ml-5 mr-5 flex-fill">
          <h1>Veículos <span class="badge badge-success">Manager</span></h1>
        </div>
        <div class="mt-4 ml-5 mr-5 flex-fill">
          <div class="form-group">
            <label for="txtUser">Nome do usuário:</label>
            <input type="text" class="form-control" style="width: 374px;" placeholder="Digite o nome do usuário..." id="txtUser" />
          </div>
          <div class="form-group">
            <label for="txtPass">Senha:</label>
            <input type="password" class="form-control" style="width: 374px;" placeholder="Digite sua senha..." id="txtPass" />
          </div>
          <button type="button" class="btn btn-primary" id="btnLogin" onclick="EfetuaLogin();">
            <span class="fas fa-check"></span>
            Login</button>
        </div>
      </div>
    </div>
  </form>
</body>
</html>
