namespace ProjetCompetences.Domain.Services.Exceptions.Equipe
{
    public class PersonneNotFoundException : Exception
    {
        public PersonneNotFoundException() { }
        public PersonneNotFoundException(string message) : base(message) { }
        public PersonneNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class EquipeNotFoundException : Exception
    {
        public EquipeNotFoundException() { }
        public EquipeNotFoundException(string message) : base(message) { }
        public EquipeNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class AlreadyInEquipeException : Exception
    {
        public AlreadyInEquipeException() { }
        public AlreadyInEquipeException(string message) : base(message) { }
        public AlreadyInEquipeException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class AlreadyInOtherEquipeException : Exception
    {
        public AlreadyInOtherEquipeException() { }
        public AlreadyInOtherEquipeException(string message) : base(message) { }
        public AlreadyInOtherEquipeException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class NotInEquipeException : Exception
    {
        public NotInEquipeException() { }
        public NotInEquipeException(string message) : base(message) { }
        public NotInEquipeException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class ManagerNotInEquipe : Exception
    {
        public ManagerNotInEquipe() { }
        public ManagerNotInEquipe(string message) : base(message) { }
        public ManagerNotInEquipe(string message, Exception innerException) : base(message, innerException) { }
    }

    public class ManagerCantLeaveEquipe : Exception
    {
        public ManagerCantLeaveEquipe() { }
        public ManagerCantLeaveEquipe(string message) : base(message) { }
        public ManagerCantLeaveEquipe(string message, Exception innerException) : base(message, innerException) { }
    }
}
