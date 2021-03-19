using System;

namespace Seshat
{
    internal static class IdGen
    {
        private static int _freeId = 0;

        public static int Next()
            => ++_freeId;

        public static int NextFree(Func<int, bool> predicate)
        {
            int id = Next();

            while (!predicate(id))
                id = Next();

            return id;
        }
    }
}
