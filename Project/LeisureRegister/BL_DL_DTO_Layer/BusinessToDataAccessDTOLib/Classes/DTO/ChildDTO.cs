using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.Attributes;

namespace BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.DTO
{
    public class ChildDTO:PersonDTO
    {
        public ChildDTO() : this(0) { }
        public ChildDTO(int iD)
        {
            ID = iD;
        }

        [StandardSQL] public int RegistrationNumber { get; set; }
        [ForeignKey] public int HomeAddressID { get; set; }
        [ForeignKey] public int CompanyID { get; set; }
        [StandardSQL] public string HealthState { get; set; }
        [StandardSQL] public string Comments { get; set; }
        [StandardSQL] public DateTime BirthDate { get; set; }
        [StandardSQL] public string SchoolName { get; set; }
        [ForeignKey] public int MotherID { get; set; }
        [ForeignKey] public int FatherID { get; set; }
        [ReferredObject][XmlIgnore] public AddressDTO HomeAddress { get; set; }
        [ReferredObject][XmlIgnore] public InsuranceCompanyDTO Insurance { get; set; }
        [ReferredObject][XmlIgnore] public ParentDTO Mother{ get; set; }
        [ReferredObject][XmlIgnore] public ParentDTO Father { get; set; }
    }
}
