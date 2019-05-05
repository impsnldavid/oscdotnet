// This file is part of OscDotNet.
// 
// OscDotNet is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// OscDotNet is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with OscDotNet.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Imp.OscDotNet
{
    [PublicAPI]
    public class OscElement : IOscPacketPart, IEquatable<OscElement>
    {
        public OscElement(string addressPattern, ImmutableList<IOscParameter> parameters)
        {
            AddressPattern = addressPattern ?? throw new ArgumentNullException(nameof(addressPattern));

            if (AddressPattern.Length < 2)
                throw new ArgumentException("Address must be at least 1 character plus '/' prefix", nameof(addressPattern));

            if (!AddressPattern.StartsWith("/"))
                throw new ArgumentException("Address must be prefixed with with '/'", nameof(addressPattern));

            if (AddressPattern.Contains(' '))
                throw new ArgumentException("Address cannot contain space",nameof(addressPattern));

            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));

            TypeTags = string.Join("", Parameters.Select(p => p.TypeTag));
        }

        public OscElement(string addressPattern, IEnumerable<IOscParameter> parameters)
            : this(addressPattern, parameters?.ToImmutableList())
        {
            
        }

        public OscElement(string addressPattern, params IOscParameter[] parameters)
            : this(addressPattern, parameters?.ToImmutableList())
        {
        }

        internal OscElement(string addressPattern, string typeTags, IEnumerable<IOscParameter> parameters)
        {
            AddressPattern = addressPattern;
            TypeTags = typeTags;
            Parameters = parameters.ToImmutableList();
        }


        public string AddressPattern { get; }
        public string TypeTags { get; }
        public ImmutableList<IOscParameter> Parameters { get; }


        public bool Equals(OscElement other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(AddressPattern, other.AddressPattern) && Parameters.Equals(other.Parameters);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((OscElement) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (AddressPattern.GetHashCode() * 397) ^ Parameters.GetHashCode();
            }
        }

        public override string ToString()
        {
            return $"{AddressPattern} {string.Join(" ", Parameters.ToString())}";
        }
    }
}