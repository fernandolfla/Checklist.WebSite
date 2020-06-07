using Checklist.WebSite.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using WebSite.Models;

namespace Checklist.WebSite.Services
{
    public static class ApiServices
    {
        public static HttpResponseMessage ResponseMessage = new HttpResponseMessage();

        public static async Task<Token> Login(string usuario, string senha)
        {
        var loginModel = new LoginModel()
            {
                Usuario = usuario,
                Senha = senha,
            };
            //criado uma instancia de um httpclient
            var httpClient = new HttpClient();
            //utilizado o nuget package para Newton Soft para fazer a conversao do formato JSON para C#
            var json = JsonConvert.SerializeObject(loginModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //Criando o método post que sera responsável por nosso registro de usuários na API
            HttpResponseMessage resposta = null;
            try
            {
               resposta = await httpClient.PostAsync(AppSettings.ApiUrl + "api/login/logar", content);
            }
            catch(Exception ex)
            {
                ResponseMessage.StatusCode = HttpStatusCode.InternalServerError;
                ResponseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
            }            
            if (!resposta.IsSuccessStatusCode) 
                return null;
            var jsonResult = await resposta.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Token>(jsonResult); // VERIFICAR COMO RECEBER 2 OBJETOS DISTINTOS
            return result;
        }

        public static async Task<Usuario> BuscarUsuario(Token token)
        {
            if (token == null)
                if (!token.logado)
                    return null;

            //criado uma instancia de um httpclient
            var httpClient = new HttpClient();
            // Faz a validação via token JWT
            httpClient.DefaultRequestHeaders.Authorization =    new AuthenticationHeaderValue("Bearer", token.access_token);
            //utilizado o nuget package para Newton Soft para fazer a conversao do formato JSON para C#
            var json = JsonConvert.SerializeObject("");    
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // Coloca o Usuário no Header
            content.Headers.Add("Usuario", token.Usuario_login);
            //content.Headers.Add("Authorization", "Bearer " + token.access_token);

            //Criando o método post que sera responsável por nosso registro de usuários na API
            HttpResponseMessage resposta = null;
            try
            {
                resposta = await httpClient.PostAsync(AppSettings.ApiUrl + "api/usuario/buscar", content);
            }
            catch (Exception ex)
            {
                ResponseMessage.StatusCode = HttpStatusCode.InternalServerError;
                ResponseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
            }

            if (!resposta.IsSuccessStatusCode)
                return null;

            var jsonResult = await resposta.Content.ReadAsStringAsync();
            Usuario result = JsonConvert.DeserializeObject<Usuario>(jsonResult);

            //Preferences.Set("accessToken", result.access_token);
            //Preferences.Set("userId", result.user_id);
            //Preferences.Set("userName", result.user_name);
            //Preferences.Set("userEmail", result.user_email);
            return result;

        }

        public static async Task<Resposta> AtualizarUsuario(Token token)
        {
            if (token == null)
                    return null;

            //criado uma instancia de um httpclient
            var httpClient = new HttpClient();
            // Faz a validação via token JWT
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            //utilizado o nuget package para Newton Soft para fazer a conversao do formato JSON para C#
            var json = JsonConvert.SerializeObject(token.Usuario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // Coloca o Usuário no Header
            content.Headers.Add("Usuario", token.Usuario.Id.ToString());
            //content.Headers.Add("Authorization", "Bearer " + token.access_token);

            //Criando o método post que sera responsável por nosso registro de usuários na API
            HttpResponseMessage resposta = null;
            try
            {
                resposta = await httpClient.PostAsync(AppSettings.ApiUrl + "api/usuario/atualizar", content);
            }
            catch (Exception ex)
            {
                ResponseMessage.StatusCode = HttpStatusCode.InternalServerError;
                ResponseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
            }

            if (!resposta.IsSuccessStatusCode)
                return null;

            var jsonResult = await resposta.Content.ReadAsStringAsync();
            Resposta result = JsonConvert.DeserializeObject<Resposta>(jsonResult);

            //Preferences.Set("accessToken", result.access_token);
            //Preferences.Set("userId", result.user_id);
            //Preferences.Set("userName", result.user_name);
            //Preferences.Set("userEmail", result.user_email);
            return result;

        }
        



    }
}