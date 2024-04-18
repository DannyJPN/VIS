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
using System.Reflection;
using System.IO;
using System.Security;
using BusinessLayer.BusinessLayerLib.Classes.Other;

namespace WindowsFormsFace.Classes.UserControls
{
    public partial class RegisterControl : GenericControl
    {
        private TableLayoutPanel registerboard;
        private TextBox tb_childname, tb_childsurname, tb_schoolname, tb_regnum, tb_street, tb_orientnum,
            tb_descriptnum, tb_city, tb_citypart, tb_healthstate, tb_comments, tb_postalcode;
        private TextBox tb_mothername, tb_mothersurname, tb_motherjob, tb_motheremail, tb_motherphone;
        private TextBox tb_fathername, tb_fathersurname, tb_fatherjob, tb_fatheremail, tb_fatherphone;
        private Label lb_childname, lb_childsurname, lb_schoolname, lb_regnum, lb_street, lb_orientnum, lb_descriptnum, lb_city, lb_citypart, lb_healthstate, lb_comments, lb_postalcode;
        private ListBox lisb_exporter;
        private void RegisterControl_Load(object sender, EventArgs e)
        {
            this.Size = this.Parent.Size;
        }

        private Label lb_mothername, lb_mothersurname, lb_motherjob, lb_motheremail, lb_motherphone;
        private Label lb_fathername, lb_fathersurname, lb_fatherjob, lb_fatheremail, lb_fatherphone;
        private Label lb_controlname, lb_mother, lb_father, lb_child;
        private Button b_save, b_saveandadd;
        private Label lb_childemail;
        private DateTimePicker dtp_birthdate;
        private Label lb_birthdate;
        private TextBox tb_childemail;
        private Label lb_childphone;
        private TextBox tb_childphone;
        private CheckBox chb_photoagree;
        private Label lb_insurcompany;
        private TextBox tb_insurcompany;
        private CheckBox chb_gpdragree;
        private CheckBox chb_newsletteragree;
        private CheckBox chb_leavingalone;
        private Child regchild;
        public RegisterControl() 
        {
            InitializeComponent();
            registerboard = new TableLayoutPanel();
            registerboard.RowCount = 28;
            registerboard.ColumnCount = 4;
            registerboard.Parent = this;
            registerboard.Width = ClientSize.Width;
            registerboard.Height = ClientSize.Height - 50;
            registerboard.Top = 0;
            registerboard.Left = 0;

            lisb_exporter = new ListBox() { Name = "lisb_exporter",Parent = this};
          
            

            
            tb_childname = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_name" };
            tb_childsurname = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_surname" };
            tb_schoolname = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_schoolname" };
            tb_regnum = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_registrationnumber" };
            tb_street = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_street" };
            tb_orientnum = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_orientationnumber" };
            tb_descriptnum = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_descriptionnumber" };
            tb_city = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_cityname" };
            tb_citypart = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_citypartname" };
            tb_healthstate = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_healthstate" };
            tb_comments = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_comments" };
            tb_postalcode = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_postalcode" };
            tb_mothername = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_mothername" };
            tb_mothersurname = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_mothersurname" };
            tb_motherjob = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_motherjob" };
            tb_motheremail = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_motheremail" };
            tb_motherphone = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_motherphone" };
            tb_fathername = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_fathername" };
            tb_fathersurname = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_fathersurname" };
            tb_fatherjob = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_fatherjob" };
            tb_fatheremail = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_fatheremail" };
            tb_fatherphone = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_fatherphone" };
            lb_childname = new Label() { Text = "Jméno", Name = "lb_name" };
            lb_childsurname = new Label() { Text = "Příjmení", Name = "lb_surname" };
            lb_schoolname = new Label() { Text = "Název školy", Name = "lb_schoolname" };
            lb_regnum = new Label() { Text = "Registrační číslo", Name = "lb_registrationnumber" };
            lb_street = new Label() { Text = "Ulice", Name = "lb_street" };
            lb_orientnum = new Label() { Text = "Číslo orientační", Name = "lb_orientationnumber" };
            lb_descriptnum = new Label() { Text = "Číslo popisné", Name = "lb_descriptionnumber" };
            lb_city = new Label() { Text = "Město", Name = "lb_cityname" };
            lb_citypart = new Label() { Text = "Část města (existuje-li)", Name = "lb_citypartname" };
            lb_healthstate = new Label() { Text = "Zdravotní stav", Name = "lb_healthstate" };
            lb_comments = new Label() { Text = "Vaše připomínky pro nás", Name = "lb_comments" };
            lb_postalcode = new Label() { Text = "PSČ", Name = "lb_postalcode" };
            lb_mothername = new Label() { Text = "Jméno", Name = "lb_mothername" };
            lb_mothersurname = new Label() { Text = "Příjmení", Name = "lb_mothersurname" };
            lb_motherjob = new Label() { Text = "Zaměstnání", Name = "lb_motherjob" };
            lb_motheremail = new Label() { Text = "Email", Name = "lb_motheremail" };
            lb_motherphone = new Label() { Text = "Telefonní číslo", Name = "lb_motherphone" };
            lb_fathername = new Label() { Text = "Jméno", Name = "lb_fathername" };
            lb_fathersurname = new Label() { Text = "Příjmení", Name = "lb_fathersurname" };
            lb_fatherjob = new Label() { Text = "Zaměstnání", Name = "lb_fatherjob" };
            lb_fatheremail = new Label() { Text = "Email", Name = "lb_fatheremail" };
            lb_fatherphone = new Label() { Text = "Telefonní číslo", Name = "lb_fatherphone" };
            lb_controlname = new Label() { Text = "Registrace dítěte", Name = "lb_controlname" };
            lb_mother = new Label() { Text = "Matka", Name = "lb_mother" };
            lb_father = new Label() { Text = "Otec", Name = "lb_father" };
            lb_child = new Label() { Text = "Dítě", Name = "lb_child" };
            b_save = new Button() { Text = "Uložit", Name = "b_save" };
            b_saveandadd = new Button() { Text = "Uložit a zapsat do kroužku", Name = "b_saveandadd" };
            lb_childemail = new Label() { Text = "Email", Name = "lb_email" };
            dtp_birthdate = new DateTimePicker() { Name = "dtp_birthdate" };
            lb_birthdate = new Label() { Text = "Datum narození", Name = "lb_birthdate" };
            tb_childemail = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_email" };
            lb_childphone = new Label() { Text = "Telefonní číslo", Name = "lb_phone" };
            tb_childphone = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_phone" };
            chb_photoagree = new CheckBox() { Text = "Focení", Name = "chb_photoagree" };
            lb_insurcompany = new Label() { Text = "Číslo pojišťovny", Name = "lb_insurancecompany" };
            tb_insurcompany = new TextBox() { AutoCompleteMode = AutoCompleteMode.SuggestAppend, Name = "tb_insurancecompany" };
            chb_gpdragree = new CheckBox() { Text = " GDPR", Name = "chb_gpdragree" };
            chb_newsletteragree = new CheckBox() { Text = "Newsletter", Name = "chb_newsletteragree" };
            chb_leavingalone = new CheckBox() { Text = "Odchod bez doprovodu", Name = "chb_leavingalone" };



            b_save.Click += B_save_Click;
            b_saveandadd.Click += B_saveandadd_Click;
            lisb_exporter.DoubleClick += Lisb_exporter_DoubleClick;















            //registerboard.Controls.Add(null, 0, 0);
            registerboard.Controls.Add(lb_child, 0, 1);
            registerboard.Controls.Add(lb_childname, 0, 2);
            registerboard.Controls.Add(tb_childname, 0, 3);
            registerboard.Controls.Add(lb_childsurname, 0, 4);
            registerboard.Controls.Add(tb_childsurname, 0, 5);
            registerboard.Controls.Add(lb_birthdate, 0, 6);
            registerboard.Controls.Add(dtp_birthdate, 0, 7);
            registerboard.Controls.Add(lb_street, 0, 8);
            registerboard.Controls.Add(tb_street, 0, 9);
            registerboard.Controls.Add(lb_descriptnum, 0, 10);
            registerboard.Controls.Add(tb_descriptnum, 0, 11);
            registerboard.Controls.Add(lb_orientnum, 0, 12);
            registerboard.Controls.Add(tb_orientnum, 0, 13);
            registerboard.Controls.Add(lb_city, 0, 14);
            registerboard.Controls.Add(tb_city, 0, 15);
            registerboard.Controls.Add(lb_postalcode, 0, 16);
            registerboard.Controls.Add(tb_postalcode, 0, 17);
            registerboard.Controls.Add(lb_healthstate, 0, 18);
            registerboard.Controls.Add(tb_healthstate, 0, 19);
            registerboard.Controls.Add(lb_comments, 0, 20);
            registerboard.Controls.Add(tb_comments, 0, 21);
            registerboard.Controls.Add(lb_childemail, 0, 22);
            registerboard.Controls.Add(tb_childemail, 0, 23);
            registerboard.Controls.Add(lb_childphone, 0, 24);
            registerboard.Controls.Add(tb_childphone, 0, 25);
            registerboard.Controls.Add(chb_photoagree, 0, 26);
            //registerboard.Controls.Add(null, 0, 27);

            registerboard.Controls.Add(lb_controlname, 1, 0);
            //registerboard.Controls.Add(null, 1, 1);
            registerboard.Controls.Add(lb_insurcompany, 1, 2);
            registerboard.Controls.Add(tb_insurcompany, 1, 3);
            registerboard.Controls.Add(lb_schoolname, 1, 4);
            registerboard.Controls.Add(tb_schoolname, 1, 5);
            registerboard.Controls.Add(lb_regnum, 1, 6);
            registerboard.Controls.Add(tb_regnum, 1, 7);
            //registerboard.Controls.Add(null, 1, 8);
            //registerboard.Controls.Add(null, 1, 9);
            //registerboard.Controls.Add(null, 1, 10);
            //registerboard.Controls.Add(null, 1, 11);
            //registerboard.Controls.Add(null, 1, 12);
            //registerboard.Controls.Add(null, 1, 13);
            registerboard.Controls.Add(lb_citypart, 1, 14);
            registerboard.Controls.Add(tb_citypart, 1, 15);
            //registerboard.Controls.Add(null, 1, 16);
            //registerboard.Controls.Add(null, 1, 17);
            registerboard.Controls.Add(lisb_exporter, 1, 18);
            registerboard.SetRowSpan(lisb_exporter, 6);
            //registerboard.Controls.Add(null, 1, 19);
            //registerboard.Controls.Add(null, 1, 20);
            //registerboard.Controls.Add(null, 1, 21);
            //registerboard.Controls.Add(null, 1, 22);
            //registerboard.Controls.Add(null, 1, 23);
            //registerboard.Controls.Add(null, 1, 24);
            //registerboard.Controls.Add(null, 1, 25);
            registerboard.Controls.Add(chb_gpdragree, 1, 26);
            //registerboard.Controls.Add(null, 1, 27);
            registerboard.Controls.Add(lb_mother, 2, 1);
            registerboard.Controls.Add(lb_mothername, 2, 2);
            registerboard.Controls.Add(tb_mothername, 2, 3);
            registerboard.Controls.Add(lb_mothersurname, 2, 4);
            registerboard.Controls.Add(tb_mothersurname, 2, 5);
            registerboard.Controls.Add(lb_motherjob, 2, 6);
            registerboard.Controls.Add(tb_motherjob, 2, 7);
            registerboard.Controls.Add(lb_motherphone, 2, 8);
            registerboard.Controls.Add(tb_motherphone, 2, 9);
            registerboard.Controls.Add(lb_motheremail, 2, 10);
            registerboard.Controls.Add(tb_motheremail, 2, 11);
            //registerboard.Controls.Add(null, 2, 11);
            //registerboard.Controls.Add(null, 2, 12);
            //registerboard.Controls.Add(null, 2, 13);
            //registerboard.Controls.Add(null, 2, 14);
            //registerboard.Controls.Add(null, 2, 15);
            //registerboard.Controls.Add(null, 2, 16);
            //registerboard.Controls.Add(null, 2, 17);
            //registerboard.Controls.Add(null, 2, 18);
            //registerboard.Controls.Add(null, 2, 19);
            //registerboard.Controls.Add(null, 2, 20);
            //registerboard.Controls.Add(null, 2, 21);
            //registerboard.Controls.Add(null, 2, 22);
            //registerboard.Controls.Add(null, 2, 23);
            //registerboard.Controls.Add(null, 2, 24);
            //registerboard.Controls.Add(null, 2, 25);
            registerboard.Controls.Add(chb_newsletteragree, 2, 26);
            registerboard.Controls.Add(b_save, 2, 27);
            registerboard.Controls.Add(lb_father, 3, 1);
            registerboard.Controls.Add(lb_fathername, 3, 2);
            registerboard.Controls.Add(tb_fathername, 3, 3);
            registerboard.Controls.Add(lb_fathersurname, 3, 4);
            registerboard.Controls.Add(tb_fathersurname, 3, 5);
            registerboard.Controls.Add(lb_fatherjob, 3, 6);
            registerboard.Controls.Add(tb_fatherjob, 3, 7);
            registerboard.Controls.Add(lb_fatherphone, 3, 8);
            registerboard.Controls.Add(tb_fatherphone, 3, 9);
            registerboard.Controls.Add(lb_fatheremail, 3, 10);
            registerboard.Controls.Add(tb_fatheremail, 3, 11);
            //registerboard.Controls.Add(null, 3, 11);
            //registerboard.Controls.Add(null, 3, 12);
            //registerboard.Controls.Add(null, 3, 13);
            //registerboard.Controls.Add(null, 3, 14);
            //registerboard.Controls.Add(null, 3, 15);
            //registerboard.Controls.Add(null, 3, 16);
            //registerboard.Controls.Add(null, 3, 17);
            //registerboard.Controls.Add(null, 3, 18);
            //registerboard.Controls.Add(null, 3, 19);
            //registerboard.Controls.Add(null, 3, 20);
            //registerboard.Controls.Add(null, 3, 21);
            //registerboard.Controls.Add(null, 3, 22);
            //registerboard.Controls.Add(null, 3, 23);
            //registerboard.Controls.Add(null, 3, 24);
            //registerboard.Controls.Add(null, 3, 25);
            registerboard.Controls.Add(chb_leavingalone, 3, 26);
            registerboard.Controls.Add(b_saveandadd, 3, 27);



            foreach (Control c in registerboard.Controls)
            {
                c.Visible = true;

            }

            LoadExporters();

        }

        private void Lisb_exporter_DoubleClick(object sender, EventArgs e)
        {
           
        }

        public void LoadExporters()
        {
            lisb_exporter.Items.Clear();
          List<string> exportnames=  ExportPluginLoader.Load();
            foreach (string name in exportnames)
            {
                lisb_exporter.Items.Add(name);
            }

        }

        private void B_saveandadd_Click(object sender, EventArgs e)
        {
            SaveChild();
            ControlClicked(0,2,regchild);
        }

        private void SaveHistory()
        {
            foreach (Control c in registerboard.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).AutoCompleteCustomSource.Add(c.Text);
                }
            }
        }

        private void SaveChild()
        {



            //Child
            regchild = Child.CreateInstance();
            Type childtype = regchild.GetType();
            PropertyInfo[] childprops = childtype.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public);

            foreach (PropertyInfo p in childprops)
            {
                if (!p.PropertyType.IsSubclassOf(typeof(LeisureObject)))
                {
                    if (p.Name.ToLower() == "birthdate")
                    {
                        p.SetValue(regchild, dtp_birthdate.Value);
                        continue;
                    }
                    if (p.Name.ToLower() == "id")
                    {
                        continue;
                    }


                    string tbname = String.Format("tb_{0}", p.Name.ToLower());
                    string valtext = registerboard.Controls[tbname].Text;
                    if (valtext != "")
                    { p.SetValue(regchild, Convert.ChangeType(valtext, p.PropertyType)); }
                }
            }
            //Address
            Address regaddress = Address.CreateInstance();
            Type addtype = regaddress.GetType();
            PropertyInfo[] addprops = addtype.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public);

            foreach (PropertyInfo p in addprops)
            {
                if (p.Name.ToLower() == "id")
                {
                    continue;
                }

                if (!p.PropertyType.IsSubclassOf(typeof(LeisureObject)))
                {

                    string tbname = String.Format("tb_{0}", p.Name.ToLower());
                    string valtext = registerboard.Controls[tbname].Text;
                    if (valtext != "")
                    { p.SetValue(regaddress, Convert.ChangeType(valtext, p.PropertyType)); }
                }
            }


            //City
            City regcity = City.CreateInstance();
            Type citytype = regcity.GetType();
            PropertyInfo[] cityprops = citytype.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public);

            foreach (PropertyInfo p in cityprops)
            {
                if (p.Name.ToLower() == "id")
                {
                    continue;
                }

                if (!p.PropertyType.IsSubclassOf(typeof(LeisureObject)))
                {

                    string tbname = String.Format("tb_city{0}", p.Name.ToLower());
                    string valtext = registerboard.Controls[tbname].Text;
                    if (valtext != "")
                    { p.SetValue(regcity, Convert.ChangeType(valtext, p.PropertyType)); }
                }
            }

            //CityPart
            CityPart regcitypart = CityPart.CreateInstance();
            Type cityparttype = regcitypart.GetType();
            PropertyInfo[] citypartprops = cityparttype.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public);

            foreach (PropertyInfo p in citypartprops)
            {
                if (p.Name.ToLower() == "id")
                {
                    continue;
                }

                if (!p.PropertyType.IsSubclassOf(typeof(LeisureObject)))
                {

                    string tbname = String.Format("tb_citypart{0}", p.Name.ToLower());
                    string valtext = registerboard.Controls[tbname].Text;
                    if (valtext != "")
                    { p.SetValue(regcitypart, Convert.ChangeType(valtext, p.PropertyType)); }
                }
            }
            //Mother
            Parent regmother = BusinessLayer.BusinessLayerLib.Classes.DataContainers.Parent.CreateInstance();
            Type mothertype = regmother.GetType();
            PropertyInfo[] motherprops = mothertype.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public);

            foreach (PropertyInfo p in motherprops)
            {
                if (p.Name.ToLower() == "id" || p.Name.ToLower() == "gender")
                {
                    continue;
                }

                if (!p.PropertyType.IsSubclassOf(typeof(LeisureObject)))
                {

                    string tbname = String.Format("tb_mother{0}", p.Name.ToLower());
                    string valtext = registerboard.Controls[tbname].Text;
                    if (valtext != "")
                    { p.SetValue(regmother, Convert.ChangeType(valtext, p.PropertyType)); }
                }
            }
            //Father
            Parent regfather = BusinessLayer.BusinessLayerLib.Classes.DataContainers.Parent.CreateInstance();
            Type fathertype = regfather.GetType();
            PropertyInfo[] fatherprops = fathertype.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public);

            foreach (PropertyInfo p in fatherprops)
            {
                if (p.Name.ToLower() == "id" || p.Name.ToLower() == "gender")
                {
                    continue;
                }

                if (!p.PropertyType.IsSubclassOf(typeof(LeisureObject)))
                {

                    string tbname = String.Format("tb_father{0}", p.Name.ToLower());
                    string valtext = registerboard.Controls[tbname].Text;
                    if (valtext != "")
                    { p.SetValue(regfather, Convert.ChangeType(valtext, p.PropertyType)); }
                }
            }

            //Unification
            regfather.Gender = 'm';
            regmother.Gender = 'f';
            InsuranceCompany reginscom = null;
            string text = registerboard.Controls["tb_insurancecompany"].Text;
            if (text != "")
            {
                InsuranceCompany critcomp = InsuranceCompany.CreateInstance();
                critcomp.Number = Convert.ToInt32(text);
                reginscom = InsuranceCompany.Load(critcomp)[0];
            }
            regcitypart.OriginCity = regcity;
            regaddress.HomeCity = regcity;
            regaddress.HomeCityPart = regcitypart;
            regchild.HomeAddress = regaddress;
            regchild.Father = regfather;
            regchild.Mother = regmother;
            regchild.Company = reginscom;

            if (!VerifyChildAge(regchild))
            { return; }
            if (!VerifyRationalAge(regchild))
            { return; }
            if (!VerifyNameSurname(regchild))
            {
                return;
            }
            if (CheckPhoto())
            {
                if (WantsPhoto())
                {
                    LoadPhoto();
                }
            }
            if (CheckNewsletter())

            {
                ChooseNewsletterEmail(regchild);
            }


            List<Child> refer = Child.Load(regchild);
            if (refer == null||refer.Count<=0)
            {
                Child.Save(regchild);
            }
            


        }


        private void B_save_Click(object sender, EventArgs e)
        {
            SaveChild();
            ControlClicked(0, 3);
        }

        private void ChooseNewsletterEmail(Child ch)
        {
            /*  Form chooser = new Form()
              { Name = "f_chooser", Text = "zvolte email" ,Width= 200,Height=150};
              chooser.Parent = TopLevelControl;
              Button b_childemail = new Button() { Text = "Email dítěte", Name = "b_childemail", Tag = tb_childemail.Text };
              Button b_motheremail = new Button() { Text = "Email matky", Name = "b_motheremail", Tag = tb_motheremail.Text };
              Button b_fatheremail = new Button() { Text = "Email otce", Name = "b_fatheremail", Tag = tb_fatheremail.Text };
              b_childemail.Click += EmailChosenClick;
              TableLayoutPanel choosedisplayer = new TableLayoutPanel() { Name="choosedisplayer",Parent=chooser,Size=chooser.Size,ColumnCount=1,RowCount=3};
              choosedisplayer.Controls.Add(b_childemail);
              choosedisplayer.Controls.Add(b_motheremail);
              choosedisplayer.Controls.Add(b_fatheremail);
              choosedisplayer.Show();*/
            DialogResult childmail = MessageBox.Show("Chcete odesílat newsletter na email dítěte?", "Dítěti", MessageBoxButtons.YesNo);
            if (childmail == DialogResult.Yes)
            {
                EmailChosen(ch.Email);
                return;
            }
            DialogResult mothermail = MessageBox.Show("Chcete odesílat newsletter na email matky?", "Matce", MessageBoxButtons.YesNo);
            if (mothermail == DialogResult.Yes)
            {
                EmailChosen(ch.Mother.Email);
                return;
            }
            DialogResult fathermail = MessageBox.Show("Chcete odesílat newsletter na email otce?", "Otce", MessageBoxButtons.YesNo);
            if (fathermail == DialogResult.Yes)
            {
                EmailChosen(ch.Father.Email);
                return;
            }
        }

        private void EmailChosen(string email)
        {
            
                using (StreamWriter sw = new StreamWriter("newsletters.csv", true, Encoding.Default))
                {
                    sw.WriteLine(String.Format("{0}",email));
                }
            
        }

        private bool CheckNewsletter()
        {
            return chb_newsletteragree.Checked;
        }

        private void LoadPhoto()
        {
           OpenFileDialog openfiler = new OpenFileDialog();
            openfiler.Filter = "Images|*.png;*.jpg";
            if (openfiler.ShowDialog() == DialogResult.OK)
            {
               Image photo = Image.FromFile(openfiler.FileName);
                photo.Save(openfiler.FileName.Split('\\').Last());
            }
        }

        private bool WantsPhoto()
        {
            DialogResult wants= MessageBox.Show("Chcete ihned vložit fotografii?", "Chcete foto?", MessageBoxButtons.YesNo);
            return wants == DialogResult.Yes;
        }

        private bool CheckPhoto()
        {
            return chb_photoagree.Checked;
        }

        private bool VerifyNameSurname(Child child)
        {
            bool ok = child.Name != "" && child.Surname != "";
            if (!ok)
            { MessageBox.Show("Zadejte celé jméno a příjmení", "Chybějící údaje", MessageBoxButtons.OK); }
            return ok;
        }

        private bool VerifyRationalAge(Child child)
        {
           bool ageok=DateTime.Now.Year - child.BirthDate.Year >= 1 && DateTime.Now.Year - child.BirthDate.Year <= 120;
            if (!ageok)
            { MessageBox.Show("Zadali jste nesmyslný věk", "Nesmyslný věk!", MessageBoxButtons.OK); }
            return ageok;
        }

        private bool VerifyChildAge(Child child)
        {
            DialogResult tooyoung = DialogResult.None, tooold=DialogResult.None;
            bool ageok = true;

            if (DateTime.Now.Year - child.BirthDate.Year < 6)
            {
                tooyoung = MessageBox.Show("Vaše dítě je příliš mladé. Opravdu jej chcete zaregistrovat?", "Příliš mladé", MessageBoxButtons.YesNo);

                ageok = false;
            }
            else if (DateTime.Now.Year - child.BirthDate.Year > 15)
            {
                tooold = MessageBox.Show("Vaše dítě je příliš staré. Opravdu jej chcete zaregistrovat?", "Příliš staré", MessageBoxButtons.YesNo);

                ageok = false;
            }
            else
            { ageok = true; }


            return (ageok == true || tooyoung == DialogResult.Yes || tooold == DialogResult.Yes);
          

        }
    }
}
