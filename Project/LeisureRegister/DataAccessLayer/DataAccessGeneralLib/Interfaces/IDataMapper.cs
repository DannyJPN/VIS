using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes;
using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.DTO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace DataAccessLayer.DataAccessGeneralLib.Interfaces
{
    public interface IDataMapper
    {    
         int Insert(LeisureObjectDTO idto);
        

        List<LeisureObjectDTO> Select(LeisureObjectDTO DTO);
        List<LeisureObjectDTO> Select(LeisureObjectDTO DTO,bool conditions_together);

        int Update(LeisureObjectDTO idto);


         int Delete(LeisureObjectDTO idto);


    
}
}
