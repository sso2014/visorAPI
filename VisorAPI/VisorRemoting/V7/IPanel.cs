using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace VisorRemoting.V7
{
    public interface IPanel
    {
        void Marcha();
        void Parada();
        void Foward();
        void Reversa();
        Display Display { get; set; }
        void AddListener(IPanelController Control);

    }
}
