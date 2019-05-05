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
using Imp.OscDotNet.Helpers;

namespace Imp.OscDotNet
{
    [PublicAPI]
    public sealed class TcpOscListenService : IOscListenService
    {
        private static int BufferLength = ushort.MaxValue;

        public enum PacketMode
        {
            PacketLengthHeaders,
            Slip
        }

        private readonly TcpListener _tcpListener;
        [CanBeNull] private CancellationTokenSource _cancellationTokenSource;
        [CanBeNull] private Task _listenTask;


        public TcpOscListenService(IPEndPoint localEndPoint, PacketMode mode)
        {
            if (!Enum.IsDefined(typeof(PacketMode), mode))

                _tcpListener = new TcpListener(localEndPoint);
        }

        public void Dispose()
        {
            if (IsListening)
                StopListening().Wait();
        }

        public event EventHandler<OscPacket> OscPacketReceived;

        public PacketMode Mode { get; }


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

        private async Task receiveMessagesAsync(CancellationToken cancellationToken)
        {
            _tcpListener.Start();

            var data = new byte[BufferLength];

            try
            {
                while (true)
                {
                    var client = await _tcpListener.AcceptTcpClientAsync(cancellationToken).ConfigureAwait(false);
                    var stream = client.GetStream();

                    while (true)
                    {
                        int bytesReceived = await stream.ReadAsync(data, 0, data.Length, cancellationToken)
                            .ConfigureAwait(false);

                        if (bytesReceived == 0)
                            break;

                        switch (Mode)
                        {
                            case PacketMode.PacketLengthHeaders:

                                break;

                            case PacketMode.Slip:

                                break;

                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }

                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                _tcpListener.Stop();
            }
        }
    }   
}