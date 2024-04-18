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
    public partial class ActionChooser : GenericControl
    {
        private int userID=0;
        TableLayoutPanel actionboard;
        Button b_register;
        Button b_addtogroup;
        Button b_childsearch;
        public ActionChooser()  
        {
            InitializeComponent();
            actionboard = new TableLayoutPanel();
            actionboard.RowCount = 3;
            actionboard.ColumnCount = 1;
            actionboard.Parent = this;
            actionboard.Width = Width;
            actionboard.Height = Height - 50;
            actionboard.Top = 0;
            actionboard.Left = 0;
           


            b_addtogroup = new Button() { Text = "Zapsat do kroužku", Name = "b_addtogroup", Visible = true };
            b_register = new Button() { Text = "Zaregistrovat", Name = "b_register", Visible = true };
            b_childsearch = new Button() { Text = "Vyhledat", Name = "b_addtogroup", Visible = true };

            actionboard.Controls.Add(b_register, 0, 0);
            actionboard.Controls.Add(b_addtogroup, 0, 1);
            actionboard.Controls.Add(b_childsearch, 0, 2);



            b_addtogroup.Click += B_addtogroup_Click;
            b_register.Click += B_register_Click;
            b_childsearch.Click += B_childsearch_Click;
        }

        private void B_childsearch_Click(object sender, EventArgs e)
        {
            ControlClicked(userID, 3);
        }

        private void B_register_Click(object sender, EventArgs e)
        {
            ControlClicked(userID, 1);
        }

        private void B_addtogroup_Click(object sender, EventArgs e)
        {
            ControlClicked(userID, 2);
        }

        public void SetUID(int userid)
        {
            userID = userid;
            b_register.Enabled = b_addtogroup.Enabled = userid == 0 ? true : false;
        }

        private void ActionChooser_Load(object sender, EventArgs e)
        {
            this.Size = this.Parent.Size;
           
        }
    }
}
