using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace He_thong_ban_thuc_an_nhanh
{
    internal class QuanLyNCC
    {
        private string _tenNCC;
        private string _maNCC;
        private string _diaChi;
        private string _sdt;
        private string _mahhoa;

        public QuanLyNCC()
        {

        }

        public QuanLyNCC(string tenNCC, string maNCC, string diaChi, string sdt, string mahhoa)
        {
            _tenNCC = tenNCC;
            _maNCC = maNCC;
            _diaChi = diaChi;
            _sdt = sdt;
            _mahhoa = mahhoa;
        }

        public string TenNCC { get => _tenNCC; set => _tenNCC = value; }
        public string MaNCC { get => _maNCC; set => _maNCC = value; }
        public string diaChi { get => _diaChi; set => _diaChi = value; }
        public string sdt { get => _sdt; set => _sdt = value; }
        public string mahhoa { get => _mahhoa; set => _mahhoa = value; }
    }
}
