using MongoDB.Driver;
using ProjetCompetences.Domain.Models;
using ProjetCompetences.Domain.Repository;
using ProjetCompetences.Infrastructure.Mongo;

namespace ProjetCompetences.Infrastructure.Adapters
{
    public class EquipeRepoAdaptMongo : EquipeRepo
    {
        private readonly IMongoCollection<Equipe> _equipe;

        public EquipeRepoAdaptMongo(EquipeRepoMongo equipe) 
        {
            _equipe = equipe.Equipes;
        }

        public async Task<Equipe> AddEquipe(Equipe equipe)
        {
            await _equipe.InsertOneAsync(equipe);
            return equipe;
        }

        public async Task DeleteEquipe(Guid id)
        {
            await _equipe.DeleteOneAsync(e => e.Id == id);
        }

        public async Task<List<Equipe>> GetAllEquipes()
        {
            return await _equipe.Find(e => true).ToListAsync();
        }

        public async Task<Equipe> GetEquipe(Guid id)
        {
            return await _equipe.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Equipe> UpdateEquipe(Equipe equipe)
        {
            await _equipe.ReplaceOneAsync(e => e.Id == equipe.Id, equipe);
            return equipe;
        }
    }
}
