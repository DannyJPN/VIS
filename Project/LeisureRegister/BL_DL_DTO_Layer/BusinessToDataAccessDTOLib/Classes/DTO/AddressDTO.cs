using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.Attributes;
using System.Xml.Serialization;

namespace BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.DTO
{
    public class AddressDTO:LeisureObjectDTO
    {
        public AddressDTO() : this(0) { }
        public AddressDTO(int iD)
        {
            ID = iD;
        }

        [StandardSQL]
        public string Street { get; set; }
        [StandardSQL]
        public int OrientationNumber { get; set; }
        [StandardSQL]
        public int DescriptionNumber { get; set; }
        [ForeignKey]
        public int HomeCityID { get; set; }
        [ForeignKey]
        public int HomeCityPartID { get; set; }
        [StandardSQL]
        public int PostalCode { get; set; }
        [ReferredObject][XmlIgnore]
        public CityDTO HomeCity { get; set; }
        [ReferredObject][XmlIgnore]
        public CityPartDTO HomeCityPart { get; set; }


    }
}