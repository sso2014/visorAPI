using System;
using System.Data;

namespace VisorRemoting.V8
{
    public interface IPanelModel
    {
        string GetData();
        void SetCommand(ValleyCommandType command);
        Display GetDisplay();
        void Disconnect();    
    }
}
