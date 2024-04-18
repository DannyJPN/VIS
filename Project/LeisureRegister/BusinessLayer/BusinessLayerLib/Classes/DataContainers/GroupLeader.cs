using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes;
using DataAccessLayer.DataAccessGeneralLib.Interfaces;
using DataAccessLayer.DataAccessMSSQLLib.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.DTO;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace BusinessLayer.BusinessLayerLib.Classes.DataContainers
{
    public class GroupLeader:Person
    {
        
        public static IDataMapper Mapper { get; set; }
        private GroupLeader() : this(0)
        {

        }
        private GroupLeader(int ID) : base(ID)
        {
            if (Mapper == null)
            {
                Mapper = new DataMapper();
            }

        }
        private GroupLeader(int ID,string name, string surname, string email, int phone) : base(ID,name,surname,email,phone)
        {
            if (Mapper == null)
            {
                Mapper = new DataMapper();
            }

        }

        public static GroupLeader CreateInstance()
        {
            return CreateInstance(0);
        }
        public static GroupLeader CreateInstance(int ID)
        {
            return CreateInstance(ID, "", "", "", 0);
        }
        public static GroupLeader CreateInstance(int ID, string name, string surname, string email, int phone)
        {
            GroupLeader g = new GroupLeader(ID,name,  surname, email, phone);
            g.SaveToMemory();


            Debug.WriteLine("{0}\tNew {1} created \n{2}.", DateTime.Now.ToString(), g.GetType().Name, g.ToString());

            return g;
        }


        public override string ToString()
        {
            return base.ToString();
        }
        public static GroupLeader Load(int ID)
        {

            if (Mapper == null) { Mapper = new DataMapper(); };List<LeisureObjectDTO> groupleaders = Mapper.Select(new GroupLeaderDTO(ID));

            if (groupleaders.Count <= 0) {
                return null; }

            GroupLeaderDTO groupleaderDTO = (GroupLeaderDTO)groupleaders[0];
            return MapFromDTO(groupleaderDTO);

        }
        public static List<GroupLeader> Load(GroupLeader crit,bool and = true)
        {
            if (Mapper == null) { Mapper = new DataMapper(); };List<LeisureObjectDTO> adds = Mapper.Select(MapToDTO(crit),and);

            if (adds.Count <= 0) {
                return null; }
            List<GroupLeader> resultcol = new List<GroupLeader>();
            foreach (LeisureObjectDTO item in adds)
            { resultcol.Add(MapFromDTO((GroupLeaderDTO)item)); }

            return resultcol;

        }

        private static GroupLeader MapFromDTO(GroupLeaderDTO lDTO)
        {
            if (lDTO == null)
            {return new GroupLeader();}

            GroupLeader l = new GroupLeader()
            {
                ID = lDTO.ID,
                Name=lDTO.Name,
                Email=lDTO.Email,
                Phone=lDTO.Phone,
                Surname=lDTO.Surname

            };
            return l;
        }
        private static GroupLeaderDTO MapToDTO(GroupLeader lead)
        {
            if (lead == null)
            {return new GroupLeaderDTO();}

            GroupLeaderDTO lDTO = new GroupLeaderDTO()
            {
                ID = lead.ID,
                Name = lead.Name,
                Email = lead.Email,
                Phone = lead.Phone,
                Surname = lead.Surname

            };
            return lDTO;
        }

       


        public static int Save(GroupLeader lead,bool newobject=true)
        {
            GroupLeaderDTO lDTO = GroupLeader.MapToDTO(lead);
            return  (newobject ? Mapper.Insert(lDTO) : Mapper.Update(lDTO));

        }
       /* public static int SaveAs(GroupLeader lead)
        {
            GroupLeaderDTO lDTO = GroupLeader.MapToDTO(lead);
            return Mapper.Insert(lDTO);

        }*/


    }
}
