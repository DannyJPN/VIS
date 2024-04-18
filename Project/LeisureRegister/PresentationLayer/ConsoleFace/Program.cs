using BusinessLayer.BusinessLayerLib.Classes;
using BusinessLayer.BusinessLayerLib.Classes.DataContainers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.ConsoleFace
{
    class Program
    {
        static void Main(string[] args)
        {
            /*  City c = City.CreateInstance(40, "Opava");
              CityPart cp = CityPart.CreateInstance(10, c, "Komárov");
              Parent m = Parent.CreateInstance(47, "Jana", "Nová", "jananova@seznam.cz", 0, "kuchařka", 'f');
              Parent f = Parent.CreateInstance(22, "Jan", "Nový", "", 789456123, "dělník", 'm');
              InsuranceCompany co = InsuranceCompany.CreateInstance(78, 213, "RBPZP", "Revírní bratrská pokaldna zdravotní pojišťovna");
              Address a = Address.CreateInstance(44, "Malá", c, cp, 78451,2541,54);
              Child ch = Child.CreateInstance(10, "Petr", "Nový", "", 478945142, 14, new DateTime(1998, 4, 10), m, f, co, "Zdravý", "Neumí plavat", "ZŠ Opava", a);

              Console.WriteLine(new DateTime().ToString());
              Console.WriteLine(new TimeSpan().ToString());

              Console.WriteLine("asf".GetType().IsSubclassOf(typeof(LeisureObject)));

                Child critchild = Child.CreateInstance(2);
                critchild.Name = "Zuzana";
                critchild.BirthDate = new DateTime(1997, 4, 5);
                critchild.Mother = Parent.CreateInstance(6);
               */

            //FullList(ch);
            /*
                        Console.WriteLine(SelectString(ch, critchild));
                        Console.WriteLine("__________________________________");
                        Console.WriteLine(InsertString(ch));

                        Console.WriteLine("__________________________________");
                        Console.WriteLine(UpdateString(ch, critchild));
                        Console.WriteLine("__________________________________");
                        Console.WriteLine(DeleteString( critchild));
                        Console.WriteLine("__________________________________");
                        Console.WriteLine(CreateSelectCommand(ch,critchild).Parameters);
                        */
            //Child.Save(ch);
            //Console.WriteLine(Assembly.GetAssembly(ch.GetType()));
            // Child loadedchild = Child.Load(20);
            //  Console.WriteLine(loadedchild.ToString());
            List<InsuranceCompany> companies = new List<InsuranceCompany>();
            companies.Add(InsuranceCompany.CreateInstance(1, 111, "VZP", "Všeobecná zdravotní pojišťovna"));
            companies.Add(InsuranceCompany.CreateInstance(2, 201, "VZPČR", "Vojenská zdravotní pojišťovna České republiky"));
            companies.Add(InsuranceCompany.CreateInstance(3, 205, "ČPZP", "Česká průmyslová zdravotní pojišťovna"));
            companies.Add(InsuranceCompany.CreateInstance(4, 207, "OZPZBPS", "Oborová zdravotní pojišťovna zaměstnanců bank pojišťoven a stavebnictví"));
            companies.Add(InsuranceCompany.CreateInstance(5, 209, "ZPŠ", "Zaměstnanecká pojišťovna Škoda"));
            companies.Add(InsuranceCompany.CreateInstance(6, 211, "ZPMVČR", "Zdravotní pojišťovna ministerstva vnitra České republiky"));
            companies.Add(InsuranceCompany.CreateInstance(7, 213, "RBPZP", "Revírní bratrská pokladna - zdravotní pojišťovna"));
            foreach (InsuranceCompany com in companies)
            {
                InsuranceCompany.Save(com);
            }

        }
      

        public static void FullList(Object o)
        {

            Type t = o.GetType();
            PropertyInfo[] props = t.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (PropertyInfo p in props)
            {
                if (!p.PropertyType.IsSubclassOf(typeof(LeisureObject)))
                {
                    Console.WriteLine("{2} {0}:\t{1}", p.Name, p.GetValue(o), p.PropertyType.Name);
                }
                else
                {

                    FullList(p.GetValue(o));
                }
            }

        }
        public static string SelectString(Object o, Object crit, bool conditions_together = true)
        {
            StringBuilder sb = new StringBuilder("select ");
            Type t = o.GetType();
            List<string> propnames = new List<string>(); string joiner = conditions_together ? "and" : "or";
            PropertyInfo[] props = t.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (PropertyInfo p in props)
            {
                if (!p.PropertyType.IsSubclassOf(typeof(LeisureObject)))
                {
                    propnames.Add(p.Name);
                }
                /* else
                 {

                     propnames.Add(String.Format("{0}id", p.Name.ToLower()));
                 }*/
            }
            sb.AppendFormat("({0}) from {1}", string.Join(",", propnames), t.Name.TrimEnd("DTO".ToCharArray()));
            sb.Append(" where ");
            propnames.Clear();

            Type t2 = crit.GetType();

            props = t2.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (PropertyInfo p in props)
            {
                if (!p.PropertyType.IsSubclassOf(typeof(LeisureObject)))
                {
                    if (p.GetValue(crit) != null && p.GetValue(crit).ToString() != "")
                    {

                        propnames.Add(String.Format("{0} = @{0}", p.Name));
                    }
                    /* else
                     {

                         propnames.Add(String.Format("{0}id = @{0}id", p.Name.ToLower()));
                     }*/
                }
            }
            sb.AppendFormat("{0}", string.Join(String.Format(" {0} ", joiner), propnames));



            return sb.ToString();
        }


        public static string InsertString(Object DTO, bool conditions_together = true)
        {


            Type tDTO = DTO.GetType();
            StringBuilder sb = new StringBuilder(String.Format("insert into {0}", tDTO.Name.TrimEnd("DTO".ToCharArray())));
            List<string> propnames = new List<string>();
            PropertyInfo[] props = tDTO.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (PropertyInfo p in props)
            {
                if (p.Name == "ID")
                { continue; }
                if (!p.PropertyType.IsSubclassOf(typeof(LeisureObject)))
                {
                    propnames.Add(p.Name);
                }
                /*  else
                  {

                      propnames.Add(String.Format("{0}id", p.Name.ToLower()));
                  }*/
            }
            sb.AppendFormat("({0}) values ", string.Join(",", propnames));

            propnames.Clear();


            foreach (PropertyInfo p in props)
            {
                if (p.Name == "ID")
                { continue; }
                if (!p.PropertyType.IsSubclassOf(typeof(LeisureObject)))
                {
                    propnames.Add(String.Format(" @{0}", p.Name));
                }
                /*  else
                  {

                      propnames.Add(String.Format("@{0}id", p.Name.ToLower()));
                  }*/

            }
            sb.AppendFormat("({0})", string.Join(",", propnames));



            return sb.ToString();

        }


        public static string UpdateString(Object o, Object crit, bool conditions_together = true)
        {

            Type t = o.GetType();
            StringBuilder sb = new StringBuilder(String.Format("update {0} set ", t.Name.TrimEnd("DTO".ToCharArray())));
            List<string> propnames = new List<string>(); string joiner = conditions_together ? "and" : "or";
            PropertyInfo[] props = t.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (PropertyInfo p in props)
            {
                if (p.Name == "ID")
                { continue; }
                if (p.GetValue(o) != null)
                {
                    if (!p.PropertyType.IsSubclassOf(typeof(LeisureObject)))
                    {
                        propnames.Add(String.Format("{0} = @{0}", p.Name));
                    }
                    /* else
                     {

                         propnames.Add(String.Format("{0}id = @{0}id", p.Name.ToLower()));
                     }*/
                }
            }
            sb.AppendFormat("{0}", string.Join(",", propnames));
            sb.Append(" where ");
            propnames.Clear();

            Type t2 = crit.GetType();

            props = t2.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (PropertyInfo p in props)
            {
                if (p.Name == "ID")
                { continue; }
                if (p.GetValue(crit) != null && p.GetValue(crit).ToString() != "")
                {
                    if (!p.PropertyType.IsSubclassOf(typeof(LeisureObject)))
                    {
                        propnames.Add(String.Format("{0} = @{0}", p.Name));
                    }
                    /*     else
                         {

                             propnames.Add(String.Format("{0}id = @{0}id", p.Name.ToLower()));
                         }*/
                }
            }
            sb.AppendFormat("{0}", string.Join(String.Format(" {0} ", joiner), propnames));



            return sb.ToString();
        }
        public static string DeleteString(Object crit, bool conditions_together = true)
        {
            StringBuilder sb = new StringBuilder("delete ");
            List<string> propnames = new List<string>(); string joiner = conditions_together ? "and" : "or";

            sb.AppendFormat(" from {0}", crit.GetType().Name.TrimEnd("DTO".ToCharArray()));
            sb.Append(" where ");


            Type t2 = crit.GetType();

            PropertyInfo[] props = t2.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (PropertyInfo p in props)
            {
                if (p.GetValue(crit) != null && p.GetValue(crit).ToString() != "")
                {
                    if (!p.PropertyType.IsSubclassOf(typeof(LeisureObject)))
                    {
                        propnames.Add(String.Format("{0} = @{0}", p.Name));
                    }
                    /*   else
                       {

                           propnames.Add(String.Format("{0}id = @{0}id", p.Name.ToLower()));
                       }*/
                }
            }
            sb.AppendFormat("{0}", string.Join(String.Format(" {0} ", joiner), propnames));



            return sb.ToString();
        }
        public static SqlCommand CreateSelectCommand(LeisureObject DTO, LeisureObject criteria)
        {
            SqlCommand select = new SqlCommand(SelectString(DTO, criteria));
            Type tcrit = criteria.GetType();
            PropertyInfo[] props = tcrit.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (PropertyInfo p in props)
            {
                if (p.GetValue(criteria) != null && p.GetValue(criteria).ToString() != "")
                {
                    if (!p.PropertyType.IsSubclassOf(typeof(LeisureObject)))
                    {
                        select.Parameters.AddWithValue(String.Format("@{0}", p.Name), p.GetValue(criteria));
                        //        Console.WriteLine(String.Format("@{0} = {1}", p.Name, p.GetValue(criteria)));
                    }

                }


            }




            return select;
        }
    }
}


