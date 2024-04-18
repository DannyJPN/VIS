using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessLayerLib.Classes.DataContainers
{
    public abstract class Person : LeisureObject
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return  string.Format("{1} {0}", Name, Surname);
        }

        protected Person() : this(0)
        {

        }
        protected Person(int ID) : base(ID)
        {


        }

        protected Person(int ID, string name, string surname, string email, int phone) : this(ID)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Phone = phone;
        }

    }
}
