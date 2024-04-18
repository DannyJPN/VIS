using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using BusinessLayer.BusinessLayerLib.Classes.DataContainers;
using System.Collections.ObjectModel;

namespace WindowsFormsFace.Classes.UserControls
{
    public partial class ChildSearcher : SearchControl
    {

        
        public ChildSearcher()
        {
            InitializeComponent();
            Type t = typeof(Child);
            PropertyInfo[] props = t.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (PropertyInfo p in props)
            {
                if (!p.PropertyType.IsSubclassOf(typeof(LeisureObject)))
                {
                    tb_crits.Add(new TextBox() { Visible = true, AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = String.Format("search_tb_{0}", p.Name) });
                    lb_crits.Add(new Label() { Visible = true, Text = p.Name, Name = String.Format("search_lb_{0}", p.Name) });

                }
            }
           
            searchboard.ColumnCount = tb_crits.Count;
            searchboard.Controls.Add(lb_controlname, 0, 0);
            searchboard.Controls.Add(b_searcher, 0, 1);
            for (int i = 0; i < tb_crits.Count; i++)
            {
                searchboard.Controls.Add(lb_crits[i], i, 2);
                searchboard.Controls.Add(tb_crits[i], i, 3);
                dgw_searchresult.Columns.Add(lb_crits[i].Text, lb_crits[i].Text);
            }
           
            b_searcher.Click += B_searcher_Click;
        }

        private List<Child> FilterChildren(List<Child> children)
        {

            if (children == null)
            {
                return new List<Child>();
            }
            List<Child> allowedchildren = new List<Child>();
            if (userID == 0)
            {
                allowedchildren = children;
            }

            else
            {
                List<GroupLeading> ledgroups = GroupLeading.Load(GroupLeading.CreateInstance(0, GroupLeader.Load(userID), null, new DateTime(), new DateTime()));

                List<GroupMembership> membs = new List<GroupMembership>();
                for (int i = 0; i < ledgroups.Count; i++)
                {
                    membs.AddRange(GroupMembership.Load(GroupMembership.CreateInstance(0, null, ledgroups[i].Group, new DateTime(), new DateTime())));

                }
                List<Child> taughtchildren = new List<Child>();
                for (int i = 0; i < membs.Count; i++)
                {
                    taughtchildren.Add(membs[i].Member);
                }



                for (int i = 0; i < children.Count; i++)
                {

                    if (taughtchildren.Contains(children[i]))
                    { allowedchildren.Add(children[i]); }
                    else
                    {
                        Child c = Child.CreateInstance(children[i].ID, children[i].Name, children[i].Surname, "", 0);

                        allowedchildren.Add(c);
                    }


                }
                


            }

            return allowedchildren;
        }

        private void B_searcher_Click(object sender, EventArgs e)
        {
            Child critchild = Child.CreateInstance(0);
            Type t = critchild.GetType();
            PropertyInfo[] props = t.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public );

            foreach (PropertyInfo p in props)
            {
                if (!p.PropertyType.IsSubclassOf(typeof(LeisureObject)))
                {

                    string tbname = String.Format("search_tb_{0}", p.Name);
                    string valtext = searchboard.Controls[tbname].Text;
                    if (valtext != "")
                    { p.SetValue(critchild, Convert.ChangeType(valtext, p.PropertyType)); }
                }
            }

            List<Child> children = Child.Load(critchild);


           

            bool nonexact_search = false;
            if (children==null)
            {
                DialogResult dr = MessageBox.Show("Chcete vyhledat děti,které splńují i jednotlivé parametry?","Žádné děti nebyly nalezeny",MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    children = Child.Load(critchild, nonexact_search); 
                }
            }
            List<Child> allowedchildren = FilterChildren(children);

            dgw_searchresult.Rows.Clear();

            Type itemtype = typeof(Child);
                PropertyInfo[] properts = t.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public);

               
                for (int i = 0; i < allowedchildren.Count; i++)
                {
                   

                    DataGridViewRow dgwrow = new DataGridViewRow();
                    foreach (PropertyInfo p in properts)
                    {
                        if (!p.PropertyType.IsSubclassOf(typeof(LeisureObject)))
                        {

                            dgwrow.Cells.Add(new DataGridViewTextBoxCell() { Value=p.GetValue(allowedchildren[i]).ToString()});
                            
                        
                        }
                    }

                    
                    dgw_searchresult.Rows.Add(dgwrow);
                    
                    dgw_searchresult.Rows[i].Tag = allowedchildren[i];

                }  //MessageBox.Show(String.Format("{0} {1}",dgw_searchresult.Width,searchboard.Width));
                dgw_searchresult.Width = searchboard.Width-50;
            

        }

        public void SetUID(int userid)
        {
            userID = userid;
        }

        private void ChildSearcher_Load(object sender, EventArgs e)
        {
            this.Size = this.Parent.Size;
            searchboard.Width = Width;
            searchboard.Height = Height / 6;
            
        }
    }
}
