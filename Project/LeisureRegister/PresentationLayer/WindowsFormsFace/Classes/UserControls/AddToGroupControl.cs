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
    public partial class AddToGroupControl : GenericControl
    {
        private TableLayoutPanel addingboard;

        private Label lb_controlname, lb_child, lb_grouplist, lb_chosengrouplist;
        private ListBox lisb_grouplist, lisb_chosengrouplist;
        private ComboBox cb_seasonselector;
        private ComboBox cb_child;
        private Button b_save, b_add, b_addall, b_remove, b_removeall;
        private Child defaultkid;
        private Child chosenchild;

        private void AddToGroupControl_Load(object sender, EventArgs e)
        {
            this.Size = this.Parent.Size;
        }


        public AddToGroupControl()

        {

            InitializeComponent();
            defaultkid = Child.CreateInstance();


            addingboard = new TableLayoutPanel();
            addingboard.RowCount = 11;
            addingboard.ColumnCount = 5;
            addingboard.Parent = this;
            addingboard.Width = ClientSize.Width;
            addingboard.Height = ClientSize.Height - 50;
            addingboard.Top = 0;
            addingboard.Left = 0;

            cb_seasonselector = new ComboBox() { Name = "cb_seasonselector" };
            for (int i = 2016; i < DateTime.Now.Year; i++)
            {
                cb_seasonselector.Items.Add(String.Format("{0}/{1}", i, i + 1));
            }

            lb_controlname = new Label() { Text = "Přidání dítěte do kroužků", Name = "lb_controlname" };
            lb_child = new Label() { Text = "Dítě", Name = "lb_child" };
            lb_grouplist = new Label() { Text = "Dostupné kroužky", Name = "lb_grouplist" };
            lb_chosengrouplist = new Label() { Text = "Zapsané kroužky", Name = "lb_chosengrouplist" };
            lisb_chosengrouplist = new ListBox() { Text = "", Name = "lisb_chosengrouplist" };
            lisb_grouplist = new ListBox() { Text = "", Name = "lisb_grouplist" };
            cb_child = new ComboBox() { Text = "", Name = "cb_child" };
            b_save = new Button() { Text = "Uložit", Name = "b_save" };
            b_add = new Button() { Text = ">", Name = "b_add" };
            b_addall = new Button() { Text = ">>", Name = "b_addall" };
            b_remove = new Button() { Text = "<", Name = "b_remove" };
            b_removeall = new Button() { Text = "<<", Name = "b_removeall" };

            b_save.Click += B_save_Click;
            //addingboard.Controls.Add(null, 0, 0);
            addingboard.Controls.Add(lb_child, 1, 1);
            addingboard.Controls.Add(cb_child, 1, 2);
            //addingboard.Controls.Add(null, 0, 3);
            //addingboard.Controls.Add(null, 0, 4);
            //addingboard.Controls.Add(null, 0, 5);
            //addingboard.Controls.Add(null, 0, 6);
            //addingboard.Controls.Add(null, 0, 7);
            //addingboard.Controls.Add(null, 0, 8);
            //addingboard.Controls.Add(null, 0, 9);
            //addingboard.Controls.Add(null, 0, 10);

            addingboard.Controls.Add(lb_controlname, 2, 0);
            addingboard.Controls.Add(lisb_grouplist, 2, 1);
            //addingboard.Controls.Add(null, 1, 2);
            //addingboard.Controls.Add(null, 1, 3);
            //addingboard.Controls.Add(null, 1, 4);
            //addingboard.Controls.Add(null, 1, 5);
            //addingboard.Controls.Add(null, 1, 6);
            //addingboard.Controls.Add(null, 1, 7);
            //addingboard.Controls.Add(null, 1, 8);
            //addingboard.Controls.Add(null, 1, 9);
            //addingboard.Controls.Add(null, 1, 10);

            //addingboard.Controls.Add(null, 2, 0);
            //addingboard.Controls.Add(null, 2, 1);
            addingboard.Controls.Add(b_add, 2, 2);
            //addingboard.Controls.Add(null, 2, 3);
            addingboard.Controls.Add(b_addall, 3, 4);
            //addingboard.Controls.Add(null, 2, 5);
            addingboard.Controls.Add(b_remove, 3, 6);
            //addingboard.Controls.Add(null, 2, 7);
            addingboard.Controls.Add(b_removeall, 3, 8);
            //addingboard.Controls.Add(null, 2, 9);
            addingboard.Controls.Add(b_save, 3, 10);

            addingboard.Controls.Add(lb_chosengrouplist, 4, 0);
            addingboard.Controls.Add(lisb_chosengrouplist, 4, 1);
            //addingboard.Controls.Add(null, 3, 2);
            //addingboard.Controls.Add(null, 3, 3);
            //addingboard.Controls.Add(null, 3, 4);
            //addingboard.Controls.Add(null, 3, 5);
            //addingboard.Controls.Add(null, 3, 6);
            //addingboard.Controls.Add(null, 3, 7);
            //addingboard.Controls.Add(null, 3, 8);
            //addingboard.Controls.Add(null, 3, 9);

            addingboard.SetRowSpan(lisb_chosengrouplist, 8);
            addingboard.SetRowSpan(lisb_grouplist, 8);
            addingboard.SetRowSpan(cb_child, 8);

            cb_child.SelectedIndexChanged += Cb_child_SelectedIndexChanged;
            cb_seasonselector.SelectedIndexChanged += Cb_seasonselector_SelectedIndexChanged;
            b_add.Click += B_add_Click;
            b_addall.Click += B_addall_Click;
            b_remove.Click += B_remove_Click;
            b_removeall.Click += B_removeall_Click;
            LoadChildren();
            LoadChosenGroups(defaultkid);
            LoadAvailableGroups();

        }

        private void B_save_Click(object sender, EventArgs e)
        {
            List<HobbyGroup> chosen = new List<HobbyGroup>();
            List<HobbyGroup> filled = new List<HobbyGroup>();
            List<HobbyGroup> duplicit_memberships = new List<HobbyGroup>();
            List<HobbyGroup> outofrange = new List<HobbyGroup>();
            List<HobbyGroup> addfailed = new List<HobbyGroup>();


            foreach (HobbyGroup hg in lisb_chosengrouplist.Items)
            {
                chosen.Add(hg);

            }

            foreach (HobbyGroup hg in chosen)
            {
                //Already filled?
                GroupMembership crit = GroupMembership.CreateInstance();
                crit.Group = hg;
                List<GroupMembership> actualmembers = GroupMembership.Load(crit);
                int actualcapacity = actualmembers == null ? 0 : actualmembers.Count;
                if (actualcapacity >= hg.Max)
                {
                    filled.Add(hg);
                 
                }
                //already a member?
                foreach (GroupMembership gm in actualmembers)
                {
                    if (gm.Member.ID == chosenchild.ID)
                    {

                        duplicit_memberships.Add(hg);
                       
                    }
                }
                

                //age borders
                if (DateTime.Now.Year - chosenchild.BirthDate.Year < hg.MinAge || DateTime.Now.Year - chosenchild.BirthDate.Year > hg.MaxAge)
                {
                    outofrange.Add(hg);
                    
                }

            }
            foreach (HobbyGroup gm in filled)
            {
                chosen.Remove(gm);
            }

            foreach (HobbyGroup gm in duplicit_memberships)
            {
                chosen.Remove(gm);
            }
            foreach (HobbyGroup gm in outofrange)
            {
                chosen.Remove(gm);
            }



            //informing user
            if (filled.Count > 0)
            {
                StringBuilder sb = new StringBuilder("Tyto kroužky jsou již přeplněné: \n");
                foreach (HobbyGroup hg in filled)
                {
                    sb.Append(String.Format("{0}\n", hg.Name));
                }
                addfailed.AddRange(filled);
                MessageBox.Show(sb.ToString(), "Pozor,přeplnění", MessageBoxButtons.OK);



            }

            if (duplicit_memberships.Count > 0)
            {

                StringBuilder sb = new StringBuilder("Tyto kroužky již dítě navštěvuje: \n");
                foreach (HobbyGroup hg in duplicit_memberships)
                {
                    sb.Append(String.Format("{0}\n", hg.Name));
                }
                addfailed.AddRange(duplicit_memberships);
                MessageBox.Show(sb.ToString(), "Tady už dítě je", MessageBoxButtons.OK);

            }
            //choosing from failed age check
            if (outofrange.Count > 0)
            {
                Form additional = new Form();
                addingboard.Parent = this;
                addingboard.Width = addingboard.Height = 300;
                ListBox lisb_adder = new ListBox() { Top=0,Left=0,Width = 100,Height = 200,Parent=additional,Name="f_additional"};
                foreach (HobbyGroup hg in outofrange)
                { lisb_adder.Items.Add(hg); }
                Button b_confirm = new Button() { Text="Uložit",Top = lisb_adder.Bottom+10,Left = 0,Parent=additional};
                b_confirm.Click += delegate(object send,EventArgs ea) 
                {
                    foreach (HobbyGroup selitem in lisb_adder.SelectedItems)
                    {
                        chosen.Add(selitem);
                        //lisb_adder.Items.Remove(selitem);
                    }
                    foreach (HobbyGroup hg in lisb_adder.Items)
                    { addfailed.Add(hg);  }
                    additional.Close();
                };
                additional.Show();                
            }

            //writing adds
            if (chosen.Count > 0)
            {
                DateTime from = DateTime.Now;
                DateTime to = from.AddMonths(6);
                foreach (HobbyGroup hg in chosen)
                {
                    GroupMembership gm =GroupMembership.CreateInstance(0, chosenchild, hg, from, to);
                    GroupMembership.Save(gm);
                }
            }
            chosen.Clear();
            //additional offer
            if (addfailed.Count > 0)
            {
                HobbyGroup hgcrit = HobbyGroup.CreateInstance();
                hgcrit.SeasonOfExistence = cb_seasonselector.SelectedText;
                List<HobbyGroup> nextgroups = HobbyGroup.Load(hgcrit);
                List<HobbyGroup> bad = new List<HobbyGroup>();
                foreach (HobbyGroup hg in nextgroups)
                {
                    //Already filled?
                    GroupMembership crit = GroupMembership.CreateInstance();
                    crit.Group = hg;
                    List<GroupMembership> actualmembers = GroupMembership.Load(crit);
                   
                    int actualcapacity = actualmembers == null ? 0 : actualmembers.Count;
                    if (actualcapacity >= hg.Max)
                    {
                        bad.Add(hg);
                        
                    }
                    //already a member?
                    foreach (GroupMembership gm in actualmembers)
                    {
                        if (gm.Member.ID == chosenchild.ID)
                        {
                            bad.Add(hg);
                        }
                    }
                    //age borders
                    if (DateTime.Now.Year - chosenchild.BirthDate.Year < hg.MinAge || DateTime.Now.Year - chosenchild.BirthDate.Year > hg.MaxAge)
                    { bad.Add(hg);
                    }

                }
                foreach (HobbyGroup hg in bad)
                { nextgroups.Remove(hg); };


                if(nextgroups.Count >0)
                {

                    Form additional = new Form();
                    addingboard.Parent = this;
                    addingboard.Width = addingboard.Height = 300;
                    ListBox lisb_adder = new ListBox() { Top = 0, Left = 0, Width = 100, Height = 200, Parent = additional, Name = "f_additional" };
                    foreach (HobbyGroup hg in nextgroups)
                    { lisb_adder.Items.Add(hg); }
                    Button b_confirm = new Button() { Text = "Uložit", Top = lisb_adder.Bottom + 10, Left = 0, Parent = additional };
                    b_confirm.Click += delegate (object send, EventArgs ea)
                    {
                        foreach (HobbyGroup selitem in lisb_adder.SelectedItems)
                        { chosen.Add(selitem); lisb_adder.Items.Remove(selitem); }
                        foreach (HobbyGroup hg in lisb_adder.Items)
                        { addfailed.Add(hg); }
                        additional.Close();
                    };
                    additional.Show();

                }
                //writing adds
                if (chosen.Count > 0)
                {
                    DateTime from = DateTime.Now;
                    DateTime to = from.AddMonths(6);
                    foreach (HobbyGroup hg in chosen)
                    {
                        GroupMembership gm = GroupMembership.CreateInstance(0, chosenchild, hg, from, to);
                        GroupMembership.Save(gm);
                    }
                }



            }


        }

        

        private void B_removeall_Click(object sender, EventArgs e)
        {
            lisb_grouplist.Items.AddRange(lisb_grouplist.Items);
            lisb_chosengrouplist.Items.Clear();
        }

        private void B_remove_Click(object sender, EventArgs e)
        {
            if (lisb_chosengrouplist.SelectedIndex < 0)
            { return; }
            lisb_grouplist.Items.Add(lisb_chosengrouplist.SelectedItem);
            lisb_chosengrouplist.Items.RemoveAt(lisb_chosengrouplist.SelectedIndex);

        }

        private void B_addall_Click(object sender, EventArgs e)
        {
            lisb_chosengrouplist.Items.AddRange(lisb_grouplist.Items);
            lisb_grouplist.Items.Clear();
        }

        private void B_add_Click(object sender, EventArgs e)
        {
            if (lisb_grouplist.SelectedIndex < 0)
            { return; }
            lisb_chosengrouplist.Items.Add(lisb_grouplist.SelectedItem);
            lisb_grouplist.Items.RemoveAt(lisb_grouplist.SelectedIndex);

        }

        private void Cb_seasonselector_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAvailableGroups();
        }

        private void LoadAvailableGroups()
        {
            lisb_grouplist.Items.Clear();
            HobbyGroup avail = HobbyGroup.CreateInstance();
            avail.SeasonOfExistence = cb_seasonselector.SelectedText;

            List<HobbyGroup> availgroups = HobbyGroup.Load(avail);
            foreach (HobbyGroup hg in availgroups)
            {
                GroupLeading crit = GroupLeading.CreateInstance();
                crit.Group = hg;
                List<GroupLeading> leadings = GroupLeading.Load(crit);

                if (hg.Max > 0 && leadings != null)
                    lisb_grouplist.Items.Add(hg);

            }
        }

        private void LoadChosenGroups(Child ch)
        {
            lisb_chosengrouplist.Items.Clear();
            if (ch == null) { return; }
            GroupMembership crit = GroupMembership.CreateInstance();
            crit.Member = ch;
            List<GroupMembership> memberships = GroupMembership.Load(crit);
            foreach (GroupMembership mem in memberships)
            {
                lisb_chosengrouplist.Items.Add(mem.Group);
            }
        }
        private void Cb_child_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] selectedprops = cb_child.SelectedItem.ToString().Split(' ');
            Child tagchild = Child.CreateInstance();
            tagchild.RegistrationNumber = Convert.ToInt32(selectedprops[0]);
            tagchild.Surname = selectedprops[1];
            tagchild.Name = selectedprops[2];


            chosenchild = Child.Load(tagchild)[0];
            LoadChosenGroups(chosenchild);
        }

        private void LoadChildren()
        {
            List<Child> children = Child.Load(null);
            foreach (Child child in children)
            {
                cb_child.Items.Add(child);
                if (child.RegistrationNumber == defaultkid.RegistrationNumber)
                {
                    cb_child.SelectedItem = child;
                }
            }


        }

        public void SetDefaultChild(Child defaultchild)
        {
            defaultkid = defaultchild;
            cb_child.SelectedItem = defaultkid;
        }
    }
}
