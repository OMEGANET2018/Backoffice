﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Comun
{
    public class TreeView
    {
        public string text { get; set; }
        public TreeView[] nodes { get; set; }
        public string icon { get; } = "glyphicons glyphicons-unchecked";
        public string selectedIcon { get; } = "glyphicons glyphicons-check";
        public TreeViewState state { get; set; }
    }

    public class TreeViewState
    {
        public bool disabled { get; set; }
        public bool expanded { get; set; }
        public bool selected { get; set; }
    }
}