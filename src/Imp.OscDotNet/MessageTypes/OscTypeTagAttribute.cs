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
    internal class OscTypeTagAttribute : Attribute
    {
        public OscTypeTagAttribute(char typeTag, Type valueType)
        {
            TypeTag = typeTag;
            ValueType = valueType;
        }

        public char TypeTag { get; }

        public Type ValueType { get; }

        public static OscTypeTagAttribute GetAttribute(Enum enumVal)
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(OscTypeTagAttribute), false);
            return (attributes.Length > 0) ? (OscTypeTagAttribute) attributes[0] : null;
        }
    }
}