using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes;
using DataAccessLayer.DataAccessGeneralLib.Interfaces;
using DataAccessLayer.DataAccessMSSQLLib.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using BL_DL_DTO_Layer.BusinessToDataAccessDTOLib.Classes.DTO;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace BusinessLayer.BusinessLayerLib.Classes.DataContainers
{
    public class GroupLeading : LeisureObject
    {
        
        public static IDataMapper Mapper { get; set; }
        private int leaderid;
        private int groupid;
         private GroupLeader leader = null;
        private HobbyGroup group = null;
        
        public GroupLeader Leader
        {
            get
            {

                if (leader == null)
                {
                    Leader = GroupLeader.Load(leaderid);

                }
                else
                {
                    if (leader.ID != leaderid)
                    {
                        Leader = GroupLeader.Load(leaderid);

                    }


                }
                return leader;
            }
            set
            {
                if (value != null)
                    leaderid = value.ID;
                leader = value;
            }
        }
        
        public HobbyGroup Group
        {
            get
            {
                if (group == null)
                {
                    Group = HobbyGroup.Load(groupid);

                }
                else
                {
                    if (group.ID != groupid)
                    {
                        Group = HobbyGroup.Load(groupid);

                    }


                }
                return group;
            }
            set
            {
                if (value != null)
                    groupid = value.ID;
                group = value;
            }
        }
        public DateTime From { get; set; }
        public DateTime To { get; set; }


        public override string ToString()
        {
            return base.ToString() + "|" + String.Format("{0}|{1}", leaderid,groupid);
        }
        private GroupLeading() : this(0)
        {

        }
        private GroupLeading(int ID) : base(ID)
        {
            if (Mapper == null)
            {
                Mapper = new DataMapper();
            }

        }
        private GroupLeading(int ID, int leaderid, int groupid, DateTime from, DateTime to):this(ID)
        {
            this.leaderid = leaderid;
            this.groupid = groupid;
            From = from;
            To = to;
        }
        private GroupLeading(int ID, GroupLeader leader, HobbyGroup group, DateTime from, DateTime to) : this(ID)
        {
            Leader = leader;
            Group = group;
            From = from;
            To = to;
        }

        public static GroupLeading CreateInstance()
        {
            return CreateInstance(0);
        }
        public static GroupLeading CreateInstance(int ID)
        {

            return CreateInstance(ID,null,null,new DateTime(),new DateTime());
        }
        public static GroupLeading CreateInstance(int ID, GroupLeader leader, HobbyGroup group, DateTime from, DateTime to)
        {
            GroupLeading g = new GroupLeading( ID, leader,  group,  from,  to);
            g.SaveToMemory();


            Debug.WriteLine("{0}\tNew {1} created \n{2}.", DateTime.Now.ToString(), g.GetType().Name, g.ToString());
            return g;
        }



        public static GroupLeading Load(int ID)
        {

            if (Mapper == null) { Mapper = new DataMapper(); };List<LeisureObjectDTO> groupleadings = Mapper.Select(new GroupLeadingDTO(ID));

            if (groupleadings.Count <= 0) {
                return null; }

            GroupLeadingDTO groupleadingDTO = (GroupLeadingDTO)groupleadings[0];
            return MapFromDTO(groupleadingDTO);

        }
        public static List<GroupLeading> Load(GroupLeading crit,bool and = true)
        {
            if (Mapper == null) { Mapper = new DataMapper(); };List<LeisureObjectDTO> adds = Mapper.Select(MapToDTO(crit),and);

            if (adds.Count <= 0) {
                return null; }
            List<GroupLeading> resultcol = new List<GroupLeading>();
            foreach (LeisureObjectDTO item in adds)
            { resultcol.Add(MapFromDTO((GroupLeadingDTO)item)); }

            return resultcol;

        }


        private static GroupLeading MapFromDTO(GroupLeadingDTO glDTO)
        {
            if (glDTO == null)
            {return new GroupLeading();}

            GroupLeading gl = new GroupLeading()
            {
                ID = glDTO.ID,
                From = glDTO.From,
                To=glDTO.To,
                leaderid = glDTO.LeaderID,
                groupid = glDTO.GroupID
            };
            return gl;
        }
        private static GroupLeadingDTO MapToDTO(GroupLeading gl)
        {
            if (gl == null)
            {return new GroupLeadingDTO();}

            GroupLeadingDTO glDTO = new GroupLeadingDTO()
            {
                ID = gl.ID,
                From = gl.From,
                To = gl.To,
                LeaderID= gl.leaderid ,
                GroupID= gl.groupid 
            };
            return glDTO;
        }




        public static int Save(GroupLeading gl,bool newobject = true)
        {
            
            int saved = 0;
            if (gl.group != null)
            {
                List<HobbyGroup> refhg = HobbyGroup.Load(gl.group);

                if (refhg == null)
                {
                    saved +=HobbyGroup.Save(gl.group, true);
                    gl.Group = HobbyGroup.Load(gl.group)[0];

                }
                else
                {gl.Group = refhg[0];
                    saved += HobbyGroup.Save(gl.group, false);
                    
                }


            }
            if (gl.leader != null)
            {
                List<GroupLeader> reflead = GroupLeader.Load(gl.leader);
                if (reflead == null)
                {
                    saved += GroupLeader.Save(gl.leader, true);
                    gl.Leader = GroupLeader.Load(gl.leader)[0];
                }
                else
                { gl.Leader = reflead[0];
                    saved += GroupLeader.Save(gl.leader, false);
                   
                }


            }
            GroupLeadingDTO glDTO = GroupLeading.MapToDTO(gl);
            return saved + (newobject ? Mapper.Insert(glDTO) : Mapper.Update(glDTO));

        }
        /*public static int SaveAs(GroupLeading gl)
        {
            GroupLeadingDTO glDTO = GroupLeading.MapToDTO(gl);
            return Mapper.Insert(glDTO);

        }
        */

    }
}
