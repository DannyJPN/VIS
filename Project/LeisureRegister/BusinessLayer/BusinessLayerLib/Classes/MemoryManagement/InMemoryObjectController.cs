

using BusinessLayer.BusinessLayerLib.Classes.DataContainers;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BusinessLayer.BusinessLayerLib.Classes.MemoryManagement
{
    public static class InMemoryObjectController
    {
        private static Dictionary<string, Dictionary<int,LeisureObject>> objectlist= new Dictionary<string, Dictionary<int, LeisureObject>>();

        public static LeisureObject GetObject(Type t,int ID)
        {
            if (objectlist.ContainsKey(t.Name))
            {
                if (objectlist[t.Name].ContainsKey(ID))
                {
                    return objectlist[t.Name][ID];

                }



            }
            return null;

        }


        public static void InsertObject(LeisureObject ob)
        {
            Type obtype = ob.GetType();
          
            
            if (!objectlist.ContainsKey(obtype.Name))
            {
                objectlist.Add(obtype.Name, new Dictionary<int, LeisureObject>());
                Console.WriteLine("{0}\tDict for type {1} added.", DateTime.Now.ToString(), obtype.Name);
                Debug.WriteLine("{0}\tDict for type {1} added.", DateTime.Now.ToString(),obtype.Name);
            }
            if (!objectlist[obtype.Name].ContainsKey(ob.ID))
            {

                objectlist[obtype.Name].Add(ob.ID, ob);
                Console.WriteLine("{0}\tDict {1} has new member \n{2}.", DateTime.Now.ToString(), obtype.Name, ob.ToString());

                Debug.WriteLine("{0}\tDict {1} has new member \n{2}.", DateTime.Now.ToString(), obtype.Name,ob.ToString());

            }


        }

       

    }
}
