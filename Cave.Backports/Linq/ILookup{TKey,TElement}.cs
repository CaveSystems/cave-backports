﻿#if NET20
#pragma warning disable CS1591 // no comments for backported extensions

using System.Collections.Generic;

namespace System.Linq
{
    public interface ILookup<TKey, TElement> : IEnumerable<IGrouping<TKey, TElement>>
    {
        #region Properties

        int Count { get; }
        IEnumerable<TElement> this[TKey key] { get; }

        #endregion

        #region Members

        bool Contains(TKey key);

        #endregion
    }
}

#endif
