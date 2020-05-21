using Desafio.Classes;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Desafio
{
  public partial class Relatorio : Page
  {
    ConDB conDB = new ConDB();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    // Popula o grid de relatórios
    protected void AtualizaGridRelatorio()
    {
      SqlCommand cmd;
      SqlDataAdapter da;
      DataSet ds;

      SqlConnection sqlconn = new SqlConnection(conDB.connectionString);
      string comando = @" Select A.DataVenda, Concat(Ma.Marca, ' - ', M.Modelo) Veiculo, 
                          (A.VrVenda - M.VrCompra) Variacao
                          From Anuncio A
                          Join Modelo M on (M.Pk = A.FkModelo)
                          Join Marca Ma on (Ma.Pk = M.FkMarca) 
                          Where (Vendido = 'Sim') and
                          (DataVenda between @DataInicial and @DataFinal)
                          
                        ";

      cmd = new SqlCommand(comando, sqlconn);
      cmd.CommandType = CommandType.Text;
      cmd.CommandTimeout = 0;
      cmd.Parameters.AddWithValue("@DataInicial", txtDataInicial.Text);
      cmd.Parameters.AddWithValue("@DataFinal", txtDataFinal.Text);


      da = new SqlDataAdapter(cmd);
      ds = new DataSet();
      da.Fill(ds);

      gridRelatorio.DataSource = ds;
      gridRelatorio.DataBind();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
      //Serve pra evitar erro de runat server 
      //
    }

    // Exportar para PDF
    [Obsolete]
    private void ExportarPDF()
    {
      try
      {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=Veiculos.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gridRelatorio.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
        gridRelatorio.AllowPaging = true;
        gridRelatorio.DataBind();
      }
      catch
      {

      }
    }

    protected void btnConsultaVeiculos_ServerClick(object sender, EventArgs e)
    {
      AtualizaGridRelatorio();
    }

    protected void btnExportaPDF_ServerClick(object sender, EventArgs e)
    {
      try
      {
        ExportarPDF();
      }
      catch
      {

      }
    }
  }
}