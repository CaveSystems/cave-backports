#if NET20

using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
    sealed class OrderedEnumerable<TElement> : IOrderedEnumerable<TElement>
    {
        readonly IEnumerable<TElement> Elements;

        public OrderedEnumerable(IEnumerable<TElement> elements) => this.Elements = elements;

        public IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            var dict = new SortedDictionary<TKey, TElement>(comparer);
            foreach (var element in Elements)
            {
                dict.Add(keySelector(element), element);
            }
            var sorted = dict.Values.ToList();
            if (descending)
            {
                sorted.Reverse();
            }

            return new OrderedEnumerable<TElement>(sorted);
        }

        public IEnumerator<TElement> GetEnumerator() => Elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Elements.GetEnumerator();
    }
}

#endif
