using System.Runtime.CompilerServices;

namespace Project_VIS.Domain.Exceptions
{
    public class EntityNotFoundExeption : Exception 
    {
        public EntityNotFoundExeption(string message) : base(message) { }
    }

    public class NoActiveTeacher : Exception
    {
        public NoActiveTeacher(string message) : base(message) { }
    }
    
    public class RelationAlreadyExists : Exception
    {
        public RelationAlreadyExists(string message) : base(message) { }

    }
}