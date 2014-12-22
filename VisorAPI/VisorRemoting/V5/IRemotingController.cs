using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisorRemoting.V5
{
    public interface IRemotingController
    {
        void OnClick(ValleyCommandType command);
    }
}
