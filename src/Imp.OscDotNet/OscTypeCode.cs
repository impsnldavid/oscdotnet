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
using System.Collections.Immutable;
using System.Linq;
using Imp.OscDotNet.MessageTypes;

namespace Imp.OscDotNet
{
    [PublicAPI]
    public enum OscTypeCode
    {
        [OscTypeTag('i', typeof(int))] Int32,
        [OscTypeTag('f', typeof(float))] Float,
        [OscTypeTag('s', typeof(string))] String,
        [OscTypeTag('b', typeof(byte[]))] Blob,
        [OscTypeTag('h', typeof(long))] Int64,
        [OscTypeTag('t', typeof(DateTime))] TimeTag,
        [OscTypeTag('d', typeof(double))] Double,
        [OscTypeTag('S', typeof(string))] SymbolString,
        [OscTypeTag('c', typeof(char))] Char,
        [OscTypeTag('r', typeof(OscColor))] RgbaColor,
        [OscTypeTag('m', typeof(OscMidiMessage))]MidiMessage,
        [OscTypeTag('T', typeof(bool))] TrueValue,
        [OscTypeTag('F', typeof(bool))] FalseValue,
        [OscTypeTag('N', typeof(OscTypeNil))] NilValue,
        [OscTypeTag('I', typeof(OscTypeImpulse))] ImpulseValue,
        [OscTypeTag('[', typeof(OscTypeImpulse))] ParameterArrayStart,
        [OscTypeTag(']', typeof(OscTypeImpulse))] ParameterArrayEnd
    }

    internal static class OscTypeCodeExtensions
    {
        private static readonly ImmutableDictionary<OscTypeCode, Tuple<char, Type>> TypeCodeChars;

        static OscTypeCodeExtensions()
        {
            TypeCodeChars = Enum.GetValues(typeof(OscTypeCode))
                .Cast<OscTypeCode>()
                .ToImmutableDictionary(t => t, t =>
                {
                    var memInfo = typeof(OscTypeCode).GetMember(t.ToString());
                    var attribute = (OscTypeTagAttribute) memInfo[0]
                        .GetCustomAttributes(typeof(OscTypeTagAttribute), false).First();
                    return Tuple.Create(attribute.TypeTag, attribute.ValueType);
                });
        }

        public static char GetTypeCodeChar(this OscTypeCode typeCode) => TypeCodeChars[typeCode].Item1;
    }
}