using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace V.Data.Net
{
    public abstract class Command
    {
        protected Receiver receiver;        
        public Command(Receiver receiver) {
            this.receiver = receiver;
        }
        public abstract void Execute();
    }
}
