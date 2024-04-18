using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.Attributes;

namespace BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.DTO
{
    public class InsuranceCompanyDTO:LeisureObjectDTO
    {
        public InsuranceCompanyDTO() : this(0) { }
        public InsuranceCompanyDTO(int iD)
        {
            ID = iD;
        }

        [StandardSQL] public int Number { get; set; }
        [StandardSQL] public string Abbreviation { get; set; }
        [StandardSQL] public string FullName { get; set; }

    }
}
