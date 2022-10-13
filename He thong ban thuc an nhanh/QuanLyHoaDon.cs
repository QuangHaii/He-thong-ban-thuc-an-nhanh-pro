using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace He_thong_ban_thuc_an_nhanh
{
    internal class QuanLyHoaDon
    {
        private string _mahd;
        private DateTime _ngay;
        private string _makh;
        private string _manv;

        public QuanLyHoaDon()
        {

        }
        public QuanLyHoaDon(string mahd, DateTime ngay, string makh, string manv)
        {
            Mahd = mahd;
            Ngay = ngay;
            Makh = makh;
            Manv = manv;
        }

        public string Mahd { get => _mahd; set => _mahd = value; }
        public DateTime Ngay { get => _ngay; set => _ngay = value; }
        public string Makh { get => _makh; set => _makh = value; }
        public string Manv { get => _manv; set => _manv = value; }
    }
}
