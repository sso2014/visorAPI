using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace VisorRemoting.V8
{
    public interface IPanel
    {
        void Marcha();
        void Parada();
        void Foward();
        void Reversa();
        IDisplay Display { get; set; }
        void AddListener(IPanelController Control);

    }
}
