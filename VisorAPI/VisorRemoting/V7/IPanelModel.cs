using System;
using System.Data;

namespace VisorRemoting.V7
{
    public interface IPanelModel
    {
        string GetData();
        void SetCommand(ValleyCommandType command);
        Display GetDisplay();
        void Disconnect();
    
    }
}
