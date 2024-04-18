using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes;
using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.DTO;
using DataAccessLayer.DataAccessGeneralLib.Interfaces;
using DataAccessLayer.DataAccessMSSQLLib.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace BusinessLayer.BusinessLayerLib.Classes.DataContainers
{
    public class Address : LeisureObject
    {
        
        public static IDataMapper Mapper { get; set; }
        private int homecityid;
        private int homecitypartid;
        private int orientationnumber;
        private int descriptionnumber;
        public override string ToString()
        {
            return base.ToString()+"|"+string.Format("{0}|{1}|{2}|{3}|{4}|{5}", homecityid,homecitypartid,Street,OrientationNumber,DescriptionNumber,PostalCode);
        }
        private City homecity = null;
        private CityPart homecitypart = null;
        private int postalcode;

        public string Street { get; set; }
        public int OrientationNumber
        {
            get { return orientationnumber; }
            set
            {
                try
                {
                    orientationnumber = value;
                }
                catch
                {
                    orientationnumber = 0;
                }
            }
        }
        public int DescriptionNumber
        {
            get { return descriptionnumber; }
            set
            {
                try
                {
                    descriptionnumber = value;
                }
                catch
                {
                    descriptionnumber = 0;
                }
            }
        }
        
        public City HomeCity
        {
            get
            {
                if (homecity == null)
                {
                    HomeCity = City.Load(homecityid);

                }
                else
                {
                    if (homecity.ID != homecityid)
                    {
                        HomeCity = City.Load(homecityid);

                    }


                }
                return homecity;
            }
            set
            {
                if (value != null)
                    homecityid = value.ID;
                homecity = value;
            }
        }
        
        public CityPart HomeCityPart
        {
            get
            {
                if (homecitypart == null)
                {
                    HomeCityPart = CityPart.Load(homecitypartid);
                    return homecitypart;
                }
                else
                {
                    if (homecitypart.ID != homecitypartid)
                    {
                        HomeCityPart = CityPart.Load(homecitypartid);
                        return homecitypart;
                    }

                    return homecitypart;
                }
            }
            set
            {
                if (value != null)
                    homecitypartid = value.ID;
                homecitypart = value;
                if (homecity != null)
                {
                    homecitypart.OriginCity = homecity;
                }
            }
        }
        public int PostalCode
        {
            get { return postalcode; }
            set
            {
                try
                {
                    postalcode = value;
                }
                catch
                {
                    postalcode = 0;
                }
            }
        }
        private Address() : this(0)
        {
            
        }
        private Address(int ID) : base(ID)
        {
            if (Mapper == null)
            {
                Mapper = new DataMapper();
            }

        }

        private Address(int ID, string street, int homecityid, int homecitypartid, int postalcode,int orientationnumber,int descriptionnumber) : this(ID)
        {
            Street = street;
            this.homecityid = homecityid;
            this.homecitypartid = homecitypartid;
            PostalCode = postalcode;

            OrientationNumber = orientationnumber;
            DescriptionNumber = descriptionnumber;
        }
        private Address(int ID, string street, City homecity, CityPart homecitypart, int postalcode, int orientationnumber, int descriptionnumber) : this(ID)
        {
            Street = street;
            HomeCity = homecity;
            HomeCityPart = homecitypart;
            PostalCode = postalcode;
            OrientationNumber = orientationnumber;
            DescriptionNumber = descriptionnumber;
        }

        public static Address CreateInstance()
        {
            return CreateInstance(0);
        }

        public static Address CreateInstance(int ID)
        {
            return CreateInstance(ID, "", null, null,0,0,0);
        }
        public static Address CreateInstance(int ID, string street, City homecity, CityPart homecitypart, int postalcode,int orientationnumber, int descriptionnumber)
        {
            Address a = new Address(ID,street,homecity,homecitypart,postalcode,orientationnumber,descriptionnumber);
            a.SaveToMemory();

            Debug.WriteLine("{0}\tNew {1} created \n{2}.", DateTime.Now.ToString(), a.GetType().Name, a.ToString());
            return a;
        }

        public static Address Load(int ID)
        {
if (Mapper == null) { Mapper = new DataMapper(); };List<LeisureObjectDTO> adds=Mapper.Select(new AddressDTO(ID));
           
            if (adds.Count<=0) {
                return null; }

            AddressDTO addressDTO = (AddressDTO)adds[0];
            return MapFromDTO(addressDTO);

        }
        public static List<Address> Load(Address crit,bool and=true)
        {
            if (Mapper == null) { Mapper = new DataMapper(); };List<LeisureObjectDTO> adds = Mapper.Select(MapToDTO(crit),and);

            if (adds.Count <= 0) {
                return null; }
            List<Address> resultcol = new List<Address>();
            foreach (LeisureObjectDTO item in adds)
            { resultcol.Add(MapFromDTO((AddressDTO)item)); }
          
            return resultcol;

        }


        private static Address MapFromDTO(AddressDTO aDTO)
        {if (aDTO == null)
            {return new Address();}


            Address a = new Address()
            {
                ID = aDTO.ID,
                Street = aDTO.Street,
                OrientationNumber = aDTO.OrientationNumber,
                DescriptionNumber = aDTO.DescriptionNumber,
                homecityid = aDTO.HomeCityID,
                homecitypartid = aDTO.HomeCityPartID,
                PostalCode = aDTO.PostalCode
            };
            return a;
        }
        private static AddressDTO MapToDTO(Address add)
        {
            if (add == null)
            {return new AddressDTO();}


            AddressDTO aDTO = new AddressDTO()
            {
                ID = add.ID,
                Street = add.Street,
                OrientationNumber = add.OrientationNumber,
                DescriptionNumber = add.DescriptionNumber,
                HomeCityID =  add.homecityid ,
                HomeCityPartID =  add.homecitypartid ,
                PostalCode = add.PostalCode
            };
            return aDTO;
        }




        public static int Save(Address add,bool newobject = true)
        {
            
            int saved = 0;
            if (add.homecity != null)
            {

                List<City> refcity = City.Load(add.homecity);
                if (refcity == null)
                {
                    saved += City.Save(add.homecity,true);
                    add.HomeCity = City.Load(add.homecity)[0];
                }
                else
                {
                    add.HomeCity = refcity[0];
                    saved += City.Save(add.homecity, false);
                   
                }
            }
            if (add.homecitypart != null)
            {
                add.homecitypart.OriginCity = add.homecity;
                List<CityPart> refcitypart = CityPart.Load(add.homecitypart);
                if (refcitypart == null)
                {
                    saved += CityPart.Save(add.homecitypart, true);

                    add.HomeCityPart = CityPart.Load(add.homecitypart)[0];
                }
                else
                {
                    add.HomeCityPart = refcitypart[0];
                    saved += CityPart.Save(add.homecitypart, false);
                    
                }
            }
            AddressDTO aDTO = Address.MapToDTO(add);
            return saved +( newobject?Mapper.Insert(aDTO):Mapper.Update(aDTO));

       

        }
      /*  public static int SaveAs(Address add)
        {
            AddressDTO aDTO = Address.MapToDTO(add);
            int saved = 0;
            if (add.homecity != null)
            {
                saved += City.SaveAs(add.homecity);
            }
            if (add.homecitypart != null)
            {
                saved += CityPart.SaveAs(add.homecitypart);
            }

            return saved + Mapper.Insert(aDTO);


        }*/


    }
}