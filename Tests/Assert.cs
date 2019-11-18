using System;

namespace Tests
{
    public static class Assert
    {
        public static void AreEqual<T>(T item1, T item2)
        {
            if (!object.Equals(item1, item2))
            {
                throw new Exception("Items are not equal!");
            }
        }
    }
}