using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.Attributes;

namespace BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.DTO
{
    public class CityDTO:LeisureObjectDTO
    {
        public CityDTO() : this(0) { }
        public CityDTO(int iD)
        {
            ID = iD;
        }

        [StandardSQL]
        public string Name { get; set; }
        
    }
}