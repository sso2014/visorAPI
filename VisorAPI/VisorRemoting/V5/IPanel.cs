using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisorRemoting.V5
{
    public interface IPanel
    {
        void Marcha();
        void Parada();
        void Foward();
        void Reversa();
        void UpdateDisplay(string data);
    }
}
