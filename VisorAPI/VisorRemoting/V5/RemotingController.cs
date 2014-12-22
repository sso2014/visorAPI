using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace VisorRemoting.V5
{
    public class RemotingController
    {
        public RemotingController(IRemoting remote, IPanel panel)
        {
            this.remoto = remote;
            this.panel = panel;

            Thread threadDoWork = new Thread(new ThreadStart(Sync));
            threadDoWork.Name = "Thread DoWork remote";
            threadDoWork.Start();
        }

        IRemoting remoto;
        IPanel panel;

        public int Interval = 3000;

        private void Sync()
        {

            do
            {

                if (!remoto.Connected)
                {
                    remoto.Connect();
                }
                else
                {
                    remoto.SendCommand();
                    System.Threading.Thread.Sleep(Interval);
                    remoto.Receive();
                    panel.UpdateDisplay(remoto.GetData());
                }

            } while (true);
        }
    }
}