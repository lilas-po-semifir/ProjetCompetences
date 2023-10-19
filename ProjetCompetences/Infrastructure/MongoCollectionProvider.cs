using MongoDB.Driver;
using ProjetCompetences.Domain.Models;

namespace ProjetCompetences.Infrastructure
{
    public class MongoCollectionProvider
    {
        IMongoDatabase Database { get; }

        public MongoCollectionProvider(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettingsMongo:ConnectionString"));
            Database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettingsMongo:DatabaseName"));
        }

        public IMongoCollection<Personne> GetPersonneCollection()
        { 
            return Database.GetCollection<Personne>("personne");
        }

        public IMongoCollection<Equipe> GetEquipeCollection()
        {
            return Database.GetCollection<Equipe>("equipe");
        }
    }
}
