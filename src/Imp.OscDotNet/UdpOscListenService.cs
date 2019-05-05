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
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Imp.OscDotNet
{
    [PublicAPI]
    public sealed class UdpOscListenService : IOscListenService
    {
        private readonly UdpClient _udpClient;
        [CanBeNull] private CancellationTokenSource _cancellationTokenSource;
        [CanBeNull] private Task _listenTask;

        public UdpOscListenService(IPEndPoint localEndPoint)
        {
            _udpClient = new UdpClient(localEndPoint)
            {
                ExclusiveAddressUse = false,
                EnableBroadcast = true
            };
        }

        public void Dispose()
        {
            if (IsListening)
                StopListening().Wait();

            _udpClient.Dispose();
        }


        public bool IsListening => _listenTask != null;

        public void StartListening()
        {
            if (IsListening)
                throw new InvalidOperationException("Cannot start listening, service is already listening");

            _cancellationTokenSource = new CancellationTokenSource();
            _listenTask = receiveMessagesAsync(_cancellationTokenSource.Token);
        }

        public async Task StopListening()
        {
            if (!IsListening)
                throw new InvalidOperationException("Cannot stop listening, service is not currently listening");

            Debug.Assert(_cancellationTokenSource != null);

            _cancellationTokenSource.Cancel();

            Debug.Assert(_listenTask != null);

            await _listenTask.ConfigureAwait(false);

            _listenTask = null;
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }

        public event EventHandler<OscPacket> OscPacketReceived;

        private async Task receiveMessagesAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                bool didReceive = false;
                UdpReceiveResult message;

                try
                {
                    message = await _udpClient.ReceiveAsync().ConfigureAwait(false);
                    didReceive = true;
                }
                catch
                {
                    // Exception may be thrown by cancellation
                    if (!cancellationToken.IsCancellationRequested)
                        throw;
                }

                // If nothing received, must have been cancelled
                if (!didReceive)
                    return;
            }
        }
    }
}