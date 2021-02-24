using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
    class Grouping<TKey, TElement> : IGrouping<TKey, TElement>
    {
        #region Internal Fields

        internal readonly IEnumerable<TElement> Group;

        #endregion Internal Fields

        #region Public Constructors

        public Grouping(TKey key, IEnumerable<TElement> group)
        {
            Group = group;
            Key = key;
        }

        #endregion Public Constructors

        #region Public Properties

        public TKey Key { get; }

        #endregion Public Properties

        #region Public Methods

        public IEnumerator<TElement> GetEnumerator() => Group.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Group.GetEnumerator();

        #endregion Public Methods
    }
}
