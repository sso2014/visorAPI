using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.VO4;

namespace VisorRemoting.V2
{
    public class RemotingObjectVisorEventArgs
    {
        public RemotingObjectVisorEventArgs() {

            panel = new Core.VO4.Panel();
        }   
        Core.VO4.Panel panel = null;
        public RemotingConfig RemoteConfig { get; set; }
        public Core.VO4.Panel Panel
        {
            get
            {
                return panel;
            }
        }
    }
}
