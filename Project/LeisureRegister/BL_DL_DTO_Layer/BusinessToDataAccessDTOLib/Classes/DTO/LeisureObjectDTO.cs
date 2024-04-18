using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.Attributes;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.DTO
{
    public class LeisureObjectDTO
    {
        [PrimaryKey]
        public int ID { get; set; }
    }
}