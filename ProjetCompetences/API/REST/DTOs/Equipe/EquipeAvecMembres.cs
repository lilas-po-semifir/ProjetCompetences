using System.Text.Json;

namespace ProjetCompetences.API.REST.DTOs.Equipe
{
    public class EquipeAvecMembres
    {
        public string Id { get; set; }

        public string Nom { get; set; }
        public string Description { get; set; }

        public List<MembreSansEquipe> Membres { get; set; }
        public MembreSansEquipe? Manager { get; set; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
