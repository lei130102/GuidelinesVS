using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ResourceLibrary
{
    public class CustomResources
    {
        public static ComponentResourceKey SadTileBrush
        {
            get
            {
                return new ComponentResourceKey(typeof(CustomResources), "SadTileBrush");
            }
        }
    }
}
