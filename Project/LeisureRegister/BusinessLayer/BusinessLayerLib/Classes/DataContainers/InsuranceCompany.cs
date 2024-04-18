using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes;
using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.DTO;
using DataAccessLayer.DataAccessGeneralLib.Interfaces;
using DataAccessLayer.DataAccessMSSQLLib.Classes;
using DataAccessXMLLib.Classes;
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
    public class InsuranceCompany:LeisureObject
    {
        
        public static IDataMapper Mapper { get; set; }
        public int Number { get; set; }
        public string Abbreviation { get; set; }
        public string FullName { get; set; }
        public override string ToString()
        {
            return base.ToString() + "|" + string.Format("{0}|{1}|{2}", Number,Abbreviation,FullName);
        }
        private InsuranceCompany() : this(0)
        {

        }
        private InsuranceCompany(int ID) : base(ID)
        {
            if (Mapper == null)
            {
                Mapper = new Mapper();
            }

        }
        private InsuranceCompany(int ID,int number,string abbreviation,string fullname) : this(ID)
        {
            Number = number;
            Abbreviation = abbreviation;
            FullName = fullname;


        }
        public static InsuranceCompany CreateInstance()
        {
            return CreateInstance(0);
        }
        public static InsuranceCompany CreateInstance(int ID)
        {
            return CreateInstance(ID, 0, "", "");
        }
        public static InsuranceCompany CreateInstance(int ID, int number, string abbreviation, string fullname)
        {
            InsuranceCompany i = new InsuranceCompany(ID, number, abbreviation,  fullname);
            i.SaveToMemory();
            Debug.WriteLine("{0}\tNew {1} created \n{2}.", DateTime.Now.ToString(), i.GetType().Name, i.ToString());
            return i;
        }


        public static InsuranceCompany Load(int ID)
        {

            if (Mapper == null) { Mapper = new DataMapper(); };List<LeisureObjectDTO> companys = Mapper.Select(new InsuranceCompanyDTO(ID));

            if (companys.Count <= 0) {
                return null; }

            InsuranceCompanyDTO companyDTO = (InsuranceCompanyDTO)companys[0];
            return MapFromDTO(companyDTO);

        }

        public static List<InsuranceCompany> Load(InsuranceCompany crit,bool and = true)
        {
            if (Mapper == null) { Mapper = new DataMapper(); };List<LeisureObjectDTO> adds = Mapper.Select(MapToDTO(crit),and);

            if (adds.Count <= 0) {
                return null; }
            List<InsuranceCompany> resultcol = new List<InsuranceCompany>();
            foreach (LeisureObjectDTO item in adds)
            { resultcol.Add(MapFromDTO((InsuranceCompanyDTO)item)); }

            return resultcol;

        }

        private static InsuranceCompany MapFromDTO(InsuranceCompanyDTO icDTO)
        {
            if (icDTO == null)
            {return new InsuranceCompany();}

            InsuranceCompany ic = new InsuranceCompany()
            {
                ID = icDTO.ID,
                Number=icDTO.Number,
                Abbreviation=icDTO.Abbreviation,
                FullName=icDTO.FullName



            };
            return ic;
        }
        private static InsuranceCompanyDTO MapToDTO(InsuranceCompany ic)
        {
            if (ic == null)
            {return new InsuranceCompanyDTO();}

            InsuranceCompanyDTO icDTO = new InsuranceCompanyDTO()
            {
                ID = ic.ID,
                Number = ic.Number,
                Abbreviation = ic.Abbreviation,
                FullName = ic.FullName




            };
            return icDTO;
        }

        
        public static int Save(InsuranceCompany ic, bool newobject = true)
        {
            InsuranceCompanyDTO icDTO = InsuranceCompany.MapToDTO(ic);


            return (newobject ? Mapper.Insert(icDTO) : Mapper.Update(icDTO));

        }
        /*public static int SaveAs(InsuranceCompany ic)
        {
            InsuranceCompanyDTO icDTO = InsuranceCompany.MapToDTO(ic);
            return Mapper.Insert(icDTO);

        }*/



    }
}
