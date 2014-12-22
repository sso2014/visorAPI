using System;
namespace VisorRemoting.V6 {
	public interface IRemoting {

		string GetData();
		void Connect();
		void SendCommand();
		void Receive();
		void Disconnect();
        void SetCommand(ValleyCommandType command);
        bool Connected { get; set; }	
    }
}
