<%@ Page Title="Relatorio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Relatorio.aspx.cs" Inherits="Desafio.Relatorio" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <%--JavaScript--%>
  <script src='/Scripts/Customizado.js'></script>

  <div class="container">
    <div class="row">
      <div class="col-3">
        <label for="txtDataInicial">Data Inicial:</label>
        <asp:TextBox runat="server" ID="txtDataInicial" CssClass="form-control form-control-sm" CausesValidation="True" TextMode="Date"></asp:TextBox>
      </div>
      <div class="col-3">
        <label for="txtDataFinal">Data Final:</label>
        <asp:TextBox runat="server" ID="txtDataFinal" CssClass="form-control form-control-sm" CausesValidation="True" TextMode="Date"></asp:TextBox>
      </div>
      <button type="button" class="mt-4 btn btn-link" runat="server" id="btnConsultaVeiculos" onserverclick="btnConsultaVeiculos_ServerClick">Consultar</button>
      <button type="button" class="mt-4 btn btn-link" runat="server" id="btnExportaPDF" onserverclick="btnExportaPDF_ServerClick">Exportar PDF</button>
    </div>

    <div class="row col mt-3">
      <asp:GridView runat="server" ID="gridRelatorio" AutoGenerateColumns="True" CssClass="mt-1 table table-borderless table-sm" ShowHeader="True" BorderWidth="1">
        <Columns>

        </Columns>
      </asp:GridView>
    </div>
  </div>
</asp:Content>
