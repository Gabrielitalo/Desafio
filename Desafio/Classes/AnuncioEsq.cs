using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desafio.Classes
{
  public class AnuncioEsq : Modelos
  {
    public string PkAnuncio { get; set; }
    public string DataCriacao { get; set; }
    public string DataVenda { get; set; }
    public string ValorVenda { get; set; }
    public string Vendido { get; set; }
  }
}