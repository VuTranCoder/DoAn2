//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoAn2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NHAN_VIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHAN_VIEN()
        {
            this.DON_DAT_HANG = new HashSet<DON_DAT_HANG>();
        }
    
        public int MA_NV { get; set; }
        public string ID_TK { get; set; }
        public string TEN_NV { get; set; }
        public Nullable<System.DateTime> NGAYSINH { get; set; }
        public string DIACHI { get; set; }
        public Nullable<System.DateTime> NGAYVAOLAM { get; set; }
        public Nullable<int> SDT { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DON_DAT_HANG> DON_DAT_HANG { get; set; }
        public virtual TAI_KHOAN TAI_KHOAN { get; set; }
    }
}
