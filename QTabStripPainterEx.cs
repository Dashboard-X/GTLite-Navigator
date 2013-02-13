using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using System.Drawing;
using Qios.DevSuite.Components;

namespace QAdvancedTabControlSample
{

    /// <summary>
    /// Defines an override of the QTabStripPainter. This painter is responsible for the drawing of the QTabCloseButton.
    /// This painter is set in the Qtabpage.CreateTabStrip method.
    /// </summary>
    public class QTabStripPainterEx : QTabStripPainter
    {

        /// <summary>
        /// Overridden. Draws the content of the QTabButton button, here the extra CloseButton is painted.
        /// </summary>
        protected override void DrawTabButtonContent(QTabButton button, QTabButtonConfiguration buttonConfiguration, string text, Icon icon, Color replaceColor, Color replaceColorWith, Color textColor, Font font, Rectangle bounds, Graphics graphics)
        {
            //First call the base.
            base.DrawTabButtonContent(button, buttonConfiguration, text, icon, replaceColor, replaceColorWith, textColor, font, bounds, graphics);

            //Get the QTabCloseButton.
            QTabCloseButton tmp_oButton = button.Control.Tag as QTabCloseButton;

            if (tmp_oButton != null)
            {
                //Set the bounds.
                tmp_oButton.Bounds = new Rectangle(bounds.Right + 2, bounds.Top + 3, 10, 11);

                //Determine its state and draw an Ellipse and a Cross image.
                if ((tmp_oButton.IsPressed) && (tmp_oButton.IsHot))
                {
                    graphics.FillEllipse(Brushes.DarkRed, new QPadding(2, 2, 1, 1).InflateRectangleWithPadding(tmp_oButton.Bounds, true, true));
                    QControlPaint.DrawImage(QTabCloseButton.CloseMask, Color.Red, Color.White, QImageAlign.Centered, tmp_oButton.Bounds, QTabCloseButton.CloseMask.Size, graphics);
                }
                else if (tmp_oButton.IsHot)
                {
                    graphics.FillEllipse(Brushes.Red, new QPadding(2,2,1,1).InflateRectangleWithPadding(tmp_oButton.Bounds, true, true));
                    QControlPaint.DrawImage(QTabCloseButton.CloseMask, Color.Red, Color.White, QImageAlign.Centered, tmp_oButton.Bounds, QTabCloseButton.CloseMask.Size, graphics);
                }
                else
                {
                    QControlPaint.DrawImage(QTabCloseButton.CloseMask, Color.Red, Color.FromArgb(50,50,50), QImageAlign.Centered, tmp_oButton.Bounds, QTabCloseButton.CloseMask.Size, graphics);
                }

            }

        }
    }
}
