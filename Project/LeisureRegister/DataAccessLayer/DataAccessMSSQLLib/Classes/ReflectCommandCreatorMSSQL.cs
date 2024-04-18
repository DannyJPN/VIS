
using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.Attributes;
using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataAccessLayer.DataAccessMSSQLLib.Classes
{
    public class ReflectCommandCreatorMSSQL
    {
        public string CreateSelect(LeisureObjectDTO criteria, bool conditions_together = true)
        {
            StringBuilder sb = new StringBuilder("select ");
            Type tDTO = criteria.GetType();
            List<string> propnames = new List<string>(); string joiner = conditions_together ? "and" : "or";
            PropertyInfo[] props = tDTO.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (PropertyInfo p in props)
            {
                //if (!p.PropertyType.IsSubclassOf(typeof(LeisureObjectDTO)))
                if (p.GetCustomAttributes().Where(x => x is StandardSQLAttribute || x is ForeignKeyAttribute || x is PrimaryKeyAttribute).Any())
                {
                    propnames.Add(String.Format("[{0}]", p.Name));
                }
                /*else
                {

                    propnames.Add(String.Format("{0}id", p.Name.ToLower()));
                }*/
            }
            sb.AppendFormat("{0} from [{1}]", string.Join(",", propnames), tDTO.Name.TrimEnd("DTO".ToCharArray()));
            //if (criteria == null)
            //{
            //    return sb.ToString();
            //}

            propnames.Clear();

            Type tcriteria = criteria.GetType();

            props = tcriteria.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (PropertyInfo p in props)
            {
                if (p.GetCustomAttributes().Where(x => x is StandardSQLAttribute || x is ForeignKeyAttribute || x is PrimaryKeyAttribute).Any())
                {
                    if (p.GetValue(criteria) != null && p.GetValue(criteria).ToString() != "" && p.GetValue(criteria).ToString() != "0" && p.GetValue(criteria).ToString() != new DateTime().ToString() && p.GetValue(criteria).ToString() != new TimeSpan().ToString() )
                    {

                        propnames.Add(String.Format("[{0}] = @{0}", p.Name));
                    }
                    /* else
                     {

                         propnames.Add(String.Format("{0}id = @{0}id", p.Name.ToLower()));
                     }*/
                }
            }
            if (propnames.Count > 0)
            {
                sb.Append(" where ");
            }
            sb.AppendFormat("{0}", string.Join(String.Format(" {0} ", joiner), propnames));



            return sb.ToString();

        }
        public string CreateInsert(LeisureObjectDTO DTO)
        {
            Type tDTO = DTO.GetType();
            StringBuilder sb = new StringBuilder(String.Format("insert into [{0}]", tDTO.Name.TrimEnd("DTO".ToCharArray())));
            List<string> propnames = new List<string>();
            PropertyInfo[] props = tDTO.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (PropertyInfo p in props)
            {
                /*if (p.Name == "ID")
                { continue; }*/
                if (p.GetCustomAttributes().Where(x => x is StandardSQLAttribute || x is ForeignKeyAttribute).Any())
                {
                    propnames.Add(String.Format("[{0}]", p.Name));
                }
                /* else
                 {

                     propnames.Add(String.Format("{0}id", p.Name.ToLower()));
                 }*/
            }
            sb.AppendFormat("({0}) values ", string.Join(",", propnames));

            propnames.Clear();


            foreach (PropertyInfo p in props)
            {
                /* if (p.Name == "ID")
                 { continue; }*/
                if (p.GetCustomAttributes().Where(x => x is StandardSQLAttribute || x is ForeignKeyAttribute).Any())
                {
                    propnames.Add(String.Format(" @{0}", p.Name));
                }
                /* else
                 {

                     propnames.Add(String.Format("@{0}id", p.Name.ToLower()));
                 }*/

            }
            sb.AppendFormat("({0})", string.Join(",", propnames));



            return sb.ToString();
        }
        public string CreateUpdate(LeisureObjectDTO DTO)
        {
            Type t = DTO.GetType();
            StringBuilder sb = new StringBuilder(String.Format("update [{0}] set ", t.Name.TrimEnd("DTO".ToCharArray())));
            List<string> propnames = new List<string>(); string joiner = "and";
            PropertyInfo[] props = t.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (PropertyInfo p in props)
            {
                if (p.GetCustomAttributes().Where(x => x is StandardSQLAttribute || x is ForeignKeyAttribute).Any())
                {
                    if (p.GetValue(DTO) != null && p.GetValue(DTO).ToString() != "" && p.GetValue(DTO).ToString() != "0" && p.GetValue(DTO).ToString() != new DateTime().ToString() && p.GetValue(DTO).ToString() != new TimeSpan().ToString())
                    {

                        propnames.Add(String.Format("[{0}] = @{0}", p.Name));
                    }
                    /*     else
                         {

                             propnames.Add(String.Format("{0}id = @{0}id", p.Name.ToLower()));
                         }*/
                }
            }
            sb.AppendFormat("{0}", string.Join(",", propnames));
            sb.Append(" where ");
                    propnames.Add(String.Format("[ID] = @ID"));
                    
                  
            
            sb.AppendFormat("{0}", string.Join(String.Format(" {0} ", joiner), propnames));



            return sb.ToString();
        }
        public string CreateDelete(LeisureObjectDTO criteria)
        {
            StringBuilder sb = new StringBuilder("delete ");

            sb.AppendFormat(" from [{0}]", criteria.GetType().Name.TrimEnd("DTO".ToCharArray()));
            sb.Append(" where ");

            Type tcriteria = criteria.GetType();
            List<string> propnames = new List<string>(); string joiner = "and";
            

                        propnames.Add(String.Format("[ID] = @ID", criteria.ID));
               
            
            sb.AppendFormat("{0}", string.Join(String.Format(" {0} ", joiner), propnames));



            return sb.ToString();
        }


    }
}
