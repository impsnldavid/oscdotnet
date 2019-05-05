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
    public struct OscMidiMessage : IEquatable<OscMidiMessage>
    {
        public static int ByteLength = 4;

        public static OscMidiMessage FromByteArray(byte[] data)
        {
            return new OscMidiMessage(data[0], data[1], data[2], data[3]);
        }

        public OscMidiMessage(byte portId, byte status, byte data1, byte data2)
            : this()
        {
            PortId = portId;
            Status = status;
            Data1 = data1;
            Data2 = data2;
        }

        public byte[] ToByteArray()
        {
            return new byte[] {PortId, Status, Data1, Data2};
        }

        public byte PortId { get; }

        public byte Status { get; }

        public byte Data1 { get; }

        public byte Data2 { get; }

        public override bool Equals(object other)
        {
            if (other == null)
                return false;

            if (!(other is OscMidiMessage))
                return false;

            return Equals((OscMidiMessage) other);
        }

        public override int GetHashCode()
        {
            return PortId.GetHashCode() ^ Status.GetHashCode() ^ Data1.GetHashCode() ^ Data2.GetHashCode();
        }

        public bool Equals(OscMidiMessage other)
        {
            return PortId == other.PortId && Status == other.Status && Data1 == other.Data1
                   && Data2 == other.Data2;
        }

        public static bool operator ==(OscMidiMessage a, OscMidiMessage b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(OscMidiMessage a, OscMidiMessage b)
        {
            return !(a == b);
        }
    }
}