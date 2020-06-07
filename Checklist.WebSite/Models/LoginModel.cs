using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Models
{
    public class LoginModel
    {
       
        public string Usuario { get; set; }
      
        public string Senha { get; set; }

    }
}