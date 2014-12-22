using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace VisorRemoting.V2
{
    public class RemotingEventArgs
    {
        public RemotingEventArgs(){
        }
        public bool PrimeraLectura { get; set; }
        public string Data { get; set; }
        public Socket WorkingSoket { get; set; }
    }
}