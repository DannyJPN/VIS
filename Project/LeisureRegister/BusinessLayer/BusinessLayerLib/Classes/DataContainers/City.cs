using System;
using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes;
using DataAccessLayer.DataAccessGeneralLib.Interfaces;
using DataAccessLayer.DataAccessMSSQLLib.Classes;
using System.Diagnostics;
using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.DTO;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BusinessLayer.BusinessLayerLib.Classes.DataContainers
{
    public class City:LeisureObject
    {
        
        public static IDataMapper Mapper { get; set; }
        public string Name { get; set; }
        private City() : this(0)
        {

        }
        private City(int ID) : base(ID)
        {
            if (Mapper == null)
            {
                Mapper = new DataMapper();
            }

        }
        private City(int ID, string name) : this(ID)
        {
            Name = name;
        }
          public override string ToString()
        {
            return base.ToString() + "|" + String.Format("{0}", Name);
        }
        public static City Load(int ID)
        {
            if (Mapper == null) { Mapper = new DataMapper(); };List<LeisureObjectDTO> cities = Mapper.Select(new CityDTO(ID));

            if (cities.Count <= 0) {
                return null; }

            CityDTO cityDTO = (CityDTO)cities[0];
            return MapFromDTO(cityDTO);

        }
        public static List<City> Load(City crit,bool and=true)
        {
            if (Mapper == null) { Mapper = new DataMapper(); };List<LeisureObjectDTO> adds = Mapper.Select(MapToDTO(crit),and);

            if (adds.Count <= 0) {
                return null; }
            List<City> resultcol = new List<City>();
            foreach (LeisureObjectDTO item in adds)
            { resultcol.Add(MapFromDTO((CityDTO)item)); }

            return resultcol;

        }
        private static City MapFromDTO(CityDTO cDTO)
        {
            if (cDTO == null)
            {return new City();}

            City c = new City()
            {
                ID = cDTO.ID,
                Name = cDTO.Name
            };
            return c;
        }

        public static City CreateInstance()
        {
            return CreateInstance(0);
        }
        public static City CreateInstance(int ID)
        {
            return CreateInstance(ID,"");
        }
        public static City CreateInstance(int ID,string name)
        {
            City c = new City(ID,name);
            c.SaveToMemory();

            Debug.WriteLine("{0}\tNew {1} created \n{2}.", DateTime.Now.ToString(), c.GetType().Name, c.ToString());

            return c;
        }


        private static CityDTO MapToDTO(City c)
        {
            if (c == null)
            {return new CityDTO();}

            CityDTO cDTO = new CityDTO()
            {
                ID = c.ID,
                Name = c.Name
            };
            return cDTO;
        }




        public static int Save(City c, bool newobject=true)
        {
            CityDTO cDTO = City.MapToDTO(c);

            return  (newobject ? Mapper.Insert(cDTO) : Mapper.Update(cDTO));

        }
     /*   public static int SaveAs(City c)
        {
            CityDTO cDTO = City.MapToDTO(c);
            return Mapper.Insert(cDTO);

        }
        */
    }
}


