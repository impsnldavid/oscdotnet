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

namespace Imp.OscDotNet
{
    [PublicAPI]
    public class OscBundle : IOscPacketPart, IEquatable<OscBundle>, IComparable<OscBundle>, IComparable
    {
        public OscBundle(OscTimeTag timeTag, ImmutableList<IOscPacketPart> parts)
        {
            TimeTag = timeTag;
            Parts = parts;
        }

        public OscBundle(OscTimeTag timeTag, IEnumerable<IOscPacketPart> parts)
            : this(timeTag, parts.ToImmutableList())
        {
        }

        public OscBundle(OscTimeTag timeTag, params IOscPacketPart[] parts)
            : this(timeTag, parts.ToImmutableList())
        {
        }

        public OscBundle(ImmutableList<IOscPacketPart> parts)
        {
            TimeTag = OscTimeTag.Now;
            Parts = parts;
        }

        public OscBundle(IEnumerable<IOscPacketPart> parts)
            : this(parts.ToImmutableList())
        {
            TimeTag = OscTimeTag.Now;
        }

        public OscBundle(params IOscPacketPart[] parts)
            : this(parts.ToImmutableList())
        {
        }


        public OscTimeTag TimeTag { get; }
        public ImmutableList<IOscPacketPart> Parts { get; }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is OscBundle other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(OscBundle)}");
        }


        public int CompareTo(OscBundle other)
        {
            return other is null ? 1 : TimeTag.Time.CompareTo(other.TimeTag.Time);
        }


        public bool Equals(OscBundle other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return TimeTag.Equals(other.TimeTag) && Parts.Equals(other.Parts);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((OscBundle) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (TimeTag.GetHashCode() * 397) ^ Parts.GetHashCode();
            }
        }

        public override string ToString()
        {
            return $"#bundle {string.Join(",", Parts.ToString())}";
        }


        public static bool operator <(OscBundle left, OscBundle right)
        {
            return Comparer<OscBundle>.Default.Compare(left, right) < 0;
        }

        public static bool operator >(OscBundle left, OscBundle right)
        {
            return Comparer<OscBundle>.Default.Compare(left, right) > 0;
        }

        public static bool operator <=(OscBundle left, OscBundle right)
        {
            return Comparer<OscBundle>.Default.Compare(left, right) <= 0;
        }

        public static bool operator >=(OscBundle left, OscBundle right)
        {
            return Comparer<OscBundle>.Default.Compare(left, right) >= 0;
        }
    }
}