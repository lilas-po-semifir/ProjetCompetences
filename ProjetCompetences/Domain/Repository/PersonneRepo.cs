using ProjetCompetences.Domain.Models;

namespace ProjetCompetences.Domain.Repository
{
    public interface PersonneRepo
    {
        public Task<Personne> GetPersonne(Guid id);
        public Task<List<Personne>> GetAllPersonnes();
        public Task<Personne> AddPersonne(Personne personne);
        public Task<Personne> UpdatePersonne(Personne personne);
        public Task DeletePersonne(Guid id);
    }
}
