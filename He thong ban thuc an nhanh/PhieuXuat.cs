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
    public partial class PhieuXuat : Form
    {
        public PhieuXuat()
        {
            InitializeComponent();
        }
        Modify modify = new Modify();
        QuanLyPhieuXuat QuanLyPhieuXuat = new QuanLyPhieuXuat();
        Form1 Form1 = new Form1();
        private void PhieuXuat_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = modify.Table("Select * from CTHD");
                dataGridView2.DataSource = modify.Table("Select (SL*GIABAN) AS 'Tổng' from CTHD,HANGHOA where CTHD.MAHH=HANGHOA.MAHH");
                dataGridView3.DataSource = modify.Table("Select sum(SL*GIABAN) from CTHD,HANGHOA where CTHD.MAHH=HANGHOA.MAHH");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            deleteTextPhieuXuat();
        }
        private void textbox_Soluong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void deleteTextPhieuXuat()
        {
            textbox_maHD.Text = String.Empty;
            textbox_maHH.Text = String.Empty;
            textbox_Soluong.Text = String.Empty;
        }
        private bool CheckTextPhieuXuat()
        {
            if (textbox_maHH.Text == String.Empty)
            {
                MessageBox.Show("Chưa nhập mã hàng hóa!");
                return false;
            }
            if (textbox_maHD.Text == String.Empty)
            {
                MessageBox.Show("Chưa nhập mã hóa đơn!");
                return false;
            }
            if (textbox_Soluong.Text == String.Empty)
            {
                MessageBox.Show("Chưa nhập số lượng!");
                return false;
            }
            return true;
        }
        private void inputPhieuXuat()
        {
            string maHD = textbox_maHD.Text;
            string maHH = textbox_maHH.Text;
            int soluong = int.Parse(textbox_Soluong.Text);
            QuanLyPhieuXuat = new QuanLyPhieuXuat(maHD, maHH, soluong);
        }
        private void button_them1_Click(object sender, EventArgs e)
        {
            if (CheckTextPhieuXuat())
            {
                inputPhieuXuat();
                string query = "insert into CTHD values ('" + QuanLyPhieuXuat.MaHD + "','" + QuanLyPhieuXuat.MaHH + "','" + QuanLyPhieuXuat.Sl + "')";
                try
                {
                    if (MessageBox.Show("Có muốn thêm dữ liệu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        modify.Command(query);
                        modify.Command("update HANGHOA set SlTON = SLTON - SL from HANGHOA,CTHD where CTHD.MAHD = '" + QuanLyPhieuXuat.MaHD + "' and CTHD.MAHH = '" + QuanLyPhieuXuat.MaHH + "' and CTHD.MAHH = HANGHOA.MAHH");
                        MessageBox.Show("Thêm thành công!");
                        PhieuXuat_Load(sender, e);
                        Form1.Form1_Load(sender, e);
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
            if (CheckTextPhieuXuat())
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                string choose = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string choose2 = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                inputPhieuXuat();
                string query = "update CTHD set MAHD = '" + QuanLyPhieuXuat.MaHD + "', MAHH = '" + QuanLyPhieuXuat.MaHH + "', SL = '" + QuanLyPhieuXuat.Sl + "' ";
                query += "where MAHD = '" + choose + "' and MAHH ='"+choose2+"'";
                try
                {
                    if (MessageBox.Show("Có muốn sửa dữ liệu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        modify.Command(query);
                        MessageBox.Show("Sửa thành công!");
                        PhieuXuat_Load(sender, e);
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
            string query = "delete from CTHD ";
            query += "where MAHD = '" + choose + "' and MAHH='"+choose2+"'";
            try
            {
                if (MessageBox.Show("Có muốn xóa dữ liệu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    modify.Command(query);
                    MessageBox.Show("Xóa thành công!");
                    PhieuXuat_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textbox_maHD.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textbox_maHH.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textbox_Soluong.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }
        private void textBox_searchHD_TextChanged(object sender, EventArgs e)
        {
            string name = textBox_searchHD.Text.Trim();
            if (textBox_searchHD.Text == "")
            {
                PhieuXuat_Load(sender, e);
            }
            else
            {
                dataGridView1.DataSource = modify.Table("Select * from CTHD where MAHD like  '%" + name + "%'");
                dataGridView2.DataSource = modify.Table("Select (SL*GIABAN) AS 'Tổng' from CTHD,HANGHOA where CTHD.MAHH=HANGHOA.MAHH and MAHD like  '%" + name + "%' ");
            }
        }

        private void textBox_searchHH_TextChanged(object sender, EventArgs e)
        {
            string name = textBox_searchHH.Text.Trim();
            if (textBox_searchHH.Text == "")
            {
                PhieuXuat_Load(sender, e);
            }
            else
            {
                dataGridView1.DataSource = modify.Table("Select * from CTHD where MAHH like  '%" + name + "%'");
                dataGridView2.DataSource = modify.Table("Select (SL*GIABAN) AS 'Tổng' from CTHD,HANGHOA where CTHD.MAHH=HANGHOA.MAHH and CTHD.MAHH like  '%" + name + "%' ");
            }
        }
    }
}
