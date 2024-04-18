using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.Attributes;
using System.Xml.Serialization;

namespace BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.DTO
{
    public class CityPartDTO:LeisureObjectDTO
    {
        public CityPartDTO() : this(0) { }
        public CityPartDTO(int iD)
        {
            ID = iD;
        }

        [StandardSQL]
        public string Name { get; set; }
        [ForeignKey]
        public int OriginCityID { get; set; }
        [ReferredObject][XmlIgnore]
        public CityDTO OriginCity { get; set; }
    }
}