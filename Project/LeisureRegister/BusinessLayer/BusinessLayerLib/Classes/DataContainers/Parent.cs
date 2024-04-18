using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes;
using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.DTO;
using DataAccessLayer.DataAccessGeneralLib.Interfaces;
using DataAccessLayer.DataAccessMSSQLLib.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessLayer.BusinessLayerLib.Classes.DataContainers
{
    public class Parent : Person
    {
        
        public static IDataMapper Mapper { get; set; }
        public string Job { get; set; }
        public char Gender { get; set; }
        public override string ToString()
        {
            return base.ToString() + "|" + string.Format("{0}|{1}", Job, Gender);
        }
        private Parent() : this(0)
        {

        }
        private Parent(int ID) : base(ID)
        {
            if (Mapper == null)
            {
                Mapper = new DataMapper();
            }

        }
        public Parent(int ID, string name, string surname, string email, int phone) : base(ID, name, surname, email, phone)
        {
            if (Mapper == null)
            {
                Mapper = new DataMapper();
            }

        }


        private Parent(int ID, string name, string surname, string email, int phone, string job, char gender) : this(ID, name, surname, email, phone)
        {

            Job = job;
            Gender = gender;
        }
        public static Parent CreateInstance()
        {
            return CreateInstance(0);
        }
        public static Parent CreateInstance(int ID)
        {
            return CreateInstance(ID, "", "", "", 0);
        }
        public static Parent CreateInstance(int ID, string name, string surname, string email, int phone)
        {
            return CreateInstance(ID, name, surname, email, phone, "", 'u');
        }
        public static Parent CreateInstance(int ID, string name, string surname, string email, int phone, string job, char gender)
        {
            Parent p = new Parent(ID, name, surname, email, phone, job, gender);
            p.SaveToMemory();


            Debug.WriteLine("{0}\tNew {1} created \n{2}.", DateTime.Now.ToString(), p.Name, p.ToString());
            return p;
        }


        public static Parent Load(int ID)
        {

            if (Mapper == null) { Mapper = new DataMapper(); };List<LeisureObjectDTO> parents = Mapper.Select(new ParentDTO(ID));

            if (parents.Count <= 0) {
                return null; }

            ParentDTO parentDTO = (ParentDTO)parents[0];
            return MapFromDTO(parentDTO);

        }

        public static List<Parent> Load(Parent crit,bool and = true)
        {
            if (Mapper == null) { Mapper = new DataMapper(); };List<LeisureObjectDTO> adds = Mapper.Select(MapToDTO(crit),and);

            if (adds.Count <= 0) {
                return null; }
            List<Parent> resultcol = new List<Parent>();
            foreach (LeisureObjectDTO item in adds)
            { resultcol.Add(MapFromDTO((ParentDTO)item)); }

            return resultcol;

        }

        private static Parent MapFromDTO(ParentDTO paDTO)
        {
            if (paDTO == null)
            {return new Parent();}

            Parent pa = new Parent()
            {
                ID = paDTO.ID,
                Name = paDTO.Name,
                Email = paDTO.Email,
                Phone = paDTO.Phone,
                Surname = paDTO.Surname,
                Gender = paDTO.Gender,
                Job = paDTO.Job
            };
            return pa;
        }
        private static ParentDTO MapToDTO(Parent pa)
        {
            if (pa == null)
            {return new ParentDTO();}

            ParentDTO paDTO = new ParentDTO()
            {
                ID = pa.ID,
                Name = pa.Name,
                Email = pa.Email,
                Phone = pa.Phone,
                Surname = pa.Surname,
                Gender = pa.Gender,
                Job = pa.Job
            };
            return paDTO;
        }


        public static int Save(Parent pa, bool newobject = true)
        {
            ParentDTO paDTO = Parent.MapToDTO(pa);

            return (newobject ? Mapper.Insert(paDTO) : Mapper.Update(paDTO));

        }
        /*public static int SaveAs(Parent pa)
        {
            ParentDTO paDTO = Parent.MapToDTO(pa);
            return Mapper.Insert(paDTO);

        }
        */
    }
}
