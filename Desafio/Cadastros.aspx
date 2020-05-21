<%@ Page Title="Anuncio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cadastros.aspx.cs" Inherits="Desafio.Cadastros" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <%--JavaScript--%>
  <script src='/Scripts/Customizado.js'></script>

  <div class="container-fluid mt-4">
    <ul class="nav nav-tabs" id="myTab" role="tablist">
      <li class="nav-item" role="presentation">
        <a class="nav-link active" id="marcas-tab" data-toggle="tab" href="#marcas" role="tab">Marcas</a>
      </li>
      <li class="nav-item" role="presentation">
        <a class="nav-link" id="modelos-tab" data-toggle="tab" href="#modelos" role="tab">Modelos</a>
      </li>
    </ul>

    <div class="tab-content mt-3 col-12" id="tabsCad">
      <div class="tab-pane fade show active" id="marcas" role="tabpanel">
        <div class="row">
          <button type="submit" id="btnCadastraMarca" class="btn btn-primary" runat="server" onserverclick="btnCadastraMarca_Click">
            <span class="fas fa-check"></span>
            Adicionar Marca
          </button>
        </div>

        <div class="row mt-2">
          <asp:GridView runat="server" ID="gridMarcas" AutoGenerateColumns="False" CssClass="mt-1 table table-sm table-hover">
            <Columns>
              <asp:TemplateField HeaderText="#" HeaderStyle-CssClass="text-center">
                <ItemTemplate>
                  <div class="row">
                    <button id="btnDeletarMarca" class="btn btn-link" onclick="ApagarGlobal(<%# Eval("Pk")%>, 'Marca', this);"><span class="fas fa-trash"></span></button>
                  </div>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Nome da Marca" HeaderStyle-CssClass="text-center">
                <ItemTemplate>
                  <asp:TextBox ID="txtNomeTrabalhador" runat="server" CssClass="form-control form-control-sm" Text='<%# Eval("Marca")%>'
                    data-pk='<%# Eval("Pk")%>' AutoPostBack="false" onchange="AlteraValorGridMarca('AlteraMarca', this);"
                    ToolTip="Clique duas vezes para excluir o registro."></asp:TextBox>
                </ItemTemplate>
              </asp:TemplateField>
            </Columns>

          </asp:GridView>

        </div>
        <%--Label resultado alterado--%>
          <p id="lbAlterado" class="text-success"></p>
      </div>
      <div class="tab-pane fade" id="modelos" role="tabpanel">
        <div class="row">
          <button type="button" id="btnCadastraModelos" class="btn btn-primary" runat="server" onclick="AbreModal('modalModelos');">
            <span class="fas fa-check"></span>
            Adicionar Modelo
          </button>
        </div>

        <div class="row mt-2">
          <asp:GridView runat="server" ID="gridModelos" AutoGenerateColumns="False" CssClass="mt-1 table table-bordered" ShowHeader="true">
            <Columns>
              <asp:TemplateField ItemStyle-CssClass="col-1">
                <ItemTemplate>
                  <div class="row">
                    <button id="btnDeletarMarca" type="button" class="btn btn-link" onclick="ApagarGlobal(<%# Eval("Pk")%>, 'Modelo', this);"><span class="fas fa-trash"></span></button>
                    <button id="btnAtualizar" type="button" class="btn btn-link" onclick="AbrirModalModelos(<%# Eval("Pk")%>);"><span class="fas fa-search-plus"></span></button>
                  </div>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField ItemStyle-CssClass="col-auto" HeaderText="Marca">
                <ItemTemplate>
                  <p class="text-center"><%# Eval("Marca")%></p>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Modelo" ItemStyle-CssClass="col-auto text-truncate">
                <ItemTemplate>
                  <p class="text-center"><%# Eval("Modelo")%></p>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Combustivel" ItemStyle-CssClass="col-auto">
                <ItemTemplate>
                  <p class="text-center"><%# Eval("Combustivel")%></p>
                </ItemTemplate>
              </asp:TemplateField>


              <asp:TemplateField HeaderText="Ano" ItemStyle-CssClass="col-auto ">
                <ItemTemplate>
                  <p class="text-center"><%# Eval("Ano")%></p>
                </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Cor" ItemStyle-CssClass="col-auto ">
                <ItemTemplate>
                  <p class="text-center"><%# Eval("Cor")%></p>
                </ItemTemplate>
              </asp:TemplateField>
            </Columns>

          </asp:GridView>
        </div>
      </div>
    </div>

    <div class="modal" id="modalModelos" tabindex="-1" role="dialog">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Modelos</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <label for="txtModeloModal">Modelo:</label>
            <asp:TextBox runat="server" ID="txtModeloModal" CssClass="form-control form-control-sm"></asp:TextBox>
            <asp:UpdatePanel ID="Up1" runat="server" onclick="focar(this);">
              <ContentTemplate>
                <label for="txtMarcaModal">Marca:</label>
                <asp:TextBox runat="server" ID="txtMarcaModal" CssClass="form-control form-control-sm" onblur="autoCompleteGlobal('txtMarcaModal', 'hdPkMarca', 'resultMarcaModal', 'AutoCompleteMarca');"></asp:TextBox>
                <asp:HiddenField runat="server" ID="hdPkMarca" />
                <ul class="list-group autoCompleteCustom" id="resultMarcaModal"></ul>
              </ContentTemplate>
            </asp:UpdatePanel>
            <label for="txtCombustivelModal">Combustível:</label>
            <asp:TextBox runat="server" ID="txtCombustivelModal" CssClass="form-control form-control-sm"></asp:TextBox>

            <label for="txtAnoModal">Ano:</label>
            <asp:TextBox runat="server" ID="txtAnoModal" CssClass="form-control form-control-sm"></asp:TextBox>

            <label for="txtCorModal">Cor:</label>
            <asp:TextBox runat="server" ID="txtCorModal" CssClass="form-control form-control-sm"></asp:TextBox>


            <label for="txtVrCompraModal">Valor de Compra R$:</label>
            <asp:TextBox runat="server" ID="txtVrCompraModal" CssClass="form-control form-control-sm"></asp:TextBox>

            <label for="txtVrVendaModal">Valor de Venda R$:</label>
            <asp:TextBox runat="server" ID="txtVrVendaModal" CssClass="form-control form-control-sm"></asp:TextBox>

            <asp:HiddenField runat="server" ID="hdPkModal" />
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-primary" id="btnSalvaModelosModa" runat="server" onserverclick="btnSalvaModelosModal_Click">Salvar</button>
          </div>
        </div>
      </div>
    </div>

    <%--Fim do container--%>
  </div>
</asp:Content>
