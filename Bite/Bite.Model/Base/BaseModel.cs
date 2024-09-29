#region Manutenção
/*
PROGRAMADOR: CLSILVA
DATA: 26/09/2024
AÇÃO: Implementação inicial
*/
#endregion

namespace Bite.Model
{
    public abstract class BaseModel
    {
        public int Id { get; set; } // Propriedade comum para identificação

        // Método para validar o modelo
        public virtual bool Validate()
        {
            // Implementar validações comuns aqui
            // Retornar true se válido, false caso contrário
            return Id > 0; // Exemplo de validação simples
        }

        // Método para clonar o modelo (opcional)
        public virtual BaseModel Clone()
        {
            return (BaseModel)this.MemberwiseClone(); // Cria uma cópia superficial do objeto
        }

        // Método para converter o modelo para string (opcional)
        public override string ToString()
        {
            return $"Id: {Id}"; // Retornar representação em string do modelo
        }

        // Métodos para comparações
        public override bool Equals(object obj)
        {
            if (obj is BaseModel other)
            {
                return Id == other.Id; // Comparar pelo Id
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode(); // Retornar o hash code baseado no Id
        }
    }
}