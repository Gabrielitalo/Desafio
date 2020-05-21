<%@ Page Title="Anuncio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Anuncio.aspx.cs" Inherits="Desafio.Anuncio" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <%--JavaScript--%>
  <script src='/Scripts/Customizado.js'></script>

  <div class="container-fluid mt-4">
    <div class="col mt-2">
      <div class="row">
        <button type="button" id="btnCadastraModelos" class="btn btn-link" runat="server" onclick="LimparModalAnuncio();">
          <span class="fas fa-check"></span>
          Adicionar Anuncio
        </button>
      </div>
      <asp:GridView runat="server" ID="gridAnuncio" AutoGenerateColumns="False" CssClass="mt-1 table table-borderless table-sm" ShowHeader="false" BorderWidth="0">
        <Columns>
          <asp:TemplateField>
            <ItemTemplate>
              <div class="row view overlay">
                <div class="col-lg-4">
                  <div class="card mb-5 mb-lg-0">
                    <div class="card-body">
                      <img class="card-img-top" src="Imagens/demo.png">
                      <h5 class="card-title text-muted text-uppercase text-center"><%# Eval("Modelo")%></h5>
                      <h6 class="card-price text-center">R$ <%# Eval("VrVendaAnu")%></h6>
                      <hr>
                      <ul class="fa-ul">
                        <li><span class="fa-li"><i class="fas fa-certificate"></i></span>Cor: <%# Eval("Cor")%></li>
                        <li><span class="fa-li"><i class="fas fa-cog"></i></span>Combustível: <%# Eval("Combustivel")%></li>
                        <li><span class="fa-li"><i class="fas fa-inbox"></i></span>Ano: <%# Eval("Ano")%></li>
                        <li><span class="fa-li"><i class="fas fa-flag"></i></span>Marca: <%# Eval("NomeMarca")%></li>
                      </ul>
                      <div class="form-inline">

                        <div class="btn-group" role="group">
                          <button type="button" class="btn btn-link" onclick="AbrirModalAnuncio( <%# Eval("PkAnuncio")%>);">
                            <span class="fas fa-cogs"></span>
                            Gerenciar</button>
                          <button type="button" class="btn btn-link" onclick="ApagarGlobal( <%# Eval("PkAnuncio")%>, 'Anuncio', this);">
                            <span class="fas fa-trash"></span>
                            Remover</button>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>

      </asp:GridView>
    </div>

    <div class="modal" id="modalIncluirAnuncio" tabindex="-1" role="dialog">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Incluir Anúncio</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <asp:UpdatePanel ID="Up3" runat="server">
            <ContentTemplate>
              <div class="modal-body">
                <asp:UpdatePanel ID="Up1" runat="server" onclick="focar(this);">
                  <ContentTemplate>
                    <label for="txtMarcaModal">Marca:</label>
                    <asp:TextBox runat="server" ID="txtMarcaModal" CssClass="form-control form-control-sm" autocomplete="off" CausesValidation="True" onblur="autoCompleteGlobal('txtMarcaModal', 'hdPkMarca', 'resultMarcaModal', 'AutoCompleteMarca');"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hdPkMarca" />
                    <ul class="list-group autoCompleteCustom" id="resultMarcaModal"></ul>
                  </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel ID="Up2" runat="server" onclick="focar(this);">
                  <ContentTemplate>
                    <label for="txtVeiculosModal">Veículo:</label>
                    <asp:TextBox runat="server" ID="txtVeiculosModal" CssClass="form-control form-control-sm" autocomplete="off" CausesValidation="True" onblur="autoCompleteVeiculos('txtVeiculosModal', 'hdPkVeiculos', 'resultVeiculos', 'RetornaModelos');"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hdPkVeiculos" />
                    <ul class="list-group autoCompleteCustom" id="resultVeiculos"></ul>
                  </ContentTemplate>
                </asp:UpdatePanel>

                <div class="row mt-2">
                  <div class="col-6">
                    <p id="lbCorModal"></p>
                  </div>
                  <div class="col-6">
                    <p id="lbCombustivelModal"></p>
                  </div>
                </div>

                <div class="row mt-2">
                  <div class="col-6">
                    <p id="lbAnoModal"></p>
                  </div>
                  <div class="col-6">
                    <p id="lbVrCompralModal"></p>
                  </div>
                </div>

                <label for="txtValorVenda">Valor Venda R$:</label>
                <asp:TextBox runat="server" ID="txtValorVenda" CssClass="form-control form-control-sm" TextMode="Number" autocomplete="off" CausesValidation="True"></asp:TextBox>

                <label for="txtDataVenda">Data Venda:</label>
                <asp:TextBox runat="server" ID="txtDataVenda" CssClass="form-control form-control-sm" TextMode="Date"></asp:TextBox>

                <asp:CheckBox runat="server" ID="ckVendido" Text="Marcar como vendido" CssClass="mt-2" />

                <asp:HiddenField ID="hdPkAnuncio" runat="server" />

              </div>
              <%--Fim do body do modal--%>
            </ContentTemplate>
          </asp:UpdatePanel>
          <div class="modal-footer">
            <button type="button" class="btn btn-primary" runat="server" id="btnSalvaAnuncio" onserverclick="btnSalvaAnuncio_Click">Salvar</button>
          </div>
        </div>
      </div>
      <%--Fim modal--%>
    </div>
    <%--Fim do container--%>
  </div>
</asp:Content>
