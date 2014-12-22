using System;
using System.Data;

namespace VisorRemoting.V6
{
    public interface IPanelModel
    {
        string GetData();
        void SetCommand(ValleyCommandType command);
        Display GetDisplay();
        void Disconnect();
    
    }
}
