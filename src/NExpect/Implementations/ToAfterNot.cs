using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class ToAfterNot<T>: ExpectationContext<T>, IToAfterNot<T>
    {
        public T Actual { get; }
        public IBe<T> Be => Factory.Create<T, Be<T>>(Actual, this);
        public IContain<T> Contain => Factory.Create<T, Contain<T>>(Actual, this);
        public IHave<T> Have => Factory.Create<T, Have<T>>(Actual, this);

        public ToAfterNot(T actual)
        {
            Actual = actual;
        }
    }
}