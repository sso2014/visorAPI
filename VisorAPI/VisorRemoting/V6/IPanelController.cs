﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisorRemoting.V6
{
    public interface IPanelController
    {
        void UpDate();
        void OnClick(ValleyCommandType command);
        void AllClose();
    }
}
