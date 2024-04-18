using BusinessLayer.BusinessLayerLib.Classes.DataContainers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;


namespace WebMVCFace.Models
{
    public class AddingFormModel
    {
         public List<string> seasons;

         public List<int> kidids;
         public List<int> groupids;
 

        public string season;
        public int chosenchildid;
    }
}
