using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisorRemoting.V2
{
    public class ClassHandlerData
    {
        public ClassHandlerData()
        {
        }

        public delegate void HandlerData(string data);
        public event HandlerData EventData;

        public void Trigger(string data)
        {
            if (EventData != null)
            {
                EventData(data);
            }
        }
    }
}
