using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Checklist.WebSite.Models
{
    public class Token
    {
      
            public string access_token { get; set; }         
            public bool logado { get; set; }
            public string Usuario_login { get; set; }
            public Filial Filial { get; set; }
            public Usuario Usuario { get; set; }
            public string Mensagem { get; set; }

    }
}