//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RentACar2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Arac
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Arac()
        {
            this.Kiralama = new HashSet<Kiralama>();
        }
    
        public int aracId { get; set; }
        public string aracModel { get; set; }
        public string aracFoto { get; set; }
        public decimal aracFiyat { get; set; }
        public int aracKategoriId { get; set; }
        public int aracUyeId { get; set; }
    
        public virtual Kategori Kategori { get; set; }
        public virtual Uye Uye { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kiralama> Kiralama { get; set; }
    }
}
