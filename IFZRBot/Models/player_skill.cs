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
    
    public partial class player_skill
    {
        public string player_id { get; set; }
        public string skill_id { get; set; }
        public Nullable<int> skill_level { get; set; }
        public Nullable<long> skill_exp { get; set; }
        public Nullable<long> skill_exp_next_level { get; set; }
    
        public virtual player player { get; set; }
        public virtual skill skill { get; set; }
    }
}
