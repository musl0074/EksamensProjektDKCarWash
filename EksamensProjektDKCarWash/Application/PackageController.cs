using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
   public class PackageController
    {
        DBConnector dbc = new DBConnector();
        PackageRepo pr = new PackageRepo();
        List<Package> packages = new List<Package>();

        public void LoadAllPackagesToPackageRepo()
        {
            packages = dbc.Sp_GetAllPackages();
            foreach (Package package in packages)
            {
                pr.AddPackageToList(package);
            }
        }
    }
}
