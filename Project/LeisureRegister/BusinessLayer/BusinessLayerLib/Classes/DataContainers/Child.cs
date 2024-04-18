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
    public class Child : Person
    {
        
        public static IDataMapper Mapper { get; set; }
        private int homeaddressid;
        private int companyid;
        private int motherid;
        private int fatherid;
        private Address homeaddress = null;
        private InsuranceCompany company = null;
        private Parent mother = null;
        private Parent father = null;
        public int RegistrationNumber { get; set; }
        
        public Address HomeAddress
        {
            get
            {
                if (homeaddress == null)
                {
                    HomeAddress = Address.Load(homeaddressid);

                }
                else
                {
                    if (homeaddress.ID != homeaddressid)
                    {
                        HomeAddress = Address.Load(homeaddressid);

                    }


                }
                return homeaddress;
            }
            set
            {
                if (value != null)
                    homeaddressid = value.ID;
                homeaddress = value;
            }
        }
        
        public InsuranceCompany Company
        {
            get
            {
                if (company == null)
                {
                    Company = InsuranceCompany.Load(companyid);

                }
                else
                {
                    if (company.ID != companyid)
                    {
                        Company = InsuranceCompany.Load(companyid);

                    }


                }
                return company;
            }
            set
            {
                if (value != null)
                    companyid = value.ID;
                company = value;
            }
        }
        public string HealthState { get; set; }
        public string Comments { get; set; }
        public DateTime BirthDate { get; set; }
        public string SchoolName { get; set; }
        
        public Parent Mother
        {
            get
            {
                if (mother == null)
                {
                    Mother = Parent.Load(motherid);

                }
                else
                {
                    if (mother.ID != motherid)
                    {
                        Mother = Parent.Load(motherid);

                    }


                }
                return mother;
            }
            set
            {
                if (value != null)
                    motherid = value.ID;
                mother = value;
            }
        }
        
        public Parent Father
        {
            get
            {
                if (father == null)
                {
                    Father = Parent.Load(fatherid);

                }
                else
                {
                    if (father.ID != fatherid)
                    {
                        Father = Parent.Load(fatherid);

                    }


                }
                return father;
            }
            set
            {
                if (value != null)
                    fatherid = value.ID;
                father = value;
            }
        }
        public override string ToString()
        {
            return string.Format("{0} {1}",RegistrationNumber,base.ToString());
        }
        
        private Child() : this(0)
        {

        }
        private Child(int ID) : base(ID)
        {
            if (Mapper == null)
            {
                Mapper = new DataMapper();
            }

        }
        private Child(int ID, string name, string surname, string email, int phone) : base(ID, name, surname, email, phone)
        {

        }
        private Child(int ID, string name, string surname, string email, int phone, int registrationnumber, DateTime birthdate, int motherid, int fatherid, int companyid, string healthstate, string comments, string schoolname, int homeaddressid) : this(ID, name, surname, email, phone)
        {
            RegistrationNumber = registrationnumber;
            BirthDate = birthdate;
            this.motherid = motherid;
            this.fatherid = fatherid;
            this.companyid = companyid;
            HealthState = healthstate;
            Comments = comments;
            SchoolName = schoolname;
            this.homeaddressid = homeaddressid;
        }
        private Child(int ID, string name, string surname, string email, int phone, int registrationnumber, DateTime birthdate, Parent mother, Parent father, InsuranceCompany company, string healthstate, string comments, string schoolname, Address homeaddress) : this(ID, name, surname, email, phone)
        {
            RegistrationNumber = registrationnumber;
            BirthDate = birthdate;
            Mother = mother;
            Father = father;
            Company = company;
            HealthState = healthstate;
            Comments = comments;
            SchoolName = schoolname;
            HomeAddress = homeaddress;
        }



        public static Child CreateInstance()
        {
            return CreateInstance(0);
        }
        public static Child CreateInstance(int ID)
        {
            return CreateInstance(ID, "", "", "", 0);
        }
        public static Child CreateInstance(int ID, string name, string surname, string email, int phone)
        {
            return CreateInstance(ID, name, surname, email, phone, 0, new DateTime(),  "", "", "");
        }
public static Child CreateInstance(int ID, string name, string surname, string email, int phone, int registrationnumber, DateTime birthdate, string healthstate, string comments, string schoolname)
        {
            return CreateInstance(ID, name, surname, email, phone, registrationnumber, birthdate, null, null, null, healthstate, comments,schoolname , null);
        }

        public static Child CreateInstance(int ID, string name, string surname, string email, int phone, int registrationnumber, DateTime birthdate, Parent mother, Parent father, InsuranceCompany company, string healthstate, string comments, string schoolname, Address homeaddress)
        {
            Child c = new Child(ID, name, surname, email, phone, registrationnumber, birthdate, mother, father, company, healthstate, comments, schoolname, homeaddress);
            c.SaveToMemory();
            Debug.WriteLine("{0}\tNew {1} created \n{2}.", DateTime.Now.ToString(), c.GetType().Name, c.ToString());
            return c;
        }



        public static Child Load(int ID)
        {
            


            if (Mapper == null) { Mapper = new DataMapper(); };List<LeisureObjectDTO> childs = Mapper.Select(new ChildDTO(ID));

            if (childs.Count <= 0) {
                return null; }

            ChildDTO childDTO = (ChildDTO)childs[0];
            return MapFromDTO(childDTO);

        }

        public static List<Child> Load(Child crit,bool and = true)
        {

            if (Mapper == null) { Mapper = new DataMapper(); };
            List<LeisureObjectDTO> adds = Mapper.Select(MapToDTO(crit),and);

            if (adds.Count <= 0) {
                return null; }
            List<Child> resultcol = new List<Child>();
            foreach (LeisureObjectDTO item in adds)
            { resultcol.Add(MapFromDTO((ChildDTO)item)); }

            return resultcol;

        }

        private static Child MapFromDTO(ChildDTO chDTO)
        {
            if (chDTO == null)
            {return new Child();}

            Child ch = new Child()
            {
                ID = chDTO.ID,
                Name = chDTO.Name,
                Email = chDTO.Email,
                Phone = chDTO.Phone,
                Surname = chDTO.Surname,
                homeaddressid = chDTO.HomeAddressID,
                companyid = chDTO.CompanyID,
                motherid = chDTO.MotherID,
                fatherid = chDTO.FatherID,
                BirthDate = chDTO.BirthDate,
                HealthState = chDTO.HealthState,
                Comments = chDTO.Comments,
                SchoolName = chDTO.SchoolName,
                RegistrationNumber = chDTO.RegistrationNumber





            };
            return ch;
        }
        private static ChildDTO MapToDTO(Child ch)
        {
            if (ch == null)
            {return new ChildDTO();}

            ChildDTO chDTO = new ChildDTO()
            {
                ID = ch.ID,
                Name = ch.Name,
                Email = ch.Email,
                Phone = ch.Phone,
                Surname = ch.Surname,
                HomeAddressID = ch.homeaddressid ,
                CompanyID =  ch.companyid ,
                MotherID =  ch.motherid   ,
                FatherID =  ch.fatherid   ,
                BirthDate = ch.BirthDate,
                HealthState = ch.HealthState,
                Comments = ch.Comments,
                SchoolName = ch.SchoolName,
                RegistrationNumber = ch.RegistrationNumber




            };
            return chDTO;
        }




        public static int Save(Child ch, bool newobject = true)
        {
          
            int saved = 0;
            if (ch.homeaddress != null)
            {
                // saved+=Address.Save(ch.homeaddress);
                List<Address> refadd = Address.Load(ch.homeaddress);
                if (refadd == null)
                {
                    saved += Address.Save(ch.homeaddress, true);
                    ch.HomeAddress = Address.Load(ch.homeaddress)[0];
                }
                else
                {     ch.HomeAddress = refadd[0];
                    saved += Address.Save(ch.homeaddress, false);
               
                }
                

            }
            if (ch.company != null)
            {
                //saved+=InsuranceCompany.Save(ch.company);
                List<InsuranceCompany> refcom = InsuranceCompany.Load(ch.company);
                if (refcom == null)
                {
                    saved += InsuranceCompany.Save(ch.company, true);
                    ch.Company = InsuranceCompany.Load(ch.company)[0];
                }
                else
                { ch.Company = refcom[0];
                    saved += InsuranceCompany.Save(ch.company, false);
                   
                }

            }
            if (ch.father != null)
            {
                // saved+=Parent.Save(ch.father);
                List<Parent> reffat = Parent.Load(ch.father);
                if (reffat == null)

                {
                    saved += Parent.Save(ch.father, true);
                    ch.Father = Parent.Load(ch.father)[0];
                }
                else
                {ch.Father = reffat[0];
                    saved += Parent.Save(ch.father, false);
                    
                }

            }
            if (ch.mother != null)
            {
                // saved+=Parent.Save(ch.mother); 
                List<Parent> refmot = Parent.Load(ch.mother);
                if (refmot == null)
                {
                    saved += Parent.Save(ch.mother, true);
                    ch.Mother = Parent.Load(ch.mother)[0];
                }
                else
                { ch.Mother = refmot[0];
                    saved += Parent.Save(ch.mother, false);
                   
                }

            }
            ChildDTO chDTO = Child.MapToDTO(ch);
            return saved + (newobject ? Mapper.Insert(chDTO) : Mapper.Update(chDTO));

        }
        /*public static int SaveAs(Child ch)
        {
            ChildDTO chDTO = Child.MapToDTO(ch);
            int saved = 0;
            if (ch.homeaddress != null)
            {
                saved += Address.SaveAs(ch.homeaddress);
            }
            if (ch.company != null)
            {
                saved += InsuranceCompany.SaveAs(ch.company);
            }
            if (ch.father != null)
            {
                saved += Parent.SaveAs(ch.father);
            }
            if (ch.mother != null)
            {
                saved += Parent.SaveAs(ch.mother);
            }

           



            return saved+Mapper.Insert(chDTO);

        }
        */





    }
}
