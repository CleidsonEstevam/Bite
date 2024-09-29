namespace Bite.Model
{
    public class Usuario : BaseModel
    {
        public int EmpresaId { get; set; } // Relacionamento com a empresa
        public string Nome { get; set; } // Nome do usuário
        public string Email { get; set; } // Email do usuário
        public string Senha { get; set; } // Senha do usuário
        public string Telefone { get; set; } // Telefone do usuário
        public string Role { get; set; } = "user"; // Papel (role), com valor padrão "user"
        public DateTime DataCadastro { get; set; } // Data de criação do usuário
        public DateTime? UltimoAcesso { get; set; } // Data do último acesso (pode ser nulo se o usuário ainda não acessou)
        public bool Status { get; set; } // Status do usuário (ativo/inativo)
    }

}
