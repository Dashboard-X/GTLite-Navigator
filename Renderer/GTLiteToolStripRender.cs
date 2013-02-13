using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GTLite
{
    class GTLiteToolStripRender : ToolStripProfessionalRenderer
    {
        public GTLiteToolStripRender()
            : base(new GTLiteColorTable())
        {
 
        }
    }
}
