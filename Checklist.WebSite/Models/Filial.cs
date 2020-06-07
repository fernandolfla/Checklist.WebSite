namespace Checklist.WebSite.Models
{
    public class Filial
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public string Razao { get; set; }
        public string Fantasia { get; set; }
        public string Cnpj { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Estado { get; set; }
        public string Serial { get; set; }
        public string Email_1 { get; set; }
        public string Email_2 { get; set; }
        public int Email_enviar { get; set; }  //Enviar para 0 - Não enviar 1- PAra o primeiro 2 - para os 2 
        public int Ativacao { get; set; }


    }
}