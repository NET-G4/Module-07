namespace Lesson05.Exceptions
{
    public class EntityNotFounException : Exception
    {
        public EntityNotFounException() { }
        public EntityNotFounException(string message) : base(message) { }
    }
}
