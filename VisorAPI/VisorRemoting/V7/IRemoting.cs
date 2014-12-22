using System;
namespace VisorRemoting.V7 {
	public interface IRemoting {

		string GetData();
		void Connect();
		void SendCommand();
		void Receive();
		void Disconnect();
        void SetCommand(ValleyCommandType command);
        bool Connected { get; set; }
        RemoteState State { get; }
        string Data { get; set; }
    }
}
