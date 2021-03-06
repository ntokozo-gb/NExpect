﻿using NExpect.Interfaces;

namespace NExpect.Implementations
{
    public class LessContinuation<T> :
        ExpectationContext<T>,
        ILessContinuation<T>
    {
        public T Actual { get; }

        public LessContinuation(T actual)
        {
            Actual = actual;
        }
    }
}