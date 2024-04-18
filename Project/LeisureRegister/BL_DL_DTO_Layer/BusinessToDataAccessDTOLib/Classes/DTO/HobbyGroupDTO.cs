using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.Attributes;

namespace BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.DTO
{
    public class HobbyGroupDTO:LeisureObjectDTO
    {
        public HobbyGroupDTO() : this(0) { }
        public HobbyGroupDTO(int iD)
        {
            ID = iD;
        }

        [StandardSQL] public string Day { get; set; }
        [StandardSQL] public TimeSpan From { get; set; }
        [StandardSQL] public TimeSpan To { get; set; }
        [StandardSQL] public int Min { get; set; }
        [StandardSQL] public int Max { get; set; }

        [StandardSQL] public string Name { get; set; }
        [StandardSQL] public int MinAge { get; set; }
        [StandardSQL] public int MaxAge { get; set; }
        [StandardSQL] public string SeasonOfExistence { get; set; }
    }
}
