﻿using System.Collections.Generic;

namespace NExpect.Interfaces
{
    public interface ICollectionNotAfterTo<T> : ICanAddMatcher<IEnumerable<T>>
    {
        IContain<IEnumerable<T>> Contain { get; }
        ICollectionBe<T> Be { get; }
    }
}