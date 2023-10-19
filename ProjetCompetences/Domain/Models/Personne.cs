using System.Text.Json;

namespace ProjetCompetences.Domain.Models
{
    public class Personne
    {
        public Guid Id { get; set; }

        public string Nom { get; set; }
        public string Prenom { get; set; }

        public Guid? Id_Equipe { get; set; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
