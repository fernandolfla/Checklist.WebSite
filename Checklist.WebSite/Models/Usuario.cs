using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Checklist.WebSite.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public int Tipo { get; set; }
        public int? FilialId { get; set; }
        public Filial Filial { get; set; }
        public string acesskey { get; set; }
    }
}