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
    internal class OscBinaryReader : BinaryReader
    {
        public OscBinaryReader(Stream input)
            : base(input, Encoding.ASCII)
        {
        }

        public void Pad()
        {
            int pad = 3 - ((int) BaseStream.Position - 1) % 4;

            for (int i = 0; i < pad; ++i)
                ReadByte();
        }

        public override int ReadInt32()
        {
            var data = base.ReadBytes(sizeof(int));

            if (BitConverter.IsLittleEndian)
                Array.Reverse(data, 0, data.Length);

            return BitConverter.ToInt32(data, 0);
        }

        public override long ReadInt64()
        {
            var data = base.ReadBytes(sizeof(long));

            if (BitConverter.IsLittleEndian)
                Array.Reverse(data, 0, data.Length);

            return BitConverter.ToInt64(data, 0);
        }

        public override float ReadSingle()
        {
            var data = base.ReadBytes(sizeof(float));

            if (BitConverter.IsLittleEndian)
                Array.Reverse(data, 0, data.Length);

            return BitConverter.ToSingle(data, 0);
        }

        public override double ReadDouble()
        {
            var data = base.ReadBytes(sizeof(double));

            if (BitConverter.IsLittleEndian)
                Array.Reverse(data, 0, data.Length);

            return BitConverter.ToDouble(data, 0);
        }

        public override string ReadString()
        {
            var result = new StringBuilder(32);

            for (int i = 0; i < BaseStream.Length; ++i)
            {
                char c = ReadChar();

                if (c == '\0')
                    break;

                result.Append(c);
            }

            string value = result.ToString();

            Pad();

            return value;
        }

        public override byte[] ReadBytes(int count)
        {
            var bytes = base.ReadBytes(count);

            Pad();

            return bytes;
        }
    }
}