using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer
{
   public class PackageController
    {
        DBConnector dbc = new DBConnector();
        PackageRepo pr = PackageRepo.GetInstance();
        
        public void LoadAllPackagesToPackageRepo()
        {
            pr.ClearPackageList();

            List<Package> packages = new List<Package>();
            packages = dbc.Sp_GetAllPackages();
            foreach (Package package in packages)
            {
                pr.AddPackageToList(package);
            }
        }

        public List<string> LoadAllPackagesToString()
        {
            LoadAllPackagesToPackageRepo();

            List<string> packageStrings = new List<string>();

            List<Package> packages = pr.GetAllPackages();

            foreach (Package package in packages)
            {
                packageStrings.Add(package.Name);
            }

            return packageStrings;
        }
    }
}
