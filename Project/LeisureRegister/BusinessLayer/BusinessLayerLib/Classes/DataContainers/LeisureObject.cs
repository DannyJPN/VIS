using BusinessLayer.BusinessLayerLib.Classes.MemoryManagement;
using DataAccessLayer.DataAccessGeneralLib.Interfaces;
using DataAccessLayer.DataAccessMSSQLLib.Classes;
using System;

namespace BusinessLayer.BusinessLayerLib.Classes.DataContainers
{
    public abstract class LeisureObject
    {

        public int ID { get; set; }

        public void SaveToMemory()
        {
            InMemoryObjectController.InsertObject(this);

        }
        public override string ToString()
        {
            return String.Format("");
        }

        protected LeisureObject() : this(0)
        {

        }
        protected LeisureObject(int ID)
        {
            this.ID = ID;
        }



    }
}
