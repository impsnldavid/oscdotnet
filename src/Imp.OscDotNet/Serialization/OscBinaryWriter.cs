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
using System.IO;
using System.Text;

namespace Imp.OscDotNet.Serialization
{
    internal class OscBinaryWriter : BinaryWriter
    {
        public OscBinaryWriter(Stream output)
            : base(output, Encoding.ASCII)
        {
        }

        public void Pad()
        {
            int pad = 3 - ((int) BaseStream.Position - 1) % 4;

            for (int i = 0; i < pad; ++i)
                base.Write(byte.MinValue);
        }

        public override void Write(int value)
        {
            var data = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(data, 0, data.Length);

            base.Write(data);
        }

        public override void Write(long value)
        {
            var data = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(data, 0, data.Length);

            base.Write(data);
        }

        public override void Write(float value)
        {
            var data = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(data, 0, data.Length);

            base.Write(data);
        }

        public override void Write(double value)
        {
            var data = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(data, 0, data.Length);

            base.Write(data);
        }

        public override void Write([CanBeNull] string value)
        {
            string terminatedValue = (value ?? string.Empty) + "\0";

            base.Write(Encoding.ASCII.GetBytes(terminatedValue));

            Pad();
        }

        public override void Write(byte[] buffer)
        {
            base.Write(buffer);
            Pad();
        }
    }
}