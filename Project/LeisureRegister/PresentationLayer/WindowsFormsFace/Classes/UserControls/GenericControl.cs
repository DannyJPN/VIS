using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer.BusinessLayerLib.Classes.DataContainers;

namespace WindowsFormsFace.Classes.UserControls
{
    public partial class GenericControl : UserControl
    {
        public GenericControl()
        {
            InitializeComponent();
           
        }

        private void GenericControl_Load(object sender,EventArgs e)
        {
            this.Size = this.Parent.Size;
        }


        protected virtual void ControlClicked()
        {
            if (ControlChosen != null)
            {
                ControlChosen(this, new RegisterControlEventArgs());

            }

        }
        protected virtual void ControlClicked(int uid)
        {
            if (ControlChosen != null)
            {
                ControlChosen(this, new RegisterControlEventArgs(uid));

            }

        }
        protected virtual void ControlClicked(int uid,int actid)
        {
            if (ControlChosen != null)
            {
                ControlChosen(this, new RegisterControlEventArgs(uid,actid));

            }

        }
        protected virtual void ControlClicked(int uid, int actid,Child defaultchild)
        {
            if (ControlChosen != null)
            {
                ControlChosen(this, new RegisterControlEventArgs(uid, actid,defaultchild));

            }

        }


        public event EventHandler<RegisterControlEventArgs> ControlChosen;

      
    }

    public class RegisterControlEventArgs : EventArgs
    {
        public int userid = 0;
        public int actionid;
       public Child defaultchild;

        public RegisterControlEventArgs() { }
        public RegisterControlEventArgs(int userID)
        { userid = userID; }
        public RegisterControlEventArgs(int userID,int actionID):this(userID)
        { actionid = actionID; }
        public RegisterControlEventArgs(int userID, int actionID,Child def) : this(userID,actionID)
        { defaultchild = def; }





    }
}
