using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.DTO
{
    public class GroupLeaderDTO : PersonDTO
    {
        public GroupLeaderDTO() : this(0) { }
        public GroupLeaderDTO(int iD)
        {
            ID = iD;
        }
    }
}
