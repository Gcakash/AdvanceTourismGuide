using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceTourismGuide.Models
{
    public class CombineModel
    {
        public List<AdminInfo> AdminInfos { get; set; }
        public List<Province> Provinces { get; set; }
        public List<District> Districts { get; set; }
        public List<City> Cities { get; set; }
        public List <Place> Places { get; set; }

       // public AdminInfo AdminInfo { get; set; }

    }
}
