using System;
using System.Threading;

namespace VisorRemoting.V7 {

    public class PanelModel : IPanelModel {

        public PanelModel(string id) {

            remote = new VisorRemoting.V7.Remoting(id);
            doWorkThread = new Thread(new ThreadStart(DoWork));
            doWorkThread.Name = "Thread remote";
            doWorkThread.Start();
        }


        private IRemoting remote;
		private Thread doWorkThread;
        private string response = string.Empty;
        private ValleyCommandType commandValley = ValleyCommandType.Query;

		public void DoWork() {

            IRemoting r = new Remoting("125");
            r.Connect();
            r.SendCommand();
            System.Threading.Thread.Sleep(3000);
            r.Receive();
            
        

        }
        private void Process(string data)
        {

            long est;
            bool aux;

            if (data != "")
            {
                if (data[0] == '(' && data[32] == Convert.ToChar(13))
                {

                    est = long.Parse(data.Substring(23, 6), System.Globalization.NumberStyles.HexNumber);
                    aux = Convert.ToBoolean(est & 0x20000);

                    Helper.Insert(DateTime.Now, data.Substring(4, 3), Convert.ToInt32(data.Substring(18, 3)),
                        Convert.ToBoolean(est & 0x40), Convert.ToInt32(data.Substring(12, 3)),
                        Convert.ToInt32(data.Substring(21, 3)), Convert.ToBoolean(est & 0x20000),
                        Convert.ToBoolean(est & 0x10000), Convert.ToBoolean(est & 0x80),
                        Convert.ToBoolean(est & 0x40000), Convert.ToBoolean(est & 0x200),
                        Convert.ToBoolean(est & 0x1000), Convert.ToBoolean(est & 0x80000),
                        Convert.ToInt32(data.Substring(15, 3)));
                }
            }
        }
         public string GetData()
        {
            return response;
        }
        public void SetCommand(ValleyCommandType command)
        {
            commandValley = command;
        }
        public Display GetDisplay()
        {
            return Helper.GetDisplay();
        }
        
        public void Disconnect()
        {
            remote.Disconnect();
        }
    }
}
