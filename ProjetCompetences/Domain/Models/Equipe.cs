using System.Text.Json;

namespace ProjetCompetences.Domain.Models
{
    public class Equipe
    {
        public Guid Id { get; set; }

        public string Nom { get; set; }
        public string Description { get; set; }

        public List<Guid> Membres { get; set; }
        public Guid? Manager { get; set; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
