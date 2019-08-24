#if NET20

using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
    sealed class OrderedEnumerable<TElement> : IOrderedEnumerable<TElement>
    {
        readonly IEnumerable<TElement> elements;

        public OrderedEnumerable(IEnumerable<TElement> elements) => this.elements = elements;

        public IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            var dict = new SortedDictionary<TKey, TElement>(comparer);
            foreach (var element in elements)
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

        public IEnumerator<TElement> GetEnumerator() => elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => elements.GetEnumerator();
    }
}

#endif
