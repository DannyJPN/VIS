using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsFace.Classes.UserControls
{
    public partial class UserChooser : GenericControl
    {
        private Button normal_user = new Button();
        private Button elevated_user = new Button();

        public UserChooser() 
        {
            InitializeComponent();

            normal_user.Parent = elevated_user.Parent = this;
            normal_user.Width = elevated_user.Width = 100;
            normal_user.Height = elevated_user.Height = 50;

            normal_user.Left = elevated_user.Left = this.Width/2-normal_user.Width/2;
            normal_user.Top = this.Height / 2 - normal_user.Height;
            elevated_user.Top = this.Height / 2 + elevated_user.Height;
            normal_user.Visible = elevated_user.Visible = true;

            normal_user.Text = "Normální uživatel";
elevated_user.Text = "Uživatel s vyššími právy";

            normal_user.Click += Normal_user_Click;
            elevated_user.Click += Elevated_user_Click;
        }

     

        private void Normal_user_Click(object sender, EventArgs e)
        {
            ControlClicked(1);
        }
        private void Elevated_user_Click(object sender, EventArgs e)
        {
            ControlClicked(0);
        }

        private void UserChooser_Load(object sender, EventArgs e)
        {
            this.Size = this.Parent.Size;
        }
    }
}
