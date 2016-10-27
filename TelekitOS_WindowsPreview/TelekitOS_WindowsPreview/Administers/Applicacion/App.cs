﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelekitOS_WindowsPreview.Administers.Applicacion
{
    class App
    {
        public string package = null;
        public string dispName = null;
        public Image icon = null;

        public App(){ }
        public App(string displayName) { dispName = displayName; }

        public App getCompleteApp()
        {
            if (package != null &&
                dispName != null &&
                icon != null)
            {
                return this;
            }else
            {
                throw new SomeValuesNotSet();
            }
        }
    }

    public class SomeValuesNotSet : Exception
    {
        public SomeValuesNotSet()
        {
        }

        public SomeValuesNotSet(string message)
            : base(message)
        {
        }

        public SomeValuesNotSet(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}