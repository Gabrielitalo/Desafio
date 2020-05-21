using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desafio.Classes
{
 // Essa é classo que faz a conexão com banco de dados, então se o banco mudar, basta alterar aqui e tudo esta certo.
  public class ConDB
  {
    public string connectionString = "Data Source=LetsUse.mssql.somee.com;Initial Catalog=LetsUse;Persist Security Info=True;User ID=Nostriker_SQLLogin_1;Password=tneo479yao;";
  }
}