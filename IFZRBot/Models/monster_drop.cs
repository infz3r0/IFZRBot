//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IFZRBot.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class monster_drop
    {
        public string item_id { get; set; }
        public string monster_id { get; set; }
        public Nullable<double> monster_drop_chance { get; set; }
    
        public virtual item item { get; set; }
        public virtual monster monster { get; set; }
    }
}