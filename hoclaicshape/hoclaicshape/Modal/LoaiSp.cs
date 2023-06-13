using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace hoclaicshape.Modal
{
    public partial class LoaiSp
    {
        public LoaiSp()
        {
            SanPham = new HashSet<SanPham>();
        }

        public string MaLoai { get; set; }
        public string TenLoai { get; set; }

        public virtual ICollection<SanPham> SanPham { get; set; }
    }
}
