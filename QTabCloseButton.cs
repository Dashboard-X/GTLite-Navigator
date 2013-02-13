using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;

using Qios.DevSuite.Components;

namespace QAdvancedTabControlSample
{

    /// <summary>
    /// This is the Close button for the tabs. It contains the information needed to draw the button.
    /// </summary>
    public class QTabCloseButton
    {

        /// <summary>
        /// Contains the CloseMask. (Cross image)
        /// </summary>
        public static Image CloseMask { get; private set; }

        /// <summary>
        /// Contains the TabPage
        /// </summary>
        public QTabPage TabPage { get; private set; }

        /// <summary>
        /// Contains the Bounds of the QTabCloseButton
        /// </summary>
        public Rectangle Bounds { get; set; }

        /// <summary>
        /// Contains the states of the button.
        /// </summary>
        public QItemStates States { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public QTabCloseButton(QTabPage tabPage)
        {
            this.TabPage = tabPage;

            if (CloseMask == null)
            {
                CloseMask = global::GTLite.Properties.Resources.CloseMask;
            }
        }

        /// <summary>
        /// Adds a state.
        /// </summary>
        public void AddState(QItemStates state)
        {
            this.States = QItemStatesHelper.AdjustState(this.States, state, true);
        }

        /// <summary>
        /// Removes a state
        /// </summary>
        public void RemoveState(QItemStates state)
        {
            this.States = QItemStatesHelper.AdjustState(this.States, state, false);
        }

        /// <summary>
        /// Returns whether this button is hot.
        /// </summary>
        public bool IsHot
        {
            get { return QItemStatesHelper.IsHot(this.States); }
        }

        
        /// <summary>
        /// Returns whether this button is pressed.
        /// </summary>
        public bool IsPressed
        {
            get { return QItemStatesHelper.IsPressed(this.States); }
        }
    }
}
