using System.Text.Json;

namespace ProjetCompetences.API.REST.DTOs.Equipe
{
    public class MembreSansEquipe
    {
        public string Id { get; set; }

        public string Nom { get; set; }
        public string Prenom { get; set; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
