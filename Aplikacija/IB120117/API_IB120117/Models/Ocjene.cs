//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace API_IB120117.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ocjene
    {
        public int OcjenaID { get; set; }
        public System.DateTime Datum { get; set; }
        public int Ocjena { get; set; }
        public int JeloID { get; set; }
        public int KlijentID { get; set; }
    
        public virtual Jela Jela { get; set; }
        public virtual Klijenti Klijenti { get; set; }
    }
}
