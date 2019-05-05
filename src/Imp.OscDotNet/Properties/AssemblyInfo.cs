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

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Imp.OscDotNet")]
[assembly: AssemblyDescription(".Net Standard library for sending/receiving OpenSoundControl packets via UDP/TCP.")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyCompany("David Butler / The Impersonal Stereo")]
[assembly: AssemblyProduct("WallSite")]
[assembly: AssemblyCopyright("Copyright © 2019 David Butler / The Impersonal Stereo")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]

[assembly: Guid("3F045A02-C1C4-491E-AC72-2FCC08641A7F")]

[assembly: InternalsVisibleTo("Imp.OscDotNet.Test")]