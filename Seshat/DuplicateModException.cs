using System;
using System.Runtime.Serialization;

namespace Seshat
{
    public class DuplicateModException : Exception
    {
        public DuplicateModException(string id)
            : base($"Duplicate mods with id {id}")
        {
        }
    }
}