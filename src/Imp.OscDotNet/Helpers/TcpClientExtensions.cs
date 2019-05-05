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
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Imp.OscDotNet.Helpers
{
    internal static class TcpClientExtensions
    {
        public static async Task<TcpClient> AcceptTcpClientAsync(this TcpListener listener, CancellationToken token)
        {
            try
            {
                return await listener.AcceptTcpClientAsync();
            }
            catch (Exception ex) when (token.IsCancellationRequested)
            {
                throw new OperationCanceledException("Cancellation was requested while awaiting TCP client connection.",
                    ex);
            }
        }
    }
}