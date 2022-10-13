using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace He_thong_ban_thuc_an_nhanh
{
    internal class QuanLyPhieuXuat
    {
        private string _maHD;
        private string _maHH;
        private int _sl;
        public QuanLyPhieuXuat()
        {

        }
        public QuanLyPhieuXuat(string maHD, string maHH, int sl)
        {
            _maHD = maHD;
            _maHH = maHH;
            _sl = sl;
        }

        public string MaHD { get => _maHD; set => _maHD = value; }
        public string MaHH { get => _maHH; set => _maHH = value; }
        public int Sl { get => _sl; set => _sl = value; }
    }
}
