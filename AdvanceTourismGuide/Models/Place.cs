//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdvanceTourismGuide.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Place
    {
        public int Place_Id { get; set; }
        public int City_Id { get; set; }
        public string Place_Name { get; set; }
        public string Place_Image { get; set; }
        public string Discription { get; set; }
        public string Place_Tag { get; set; }
        public Nullable<decimal> Cost_Per_Day { get; set; }
    }
}