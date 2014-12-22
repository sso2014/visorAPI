using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace VisorRemoting.V7
{
    public class SyncMaster
    {
        public SyncMaster(List<IRemoting> remoting)
        {

            this.Remote = remoting;
            this.total = remoting.Count;
            this.response = new List<string>();

        }

        List<IRemoting> Remote = null;
        public List<string> response = null;

        int index = 0;
        int total = 0;

        public void SelectData(int index)
        {
            this.index = index;
        }
        public void Poll()
        {
            if (!Remote[0].Connected)
            {
                Remote[0].Connect();
            }
            else
            {
                Remote[0].SendCommand();
                Remote[0].Receive();
                response.Add(Remote[0].GetData());
            }
        }
        public void ConnectAll()
        {
            for (int i = 0; i < total; i++)
            {
                if (Remote[i].State == RemoteState.Disconnect)
                {
                    Remote[i].Connect();
                }
            }
        }
        public void SendQueryAll()
        {
            for (int i = 0; i < total; i++)
            {
                if (Remote[i].State == RemoteState.Connected)
                {
                    Remote[index].SendCommand();
                }
            }
        }
        private void ReadResponseAll()
        {
            for (int i = 0; i < total; i++)
            {
                if (Remote[i].Connected)
                {
                    Remote[index].Receive();

                    response.Add(Remote[i].GetData());
                }
            }
        }
    }
}