#if NET20
#pragma warning disable CS1591 // no comments for backported extensions

using System.Collections.Generic;

namespace System.Linq
{
    public interface IGrouping<TKey, TElement> : IEnumerable<TElement>

    {
        #region Properties

        TKey Key { get; }

        #endregion
    }
}

#endif
