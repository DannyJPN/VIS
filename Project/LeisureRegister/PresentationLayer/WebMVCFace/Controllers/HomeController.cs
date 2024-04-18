using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.BusinessLayerLib.Classes.DataContainers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebMVCFace.Models;

namespace WebMVCFace.Controllers
{
    public class HomeController : Controller
    {
        static int userid = -0;
        User[] users = new User[]{
        new User(0,"kral1234","P@ssW0rd"),
new User(1,"knez4512","teology"),
new User(0,"krup4534","vsb-tuo"),
new User(1,"skol2544","4nime"),


        };
        [HttpGet]
        public IActionResult ChildSearch()
        {
            ViewBag.Info = new List<List<string>>();

            return View();
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public IActionResult Register()
        {

            return View();
        }



        public List<SelectListItem> MakeList(List<Child> entry)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            for (int i = 0; i < entry.Count; i++)
            {
                result.Add(new SelectListItem() { Text = entry[i].ToString(), Value = entry[i].ID.ToString() });
            }

            return result;

        }
        public List<SelectListItem> MakeList(List<HobbyGroup> entry)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            for (int i = 0; i < entry.Count; i++)
            {
                result.Add(new SelectListItem() { Text = entry[i].ToString(), Value = entry[i].ID.ToString() });
            }

            return result;

        }

        [HttpPost]
        public IActionResult Save([FromForm] RegisterFormModel regformmodel)
        {

            SaveChild(regformmodel);

            if (regformmodel.action == "search")
            {
                ViewBag.Info = new List<List<string>>();
                return View("ChildSearch");
            }
            else if (regformmodel.action == "add")
            {
                //ViewBag.DefaultChild = regformmodel.;
                return View("AddToGroup");
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult AddToGroup(AddingFormModel addingboard, Child def)
        {

            List<string> seasons = new List<string>();
            for (int i = 2016; i < DateTime.Now.Year; i++)
            {
                seasons.Add(String.Format("{0}/{1}", i, i + 1));
            }


            ViewBag.Seasons = seasons;

            List<Child> children = LoadChildren();
            List<int> kidids = new List<int>();
            foreach (Child c in children)
            { kidids.Add(c.ID); }
            ViewBag.KidIDs = kidids;
            ViewBag.DefaultKid = def;

            List<HobbyGroup> groups = LoadAvailableGroups(addingboard);
            List<int> groupids = new List<int>();
            foreach (HobbyGroup hg in groups)
            { groupids.Add(hg.ID); }
            ViewBag.GroupIDs = groupids;

            return View("AddToGroup");

        }

        [HttpPost]
        public IActionResult Login([FromForm] LoginModel logmodel)
        {
            bool? success = false;
            foreach (User u in users)
            {
                if (logmodel.Password == u.Password && logmodel.Username == u.Username)
                {
                    success = true;
                    ViewBag.UserID = u.Right;
                    userid = u.Right;
                    break;
                }
            }
            if (success.Value == true)
            { return View("Index"); }

            ViewBag.Loginsuccessful = success;
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }


        //Search Methods
        [HttpPost]
        public IActionResult ChildSearch([FromForm] ChildSearchModel searchboard)
        {
            Child critchild = Child.CreateInstance(0);
            Type t = critchild.GetType();
            PropertyInfo[] props = t.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public);

            foreach (PropertyInfo p in props)
            {
                if (!p.PropertyType.IsSubclassOf(typeof(LeisureObject)))
                {

                    string tbname = String.Format("{0}", p.Name);

                    object o = searchboard.GetType().GetProperty(tbname.ToLower()).GetValue(searchboard);
                    string valtext = o == null ? "" : o.ToString();
                    if (valtext != "")
                    { p.SetValue(critchild, Convert.ChangeType(valtext, p.PropertyType)); }
                }
            }

            List<Child> children = Child.Load(critchild, searchboard.conditions_together);




            //bool nonexact_search = false;
            /*  if (children == null)
              {
                  DialogResult dr = MessageBox.Show("Chcete vyhledat děti,které splńují i jednotlivé parametry?", "Žádné děti nebyly nalezeny", MessageBoxButtons.YesNo);
                  if (dr == DialogResult.Yes)
                  {
                      children = Child.Load(critchild, nonexact_search);
                  }
              }*/
            List<Child> allowedchildren = FilterChildren(children);
            List<List<string>> info = new List<List<string>>();
            info.Clear();

            Type itemtype = typeof(Child);
            PropertyInfo[] properts = t.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public);


            for (int i = 0; i < allowedchildren.Count; i++)
            {


                List<string> line = new List<string>();
                foreach (PropertyInfo p in properts)
                {
                    if (!p.PropertyType.IsSubclassOf(typeof(LeisureObject)))
                    {

                        //dgwrow.Cells.Add(new DataGridViewTextBoxCell() { Value = p.GetValue(allowedchildren[i]).ToString() });
                        line.Add(p.GetValue(allowedchildren[i]).ToString());

                    }
                }


                info.Add(line);

                //dgw_searchresult.Rows[i].Tag = allowedchildren[i];

            }  //MessageBox.Show(String.Format("{0} {1}",dgw_searchresult.Width,searchboard.Width));
            //dgw_searchresult.Width = searchboard.Width - 50;

            ViewBag.Info = info;
            return View();


        }
        private List<Child> FilterChildren(List<Child> children)
        {

            if (children == null)
            {
                return new List<Child>();
            }
            List<Child> allowedchildren = new List<Child>();
            if (userid == 0)
            {
                allowedchildren = children;
            }

            else
            {
                List<GroupLeading> ledgroups = GroupLeading.Load(GroupLeading.CreateInstance(0, GroupLeader.Load(userid), null, new DateTime(), new DateTime()));

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

        //~Search Methods



        //Registration Methods
        private void SaveChild(RegisterFormModel registerboard)
        {



            //Child
            Child regchild = Child.CreateInstance();
            Type childtype = regchild.GetType();
            PropertyInfo[] childprops = childtype.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public);

            foreach (PropertyInfo p in childprops)
            {
                if (!p.PropertyType.IsSubclassOf(typeof(LeisureObject)))
                {
                    if (p.Name.ToLower() == "birthdate")
                    {
                        p.SetValue(regchild, registerboard.birthdate);
                        continue;
                    }
                    if (p.Name.ToLower() == "id")
                    {
                        continue;
                    }

                    //Type modeltype = registerboard.GetType();
                    string tbname = String.Format("{0}", p.Name.ToLower());

                    //PropertyInfo curprop = modeltype.GetProperty(tbname.ToLower());
                    object o = registerboard.GetType().GetProperty(tbname.ToLower()).GetValue(registerboard);
                    string valtext = o == null ? "" : o.ToString();
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

                    string tbname = String.Format("{0}", p.Name.ToLower());
                    object o = registerboard.GetType().GetProperty(tbname.ToLower()).GetValue(registerboard); string valtext = o == null ? "" : o.ToString();
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

                    string tbname = String.Format("city{0}", p.Name.ToLower());
                    object o = registerboard.GetType().GetProperty(tbname.ToLower()).GetValue(registerboard); string valtext = o == null ? "" : o.ToString();
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

                    string tbname = String.Format("citypart{0}", p.Name.ToLower());
                    object o = registerboard.GetType().GetProperty(tbname.ToLower()).GetValue(registerboard); string valtext = o == null ? "" : o.ToString();
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

                    string tbname = String.Format("mother{0}", p.Name.ToLower());
                    object o = registerboard.GetType().GetProperty(tbname.ToLower()).GetValue(registerboard); string valtext = o == null ? "" : o.ToString();
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

                    string tbname = String.Format("father{0}", p.Name.ToLower());
                    object o = registerboard.GetType().GetProperty(tbname.ToLower()).GetValue(registerboard); string valtext = o == null ? "" : o.ToString();
                    if (valtext != "")
                    { p.SetValue(regfather, Convert.ChangeType(valtext, p.PropertyType)); }
                }
            }

            //Unification
            regfather.Gender = 'm';
            regmother.Gender = 'f';
            InsuranceCompany reginscom = null;
            string text = registerboard.GetType().GetProperty("insurancecompany").GetValue(registerboard).ToString();
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
            if (CheckPhoto(registerboard))
            {
                if (WantsPhoto(registerboard))
                {
                    LoadPhoto();
                }
            }
            if (CheckNewsletter(registerboard))

            {
                ChooseNewsletterEmail(regchild);
            }


            List<Child> refer = Child.Load(regchild);
            if (refer == null || refer.Count <= 0)
            {
                Child.Save(regchild);
            }



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
            /*DialogResult childmail = MessageBox.Show("Chcete odesílat newsletter na email dítěte?", "Dítěti", MessageBoxButtons.YesNo);
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
            }*/
        }

        private void EmailChosen(string email)
        {

            using (StreamWriter sw = new StreamWriter("newsletters.csv", true, Encoding.Default))
            {
                sw.WriteLine(String.Format("{0}", email));
            }

        }

        private bool CheckNewsletter(RegisterFormModel registerboard)
        {
            return registerboard.newsletteragree;
        }

        private void LoadPhoto()
        {
            /*  OpenFileDialog openfiler = new OpenFileDialog();
              openfiler.Filter = "Images|*.png;*.jpg";
              if (openfiler.ShowDialog() == DialogResult.OK)
              {
                  Image photo = Image.FromFile(openfiler.FileName);
                  photo.Save(openfiler.FileName.Split('\\').Last());
              }*/
        }

        private bool WantsPhoto(RegisterFormModel registerboard)
        {/*
            DialogResult wants = MessageBox.Show("Chcete ihned vložit fotografii?", "Chcete foto?", MessageBoxButtons.YesNo);
            return wants == DialogResult.Yes;*/
            return false;
        }

        private bool CheckPhoto(RegisterFormModel registerboard)
        {
            return registerboard.photoagree;
        }

        private bool VerifyNameSurname(Child child)
        {
            bool ok = child.Name != "" && child.Surname != "";
            //  if (!ok)
            // { MessageBox.Show("Zadejte celé jméno a příjmení", "Chybějící údaje", MessageBoxButtons.OK); }
            ViewBag.HasNameAndSurname = ok;
            return ok;
        }

        private bool VerifyRationalAge(Child child)
        {
            bool ageok = DateTime.Now.Year - child.BirthDate.Year >= 1 && DateTime.Now.Year - child.BirthDate.Year <= 120;
            if (!ageok)
            { ViewBag.AgeRationalMessage = "Zadali jste nesmyslný věk"; }
            ViewBag.AgeRational = ageok;
            return ageok;
        }

        private bool VerifyChildAge(Child child)
        {
            //DialogResult tooyoung = DialogResult.None, tooold = DialogResult.None;
            bool ageok = true;
            string tooyoung = "", tooold = "";
            if (DateTime.Now.Year - child.BirthDate.Year < 6)
            {
                ViewBag.TooYoungMessage = "Vaše dítě je příliš mladé. Opravdu jej chcete zaregistrovat?";
                tooyoung = "no";
                ageok = false;
            }
            else if (DateTime.Now.Year - child.BirthDate.Year > 15)
            {
                ViewBag.TooOldMessage = "Vaše dítě je příliš staré. Opravdu jej chcete zaregistrovat?";
                tooold = "no";
                ageok = false;
            }
            else
            { ageok = true; }


            return (ageok == true || tooyoung == "yes" || tooold == "yes");


        }

        //~Registration Methods


        //AddToGroupMethods

        [HttpPost]
        private void AddToGroup([FromForm] AddingFormModel addingboard)
        {
            List<HobbyGroup> chosen = new List<HobbyGroup>();
            List<HobbyGroup> filled = new List<HobbyGroup>();
            List<HobbyGroup> duplicit_memberships = new List<HobbyGroup>();
            List<HobbyGroup> outofrange = new List<HobbyGroup>();
            List<HobbyGroup> addfailed = new List<HobbyGroup>();

            Child chosenchild = Child.Load(addingboard.chosenchildid);

            foreach (int hgID in addingboard.groupids)
            {
                chosen.Add(HobbyGroup.Load(hgID));

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
                //  MessageBox.Show(sb.ToString(), "Pozor,přeplnění", MessageBoxButtons.OK);



            }

            if (duplicit_memberships.Count > 0)
            {

                StringBuilder sb = new StringBuilder("Tyto kroužky již dítě navštěvuje: \n");
                foreach (HobbyGroup hg in duplicit_memberships)
                {
                    sb.Append(String.Format("{0}\n", hg.Name));
                }
                addfailed.AddRange(duplicit_memberships);
                //MessageBox.Show(sb.ToString(), "Tady už dítě je", MessageBoxButtons.OK);

            }
            //choosing from failed age check
            if (outofrange.Count > 0)
            {
                /*   Form additional = new Form();
                   addingboard.Parent = this;
                   addingboard.Width = addingboard.Height = 300;
                   ListBox lisb_adder = new ListBox() { Top = 0, Left = 0, Width = 100, Height = 200, Parent = additional, Name = "f_additional" };
                   foreach (HobbyGroup hg in outofrange)
                   { lisb_adder.Items.Add(hg); }
                   Button b_confirm = new Button() { Text = "Uložit", Top = lisb_adder.Bottom + 10, Left = 0, Parent = additional };
                   b_confirm.Click += delegate (object send, EventArgs ea)
                   {
                       foreach (HobbyGroup selitem in lisb_adder.SelectedItems)
                       {
                           chosen.Add(selitem);
                           //lisb_adder.Items.Remove(selitem);
                       }
                       foreach (HobbyGroup hg in lisb_adder.Items)
                       { addfailed.Add(hg); }
                       additional.Close();
                   };
                   additional.Show();*/
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
            chosen.Clear();
            //additional offer
            if (addfailed.Count > 0)
            {
                HobbyGroup hgcrit = HobbyGroup.CreateInstance();
                hgcrit.SeasonOfExistence = addingboard.season;
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
                    {
                        bad.Add(hg);
                    }

                }
                foreach (HobbyGroup hg in bad)
                { nextgroups.Remove(hg); };


                if (nextgroups.Count > 0)
                {

                    /*   Form additional = new Form();
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
                       */
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
        private List<Child> LoadChildren()
        {
            return Child.Load(null);



        }
        private List<HobbyGroup> LoadAvailableGroups(AddingFormModel addingboard)
        {
            List<HobbyGroup> lisb_grouplist = new List<HobbyGroup>();
            HobbyGroup avail = HobbyGroup.CreateInstance();
            avail.SeasonOfExistence = addingboard.season;

            List<HobbyGroup> availgroups = HobbyGroup.Load(avail);
            foreach (HobbyGroup hg in availgroups)
            {
                GroupLeading crit = GroupLeading.CreateInstance();
                crit.Group = hg;
                List<GroupLeading> leadings = GroupLeading.Load(crit);

                if (hg.Max > 0 && leadings != null)
                    lisb_grouplist.Add(hg);

            }

            return availgroups;

        }



        //~AddToGroupMethods


    }

    internal class User
    {
        public int Right { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User(int right, string username, string password)
        {
            this.Right = right;
            this.Username = username;
            this.Password = password;
        }
    }
}
