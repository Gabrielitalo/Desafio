using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Desafio.Classes;

namespace Desafio.Funcoes
{
  public class FuncoesGlobais
  {

    // Convert varchar para numeric para o banco de dados
    public static string ConvertVarcharParaNumeric(string valor)
    {
      valor = valor.Replace(",", ".");

      return valor;
    }

    public static DataSet RetornaModelos(string Pk = "", string FkMarca = "", string texto = "")
    {
      SqlCommand cmd;
      SqlDataAdapter da;
      DataSet ds;
      ConDB conDB = new ConDB();

      SqlConnection sqlconn = new SqlConnection(conDB.connectionString);
      string comando = "";
      if (Pk == "" && FkMarca == "" && texto == "") // Grid
      {
        comando = @" Select M.*, Ma.Marca, 
                          (M.VrCompra - M.VrVenda) VrVariacao 
                          From Modelo M
                          Left Join Marca Ma on (Ma.Pk = M.FkMarca)

                        ";
      }
      else if (FkMarca != "") // Auto Complete
      {
        comando = @" Select M.*, Ma.Marca, Ma.Pk PkMarca, 
                          (M.VrCompra - M.VrVenda) VrVariacao 
                          From Modelo M
                          Left Join Marca Ma on (Ma.Pk = M.FkMarca)
                          Where (Ma.Pk = @fkmarca) and
                          (M.Modelo like '%' + @texto + '%')
                        ";
      }
      else if (Pk != "" && FkMarca == "" && texto == "") // Modal
      {
        comando = @" Select M.*, Ma.Marca, Ma.Pk PkMarca, 
                          (M.VrCompra - M.VrVenda) VrVariacao 
                          From Modelo M
                          Left Join Marca Ma on (Ma.Pk = M.FkMarca)
                          Where (M.Pk = @pk)
                        ";

      }

      cmd = new SqlCommand(comando, sqlconn);
      cmd.CommandType = CommandType.Text;
      cmd.CommandTimeout = 0;
      cmd.Parameters.AddWithValue("@pk", Pk);
      cmd.Parameters.AddWithValue("@fkmarca", FkMarca);
      cmd.Parameters.AddWithValue("@texto", texto);


      da = new SqlDataAdapter(cmd);
      ds = new DataSet();
      da.Fill(ds);

      return ds;
    }


    public static string FLimitaIntervaloData(DateTime Data)
    {
      //Limita o Valor do intervalo de datas
      int Ano = Convert.ToInt32(Data.ToString("yyyy"));
      if (Ano < 1910)
      {
        Data = Convert.ToDateTime("01/01/1910");
      }
      if (Ano > 2077)
      {
        Data = Convert.ToDateTime("31/12/2077");
      }
      return Data.ToString();
    }

    public static object FDataStrNull(string Data)
    {
      object Ret;
      if (Data == "")
      {
        Ret = DBNull.Value;
      }
      else
      {
        try
        {
          Data = FLimitaIntervaloData(Convert.ToDateTime(Data));
          Ret = Convert.ToDateTime(Data).ToString("MM/dd/yyyy");
        }
        catch
        {
          Ret = DBNull.Value;
        }
      }
      return Ret;
    }

    //-------------------------------------------------------------------------------------------------------
    // Fim da classe
  }
}