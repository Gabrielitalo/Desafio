using Desafio.Classes;
using Desafio.Funcoes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Desafio
{
  public partial class Anuncio : Page
  {
    ConDB conDB = new ConDB();

    protected void Page_Load(object sender, EventArgs e)
    {
      AtualizaGridAnuncio();
    }

    protected void AtualizaGridAnuncio()
    {
      SqlCommand cmd;
      SqlDataAdapter da;
      DataSet ds;

      SqlConnection sqlconn = new SqlConnection(conDB.connectionString);
      string comando = @" Select A.Pk PkAnuncio, A.DataCricao, A.DataVenda, M.*, Ma.Marca NomeMarca, Ma.Pk PkMarca, A.VrVenda VrVendaAnu, A.Vendido Vendido
                          From Anuncio A
                          Join Modelo M on (M.Pk = A.FkModelo)
                          Join Marca Ma on (Ma.Pk = M.FkMarca) 
                          
                        ";

      cmd = new SqlCommand(comando, sqlconn);
      cmd.CommandType = CommandType.Text;
      cmd.CommandTimeout = 0;

      da = new SqlDataAdapter(cmd);
      ds = new DataSet();
      da.Fill(ds);

      gridAnuncio.DataSource = ds;
      gridAnuncio.DataBind();
    }

    protected void btnSalvaAnuncio_Click(object sender, EventArgs e)
    {
      SqlCommand cmd;
      SqlDataAdapter da;
      DataSet ds;

      SqlConnection sqlconn = new SqlConnection(conDB.connectionString);

      // Irá salvar o anuncio, se o anuncio existe irá dar update, se não existir insert.
      string comando = @" If (Coalesce(@PkAnuncio, 0) = 0)
	                          Begin
		                          -- Insert
		                          Insert Anuncio(FkModelo, DataCricao, DataVenda, VrVenda)
		                          Select @FkModelo, GetDate(), @DataVenda, @VrVenda

		                          If (Coalesce(Convert(Numeric(18,2), @VrVenda), 0.00) > 0.00)
			                          Begin
				                          Update Modelo
				                          Set VrVenda = Convert(Numeric(18,2), @VrVenda)
				                          Where (Pk = @FkModelo)
			                          End
	                          End
                          Else
	                          Begin
		                          -- Update
		                          Update Anuncio
		                          Set FkModelo = @FkModelo,
		                          DataVenda = @DataVenda,
		                          VrVenda = @VrVenda,
                              Vendido = @Vendido
		                          Where (Pk = @PkAnuncio)
	                          End
                        ";

      string vendido = "Não";
      if (ckVendido.Checked == true)
      {
        vendido = "Sim";
      }
      cmd = new SqlCommand(comando, sqlconn);
      cmd.CommandType = CommandType.Text;
      cmd.CommandTimeout = 0;
      cmd.Parameters.AddWithValue("@DataVenda", FuncoesGlobais.FDataStrNull(txtDataVenda.Text));
      cmd.Parameters.AddWithValue("@FkModelo", hdPkVeiculos.Value);
      cmd.Parameters.AddWithValue("@PkAnuncio", hdPkAnuncio.Value);
      cmd.Parameters.AddWithValue("@Vendido", vendido);
      cmd.Parameters.AddWithValue("@VrVenda", FuncoesGlobais.ConvertVarcharParaNumeric(txtValorVenda.Text));

      da = new SqlDataAdapter(cmd);
      ds = new DataSet();
      da.Fill(ds);

      AtualizaGridAnuncio();

      // Evitando requisição presa.
      Response.Redirect(Request.RawUrl);
    }
  }
}