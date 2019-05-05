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
using Imp.OscDotNet.Serialization;

namespace Imp.OscDotNet
{
    [PublicAPI]
    public class OscPacket
    {
        [CanBeNull]
        public static OscPacket FromByteArray(byte[] data)
        {
            OscPacket packet = null;

            try
            {
                var ms = new MemoryStream(data);
                var reader = new OscBinaryReader(ms);

                var idChar = reader.ReadChar();

                switch (idChar)
                {
                    case '/':
                        break;

                    case '#':
                        break;

                    default:
                        throw new FormatException("Unknown packet identifier");
                }
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            catch (IOException)
            {
            }
            finally
            {
            }

            return packet;
        }
    }

    public interface IOscPacketPart
    {
    }
}