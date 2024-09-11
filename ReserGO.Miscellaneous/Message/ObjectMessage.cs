using ReserGO.Utils.Event;

namespace ReserGO.Miscellaneous.Message
{
    public class ObjectMessage<T> : IMessage<T>
    {
        private readonly T _value;

        public ObjectMessage(T value)
        {
            _value = value;
        }

        public T Value => _value;
    }
}
