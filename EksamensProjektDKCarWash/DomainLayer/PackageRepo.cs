using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
   public class PackageRepo
    {
        private List<Package> packages = new List<Package>();
        


        public void AddPackageToList(Package p)
        {
            packages.Add(p);
        }

        public Package FindPackage(string packageName)
        {
            foreach (Package package in packages)
            {
                if(package.Name == packageName)
                {
                    return package;
                }
            }
            return null;
        }

        public List<Package> GetAllPackages()
        {
            return packages;
        }

        public void ClearPackageList ()
        {
            packages.Clear();
        }
    }
}
