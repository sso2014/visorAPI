﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisorRemoting.V7
{
    public class PanelAction
    {
        delegate void ActionPanelHandler(ValleyCommandType command);
        event ActionPanelHandler ActionPanelEvent;
    }
}
