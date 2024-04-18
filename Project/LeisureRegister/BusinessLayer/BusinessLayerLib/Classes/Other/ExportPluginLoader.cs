using DataAccessLayer.DataAccessGeneralLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessLayerLib.Classes.Other
{
    public static  class ExportPluginLoader
    {
        public static List<string> Load()
        {
           
            string path = @"..\..\..\..\DataAccessLayer\";
            string[] files = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
            PermissionSet pset = new PermissionSet(System.Security.Permissions.PermissionState.Unrestricted);

            AppDomain pluginloader = AppDomain.CreateDomain("CustomPlugins", null, new AppDomainSetup() { ApplicationBase = path }, pset);
            List<string> names =
                 pluginloader.GetAssemblies().SelectMany(x => x.GetTypes())
                      .Where(x => typeof(IDataMapper).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                      .Select(x => x.Name).ToList();

            return names;
        }
    }
}
