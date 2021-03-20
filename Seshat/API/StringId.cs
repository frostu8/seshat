using System;

namespace Seshat.API
{
    /// <summary>
    /// Helper functions to interact with Seshat string IDs.
    /// </summary>
    /// <remarks>
    /// A string ID is fully qualified if it has a domain component. The
    /// following string IDs are fully qualified string IDs.
    /// <list type="bullet">
    /// <item><c>"com.projectmoon.libraryofruina:60696"</c></item>
    /// <item><c>"com.frostu8.coolmod:coolcard"</c></item>
    /// <item><c>":publicdomain"</c></item>
    /// </list>
    /// </remarks>
    public static class StringId
    {
        /// <summary>
        /// The delimiter used to seperate a domain and its basename.
        /// 
        /// <para>
        /// Use this character only to seperate a domain and its basename and
        /// nothing more. Using the delimiter in a basename is valid, but it is 
        /// bad practice. Using the delimiter in a domain will invalidate it,
        /// causing undefined behaviour.
        /// </para>
        /// </summary>
        public const char DomainDelimiter = ':';

        /// <summary>
        /// Gets the domain of the string ID.
        /// </summary>
        /// <param name="sid">A fully-qualified string ID.</param>
        /// <returns>The domain.</returns>
        /// <exception cref="ArgumentException">
        /// The ID does not have a domain component.
        /// </exception>
        public static string GetDomain(string sid)
        {
            int sepIdx = sid.IndexOf(DomainDelimiter);

            if (sepIdx >= 0)
                return sid.Substring(0, sepIdx);
            else
                throw new ArgumentException($"The SID {sid} does not have a domain component.", "sid");
        }

        /// <summary>
        /// Checks if a string ID is fully qualified (has a domain).
        /// </summary>
        /// <param name="sid">A string ID.</param>
        /// <returns>
        /// <c>true</c> if the string ID has a domain component; otherwise
        /// <c>false</c>.
        /// </returns>
        public static bool HasDomain(string sid)
        {
            return sid.IndexOf(DomainDelimiter) >= 0;
        }

        /// <summary>
        /// Get the basename of a string ID, stripping it of its domain.
        /// </summary>
        /// <param name="sid">A string ID.</param>
        /// <returns>The basename of the string ID.</returns>
        public static string GetBasename(string sid)
        {
            int sepIdx = sid.IndexOf(DomainDelimiter);

            if (sepIdx >= 0)
                return sid.Substring(sepIdx+1, sid.Length - (sepIdx + 1));
            else
                return sid;
        }

        /// <summary>
        /// Concatenates a string ID and a domain.
        /// </summary>
        /// <param name="domain">A domain.</param>
        /// <param name="sid">A string ID with no domain.</param>
        /// <returns>A fully qualified String ID with the specified domain and basename.</returns>
        /// <exception cref="ArgumentException">
        /// The domain or string ID has a domain of their own.
        /// </exception>
        public static string Concat(string domain, string sid)
        {
            if (HasDomain(domain))
                throw new ArgumentException($"domain contains a bad delimiter ({domain})", "domain");
            else if (HasDomain(sid))
                throw new ArgumentException($"sid contains a domain ({sid})", "sid");
            else
                return string.Concat(domain, DomainDelimiter, sid);
        }

        /// <summary>
        /// Appends a domain to a string ID if it doesn't already have a domain.
        /// </summary>
        /// <param name="sid">The string ID.</param>
        /// <param name="domain">A domain.</param>
        /// <returns>The new string ID.</returns>
        public static string HasDomainOr(string sid, string domain)
        {
            if (HasDomain(sid))
                return sid;
            else
                return Concat(domain, sid);
        }

        /// <summary>
        /// Checks if two string ID are in the same domain.
        /// </summary>
        /// <param name="sid">A fully-qualified string ID.</param>
        /// <param name="otherSid">A fully-qualified string ID.</param>
        /// <returns>
        /// <c>true</c> if the specified IDs are in the same domain; otherwise
        /// <c>false</c>.
        /// </returns>
        public static bool SameDomain(string sid, string otherSid)
        {
            return InDomain(sid, GetDomain(otherSid));
        }

        /// <summary>
        /// Checks if an sid is in the specified domain.
        /// </summary>
        /// <param name="sid">A fully-qualified string ID.</param>
        /// <param name="domain">A domain.</param>
        /// <returns>
        /// <c>true</c> if the specified ID is in the domain; otherwise
        /// <c>false</c>.
        /// </returns>
        public static bool InDomain(string sid, string domain)
        {
            return GetDomain(sid).Equals(domain);
        }

        internal static string VanillaSid(string sid)
            => Concat(Seshat.VanillaDomain, sid);
    }
}
