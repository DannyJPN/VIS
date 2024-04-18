using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.Attributes;

namespace BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.DTO
{
    public abstract class PersonDTO:LeisureObjectDTO
    {
        [StandardSQL] public string Name { get; set; }
        [StandardSQL] public string Surname { get; set; }
        [StandardSQL] public int Phone { get; set; }
        [StandardSQL] public string Email { get; set; }




    }
}
