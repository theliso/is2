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
    
    public partial class Conferencia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Conferencia()
        {
            this.Registo = new HashSet<Registo>();
            this.Submissao = new HashSet<Submissao>();
        }
    
        public int id { get; set; }
        public Nullable<int> idPresidente { get; set; }
        public Nullable<int> notaMinima { get; set; }
        public string acronimo { get; set; }
        public string nome { get; set; }
        public int ano { get; set; }
        public System.DateTime dataRealizacao { get; set; }
        public Nullable<System.DateTime> dataLimite { get; set; }
    
        public virtual Utilizador Utilizador { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Registo> Registo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Submissao> Submissao { get; set; }
    }
}
