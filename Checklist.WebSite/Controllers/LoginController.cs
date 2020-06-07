using Checklist.WebSite.Services;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebSite.Models;

namespace Checklist.WebSite.Controllers
{
    [AllowAnonymous]
    [OutputCache(Duration = 20)]
    public class LoginController : Controller
    {
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Login()
        {
            if (Session["USR"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            LoginModel model = new LoginModel();
            return View(model);
        }



        [HttpPost]
        public async Task<ActionResult> Logar(LoginModel login)
        {
            var token = await ApiServices.Login(login.Usuario, login.Senha);

            if(token != null)
            if (!string.IsNullOrEmpty(token.access_token))
            {             
                token.logado = true;
                token.Usuario_login = login.Usuario;
                token.Usuario = await ApiServices.BuscarUsuario(token);
               if(token.Usuario != null) 
                if (token.logado)
                {
                    ViewBag.Validacao = true;
                    Session["USR"] = token;
                    Session.Timeout = 10;
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ViewBag.Validacao = false;
                    if (!string.IsNullOrEmpty(token.Mensagem))
                        ViewBag.Message = token.Mensagem;
                    else ViewBag.Message = "Usuário ou Senha inválidos";
                   
                    return View("login", login);
                }
            }

            ViewBag.Message = "Usuário ou Senha inválidos";
            ViewBag.Validacao = false;
            return View("login", login);
        }

        public ActionResult LogOut()
        {
            Session["USR"] = null; //Sessão do usuário
            Session["ATU"] = null; // Sessão de atualização de senha

            return RedirectToAction("Login", "Login");

        }



        //public partial class RegisterView : ContentPage
        //{
        //    public RegisterView()
        //    {
        //        InitializeComponent();
        //    }

        //    private async void BtnSignUp_Clicked(object sender, EventArgs e)
        //    {
        //        if (!EntPassword.Text.Equals(EntConfirmPassword.Text))
        //        {
        //            await DisplayAlert("Senhas diferentes", "Por favor verifique se digitou corretamente.", "OK");
        //        }
        //        else
        //        {
        //            var resposta = await ApiService.RegistroUsuarios(EntNome.Text, EntSobrenome.Text, EntEndereco.Text, EntTelefone.Text,
        //                EntEmail.Text, EntDataNascimento.Text, EntPassword.Text, EntConfirmPassword.Text); ;
        //            if (resposta)
        //            {
        //                await DisplayAlert("Bem vindo", "Sua conta foi criada com sucesso", "OK");
        //                Application.Current.MainPage = new NavigationPage(new LoginView());
        //            }
        //            else
        //            {
        //                await DisplayAlert("Oops", "Alguma coisa deu errado. Tente novamente mais tarde", "OK");
        //                await DisplayAlert("AVISO", "Sua senha deve conter pelo menos 1 letra maiscula, 1 caracter especial e 1 digito", "OK");
        //            }
        //        }
        //    }
        //    private void TapBackArrow_Tapped(object sender, EventArgs e)
        //    {
        //        Navigation.PopModalAsync();
        //    }
        //}

    }
}