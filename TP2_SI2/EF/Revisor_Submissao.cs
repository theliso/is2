//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Revisor_Submissao
    {
        public int idSubmissao { get; set; }
        public int idRevisor { get; set; }
        public Nullable<decimal> nota { get; set; }
        public string texto { get; set; }
        public bool revisto { get; set; }
    
        public virtual Revisor Revisor { get; set; }
        public virtual Submissao Submissao { get; set; }
    }
}