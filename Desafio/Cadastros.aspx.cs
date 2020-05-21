using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Desafio.Classes;
using Desafio.Funcoes;

namespace Desafio
{
  public partial class Cadastros : Page
  {
    ConDB conDB = new ConDB();

    protected void Page_Load(object sender, EventArgs e)
    {
      // Popula Grid Marcas
      AtualizaGridMarcas();
      // Popula Grid Modelos
      AtualizaGridModelos();
      //// Popula dropdown com as marcas que existem no banco
      //AtualizaDropDownMarcas(sender, e);
    }

    protected void AtualizaGridMarcas()
    {
      SqlCommand cmd;
      SqlDataAdapter da;
      DataSet ds;

      SqlConnection sqlconn = new SqlConnection(conDB.connectionString);
      string comando = @" Select * From Marca
                        ";

      cmd = new SqlCommand(comando, sqlconn);
      cmd.CommandType = CommandType.Text;
      cmd.CommandTimeout = 0;

      da = new SqlDataAdapter(cmd);
      ds = new DataSet();
      da.Fill(ds);

      gridMarcas.DataSource = ds;
      gridMarcas.DataBind();
    }


    protected void AtualizaGridModelos()
    {
      DataSet ds;

      ds = FuncoesGlobais.RetornaModelos();

      gridModelos.DataSource = ds;
      gridModelos.DataBind();
    }

    protected void btnCadastraMarca_Click(object sender, EventArgs e)
    {
      SqlCommand cmd;
      SqlDataAdapter da;
      DataSet ds;

      SqlConnection sqlconn = new SqlConnection(conDB.connectionString);
      string comando = @" Insert Marca(Marca)
                          Select ''
                        ";

      cmd = new SqlCommand(comando, sqlconn);
      cmd.CommandType = CommandType.Text;
      cmd.CommandTimeout = 0;

      da = new SqlDataAdapter(cmd);
      ds = new DataSet();
      da.Fill(ds);

      // Evitando requisição presa.
      Response.Redirect(Request.RawUrl);
    }

    protected void AtualizaDropDownMarcas(object sender, EventArgs e)
    {

      SqlCommand cmd;
      SqlDataAdapter da;
      DataSet ds;

      SqlConnection sqlconn = new SqlConnection(conDB.connectionString);
      string comando = @" Declare @Res Table(Pk int, Marca varchar(150))

                          Insert @Res
                          Select 0, ''

                          Insert @Res
                          Select distinct Pk, Marca 
                          From Marca

                          Select *
                          From @Res";

      cmd = new SqlCommand(comando, sqlconn);
      cmd.CommandType = CommandType.Text;
      cmd.CommandTimeout = 0;


      da = new SqlDataAdapter(cmd);
      ds = new DataSet();
      da.Fill(ds);

      //comboMarcasModalModal.DataSource = ds;
      //comboMarcasModalModal.DataBind();
    }

    protected void btnCadastraModelo_Click(object sender, EventArgs e)
    {
      SqlCommand cmd;
      SqlDataAdapter da;
      DataSet ds;

      SqlConnection sqlconn = new SqlConnection(conDB.connectionString);
      string comando = @" Insert Modelo(Modelo)
                          Select ''
                        ";

      cmd = new SqlCommand(comando, sqlconn);
      cmd.CommandType = CommandType.Text;
      cmd.CommandTimeout = 0;

      da = new SqlDataAdapter(cmd);
      ds = new DataSet();
      da.Fill(ds);

      // Evitando requisição presa.
      Response.Redirect(Request.RawUrl);
    }

    protected void btnSalvaModelosModal_Click(object sender, EventArgs e)
    {
      SqlCommand cmd;
      SqlDataAdapter da;
      DataSet ds;
      string Pk = hdPkModal.Value;

      SqlConnection sqlconn = new SqlConnection(conDB.connectionString);
      string comando;
      
      if (Pk == "0" || Pk == "")
      {
        // Insert
        comando = @" Insert Modelo(Modelo, FkMarca, Ano, Cor, VrCompra, VrVenda, Combustivel)
                     Select @Modelo, @FkMarca, @Ano, @Cor, @VrCompra, @VrVenda, @Combustivel
                        ";
      }
      else
      {
        // Update
        comando = @"Update Modelo
                    Set Modelo = @Modelo, FkMarca = @FkMarca, Ano = @Ano, Cor = @Cor, VrCompra = @VrCompra, VrVenda = @VrVenda, Combustivel = @Combustivel
                    Where Pk = @Pk";

      }


      cmd = new SqlCommand(comando, sqlconn);
      cmd.CommandType = CommandType.Text;
      cmd.CommandTimeout = 0;
      cmd.Parameters.AddWithValue("@Pk", Pk);
      cmd.Parameters.AddWithValue("@Modelo", txtModeloModal.Text);
      cmd.Parameters.AddWithValue("@FkMarca", hdPkMarca.Value);
      cmd.Parameters.AddWithValue("@Ano", txtAnoModal.Text);
      cmd.Parameters.AddWithValue("@Cor", txtCorModal.Text);
      cmd.Parameters.AddWithValue("@Combustivel", txtCombustivelModal.Text);
      cmd.Parameters.AddWithValue("@VrCompra", FuncoesGlobais.ConvertVarcharParaNumeric(txtVrCompraModal.Text));
      cmd.Parameters.AddWithValue("@VrVenda", FuncoesGlobais.ConvertVarcharParaNumeric(txtVrVendaModal.Text));

      da = new SqlDataAdapter(cmd);
      ds = new DataSet();
      da.Fill(ds);

      // Evitando requisição presa.
      Response.Redirect(Request.RawUrl);
    }

    protected void comboMarcasModalModal_SelectedIndexChanged(object sender, EventArgs e)
    {
      //comboMarcasModalModal.SelectedValue = ;
    }
  }
}