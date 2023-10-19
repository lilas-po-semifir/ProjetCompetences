using ProjetCompetences.Domain.Models;

namespace ProjetCompetences.Domain.Repository
{
    public interface EquipeRepo
    {
        public Task<Equipe> GetEquipe(Guid id);
        public Task<List<Equipe>> GetAllEquipes();
        public Task<Equipe> AddEquipe(Equipe equipe);
        public Task<Equipe> UpdateEquipe(Equipe equipe);
        public Task DeleteEquipe(Guid id);
    }
}
