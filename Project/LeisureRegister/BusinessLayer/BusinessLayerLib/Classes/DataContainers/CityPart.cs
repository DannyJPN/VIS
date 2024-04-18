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
    public class CityPart : LeisureObject
    {
        
        public static IDataMapper Mapper { get; set; }
        private int origincityid;
         private City origincity = null;
        public string Name { get; set; }
        public City OriginCity
        {
            get
            {
                if (origincity == null)
                {
                    OriginCity = City.Load(origincityid);

                }
                else

                {
                    if (origincity.ID != origincityid)
                    {
                        OriginCity = City.Load(origincityid);

                    }


                }
                return origincity;
            }
            set
            {
                if (value != null)
                    origincityid = value.ID;
                origincity = value;
            }
        }

        public override string ToString()
        {
            return base.ToString() + "|" + String.Format("{0}|{1}", Name, origincityid);
        }
        private CityPart() : this(0)
        {

        }
        private CityPart(int ID) : base(ID)
        {
            if (Mapper == null)
            {
                Mapper = new DataMapper();
            }

        }
        private CityPart(int ID, int origincityid, string name) : this(ID)
        {
            this.origincityid = origincityid;
            Name = name;
        }
        private CityPart(int ID, City origincity, string name) : this(ID)
        {
            OriginCity = origincity;
            Name = name;
        }


        public static CityPart CreateInstance()
        {
            return CreateInstance(0);
        }
        public static CityPart CreateInstance(int ID)
        {
            return CreateInstance(ID, null, "");
        }
        public static CityPart CreateInstance(int ID, City origincity, string name)
        {
            CityPart c = new CityPart(ID, origincity, name);
            c.SaveToMemory();

            Debug.WriteLine("{0}\tNew {1} created \n{2}.", DateTime.Now.ToString(), c.GetType().Name, c.ToString());

            return c;
        }

        public static CityPart Load(int ID)
        {

            if (Mapper == null) { Mapper = new DataMapper(); };List<LeisureObjectDTO> cityparts = Mapper.Select(new CityPartDTO(ID));

            if (cityparts.Count <= 0) {
                return null; }

            CityPartDTO citypartDTO = (CityPartDTO)cityparts[0];
            return MapFromDTO(citypartDTO);

        }
        public static List<CityPart> Load(CityPart crit,bool and = true)
        {
            if (Mapper == null) { Mapper = new DataMapper(); };List<LeisureObjectDTO> adds = Mapper.Select(MapToDTO(crit),and);

            if (adds.Count <= 0) {
                return null; }
            List<CityPart> resultcol = new List<CityPart>();
            foreach (LeisureObjectDTO item in adds)
            { resultcol.Add(MapFromDTO((CityPartDTO)item)); }

            return resultcol;

        }


        private static CityPart MapFromDTO(CityPartDTO cpDTO)
        {
            if (cpDTO == null)
            {return new CityPart();}

            CityPart a = new CityPart()
            {
                ID = cpDTO.ID,
                Name = cpDTO.Name,
                origincityid = cpDTO.OriginCityID
            };
            return a;
        }
        private static CityPartDTO MapToDTO(CityPart cp)
        {
            if (cp == null)
            {return new CityPartDTO();}

            CityPartDTO cpDTO = new CityPartDTO()
            {
                ID = cp.ID,
                Name = cp.Name,
                OriginCityID = cp.origincityid 
            };
            return cpDTO;
        }




        public static int Save(CityPart cp, bool newobject=true)
        {
           
            int saved = 0;
            if (cp.origincity != null)
            {
                List<City> refcity = City.Load(cp.origincity);
                if (refcity == null)
                {
                    saved += City.Save(cp.origincity, true);
                    cp.OriginCity = City.Load(cp.origincity)[0];
                }
                else
                {
                    cp.OriginCity = refcity[0];
                    saved += City.Save(cp.origincity, false);
                }
            }
            CityPartDTO cpDTO = CityPart.MapToDTO(cp);
            return saved + (newobject ? Mapper.Insert(cpDTO) : Mapper.Update(cpDTO));

        }
        /*  public static int SaveAs(CityPart cp)
          {
              CityPartDTO cpDTO = CityPart.MapToDTO(cp);
              int saved = 0;
              if (cp.origincity != null) { saved += City.SaveAs(cp.origincity); }
              return Mapper.Insert(cpDTO);

          }*/

    }
}