using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.Attributes;

namespace BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.DTO
{
    public class ParentDTO:PersonDTO
    {
        public ParentDTO() : this(0) { }
        public ParentDTO(int iD)
        {
            ID = iD;
        }

        [StandardSQL] public string Job { get; set; }
        [StandardSQL] public char Gender { get; set; }
    }
}
