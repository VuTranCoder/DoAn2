//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoAn2.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class DOANH_THU
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> Ngay { get; set; }
        public Nullable<int> Thang { get; set; }
        public Nullable<int> Nam { get; set; }
        public Nullable<decimal> TongDoanhThu { get; set; }
        public Nullable<int> Ma_DH { get; set; }
    
        public virtual DON_DAT_HANG DON_DAT_HANG { get; set; }
    }
}