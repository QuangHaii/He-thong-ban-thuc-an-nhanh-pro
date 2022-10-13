using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace He_thong_ban_thuc_an_nhanh
{
    public partial class PhieuNhap : Form
    {
        public PhieuNhap()
        {
            InitializeComponent();
        }
        Modify modify = new Modify();
        QuanLyPhieuNhap QuanLyPhieuNhap = new QuanLyPhieuNhap();
        QuanLyPhieuNhap[] listPhieuNhap = null;
        SqlConnection con = Connection.GetConnection();
        private void PhieuNhap_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = modify.Table("Select * from PHIEUNHAP");
                dataGridView2.DataSource = modify.Table("Select (SL*DONGIA) AS 'Tổng' from PHIEUNHAP");
                dataGridView3.DataSource = modify.Table("Select sum(SL*DONGIA) from PHIEUNHAP");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: "+ex.Message);
            }
            deleteTextPhieuNhap();
            loadPN();
        }
        public void loadPN()
        {
            string query = "select MANCC, HANGHOA.MAHH, TENHH from HANGHOA,NCC where HANGHOA.MAHH=NCC.MAHH";
            using (var command = new SqlCommand(query, con))
            {
                con.Open();
                using (var reader = command.ExecuteReader())
                {
                    var list = new List<QuanLyPhieuNhap>();
                    while (reader.Read())
                        list.Add(new QuanLyPhieuNhap { MaNCC = reader.GetString(0), MaHH = reader.GetString(1), TenHH = reader.GetString(2) });
                    listPhieuNhap = list.ToArray();
                }
            }
            comboBox_tenHH.DataSource = listPhieuNhap;
            comboBox_tenHH.DisplayMember = "TenHH";
        }
        private void textbox_Soluong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textbox_dongia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void deleteTextPhieuNhap()
        {
            textbox_manhaCungcap.Text = String.Empty;
            textbox_maHangHoa.Text = String.Empty;
            textbox_maNhanVien.Text = String.Empty;
            textbox_Soluong.Text = String.Empty;
            textbox_maPN.Text = String.Empty;
            textbox_dongia.Text = String.Empty;
        }
        private bool CheckTextPhieuNhap()
        {
            if (textbox_maHangHoa.Text == String.Empty)
            {
                MessageBox.Show("Chưa nhập mã hàng hóa!");
                return false;
            }
            if (textbox_manhaCungcap.Text == String.Empty)
            {
                MessageBox.Show("Chưa nhập mã nhà cung cấp!");
                return false;
            }
            if (textbox_maNhanVien.Text == String.Empty)
            {
                MessageBox.Show("Chưa nhập mã nhân viên!");
                return false;
            }
            if (textbox_Soluong.Text == String.Empty)
            {
                MessageBox.Show("Chưa nhập số lượng!");
                return false;
            }
            if (textbox_maPN.Text == String.Empty)
            {
                MessageBox.Show("Chưa nhập mã phiếu nhập!");
                return false;
            }
            if (textbox_dongia.Text == String.Empty)
            {
                MessageBox.Show("Chưa nhập đơn giá!");
                return false;
            }
            return true;
        }
        private void inputPhieuNhap()
        {
            string maPN = textbox_maPN.Text;
            string maNCC = textbox_manhaCungcap.Text;
            string maHH = textbox_maHangHoa.Text;
            string maNV = textbox_maNhanVien.Text;
            int soluong = int.Parse(textbox_Soluong.Text);
            string tenHH = comboBox_tenHH.Text;
            float dongia = float.Parse(textbox_dongia.Text);
            QuanLyPhieuNhap = new QuanLyPhieuNhap(maPN,maNCC,maHH,maNV,soluong,tenHH,dongia);
        }
        private void button_them1_Click(object sender, EventArgs e)
        {
            if (CheckTextPhieuNhap())
            {
                inputPhieuNhap();
                string query = "insert into PHIEUNHAP values ('"+QuanLyPhieuNhap.MaPN+"','" + QuanLyPhieuNhap.MaNCC + "','" + QuanLyPhieuNhap.MaHH + "','" + QuanLyPhieuNhap.MaNV + "','" + QuanLyPhieuNhap.Sl + "',N'"+QuanLyPhieuNhap.TenHH+"','"+QuanLyPhieuNhap.Dongia+"')";
                try
                {
                    if (MessageBox.Show("Có muốn thêm dữ liệu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        modify.Command(query);
                        modify.Command("update HANGHOA set SLTON=SLTON+SL from HANGHOA,PHIEUNHAP where MAPN ='" + QuanLyPhieuNhap.MaPN + "' and PHIEUNHAP.MAHH=HANGHOA.MAHH");
                        MessageBox.Show("Thêm thành công!");
                        PhieuNhap_Load(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void button_sua1_Click(object sender, EventArgs e)
        {
            if (CheckTextPhieuNhap())
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                string choose = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string choose2 = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                inputPhieuNhap();
                string query = "update PHIEUNHAP set MAPN = '"+QuanLyPhieuNhap.MaPN+"', MANCC = '" + QuanLyPhieuNhap.MaNCC + "', MAHH = '" + QuanLyPhieuNhap.MaHH + "', MANV = '" + QuanLyPhieuNhap.MaNV + "', SL = '" + QuanLyPhieuNhap.Sl + "',TENHH = N'"+ QuanLyPhieuNhap.TenHH+"', DONGIA = '"+ QuanLyPhieuNhap.Dongia+"' ";
                query += "where MANCC = '" + choose + "' and MAHH = '" + choose2 + "'";
                try
                {
                    if (MessageBox.Show("Có muốn sửa dữ liệu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        modify.Command(query);
                        MessageBox.Show("Sửa thành công!");
                        PhieuNhap_Load(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void button_xoa1_Click(object sender, EventArgs e)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string choose = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            string choose2 = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            string query = "delete from PHIEUNHAP ";
            query += "where MANCC = '" + choose + "' and MAHH = '"+choose2+"'";
            try
            {
                if (MessageBox.Show("Có muốn xóa dữ liệu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    modify.Command(query);
                    MessageBox.Show("Xóa thành công!");
                    PhieuNhap_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textbox_maPN.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textbox_manhaCungcap.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textbox_maHangHoa.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textbox_maNhanVien.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textbox_Soluong.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            comboBox_tenHH.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            textbox_dongia.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
        }
        private void search_tenCC_TextChanged(object sender, EventArgs e)
        {
            string name = search_tenCC.Text.Trim();
            if (search_tenCC.Text == "")
            {
                PhieuNhap_Load(sender, e);
            }
            else
            {
                dataGridView1.DataSource = modify.Table("Select * from PHIEUNHAP where MANCC like  '%" + name + "%'");
                dataGridView2.DataSource = modify.Table("Select (SL*DONGIA) AS 'Tổng' from PHIEUNHAP where MANCC like  '%" + name + "%' ");
            }
        }

        private void search_maHang_TextChanged(object sender, EventArgs e)
        {
            string name = search_maHang.Text.Trim();
            if (search_maHang.Text == "")
            {
                PhieuNhap_Load(sender, e);
            }
            else
            {
                string query = "Select * from PHIEUNHAP ";
                query += "where MAHH like  '%" + name + "%'";
                dataGridView1.DataSource = modify.Table(query);
                query = "Select (SL*DONGIA) AS 'Tổng' from PHIEUNHAP ";
                query += "where MAHH like  '%" + name + "%'";
                dataGridView2.DataSource = modify.Table(query);
            }
        }

        private void comboBox_tenHH_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if(cb.SelectedValue != null)
            {
                QuanLyPhieuNhap ql = cb.SelectedValue as QuanLyPhieuNhap;
                textbox_maHangHoa.Text = ql.MaHH;
                textbox_manhaCungcap.Text = ql.MaNCC;
            }
        }
    }
}
