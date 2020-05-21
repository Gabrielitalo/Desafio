using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using Desafio.Classes;
using Desafio.Funcoes;

namespace Desafio.WebMetodos
{
  /// <summary>
  /// Descrição resumida de AcessoBanco
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  [System.ComponentModel.ToolboxItem(false)]
  // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
  [System.Web.Script.Services.ScriptService]
  public class AcessoBanco : System.Web.Services.WebService
  {
    ConDB conDB = new ConDB();
    DataSet ds;


    /*
     Tem por função validar os dados do login de forma simples.
     */
    [WebMethod]
    public string EfeturaLogin(string user, string pass)
    {
      string ret = "";
      SqlCommand cmd;
      SqlDataAdapter da;

      SqlConnection sqlconn = new SqlConnection(conDB.connectionString);
      string comando = @"Declare @Pk int = 0

                        Select @Pk = Pk
                        From Usuario
                        Where (Nome = @user) and
                        (Senha = @pass)

                        If (@Pk <> 0)
                          Begin
                            Select 'OK' Resultado
                          End
                        Else
                          Begin
                            Select 'FAIL' Resultado
                          End
                        ";

      cmd = new SqlCommand(comando, sqlconn);
      cmd.CommandType = CommandType.Text;
      cmd.CommandTimeout = 0;
      cmd.Parameters.AddWithValue("@user", user);
      cmd.Parameters.AddWithValue("@pass", pass);

      da = new SqlDataAdapter(cmd);
      ds = new DataSet();
      da.Fill(ds);

      ret = ds.Tables[0].Rows[0]["Resultado"].ToString();

      return ret;
    }

    // Faz o update na tabela marca recebendo o Pk e Valor para setar.
    [WebMethod]
    public string AlteraMarca(string pk, string valor)
    {
      SqlCommand cmd;
      SqlDataAdapter da;

      SqlConnection sqlconn = new SqlConnection(conDB.connectionString);
      string comando = @" Update Marca
                          Set Marca = @marca
                          Where Pk = @pk
                        ";

      cmd = new SqlCommand(comando, sqlconn);
      cmd.CommandType = CommandType.Text;
      cmd.CommandTimeout = 0;
      cmd.Parameters.AddWithValue("@marca", valor);
      cmd.Parameters.AddWithValue("@pk", pk);

      da = new SqlDataAdapter(cmd);
      ds = new DataSet();
      da.Fill(ds);

      return "Alterado";
    }

    // Tem diversas funções, como autocompletes, popular modais...
    [WebMethod]
    public string RetornaModelos(string pk = "", string fkmarca = "", string texto = "")
    {
      List<Modelos> retorno = new List<Modelos>();

      ds = FuncoesGlobais.RetornaModelos(pk, fkmarca, texto);

      int contRet = ds.Tables[0].Rows.Count;
      int i;

      if (contRet > 0)
      {
        for (i = 0; i < contRet; i++)
        {
          retorno.Add(
            new Modelos
            {
              Pk = ds.Tables[0].Rows[i]["Pk"].ToString(),
              Marca = ds.Tables[0].Rows[i]["Marca"].ToString(),
              Modelo = ds.Tables[0].Rows[i]["Modelo"].ToString(),
              Cor = ds.Tables[0].Rows[i]["Cor"].ToString(),
              Ano = ds.Tables[0].Rows[i]["Ano"].ToString(),
              Combustivel = ds.Tables[0].Rows[i]["Combustivel"].ToString(),
              VrCompra = ds.Tables[0].Rows[i]["VrCompra"].ToString(),
              VrVenda = ds.Tables[0].Rows[i]["VrVenda"].ToString(),
              PkMarca = ds.Tables[0].Rows[i]["PkMarca"].ToString()

            });
        }
      }

      return new JavaScriptSerializer().Serialize(retorno);
    }


    [WebMethod]
    public string AutoCompleteMarca(string texto)
    {
      SqlCommand cmd;
      SqlDataAdapter da;

      List<Marca> retorno = new List<Marca>();

      SqlConnection sqlconn = new SqlConnection(conDB.connectionString);
      string comando = @" Select Pk, Marca
                          From Marca
                          Where (Marca like '%' + @texto + '%')";

      cmd = new SqlCommand(comando, sqlconn);
      cmd.CommandType = CommandType.Text;
      cmd.CommandTimeout = 0;
      cmd.Parameters.AddWithValue("@texto", texto);

      da = new SqlDataAdapter(cmd);
      ds = new DataSet();
      da.Fill(ds);

      int contRet = ds.Tables[0].Rows.Count;
      int i;

      if (contRet > 0)
      {
        for (i = 0; i < contRet; i++)
        {
          retorno.Add(
            new Marca
            {
              Pk = ds.Tables[0].Rows[i]["Pk"].ToString(),
              NomeMarca = ds.Tables[0].Rows[i]["Marca"].ToString()
            });
        }
      }

      return new JavaScriptSerializer().Serialize(retorno);
    }

    // Faz o delete de um item unico em qualquer tabela passando o pk como parametro.
    [WebMethod]
    public void ApagarGlobal(string pk, string tabela)
    {
      SqlCommand cmd;
      SqlDataAdapter da;

      SqlConnection sqlconn = new SqlConnection(conDB.connectionString);
      string comando = @" Delete " + tabela + @" Where Pk = @pk";

      cmd = new SqlCommand(comando, sqlconn);
      cmd.CommandType = CommandType.Text;
      cmd.CommandTimeout = 0;
      cmd.Parameters.AddWithValue("@pk", pk);

      da = new SqlDataAdapter(cmd);
      ds = new DataSet();
      da.Fill(ds);

    }

    // Retorna o anuncio passando o Pk em forma de json.
    [WebMethod]
    public string RetornaAnuncio(string pk)
    {
      List<AnuncioEsq> retorno = new List<AnuncioEsq>();
      

      SqlCommand cmd;
      SqlDataAdapter da;
      SqlConnection sqlconn = new SqlConnection(conDB.connectionString);
      string comando = @" Select M.*, A.DataCricao, A.DataVenda, A.Pk PkAnuncio, Ma.Marca, Ma.Pk PkMarca, A.Vendido
                          From Anuncio A
                          Join Modelo M on (M.Pk = A.FkModelo)
                          Join Marca Ma on (Ma.Pk = M.FkMarca)
                          Where (A.Pk = @Pk)";

      cmd = new SqlCommand(comando, sqlconn);
      cmd.CommandType = CommandType.Text;
      cmd.CommandTimeout = 0;
      cmd.Parameters.AddWithValue("@Pk", pk);

      da = new SqlDataAdapter(cmd);
      ds = new DataSet();
      da.Fill(ds);

      int contRet = ds.Tables[0].Rows.Count;
      int i;

      if (contRet > 0)
      {
        for (i = 0; i < contRet; i++)
        {
          retorno.Add(
            new AnuncioEsq
            {
              PkAnuncio = ds.Tables[0].Rows[i]["PkAnuncio"].ToString(),
              DataCriacao = ds.Tables[0].Rows[i]["DataCricao"].ToString(),
              DataVenda = ds.Tables[0].Rows[i]["DataVenda"].ToString(),
              Marca = ds.Tables[0].Rows[i]["Marca"].ToString(),
              Modelo = ds.Tables[0].Rows[i]["Modelo"].ToString(),
              Cor = ds.Tables[0].Rows[i]["Cor"].ToString(),
              Ano = ds.Tables[0].Rows[i]["Ano"].ToString(),
              Combustivel = ds.Tables[0].Rows[i]["Combustivel"].ToString(),
              VrCompra = ds.Tables[0].Rows[i]["VrCompra"].ToString(),
              VrVenda = ds.Tables[0].Rows[i]["VrVenda"].ToString(),
              PkMarca = ds.Tables[0].Rows[i]["PkMarca"].ToString(),
              Pk = ds.Tables[0].Rows[i]["Pk"].ToString(), // PkModelo
              Vendido = ds.Tables[0].Rows[i]["Vendido"].ToString() // Vendido

            });
        }
      }

      return new JavaScriptSerializer().Serialize(retorno);
    }


    //Fim da classe
  }
}
