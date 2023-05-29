#if (NETSTANDARD1_0_OR_GREATER && !NETSTANDARD2_0_OR_GREATER)
#pragma warning disable CA1010

using System.Collections.Generic;
using System.Linq;

namespace System.Collections;

public class ArrayList : List<object>, IList
{
    public ArrayList() { }

    public ArrayList(int capacity) : base(capacity) { }

    public ArrayList(ICollection collection) : base(collection.Cast<object>()) { }

}
#endif
