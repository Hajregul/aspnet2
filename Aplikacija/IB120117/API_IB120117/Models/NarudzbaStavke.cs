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
    
    public partial class NarudzbaStavke
    {
        public int NarudzbaStavkeID { get; set; }
        public int NarudzbaID { get; set; }
        public int JeloID { get; set; }
        public int Kolicina { get; set; }
        public Nullable<int> VelicinaJelaID { get; set; }
    
        public virtual Jela Jela { get; set; }
        public virtual Narudzbe Narudzbe { get; set; }
        public virtual VelicinaJela VelicinaJela { get; set; }
    }
}
