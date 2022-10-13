using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace He_thong_ban_thuc_an_nhanh
{
    internal class QuanLyHangHoa
    {
        private string _maHH;
        private string _tenHH;
        private string _dvt;
        private int _giaban;
        private int _slton;

        public QuanLyHangHoa()
        {

        }

        public QuanLyHangHoa(string maHH, string tenHH, string dvt, int giaban, int slton)
        {
            _maHH = maHH;
            _tenHH = tenHH;
            _dvt = dvt;
            _giaban = giaban;
            _slton = slton;
        }

        public string MaHH { get => _maHH; set => _maHH = value; }
        public string TenHH { get => _tenHH; set => _tenHH = value; }
        public string Dvt { get => _dvt; set => _dvt = value; }
        public int Giaban { get => _giaban; set => _giaban = value; }
        public int Slton { get => _slton; set => _slton = value; }
    }
}
