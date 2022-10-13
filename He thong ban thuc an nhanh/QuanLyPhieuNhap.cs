using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace He_thong_ban_thuc_an_nhanh
{
    internal class QuanLyPhieuNhap
    {
        private string _maPN;
        private string _maNCC;
        private string _maHH;
        private string _maNV;
        private int _sl;
        private string _tenHH;
        private float _dongia;

        public QuanLyPhieuNhap()
        {

        }

        public QuanLyPhieuNhap(string maPN, string maNCC, string maHH, string maNV, int sl, string tenHH, float dongia)
        {
            _maPN = maPN;
            _maNCC = maNCC;
            _maHH = maHH;
            _maNV = maNV;
            _sl = sl;
            _tenHH = tenHH;
            _dongia = dongia;
        }

        public string MaNCC { get => _maNCC; set => _maNCC = value; }
        public string MaHH { get => _maHH; set => _maHH = value; }
        public string MaNV { get => _maNV; set => _maNV = value; }
        public int Sl { get => _sl; set => _sl = value; }
        public string TenHH { get => _tenHH; set => _tenHH = value; }
        public float Dongia { get => _dongia; set => _dongia = value; }
        public string MaPN { get => _maPN; set => _maPN = value; }
    }
}
