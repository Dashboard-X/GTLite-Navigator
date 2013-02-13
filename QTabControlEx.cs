using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Qios.DevSuite.Components;


namespace QAdvancedTabControlSample
{

    /// <summary>
    /// This class overrides the original QTabControl to add a close button to every tab. It does this by setting a QTabStripPainterEx to every tabStrip
    /// that will be responsible for drawing the button. Besides that it adds a QTabCloseButton to every tabPage for the administrative purposes of the
    /// close button.
    /// </summary>
    public partial class QTabControlEx : QTabControl
    {

        /// <summary>
        /// Overridden. Creates a QTabStrip. It sets the painter and adjusts soms configuration.
        /// </summary>
        protected override QTabStrip CreateTabStrip(DockStyle dock)
        {

            QTabStrip tmp_oTabStrip = base.CreateTabStrip(dock);

            //Note: we only do the Top docked strip. But you can do the same for every TabStrip.
            if (dock == DockStyle.Top)
            {
                tmp_oTabStrip.Painter = new QTabStripPainterEx();
                tmp_oTabStrip.Configuration.ButtonConfiguration.Padding = new QPadding(3, 1, 1, 13);
            }
            return tmp_oTabStrip;
        }


        /// <summary>
        /// Handles the add of a control. It sets a QTabCloseButton to the Tag property to maintain the states and locations of the close buttons.
        /// </summary>
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (e.Control is QTabPage)
            {
                if (e.Control.Tag == null)
                {
                    e.Control.Tag = new QTabCloseButton(e.Control as QTabPage);
                }
            }
        }


        /// <summary>
        /// Handles a mouse move, it adjusts the hot states of the close buttons.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            try
            {
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    QTabCloseButton tmp_oCloseButton = this.Controls[i].Tag as QTabCloseButton;
                    if (tmp_oCloseButton != null)
                    {
                        if ((tmp_oCloseButton.Bounds.Contains(e.Location)) && (!tmp_oCloseButton.IsHot))
                        {
                            this.Invalidate();
                            tmp_oCloseButton.AddState(QItemStates.Hot);
                        }
                        else if ((!tmp_oCloseButton.Bounds.Contains(e.Location)) && (tmp_oCloseButton.IsHot))
                        {
                            this.Invalidate();
                            tmp_oCloseButton.RemoveState(QItemStates.Hot);
                        }
                    }
                }
            }
            catch { }            
        }

        /// <summary>
        /// Handles a MouseDown, it adjusts the pressed state of the close buttons.
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            try
            {
                bool tmp_bCallBase = true;
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    QTabCloseButton tmp_oCloseButton = this.Controls[i].Tag as QTabCloseButton;
                    if (tmp_oCloseButton != null)
                    {
                        if (tmp_oCloseButton.Bounds.Contains(e.Location))
                        {
                            tmp_bCallBase = false;
                            tmp_oCloseButton.AddState(QItemStates.Pressed);
                        }
                    }
                }

                if (tmp_bCallBase)
                {
                    base.OnMouseDown(e);
                }
                this.Invalidate();
            }
            catch { }
        }

        /// <summary>
        /// Handles the MouseUp. if the button was pressed and the mouse is still above the button, it is clicked.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            try
            {
                bool tmp_bCallBase = true;
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    QTabCloseButton tmp_oCloseButton = this.Controls[i].Tag as QTabCloseButton;
                    if (tmp_oCloseButton != null)
                    {

                        if ((tmp_oCloseButton.IsPressed) &&
                            (tmp_oCloseButton.Bounds.Contains(e.Location)))
                        {
                            tmp_bCallBase = false;
                            this.HandleCloseButtonClick(tmp_oCloseButton);
                        }

                        //Remove the pressed of every button. The mouse might moved towards another button while it was down.
                        tmp_oCloseButton.RemoveState(QItemStates.Pressed);
                    }
                }

                if (tmp_bCallBase)
                {
                    base.OnMouseUp(e);
                }
                this.Invalidate();
            }
            catch { }
        }

        /// <summary>
        /// Handles a complete mouse leave, undoes all the hot states.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            try
            {
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    QTabCloseButton tmp_oCloseButton = this.Controls[i].Tag as QTabCloseButton;
                    if (tmp_oCloseButton != null) tmp_oCloseButton.RemoveState(QItemStates.Hot);
                }
                base.OnMouseLeave(e);
            }
            catch { }
        }

        /// <summary>
        /// Is called when the CloseButton is clicked.
        /// </summary>
        private void HandleCloseButtonClick(QTabCloseButton button)
        {
            button.TabPage.Close();
        }
    }
}

