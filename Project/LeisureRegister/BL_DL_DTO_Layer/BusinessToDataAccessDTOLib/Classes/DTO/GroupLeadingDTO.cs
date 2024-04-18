
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.Attributes;

namespace BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.DTO
{
    public class GroupLeadingDTO : LeisureObjectDTO
    {
        public GroupLeadingDTO() : this(0) { }
        public GroupLeadingDTO(int iD)
        {
            ID = iD;
        }

        [ForeignKey]
        public int LeaderID { get; set; }
        [ForeignKey]
        public int GroupID { get; set; }
        [StandardSQL]public DateTime From { get; set; }
        [StandardSQL] public DateTime To { get; set; }
        [ReferredObject][XmlIgnore] public GroupLeaderDTO Leader{get;set;}
        [ReferredObject][XmlIgnore] public HobbyGroupDTO Group {get;set;}

}
}