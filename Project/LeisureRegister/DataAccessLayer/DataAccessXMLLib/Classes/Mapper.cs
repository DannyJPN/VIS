using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.Attributes;
using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.DTO;
using DataAccessLayer.DataAccessGeneralLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataAccessXMLLib.Classes
{
    public class Mapper : IDataMapper
    {
        public int Delete(LeisureObjectDTO idto)
        {
            string filename = String.Format("{0}.xml", idto.GetType().Name);

            if (!File.Exists(filename))
            { File.Create(filename); }
            List<LeisureObjectDTO> leisobs = LoadFromXML(filename,idto);
            int todel = -1;
            for (int i = 0; i < leisobs.Count; i++)
            {
                if (leisobs[i].ID == idto.ID)
                { todel = i; break; }
            }
            if (todel >= 0)
            { leisobs.RemoveAt(todel); }
            ExportToXML(leisobs, filename);
            return todel >= 0 ? 1 : 0;
        }

        public int Insert(LeisureObjectDTO idto)
        {
           
            string filename = String.Format("{0}.xml", idto.GetType().Name);
  /*          if (!File.Exists(filename))
            { File.Create(filename); }
*/
            List<LeisureObjectDTO> leisobs = LoadFromXML(filename,idto);
            int ret = leisobs.Count;
            leisobs.Add(idto);
            ExportToXML(leisobs, filename);
            return leisobs.Count - ret;
        }

        public List<LeisureObjectDTO> Select(LeisureObjectDTO DTO)
        {
            return Select(DTO, true);
        }

        public List<LeisureObjectDTO> Select(LeisureObjectDTO DTO, bool conditions_together=true)
        {
            string filename = String.Format("{0}.xml", DTO.GetType().Name);
            if (!File.Exists(filename))
            { File.Create(filename); }
            List<LeisureObjectDTO> leisobs = LoadFromXML(filename,DTO);
            Type tcrit = DTO.GetType();
            List<LeisureObjectDTO> rets = new List<LeisureObjectDTO>();

            PropertyInfo[] props = tcrit.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (LeisureObjectDTO l in leisobs)
            {
                bool matches = false;
                foreach (PropertyInfo p in props)
                {
                    if (p.GetCustomAttributes().Where(x => x is StandardSQLAttribute || x is ForeignKeyAttribute || x is PrimaryKeyAttribute).Any())
                    {
                        if (p.GetValue(DTO) != null && p.GetValue(DTO).ToString() != "" && p.GetValue(DTO).ToString() != "0" && p.GetValue(DTO).ToString() != new DateTime().ToString() && p.GetValue(DTO).ToString() != new TimeSpan().ToString())
                        {
                            string a = p.GetValue(DTO).ToString();
                            string b = p.GetValue(l).ToString();

                            if (a == b)
                            {
                                matches = true;
                                if (conditions_together == false)
                                {
                                    break;
                                }
                            }
                            else
                            { matches = false; }
                        }

                    }



                }
                if (matches)
                {
                    rets.Add(l);
                }

            }


            return rets;
        }

        public int Update(LeisureObjectDTO idto)
        {
            string filename = String.Format("{0}.xml", idto.GetType().Name);
            if (!File.Exists(filename))
            { File.Create(filename); }
            List<LeisureObjectDTO> leisobs = LoadFromXML(filename,idto);
            int toupd = -1;
            for (int i = 0; i < leisobs.Count; i++)
            {
                if (leisobs[i].ID == idto.ID)
                { toupd = i; break; }
            }
            if (toupd >= 0)
            { leisobs[toupd] = idto; }
            ExportToXML(leisobs, filename);
            return toupd >= 0 ? 1 : 0;
        }


        void ExportToXML(List<LeisureObjectDTO> leisobs, string filename)
        {

            

            XmlSerializer serializer = null;
            if (leisobs == null||leisobs.Count<=0)
            { return; }
            if (leisobs[0].GetType() == typeof(AddressDTO))
            {
                serializer = new XmlSerializer(typeof(List<AddressDTO>));
                List<AddressDTO> leis = new List<AddressDTO>();
                foreach (LeisureObjectDTO l in leisobs)
                { leis.Add((AddressDTO)l); }
                using (TextWriter textWriter = new StreamWriter(filename))
                {
                    serializer.Serialize(textWriter, leis);
                }
            }
           else if (leisobs[0].GetType() == typeof(CityDTO))
            {
                serializer = new XmlSerializer(typeof(List<CityDTO>));
                List<CityDTO> leis = new List<CityDTO>();
                foreach (LeisureObjectDTO l in leisobs)
                { leis.Add((CityDTO)l); }
                using (TextWriter textWriter = new StreamWriter(filename))
                {
                    serializer.Serialize(textWriter, leis);
                }
            }
            else if (leisobs[0].GetType() == typeof(CityPartDTO))
            {
                serializer = new XmlSerializer(typeof(List<CityPartDTO>));
                List<CityPartDTO> leis = new List<CityPartDTO>();
                foreach (LeisureObjectDTO l in leisobs)
                { leis.Add((CityPartDTO)l); }
                using (TextWriter textWriter = new StreamWriter(filename))
                {
                    serializer.Serialize(textWriter, leis);
                }
            }
            else if (leisobs[0].GetType() == typeof(GroupLeaderDTO))
            {
                serializer = new XmlSerializer(typeof(List<GroupLeaderDTO>));
                List<GroupLeaderDTO> leis = new List<GroupLeaderDTO>();
                foreach (LeisureObjectDTO l in leisobs)
                { leis.Add((GroupLeaderDTO)l); }
                using (TextWriter textWriter = new StreamWriter(filename))
                {
                    serializer.Serialize(textWriter, leis);
                }
            }
            else if (leisobs[0].GetType() == typeof(GroupLeadingDTO))
            {
                serializer = new XmlSerializer(typeof(List<GroupLeadingDTO>));
                List<GroupLeadingDTO> leis = new List<GroupLeadingDTO>();
                foreach (LeisureObjectDTO l in leisobs)
                { leis.Add((GroupLeadingDTO)l); }
                using (TextWriter textWriter = new StreamWriter(filename))
                {
                    serializer.Serialize(textWriter, leis);
                }
            }
            else if (leisobs[0].GetType() == typeof(HobbyGroupDTO))
            {
                serializer = new XmlSerializer(typeof(List<HobbyGroupDTO>));
                List<HobbyGroupDTO> leis = new List<HobbyGroupDTO>();
                foreach (LeisureObjectDTO l in leisobs)
                { leis.Add((HobbyGroupDTO)l); }
                using (TextWriter textWriter = new StreamWriter(filename))
                {
                    serializer.Serialize(textWriter, leis);
                }
            }
            else if (leisobs[0].GetType() == typeof(ChildDTO))
            {
                serializer = new XmlSerializer(typeof(List<ChildDTO>));
                List<ChildDTO> leis = new List<ChildDTO>();
                foreach (LeisureObjectDTO l in leisobs)
                { leis.Add((ChildDTO)l); }
                using (TextWriter textWriter = new StreamWriter(filename))
                {
                    serializer.Serialize(textWriter, leis);
                }
            }
            else if (leisobs[0].GetType() == typeof(InsuranceCompanyDTO))
            {
                serializer = new XmlSerializer(typeof(List<InsuranceCompanyDTO>));
                List<InsuranceCompanyDTO> leis = new List<InsuranceCompanyDTO>();
                foreach (LeisureObjectDTO l in leisobs)
                { leis.Add((InsuranceCompanyDTO)l); }
                using (TextWriter textWriter = new StreamWriter(filename))
                {
                    serializer.Serialize(textWriter, leis);
                }
            }
            else if (leisobs[0].GetType() == typeof(ParentDTO))
            {
                serializer = new XmlSerializer(typeof(List<ParentDTO>));
                List<ParentDTO> leis = new List<ParentDTO>();
                foreach (LeisureObjectDTO l in leisobs)
                { leis.Add((ParentDTO)l); }
                using (TextWriter textWriter = new StreamWriter(filename))
                {
                    serializer.Serialize(textWriter, leis);
                }
            }
            else return;









          


            
        }
        List<LeisureObjectDTO> LoadFromXML(string filename,LeisureObjectDTO sample)
        {
            List<LeisureObjectDTO> leisobs= new List<LeisureObjectDTO>();
            XmlSerializer deserializer = null;

            if (sample.GetType() == typeof(AddressDTO))
            {
                deserializer = new XmlSerializer(typeof(List<AddressDTO>));
                using (TextReader textReader = new StreamReader(filename))
                {

                    List<AddressDTO> leis = (List<AddressDTO>)deserializer.Deserialize(textReader);
                    foreach (AddressDTO x in leis)
                    {
                        leisobs.Add(x);
                    }
                }
            }
            else if (sample.GetType() == typeof(CityDTO))
            {
                deserializer = new XmlSerializer(typeof(List<CityDTO>));
                using (TextReader textReader = new StreamReader(filename))
                {

                    List<CityDTO> leis = (List<CityDTO>)deserializer.Deserialize(textReader);
                    foreach (CityDTO x in leis)
                    {
                        leisobs.Add(x);
                    }
                }
            }
            else if (sample.GetType() == typeof(CityPartDTO))
            {
                deserializer = new XmlSerializer(typeof(List<CityPartDTO>));
                using (TextReader textReader = new StreamReader(filename))
                {

                    List<CityPartDTO> leis = (List<CityPartDTO>)deserializer.Deserialize(textReader);
                    foreach (CityPartDTO x in leis)
                    {
                        leisobs.Add(x);
                    }
                }
            }
            else if (sample.GetType() == typeof(GroupLeaderDTO))
            {
                deserializer = new XmlSerializer(typeof(List<GroupLeaderDTO>));
                using (TextReader textReader = new StreamReader(filename))
                {

                    List<GroupLeaderDTO> leis = (List<GroupLeaderDTO>)deserializer.Deserialize(textReader);
                    foreach (GroupLeaderDTO x in leis)
                    {
                        leisobs.Add(x);
                    }
                }
            }
            else if (sample.GetType() == typeof(GroupLeadingDTO))
            {
                deserializer = new XmlSerializer(typeof(List<GroupLeadingDTO>));
                using (TextReader textReader = new StreamReader(filename))
                {

                    List<GroupLeadingDTO> leis = (List<GroupLeadingDTO>)deserializer.Deserialize(textReader);
                    foreach (GroupLeadingDTO x in leis)
                    {
                        leisobs.Add(x);
                    }
                }
            }
            else if (sample.GetType() == typeof(HobbyGroupDTO))
            {
                deserializer = new XmlSerializer(typeof(List<HobbyGroupDTO>));
                using (TextReader textReader = new StreamReader(filename))
                {

                    List<HobbyGroupDTO> leis = (List<HobbyGroupDTO>)deserializer.Deserialize(textReader);
                    foreach (HobbyGroupDTO x in leis)
                    {
                        leisobs.Add(x);
                    }
                }
            }
            else if (sample.GetType() == typeof(ChildDTO))
            {
                deserializer = new XmlSerializer(typeof(List<ChildDTO>));
                using (TextReader textReader = new StreamReader(filename))
                {

                    List<ChildDTO> leis = (List<ChildDTO>)deserializer.Deserialize(textReader);
                    foreach (ChildDTO x in leis)
                    {
                        leisobs.Add(x);
                    }
                }
            }
            else if (sample.GetType() == typeof(InsuranceCompanyDTO))
            {
                deserializer = new XmlSerializer(typeof(List<InsuranceCompanyDTO>));
                using (TextReader textReader = new StreamReader(filename))
                {

                    List<InsuranceCompanyDTO> leis = (List<InsuranceCompanyDTO>)deserializer.Deserialize(textReader);
                    foreach (InsuranceCompanyDTO x in leis)
                    {
                        leisobs.Add(x);
                    }
                }
            }
            else if (sample.GetType() == typeof(ParentDTO))
            {
                deserializer = new XmlSerializer(typeof(List<ParentDTO>));
                using (TextReader textReader = new StreamReader(filename))
                {

                    List<ParentDTO> leis = (List<ParentDTO>)deserializer.Deserialize(textReader);
                    foreach (ParentDTO x in leis)
                    {
                        leisobs.Add(x);
                    }
                }
            }
            else return null;



        

            return leisobs;
        }

    }
}
