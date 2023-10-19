using ProjetCompetences.Domain.Models;
using ProjetCompetences.Domain.Repository;
using ProjetCompetences.Domain.Services.Exceptions.Equipe;

namespace ProjetCompetences.Domain.Services
{
    public class EquipeService
    {
        private readonly EquipeRepo _EquipeRepo;
        private readonly PersonneRepo _PersonneRepo;

        public EquipeService(EquipeRepo equipeRepo, PersonneRepo personneRepo) 
        { 
            _EquipeRepo = equipeRepo;
            _PersonneRepo = personneRepo;
        }

        public async Task<Equipe> GetEquipe(Guid id)
        {
            return await _EquipeRepo.GetEquipe(id);
        }

        public async Task<List<Equipe>> GetAllEquipes()
        {
            return await _EquipeRepo.GetAllEquipes();
        }

        public async Task<Equipe> CreateEquipe(Equipe equipe)
        {
            if(equipe.Membres == null)
            {
                equipe.Membres = new List<Guid>();
            }
            return await _EquipeRepo.AddEquipe(equipe);
        }

        public async Task<Equipe> AddMembreToEquipe(Guid idEquipe, Guid idMembre)
        {
            var currentEquipe = await _EquipeRepo.GetEquipe(idEquipe);
            var currentPersonne = await _PersonneRepo.GetPersonne(idMembre);

            if (currentPersonne == null) throw new PersonneNotFoundException("La personne ayant l'ID " + idMembre 
                + " n'a pas été trouvée.");
            if (currentEquipe == null) throw new EquipeNotFoundException("L'équipe ayant l'ID " + idEquipe
                + " n'a pas été trouvée.");
            if (currentPersonne.Id_Equipe == idEquipe)
            {
                throw new AlreadyInEquipeException("La personne ayant l'ID " + idMembre + " est déjà dans cette équipe.");
            }
            if(currentPersonne.Id_Equipe != null)
            {
                throw new AlreadyInOtherEquipeException("La personne ayant l'ID " + idMembre + " est déjà dans une autre équipe.");
            }
            
            currentPersonne.Id_Equipe = idEquipe;
            currentEquipe.Membres.Add(idMembre);

            await _PersonneRepo.UpdatePersonne(currentPersonne);
            return await _EquipeRepo.UpdateEquipe(currentEquipe);
        }

        public async Task<Equipe> RemoveMembreFromEquipe(Guid idEquipe, Guid idPersonne)
        {
            var currentEquipe = await _EquipeRepo.GetEquipe(idEquipe);
            var currentPersonne = await _PersonneRepo.GetPersonne(idPersonne);

            if (currentPersonne == null) throw new PersonneNotFoundException("La personne ayant l'ID " + idPersonne
                + " n'a pas été trouvée.");
            if (currentEquipe == null) throw new EquipeNotFoundException("L'équipe ayant l'ID " + idEquipe
                + " n'a pas été trouvée.");
            if (currentEquipe.Membres.Count == 0 || currentPersonne.Id_Equipe != idEquipe) throw new NotInEquipeException("La personne ayant l'ID " +
                idPersonne + " n'est pas dans cette equipe");
            if (currentPersonne.Id == currentEquipe.Manager) throw new ManagerCantLeaveEquipe("La personne ayant l'ID "
                + idPersonne + " est manager de l'équipe, et ne peut la quitter.");

            currentPersonne.Id_Equipe = null;
            currentEquipe.Membres.Remove(idPersonne);

            await _PersonneRepo.UpdatePersonne(currentPersonne);
            return await _EquipeRepo.UpdateEquipe(currentEquipe);
        }

        public async Task DeleteEquipe(Guid idEquipe)
        {
            var currentEquipe = await _EquipeRepo.GetEquipe(idEquipe);
            if (currentEquipe == null)
            {
                throw new EquipeNotFoundException("L'équipe ayant l'ID " + idEquipe
                + " n'a pas été trouvée.");
            }

            foreach (var membreId in currentEquipe.Membres)
            {
                var currentPersonne = await _PersonneRepo.GetPersonne(membreId);
                if (currentPersonne != null)
                {
                    if (currentPersonne.Id_Equipe == idEquipe)
                    {
                        currentPersonne.Id_Equipe = null;
                    }

                    await _PersonneRepo.UpdatePersonne(currentPersonne);
                }
            }
            
            await _EquipeRepo.DeleteEquipe(idEquipe);
        }

        public async Task<Equipe> ChangeManager(Guid idEquipe, Guid idManager)
        {
            var currentEquipe = await _EquipeRepo.GetEquipe(idEquipe)
                ?? throw new EquipeNotFoundException("L'équipe ayant l'ID " + idEquipe + " n'a pas été trouvée.");

            var currentPersonne = await _PersonneRepo.GetPersonne(idManager)
                ?? throw new PersonneNotFoundException("La personne ayant l'ID " + idManager + " n'a pas été trouvée.");

            if (currentEquipe.Membres.Contains(idManager)){
                currentEquipe.Manager = idManager;
            }
            else
            {
                throw new ManagerNotInEquipe("Le manager avec l'ID " + idManager + " n'est pas dans cette équipe.");
            }

            return await _EquipeRepo.UpdateEquipe(currentEquipe);
        }
    }
}
