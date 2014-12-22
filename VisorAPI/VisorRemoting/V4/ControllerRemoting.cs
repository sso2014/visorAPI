using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisorRemoting.V4
{
    public class ControllerRemoting
    {
        public ControllerRemoting() {
            conn = new RemotingConnection();
        }

        private List<RemotingConnection> remotingList = null;

       
        public void AddListener() { 
         

        }
        
        private RemotingConnection conn = null;

    }
}
