using Checklist.WebSite.Models;
using Checklist.WebSite.Services;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebSite.Models;

namespace Checklist.WebSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session.Count <= 0)
                return RedirectToAction("Login", "Login");

            if (Session["USR"] != null)
                if (((Token)Session["USR"]).logado)
                {
                    IndexModel model = new IndexModel();
                    model.Token = ((Token)Session["USR"]);

                    if (Session["ATU"] != null)
                        if ((bool)Session["ATU"])
                        {
                            ViewBag.Message = "Atualizado com Sucesso";
                            ViewBag.Validacao = true;
                        }
                        else
                        {
                            ViewBag.Message = "Erro ao Atualizar";
                            ViewBag.Validacao = false;
                        }

                    //model.Usuario = await ApiServices.BuscarUsuario(model.Token);

                    ViewData["Usuario"] = model.Token.Usuario.Login;
                    ViewData["Token"] = model.Token.access_token;
                    ViewData["Nome"] = model.Token.Usuario.Nome;
                    ViewData["UsuarioId"] = model.Token.Usuario.Id;
                    ViewData["FilialId"] = model.Token.Usuario.FilialId;
                    ViewData["Filial"] = "TESTE"; //model.Usuario.Filial.Fantasia;
                    ViewData["Usuario.Nome"] = model.Token.Usuario.Nome;
                    ViewData["Usuario.usuario"] = model.Token.Usuario.Login;
                    ViewData["Usuario.Filial"] = "TESTE";//= model.Usuario.Filial.Razao;
                    ViewData["Usuario.Tipo"] = model.Token.Usuario.Tipo;
                    ViewData["Usuario.Tipo.Nome"] = "Administrador";


                    Session.Timeout = 10;
                    return View("~/Views/Home/Index.cshtml", model);
                }

            return RedirectToAction("Login", "Login");
        }

        public async Task<ActionResult> Atualizar(IndexModel model)
        {  
           var token = ((Token)Session["USR"]);

            if (Session["USR"] != null)
                if (((Token)Session["USR"]).logado)
                {
                    token.Usuario.Senha = model.Token.Usuario.Senha;
                    token.Usuario.Nome = model.Token.Usuario.Nome;
                    Resposta resposta = await ApiServices.AtualizarUsuario(token);
                    if (resposta != null)
                        if(resposta.Ok)
                    {
                            Session["ATU"] = true;
                        return RedirectToAction("Index", "Home");
                    }
                }
            else
                {
                    Session["ATU"] = false;
                }

           ViewBag.Message = "Erro ao atualizar, verifique os campos e tente novamente";
            return null;

        }

       
    }
}