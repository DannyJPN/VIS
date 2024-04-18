using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes;
using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.DTO;
using DataAccessLayer.DataAccessGeneralLib.Interfaces;
using DataAccessLayer.DataAccessMSSQLLib.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessLayerLib.Classes.DataContainers
{
    public class HobbyGroup:LeisureObject
    {

        public static IDataMapper Mapper { get; set; }
        public string Day { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }


        public string Name { get; set; }
        public string SeasonOfExistence { get; set; }

        private HobbyGroup() : this(0)
        {

        }
        private HobbyGroup(int ID) : base(ID)
        {
            if (Mapper == null)
            {
                Mapper = new DataMapper();
            }

        }
private HobbyGroup(int ID,string name,string day,TimeSpan from,TimeSpan to,int min,int max, int minage,int maxage,string season) :this(ID)
        {
            Name = name;
            Day = day;
            From = from;
            To = to;
            Min = min;
            Max = max;
            MinAge = minage;
            MaxAge = maxage;
            SeasonOfExistence = season;
        }
        public override string ToString()
        {
            return base.ToString() + "|" + String.Format("{0} {1}",Name,SeasonOfExistence);
        }
        public static HobbyGroup CreateInstance()
        {
            HobbyGroup g = new HobbyGroup();
            g.SaveToMemory();


            Debug.WriteLine("{0}\tNew {1} created \n{2}.", DateTime.Now.ToString(), g.GetType().Name, g.ToString());
            return g;
        }
        public static HobbyGroup CreateInstance(int ID)
        {
            int year = DateTime.Now.Year;
            string season = String.Format("{0}/{1}",year,year+1);
            return CreateInstance(ID, "", "", new TimeSpan(), new TimeSpan(), 0, 0,0,0,season);
        }
        public static HobbyGroup CreateInstance(int ID, string name, string day, TimeSpan from, TimeSpan to, int min, int max,int minage,int maxage,string season)
        {
            HobbyGroup g = new HobbyGroup(ID,  name, day, from,  to,  min,  max,minage,maxage,season);
            g.SaveToMemory();


            Debug.WriteLine("{0}\tNew {1} created \n{2}.", DateTime.Now.ToString(), g.GetType().Name, g.ToString());
            return g;
        }

        public static HobbyGroup Load(int ID)
        {

            if (Mapper == null) { Mapper = new DataMapper(); };List<LeisureObjectDTO> hobbygroups = Mapper.Select(new HobbyGroupDTO(ID));

            if (hobbygroups.Count <= 0) {
                return null; }

            HobbyGroupDTO hobbygroupDTO = (HobbyGroupDTO)hobbygroups[0];
            return MapFromDTO(hobbygroupDTO);

        }
        public static List<HobbyGroup> Load(HobbyGroup crit,bool and = true)
        {
            if (Mapper == null) { Mapper = new DataMapper(); };List<LeisureObjectDTO> adds = Mapper.Select(MapToDTO(crit),and);

            if (adds.Count <= 0) {
                return null; }
            List<HobbyGroup> resultcol = new List<HobbyGroup>();
            foreach (LeisureObjectDTO item in adds)
            { resultcol.Add(MapFromDTO((HobbyGroupDTO)item)); }

            return resultcol;

        }
        private static HobbyGroup MapFromDTO(HobbyGroupDTO hgDTO)
        {
            if (hgDTO == null)
            {return new HobbyGroup();}

            HobbyGroup hg = new HobbyGroup()
            {
                Name=hgDTO.Name,
                ID = hgDTO.ID,
                From = hgDTO.From,
                To = hgDTO.To,
                Min = hgDTO.Min,
                Max = hgDTO.Max,
                Day = hgDTO.Day,
                MinAge = hgDTO.MinAge,
                MaxAge = hgDTO.MaxAge,
                SeasonOfExistence=hgDTO.SeasonOfExistence
            };
            return hg;
        }
        private static HobbyGroupDTO MapToDTO(HobbyGroup hg)
        {
            if (hg == null)
            {return new HobbyGroupDTO();}

            HobbyGroupDTO hgDTO = new HobbyGroupDTO()
            {
                Name=hg.Name,
                ID = hg.ID,
                From = hg.From,
                To = hg.To,
                Min = hg.Min,
                Max = hg.Max,
                Day = hg.Day,
                MinAge = hg.MinAge,
                MaxAge = hg.MaxAge,
                SeasonOfExistence = hg.SeasonOfExistence
            };
            return hgDTO;
        }




        public static int Save(HobbyGroup hg, bool newobject = true)
        {
            HobbyGroupDTO hgDTO = HobbyGroup.MapToDTO(hg);
            return (newobject ? Mapper.Insert(hgDTO) : Mapper.Update(hgDTO));

        }
        /*public static int SaveAs(HobbyGroup hg)
        {
            HobbyGroupDTO hgDTO = HobbyGroup.MapToDTO(hg);
            return Mapper.Insert(hgDTO);

        }
        */






    }
}
