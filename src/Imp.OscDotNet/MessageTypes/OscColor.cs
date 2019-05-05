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

namespace Imp.OscDotNet.MessageTypes
{
    [PublicAPI]
    public struct OscColor : IEquatable<OscColor>
    {
        public static int ByteLength = 4;

        public static OscColor FromByteArray(byte[] data)
        {
            return new OscColor(data[0], data[1], data[2], data[3]);
        }

        public OscColor(byte red, byte green, byte blue, byte alpha)
            : this()
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        public byte[] ToByteArray()
        {
            return new[] {Red, Green, Blue, Alpha};
        }

        public byte Red { get; }

        public byte Green { get; }

        public byte Blue { get; }

        public byte Alpha { get; }


        public bool Equals(OscColor other)
        {
            return Red == other.Red && Green == other.Green && Blue == other.Blue && Alpha == other.Alpha;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is OscColor other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Red.GetHashCode();
                hashCode = (hashCode * 397) ^ Green.GetHashCode();
                hashCode = (hashCode * 397) ^ Blue.GetHashCode();
                hashCode = (hashCode * 397) ^ Alpha.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(OscColor a, OscColor b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(OscColor a, OscColor b)
        {
            return !(a == b);
        }
    }
}