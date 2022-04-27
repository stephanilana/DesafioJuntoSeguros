namespace DesafioJuntoSeguros.Application.DTOs
{
    public class UpdateUsuarioDto
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string SenhaAtual { get; set; }
        public string SenhaNova { get; set; }
    }
}
