using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace VisorRemoting.V1
{
      public class TimeOutSocket
        {
            private static bool IsConnectionSuccessful = false;
            private static Exception socketexception;
            private static ManualResetEvent TimeoutObject = new ManualResetEvent(false);


            public static Socket Connect(IPEndPoint localPoint, IPEndPoint remoteEndPoint, int timeoutMSec)
            {
                TimeoutObject.Reset();
                socketexception = null;

                string serverip = Convert.ToString(remoteEndPoint.Address);
                int serverport = remoteEndPoint.Port;
                Socket sck = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sck.Bind(localPoint);
               
                sck.BeginConnect(remoteEndPoint, new AsyncCallback(CallBackMethod), sck);

                if (TimeoutObject.WaitOne(timeoutMSec, false))
                {
                    if (IsConnectionSuccessful)

                    {
                        return sck;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    sck.Close();
                    throw new TimeoutException("TimeOut Exception");
                }
            }
            private static void CallBackMethod(IAsyncResult asyncresult)
            {
                try
                {
                    IsConnectionSuccessful = false;
                    Socket sck = asyncresult.AsyncState as Socket;
                                  
                    if (sck != null)
                    {
                        sck.EndConnect(asyncresult);
                        IsConnectionSuccessful = true;
                    }
                }
                catch (Exception ex)
                {
                    IsConnectionSuccessful = false;
                    socketexception = ex;
                }
                finally
                {
                    TimeoutObject.Set();
                }
         }
    }
}
