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
    
    public partial class Autor_Submissao
    {
        public int idUtilizador { get; set; }
        public int idSubmissao { get; set; }
        public Nullable<bool> receiveMail { get; set; }
    
        public virtual Autor Autor { get; set; }
        public virtual Submissao Submissao { get; set; }
    }
}
