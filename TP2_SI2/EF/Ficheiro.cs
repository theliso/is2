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
    
    public partial class Ficheiro
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string extensao { get; set; }
        public int idSubmissao { get; set; }
    
        public virtual Submissao Submissao { get; set; }
    }
}
