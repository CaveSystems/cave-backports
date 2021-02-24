#if NET20

using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
    public class Lookup<TKey, TElement> : IEnumerable<IGrouping<TKey, TElement>>, ILookup<TKey, TElement>
    {
        readonly IGrouping<TKey, TElement> DefaultGroup;
        readonly Dictionary<TKey, IGrouping<TKey, TElement>> Groups;

        internal Lookup(Dictionary<TKey, List<TElement>> lookup, IEnumerable<TElement> defaultKeyElements)
        {
            Groups = new Dictionary<TKey, IGrouping<TKey, TElement>>(lookup.Comparer);
            foreach (var item in lookup)
            {
                Groups.Add(item.Key, new Grouping<TKey, TElement>(item.Key, item.Value));
            }
            if (defaultKeyElements != null)
            {
                DefaultGroup = new Grouping<TKey, TElement>(default, defaultKeyElements);
            }
        }

        public int Count => (DefaultGroup == null) ? Groups.Count : Groups.Count + 1;

        public IEnumerable<TElement> this[TKey key]
        {
            get
            {
                if (key == null && DefaultGroup != null)
                {
                    return DefaultGroup;
                }
                else if (key != null)
                {
                    if (Groups.TryGetValue(key, out var group))
                    {
                        return group;
                    }
                }
                return new TElement[0];
            }
        }

        public IEnumerable<TResult> ApplyResultSelector<TResult>(Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
        {
            if (DefaultGroup != null)
            {
                yield return resultSelector(DefaultGroup.Key, DefaultGroup);
            }
            foreach (var group in Groups.Values)
            {
                yield return resultSelector(group.Key, group);
            }
        }

        public bool Contains(TKey key) => (key == null) ? DefaultGroup != null : Groups.ContainsKey(key);

        public bool TryGetGroup(TKey key, out IGrouping<TKey, TElement> group)
        {
            if (key == null)
            {
                group = DefaultGroup;
                return group != null;
            }
            else
            {
                return Groups.TryGetValue(key, out group);
            }
        }

        public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
        {
            if (DefaultGroup != null)
            {
                yield return DefaultGroup;
            }
            foreach (var group in Groups.Values)
            {
                yield return group;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

#endif
