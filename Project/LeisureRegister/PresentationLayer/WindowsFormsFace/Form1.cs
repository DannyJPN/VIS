using BusinessLayer.BusinessLayerLib.Classes.DataContainers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsFace.Classes.UserControls;

namespace PresentationLayer.WindowsFormsFace
{
    public partial class Form1 : Form
    {
private        GenericControl act_control=null;
        public Panel displayer = new Panel();
        
        private ActionChooser actioncontrol      = new ActionChooser();
        private ChildSearcher childsearchcontrol = new ChildSearcher();
        private UserChooser usercontrol          = new UserChooser();
        private RegisterControl registercontrol  = new RegisterControl();
        private AddToGroupControl addtogroupcontrol = new AddToGroupControl();

        public Form1()
        {
            
            InitializeComponent();

           

           
            actioncontrol.ControlChosen += Actioncontrol_ControlChosen;
            childsearchcontrol.ControlChosen += Childsearchcontrol_ControlChosen;
            registercontrol.ControlChosen += Registercontrol_ControlChosen;
            usercontrol.ControlChosen += Usercontrol_ControlChosen;
            addtogroupcontrol.ControlChosen += Addtogroupcontrol_ControlChosen;
            Width = 1500;
            Height = 800;
            actioncontrol.Width = Width;
            childsearchcontrol.Width = Width;
            usercontrol.Width = Width;
            registercontrol.Width = Width;
            addtogroupcontrol.Width = Width;

            actioncontrol.Height = Height;
            childsearchcontrol.Height = Height;
            usercontrol.Height = Height;
            registercontrol.Height = Height;
            addtogroupcontrol.Height = Height;


        }

        private void Addtogroupcontrol_ControlChosen(object sender, RegisterControlEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Usercontrol_ControlChosen(object sender, RegisterControlEventArgs e)
        {
            
            actioncontrol.SetUID(e.userid);
            SetActiveControl(actioncontrol);
        }

        private void Registercontrol_ControlChosen(object sender, RegisterControlEventArgs e)
        {
            switch (e.actionid)
            {
                case 2:
                    {
                        addtogroupcontrol.SetDefaultChild(e.defaultchild);
                        SetActiveControl(addtogroupcontrol);
                        break;
                    }
                case 3:
                    {
                        childsearchcontrol.SetUID(e.userid);
                        SetActiveControl(childsearchcontrol);
                        break;
                    }


            }
        }

        private void Childsearchcontrol_ControlChosen(object sender, RegisterControlEventArgs e)
        {
                



        }

        private void Actioncontrol_ControlChosen(object sender, RegisterControlEventArgs e)
        {
            switch (e.actionid)
            {
                case 1:
                    {

                        SetActiveControl(registercontrol);
                        break;
                    }
                case 2:
                    {

                        SetActiveControl(addtogroupcontrol);
                        break;
                    }
                case 3:
                    {
                       childsearchcontrol.SetUID(e.userid);
                        SetActiveControl(childsearchcontrol);
                        break;
                    }

                default: { break; }

            }
        }

        private void SetActiveControl(GenericControl control)
        {
            displayer.Controls.Clear();
            control.Size = displayer.Size;
            displayer.Controls.Add(control);

            control.Dock = DockStyle.Fill;
            act_control = control;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            displayer.Name = "displayer";
            displayer.Parent = this;
            displayer.Top = 0;
            displayer.Left = 0;
            displayer.Width = ClientSize.Width;
            displayer.Height = ClientSize.Height ;
            displayer.Visible = true;
            displayer.BorderStyle = BorderStyle.FixedSingle;
            SetActiveControl(usercontrol);
        }
    }
}
