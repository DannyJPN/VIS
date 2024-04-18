
using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes;
using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.Attributes;
using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.DTO;
using DataAccessLayer.DataAccessGeneralLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataAccessMSSQLLib.Classes
{
    public class DataMapper : IDataMapper
    {
        private static ReflectCommandCreatorMSSQL Cmdcreator;

        public DataMapper()
        {
            if (Cmdcreator == null)
            {
                Cmdcreator = new ReflectCommandCreatorMSSQL();
            }
        }

        public int Delete(LeisureObjectDTO idto)
        {


            MSSQLDatabase db = new MSSQLDatabase();
            db.Connect();


            SqlCommand command = db.CreateCommand(Cmdcreator.CreateDelete(idto));
            PrepareDeleteCommand(command, idto);


            int ret = db.ExecuteNonQuery(command);

            db.Close();

            return ret;


            //return 0;
        }

        public int Insert(LeisureObjectDTO idto)
        {
            MSSQLDatabase db = new MSSQLDatabase();
            db.Connect();
            SqlCommand command = db.CreateCommand(Cmdcreator.CreateInsert(idto));
            PrepareInsertCommand(command, idto);
            int ret = db.ExecuteNonQuery(command);
            db.Close();

            return ret;
        }

        public List<LeisureObjectDTO> Select(LeisureObjectDTO criteria, bool conditions_together = true)
        {
            MSSQLDatabase db = new MSSQLDatabase();
            db.Connect();
           SqlCommand command = db.CreateCommand(Cmdcreator.CreateSelect(criteria, conditions_together));
            PrepareSelectCommand(command, criteria);
            SqlDataReader reader = db.Select(command);

            List<LeisureObjectDTO> resulttab = LoadData(reader, criteria);

            reader.Close();
            db.Close();



            return resulttab;
        }



        public int Update(LeisureObjectDTO idto)
        {
            MSSQLDatabase db = new MSSQLDatabase();
            db.Connect();
            SqlCommand command = db.CreateCommand(Cmdcreator.CreateUpdate(idto));
            PrepareUpdateCommand(command, idto);
            int ret = db.ExecuteNonQuery(command);
            db.Close();





            return ret;
        }


        private List<LeisureObjectDTO> LoadData(SqlDataReader reader, LeisureObjectDTO pattern)
        {
            Type t = pattern.GetType();
            PropertyInfo[] props = t.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic);
            List<LeisureObjectDTO> result = new List<LeisureObjectDTO>();
            while (reader.Read())
            {
                LeisureObjectDTO target = (LeisureObjectDTO)Activator.CreateInstance(t);
                foreach (PropertyInfo p in props)
                {
                    if (p.GetCustomAttributes().Where(x => x is StandardSQLAttribute || x is ForeignKeyAttribute || x is PrimaryKeyAttribute).Any())
                    { 
                        Type proptype = p.PropertyType;
                    object val = Convert.ChangeType(reader[p.Name], proptype);
                    p.SetValue(target, val);
                    }
                }
                result.Add(target);
            }


            return result;
        }


        public static void PrepareSelectCommand(SqlCommand select, LeisureObjectDTO criteria)
        {
            if (criteria == null)
            { return; }
            //SqlCommand select = new SqlCommand(CreateSelect(DTO, criteria));
            Type tcrit = criteria.GetType();
            PropertyInfo[] props = tcrit.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (PropertyInfo p in props)
            {
                if (p.GetCustomAttributes().Where(x => x is StandardSQLAttribute || x is ForeignKeyAttribute || x is PrimaryKeyAttribute).Any())
                {
                    if (p.GetValue(criteria) != null && p.GetValue(criteria).ToString() != "" && p.GetValue(criteria).ToString() != "0" && p.GetValue(criteria).ToString() != new DateTime().ToString() && p.GetValue(criteria).ToString() != new TimeSpan().ToString())
                    {

                        select.Parameters.AddWithValue(String.Format("@{0}", p.Name), p.GetValue(criteria));

                    }

                }


            }




            //return select;
        }
        public static void PrepareInsertCommand(SqlCommand insert, LeisureObjectDTO DTO)
        {
            // SqlCommand insert = new SqlCommand(CreateInsert(DTO));

            Type tDTO = DTO.GetType();
            PropertyInfo[] props = tDTO.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (PropertyInfo p in props)
            {
                if (p.GetValue(DTO) != null && p.GetValue(DTO).ToString() != "" && p.GetValue(DTO).ToString() != "0" && p.GetValue(DTO).ToString() != new DateTime().ToString() && p.GetValue(DTO).ToString() != new TimeSpan().ToString())
                {
                    if (p.GetCustomAttributes().Where(x => x is StandardSQLAttribute || x is ForeignKeyAttribute).Any())
                    {
                        insert.Parameters.AddWithValue(String.Format("@{0}", p.Name), p.GetValue(DTO));

                    }

                }


            }




            //return insert;
        }
        public static void PrepareUpdateCommand(SqlCommand update, LeisureObjectDTO DTO)
        {
            //SqlCommand update = new SqlCommand(CreateUpdate(DTO, DTO));
            Type t = DTO.GetType();
            PropertyInfo[] props = t.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (PropertyInfo p in props)
            {
                if (p.GetValue(DTO) != null && p.GetValue(DTO).ToString() != "" && p.GetValue(DTO).ToString() != "0" && p.GetValue(DTO).ToString() != new DateTime().ToString() && p.GetValue(DTO).ToString() != new TimeSpan().ToString())
                {
                    if (p.GetCustomAttributes().Where(x => x is StandardSQLAttribute || x is ForeignKeyAttribute || x is PrimaryKeyAttribute).Any())
                    {
                        update.Parameters.AddWithValue(String.Format("@{0}", p.Name), p.GetValue(DTO));

                    }

                }


            }





            //return update;
        }
        public static void PrepareDeleteCommand(SqlCommand delete, LeisureObjectDTO criteria)
        {
            //SqlCommand delete = new SqlCommand(CreateDelete(criteria));
            Type tcrit = criteria.GetType();
            PropertyInfo[] props = tcrit.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (PropertyInfo p in props)
            {
                if (p.GetValue(criteria) != null && p.GetValue(criteria).ToString() != "" && p.GetValue(criteria).ToString() != "0" && p.GetValue(criteria).ToString() != new DateTime().ToString() && p.GetValue(criteria).ToString() != new TimeSpan().ToString())
                {
                    if (p.GetCustomAttributes().Where(x => x is PrimaryKeyAttribute).Any())
                    {
                        delete.Parameters.AddWithValue(String.Format("@{0}", p.Name), p.GetValue(criteria));

                    }

                }


            }





            //return delete;
        }




        public List<LeisureObjectDTO> Select(LeisureObjectDTO DTO)
        {
            return Select(DTO, true);
        }

        
    }
}
