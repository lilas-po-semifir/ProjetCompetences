namespace ProjetCompetences.Domain.Services.Exceptions.Personne
{
    public class PersonneNotFoundException : Exception
    {
        public PersonneNotFoundException() { }
        public PersonneNotFoundException(string message) : base(message) { }
        public PersonneNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
