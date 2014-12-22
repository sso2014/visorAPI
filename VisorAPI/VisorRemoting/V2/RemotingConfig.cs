using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisorRemoting.V2
{
    public class RemotingConfig
    {
        public string localHost { get; set; }
        public int LocalPort { get; set; }
        public string RemoteHost { get; set; }
        public int RemotePort { get; set; }
    }
}
