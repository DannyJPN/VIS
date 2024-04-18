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
    public class GroupMembership:LeisureObject
    {
        
        public static IDataMapper Mapper { get; set; }

        private Child member = null;
        private HobbyGroup group = null;
        private int memberid;
        private int groupid;
        
        public Child Member
        {
            get
            {
                if (member == null)
                {
                    Member = Child.Load(memberid);

                }
                else
                {
                    if (member.ID != memberid)
                    {
                        Member = Child.Load(memberid);

                    }


                }
                return member;
            }
            set
            {
                if (value != null)
                    memberid = value.ID;
                member = value;
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
        private GroupMembership() : this(0)
        {

        }
        private GroupMembership(int ID) : base(ID)
        {
            if (Mapper == null)
            {
                Mapper = new DataMapper();
            }

        }
        private GroupMembership(int ID, int memberid, int groupid, DateTime from, DateTime to):this(ID)
        {
            this.memberid = memberid;
            this.groupid = groupid;
            From = from;
            To = to;
        }
        private GroupMembership(int ID, Child member, HobbyGroup group, DateTime from, DateTime to) : this(ID)
        {
            Member = member;
            Group = group;
            From = from;
            To = to;
        }

        public override string ToString()
        {
            return base.ToString() + "|" + String.Format("{0}|{1}", memberid, groupid);
        }
        public static GroupMembership CreateInstance()
        {
            return CreateInstance(0);
        }
        public static GroupMembership CreateInstance(int ID)
        {
            return CreateInstance(ID, null, null, new DateTime(), new DateTime());
        }
        public static GroupMembership CreateInstance(int ID, Child member, HobbyGroup group, DateTime from, DateTime to)
        {
            GroupMembership g = new GroupMembership(ID,member,group,from,to);
            g.SaveToMemory();


            Debug.WriteLine("{0}\tNew {1} created \n{2}.", DateTime.Now.ToString(), g.GetType().Name, g.ToString());
            return g;
        }

        public static GroupMembership Load(int ID)
        {

            if (Mapper == null) { Mapper = new DataMapper(); };List<LeisureObjectDTO> groupmemberships = Mapper.Select(new GroupMembershipDTO(ID));

            if (groupmemberships.Count <= 0) {
                return null; }

            GroupMembershipDTO groupmembershipDTO = (GroupMembershipDTO)groupmemberships[0];
            return MapFromDTO(groupmembershipDTO);

        }

        public static List<GroupMembership> Load(GroupMembership crit,bool and = true)
        {
            if (Mapper == null) { Mapper = new DataMapper(); };List<LeisureObjectDTO> adds = Mapper.Select(MapToDTO(crit),and);

            if (adds.Count <= 0) {
                return null; }
            List<GroupMembership> resultcol = new List<GroupMembership>();
            foreach (LeisureObjectDTO item in adds)
            { resultcol.Add(MapFromDTO((GroupMembershipDTO)item)); }

            return resultcol;

        }

        private static GroupMembership MapFromDTO(GroupMembershipDTO gmDTO)
        {
            if (gmDTO == null)
            {return new GroupMembership();}

            GroupMembership gm = new GroupMembership()
            {
                ID = gmDTO.ID,
                From = gmDTO.From,
                To = gmDTO.To,
                memberid = gmDTO.MemberID,
                groupid = gmDTO.GroupID
            };
            return gm;
        }
        private static GroupMembershipDTO MapToDTO(GroupMembership gm)
        {
            if (gm == null)
            {return new GroupMembershipDTO();}

            GroupMembershipDTO gmDTO = new GroupMembershipDTO()
            {
                ID = gm.ID,
                From = gm.From,
                To = gm.To,
                MemberID = gm.member == null ? gm.memberid : gm.Member.ID,
                GroupID = gm.group == null ? gm.groupid : gm.Group.ID
            };
            return gmDTO;
        }




        public static int Save(GroupMembership gm,bool newobject = true)
        {
        
int saved = 0;
            if (gm.group != null)
            {
                List<HobbyGroup> refhg = HobbyGroup.Load(gm.group);

                if (refhg == null)
                {
                    saved += HobbyGroup.Save(gm.group, true);
                    gm.Group = HobbyGroup.Load(gm.group)[0];

                }
                else
                {  gm.Group = refhg[0];
                    saved += HobbyGroup.Save(gm.group, false);
                  
                }


            }
            if (gm.member != null)
            {
                List<Child> refch = Child.Load(gm.member);
                if (refch == null)
                {
                    saved += Child.Save(gm.member, true);
                    gm.Member =Child.Load(gm.member)[0];
                }
                else
                {gm.Member = refch[0];
                    saved += Child.Save(gm.member, false);
                    
                }


            }
            GroupMembershipDTO gmDTO = GroupMembership.MapToDTO(gm);
            return saved + (newobject ? Mapper.Insert(gmDTO) : Mapper.Update(gmDTO));
        }
       /* public static int SaveAs(GroupMembership gm)
        {
            GroupMembershipDTO gmDTO = GroupMembership.MapToDTO(gm);
            return Mapper.Insert(gmDTO);

        }
        */



    }
}
