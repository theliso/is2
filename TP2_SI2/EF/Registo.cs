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
    
    public partial class Registo
    {
        public int idUtilizador { get; set; }
        public int idConferencia { get; set; }
        public System.DateTime dataRegisto { get; set; }
    
        public virtual Conferencia Conferencia { get; set; }
        public virtual Utilizador Utilizador { get; set; }
    }
}