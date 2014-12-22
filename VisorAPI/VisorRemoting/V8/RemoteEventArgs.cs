using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisorRemoting.V8
{
    public class RemoteEventArgs
    {
        public RemoteEventArgs(Panel panel, int index) {

            this.Panel = panel;
        }
        private Panel Panel { set; get; }
     }
}
