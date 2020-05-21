var caminho = "/WebMetodos/AcessoBanco.asmx/";


// Faz a requisição do login que irá validar os dados informados.
function EfetuaLogin()
{
  let user = document.getElementById("txtUser");
  let pass = document.getElementById("txtPass");

  $.ajax({
    type: "POST",
    contentType: "application/json; charset=utf-8",
    url: caminho + "EfeturaLogin",
    data: "{'user':'" + user.value + "', 'pass': '" + pass.value + "'}",
    success: function (data)
    {
      if (data.d === "OK")
      {
        alert("Sucesso ao efetuar login, você será redirecionado ao sistema.");
        window.location.href = "Cadastros.aspx";
      }
      else
      {
        alert("Falha ao efetuar login.");
      }
    }
  });
}

// A medida que o usuário vai editando o grid das marcas o valor é alterado.
function AlteraValorGridMarca(modulo, el)
{
  let pk = el.getAttribute("data-pk");

  $.ajax({
    type: "POST",
    contentType: "application/json; charset=utf-8",
    url: caminho + modulo,
    data: "{'pk':'" + pk + "', 'valor': '" + el.value + "'}",
    success: function (data)
    {
      let label = document.getElementById("lbAlterado");

      label.innerText = "";
      label.style.display = 'block';
      label.innerText = "Alterado com sucesso.";

      // Faz com que a mensagem de alterado apareça e depois suma no tempo determinado em milisegundo
      setTimeout(function ()
      {
        label.style.display = 'none';
      }, 3000);
    }
  });
}


// Abri o modal de modelos para poder ser editado as informaçõs, é informado o Pk do modelo e 
// depois select para popular os campos.
function AbrirModalModelos(Pk)
{
  $.ajax({
    type: "POST",
    contentType: "application/json; charset=utf-8",
    url: caminho + "RetornaModelos",
    data: "{'texto':'', 'fkmarca':'', 'pk': '" + Pk + "'}",
    success: function (data)
    {
      var obj = JSON.parse(data.d);

      // Pegando objetos com seus atributos, pois pode ser que no futuro se queira alterar alguma proprienda
      let modelo = document.getElementById("MainContent_txtModeloModal");
      let combustivel = document.getElementById("MainContent_txtCombustivelModal");
      let ano = document.getElementById("MainContent_txtAnoModal");
      let cor = document.getElementById("MainContent_txtCorModal");
      let vrcompra = document.getElementById("MainContent_txtVrCompraModal");
      let vrvenda = document.getElementById("MainContent_txtVrVendaModal");
      let marca = document.getElementById("MainContent_txtMarcaModal");
      let hdPk = document.getElementById("MainContent_hdPkModal");
      let hdPkMarca = document.getElementById("MainContent_hdPkMarca");

      // Setando valores com base no objeto retornado pelo WebService.
      modelo.value = obj[0].Modelo;
      combustivel.value = obj[0].Combustivel;
      ano.value = obj[0].Ano;
      cor.value = obj[0].Cor;
      vrcompra.value = obj[0].VrCompra;
      vrvenda.value = obj[0].VrVenda;
      marca.value = obj[0].Marca;
      hdPk.value = obj[0].Pk;
      hdPkMarca.value = obj[0].PkMarca;

      AbreModal("modalModelos");
    }
  });

}

// Abre qualquer modal informando apenas o ID.
function AbreModal(modal)
{
  $('#' + modal).modal('toggle');
}

// Altera/Seta o atributo value do objeto.
function AlteraValue(el)
{
  var valor = el.value;
  var idElement = el.getAttribute("id");
  $('#' + idElement + '').attr('value', valor);
}


/*
 Esta função pode ser para qualquer autocomplete seguindo o padrão abaixo:
 campo: objeto de busca.
 hidden: hidden fiel que irá trazer o código Pk.
 listResult: elemento list que irá mostrar o autocomplete.
 modulo: Nome do Web Método que irá ser chamado.

Se o objetivo é retornar apenas duas informações, código e descrição esta função atende.
 */
function autoCompleteGlobal(campo, hidden, listResult, modulo)
{
  $.ajaxSetup({ cache: false });
  $('#MainContent_' + campo).keyup(function ()
  {
    $('#' + listResult).html('');
    $('#state').val('');
    var searchField = $('#MainContent_' + campo).val();
    var expression = new RegExp(searchField, "i");

    if (searchField.length <= 1)
    {
      return;
    }

    $.ajax({
      type: "POST",
      contentType: "application/json; charset=utf-8",
      url: caminho + modulo,
      data: "{'texto':'" + searchField + "'}",
      success: function (data)
      {
        $("#" + listResult).html('');

        var obj = JSON.parse(data.d);
        $.each(obj, function (key, value)
        {
          if (value.NomeMarca.search(expression) !== -1 || value.Pk.search(expression) !== -1)
          {
            $('#' + listResult).append('<li class="list-group-item link-class"> ' + value.NomeMarca + ' | <span class="text-muted">' + value.Pk + '</span> </li>');
          }
        });
      }
    });
  });

  // Pega evento click na list do autocomplete.
  $('#' + listResult).on('click', 'li', function ()
  {
    var click_text = $(this).text().split('|');
    $('#MainContent_' + campo).val($.trim(click_text[0]));
    $("#" + listResult).html('');

    $('#MainContent_' + hidden).val($.trim(click_text[1]));

  });
}

/*
 Esta função é usado no autocomplete do anúncio que tem uma particularidade, precisa ser informado
 a marca com filtro, só foi criado por causa da estrutura que tenho disponível no momento(SQL Server free),
 pois se fosse em estrutura própria todo o código seguiria orientado a banco de dados, inclusive mais tabelas seriam criadas.

 campo: objeto de busca.
 hidden: hidden fiel que irá trazer o código Pk.
 listResult: elemento list que irá mostrar o autocomplete.
 modulo: Nome do Web Método que irá ser chamado.
*/
function autoCompleteVeiculos(campo, hidden, listResult, modulo)
{
  $.ajaxSetup({ cache: false });
  $('#MainContent_' + campo).keyup(function ()
  {
    $('#' + listResult).html('');
    $('#state').val('');
    var searchField = $('#MainContent_' + campo).val();
    var expression = new RegExp(searchField, "i");

    if (searchField.length <= 1)
    {
      return;
    }

    // Pegando valor do código da marca para passar com parâmetro
    var FkMarca = document.getElementById("MainContent_hdPkMarca").value;

    $.ajax({
      type: "POST",
      contentType: "application/json; charset=utf-8",
      url: caminho + modulo,
      data: "{'texto':'" + searchField + "', 'fkmarca':'" + FkMarca + "', 'pk': ''}",
      success: function (data)
      {
        // Limpa o list.
        $("#" + listResult).html('');

        // Converte para objeto o json.
        var obj = JSON.parse(data.d);
        // Para cada item no objeto...
        $.each(obj, function (key, value)
        {
          if (value.Modelo.search(expression) !== -1 || value.Pk.search(expression) !== -1)
          {
            $('#' + listResult).append('<li class="list-group-item link-class"> ' + value.Modelo + ' | <span class="text-muted">' + value.Pk + '</span> </li>');
          }
        });
      }
    });
  });

  // Pega evento click na list.
  $('#' + listResult).on('click', 'li', function ()
  {
    var click_text = $(this).text().split('|');
    $('#MainContent_' + campo).val($.trim(click_text[0]));
    $("#" + listResult).html('');

    $('#MainContent_' + hidden).val($.trim(click_text[1]));
    completarDadosAnuncio(click_text[1]);
  });
}


// Força o foco na div do autocomplete pra evitar problemas do autocomplete em alguns navegadores.
function focar(el)
{
  var x = el.querySelectorAll("input");
  el.removeAttribute("OnClick");
  //x[0].preventDefault();
  x[0].onblur();
}

/*
Tem por função efeutar o delete em um item único em qualquer tabela, informando apenas o código.
*/
function ApagarGlobal(Pk, Tabela, El)
{
  $.ajax({
    type: "POST",
    contentType: "application/json; charset=utf-8",
    url: caminho + "ApagarGlobal",
    data: "{'pk':'" + Pk + "', 'tabela': '" + Tabela + "'}",
    success: function (data)
    {
      // Remove elemento do Grid sem postback
      El.parentNode.parentNode.parentNode.remove();
    }
  });
}

// Tem por função popular os dados do anuncio quando o autocomplete do veículo é chamado.
function completarDadosAnuncio(Pk)
{
  $.ajax({
    type: "POST",
    contentType: "application/json; charset=utf-8",
    url: caminho + "RetornaModelos",
    data: "{'texto':'', 'fkmarca':'', 'pk': '" + Pk + "'}",
    success: function (data)
    {

      var obj = JSON.parse(data.d);

      document.getElementById("lbCorModal").innerText = "Cor: " + obj[0].Cor;
      document.getElementById("lbCombustivelModal").innerText = "Combustível: " + obj[0].Combustivel;
      document.getElementById("lbAnoModal").innerText = "Ano: " + obj[0].Ano;
      document.getElementById("lbVrCompralModal").innerText = "Compra R$: " + obj[0].VrCompra;


    }
  });
}

// Tem por função abrir o modal do anúncio populando com base na requisição.
function AbrirModalAnuncio(Pk)
{
  $.ajax({
    type: "POST",
    contentType: "application/json; charset=utf-8",
    url: caminho + "RetornaAnuncio",
    data: "{'pk':'" + Pk + "'}",
    success: function (data)
    {
      var obj = JSON.parse(data.d);


      if (obj[0].Vendido === "Sim")
      {
        document.getElementById("MainContent_ckVendido").checked = true
      }
      else
      {
        document.getElementById("MainContent_ckVendido").checked = false;
      }

      document.getElementById("MainContent_txtVeiculosModal").value = obj[0].Modelo;
      document.getElementById("MainContent_hdPkVeiculos").value = obj[0].Pk;
      document.getElementById("MainContent_txtValorVenda").value = obj[0].VrVenda;
      document.getElementById("MainContent_txtMarcaModal").value = obj[0].Marca;
      document.getElementById("MainContent_hdPkMarca").value = obj[0].PkMarca;
      document.getElementById("MainContent_hdPkAnuncio").value = obj[0].PkAnuncio;
      document.getElementById("lbCorModal").innerText = "Cor: " + obj[0].Cor;
      document.getElementById("lbCombustivelModal").innerText = "Combustível: " + obj[0].Combustivel;
      document.getElementById("lbAnoModal").innerText = "Ano: " + obj[0].Ano;
      document.getElementById("lbVrCompralModal").innerText = "Compra R$: " + obj[0].VrCompra;

      AbreModal("modalIncluirAnuncio");
    }
  });
}

// Limpar dados do modal do anuncio.
function LimparModalAnuncio()
{
  document.getElementById("MainContent_txtVeiculosModal").value = "";
  document.getElementById("MainContent_hdPkVeiculos").value = "";
  document.getElementById("MainContent_txtValorVenda").value = "";
  document.getElementById("MainContent_txtMarcaModal").value = "";
  document.getElementById("MainContent_hdPkMarca").value = "";
  document.getElementById("MainContent_hdPkAnuncio").value = "";

  // Postback apenas no modal do anuncio para remover os labels criado, chamado na inclusão de um novo anucio.
  __doPostBack('ctl00$MainContent$Up3');
  AbreModal('modalIncluirAnuncio');
}

// Obtém os dados do cookie pelo nome
function getCookie(cname)
{
  var name = cname + "=";
  var decodedCookie = decodeURIComponent(document.cookie);
  var ca = decodedCookie.split(';');
  for (var i = 0; i < ca.length; i++)
  {
    var c = ca[i];
    while (c.charAt(0) === ' ')
    {
      c = c.substring(1);
    }
    if (c.indexOf(name) === 0)
    {
      return c.substring(name.length, c.length);
    }
  }
  return "";
}

// Remove o cookie.
function removeItem(sKey, sPath, sDomain)
{
  document.cookie = encodeURIComponent(sKey) +
    "=; expires=Thu, 01 Jan 1970 00:00:00 GMT" +
    (sDomain ? "; domain=" + sDomain : "") +
    (sPath ? "; path=" + sPath : "");
}
