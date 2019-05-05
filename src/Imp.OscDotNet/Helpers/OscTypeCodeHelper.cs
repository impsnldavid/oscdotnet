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

namespace Imp.OscDotNet.Helpers
{
    [PublicAPI]
    public static class OscTypeCodeHelper
    {
        static OscTypeCodeHelper()
        {
            CharToTypeCodes = Enum.GetValues(typeof(OscTypeCode))
                .Cast<OscTypeCode>()
                .ToImmutableDictionary(e => OscTypeTagAttribute.GetAttribute(e).TypeTag);

            TypeCodesToChar = CharToTypeCodes.ToImmutableDictionary(p => p.Value, p => p.Key);

            OscTypeCodeToSystemTypeCode = Enum.GetValues(typeof(OscTypeCode))
                .Cast<OscTypeCode>()
                .ToImmutableDictionary(e => e, e => Type.GetTypeCode(OscTypeTagAttribute.GetAttribute(e).ValueType));
        }
        

        public static ImmutableDictionary<char, OscTypeCode> CharToTypeCodes { get; }

        public static ImmutableDictionary<OscTypeCode, char> TypeCodesToChar { get; }

        public static ImmutableDictionary<OscTypeCode, TypeCode> OscTypeCodeToSystemTypeCode { get; }
    }
}