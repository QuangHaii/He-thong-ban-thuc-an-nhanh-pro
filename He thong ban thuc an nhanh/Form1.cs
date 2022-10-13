namespace He_thong_ban_thuc_an_nhanh
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Modify modify = new Modify();
        QuanLyNCC QuanLyNCC = new QuanLyNCC();
        QuanLyHangHoa QuanLyHangHoa = new QuanLyHangHoa();
        QuanLyHoaDon QuanLyHoaDon = new QuanLyHoaDon();
        public void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = modify.Table("Select * from NCC");
                dataGridView2.DataSource = modify.Table("Select * from HANGHOA");
                dataGridView3.DataSource = modify.Table("Select * from HOADON");
                dataGridView9.DataSource = modify.Table("Select sum(SL * DONGIA) from PHIEUNHAP");
                dataGridView8.DataSource = modify.Table("Select sum(SL*GIABAN) from CTHD,HANGHOA where CTHD.MAHH=HANGHOA.MAHH");
                dataGridView4.DataSource = modify.Table("Select sum(SL) from PHIEUNHAP");
                dataGridView5.DataSource = modify.Table("Select sum(SL) from CTHD");
                dataGridView6.DataSource = modify.Table("Select sum(SLTON) from HANGHOA"); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: "+ex.Message);
            }
            deleteTextNCC();
            deleteTextHH();
        }
        //Nhà cung cấp
        private void textbox_SDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void deleteTextNCC()
        {
            textbox_tenNCC.Text = String.Empty;
            textbox_diachiNCC.Text = String.Empty;
            textbox_maNCC.Text = String.Empty;
            textbox_SDT.Text = String.Empty;
            textbox_mahhoa.Text = String.Empty;
        }
        private bool CheckTextNCC()
        {
            if (textbox_tenNCC.Text == String.Empty)
            {
                MessageBox.Show("Chưa nhập tên nhà cung cấp!");
                return false;
            }
            if (textbox_maNCC.Text == String.Empty)
            {
                MessageBox.Show("Chưa nhập mã nhà cung cấp!");
                return false;
            }
            if (textbox_diachiNCC.Text == String.Empty)
            {
                MessageBox.Show("Chưa nhập địa chỉ!");
                return false;
            }
            if (textbox_SDT.Text == String.Empty)
            {
                MessageBox.Show("Chưa nhập số điện thoại!");
                return false;
            }
            if (textbox_mahhoa.Text == String.Empty)
            {
                MessageBox.Show("Chưa nhập mã hàng hóa!");
                return false;
            }
            return true;
        }
        private void inputNCC()
        {
            string tenNCC = textbox_tenNCC.Text;
            string maNCC = textbox_maNCC.Text;
            string diaChi = textbox_tenNCC.Text;
            string sdt = textbox_SDT.Text;
            string mahhoa = textbox_mahhoa.Text;
            QuanLyNCC = new QuanLyNCC(tenNCC,maNCC,diaChi,sdt,mahhoa);
    }
        private void button_them1_Click(object sender, EventArgs e)
        {
            if(CheckTextNCC())
            {
                inputNCC();
                string query = "insert into NCC values ('"+QuanLyNCC.MaNCC+"','"+QuanLyNCC.mahhoa+"',N'"+QuanLyNCC.TenNCC+"',N'"+QuanLyNCC.diaChi+"','"+QuanLyNCC.sdt+"')";
                try
                {
                    if(MessageBox.Show("Có muốn thêm dữ liệu không?","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        modify.Command(query);
                        MessageBox.Show("Thêm thành công!");
                        Form1_Load(sender, e);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Lỗi: "+ex.Message);
                }
            }
        }
        private void button_sua1_Click(object sender, EventArgs e)
        {
            if (CheckTextNCC())
            {
                inputNCC();
                string query = "update NCC set MANCC = '"+QuanLyNCC.MaNCC+"',MAHH = '"+QuanLyNCC.mahhoa+"', TENNCC = N'"+QuanLyNCC.TenNCC+"', DIACHI = N'"+QuanLyNCC.diaChi+"', SDT = '"+QuanLyNCC.sdt+"'";
                query += "where MANCC ='"+QuanLyNCC.MaNCC+"';";
                try
                {
                    if (MessageBox.Show("Có muốn sửa dữ liệu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        modify.Command(query);
                        MessageBox.Show("Sửa thành công!");
                        Form1_Load(sender, e);
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
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            string query = "delete from NCC ";
            query += "where MANCC='" + choose + "'";
            try
            {
                if (MessageBox.Show("Có muốn xóa dữ liệu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    modify.Command(query);
                    MessageBox.Show("Xóa thành công!");
                    Form1_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textbox_maNCC.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textbox_mahhoa.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textbox_tenNCC.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textbox_diachiNCC.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textbox_SDT.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }
        private void search_mancc_TextChanged(object sender, EventArgs e)
        {
            string name = search_mancc.Text.Trim();
            if(search_mancc.Text=="")
            {
                Form1_Load(sender,e);
            }
            else
            {
                string query = "Select * from NCC ";
                query += "where MANCC like  '%" + name + "%'";
                dataGridView1.DataSource = modify.Table(query);
            }
        }
        private void search_tenncc_TextChanged(object sender, EventArgs e)
        {
            string name = search_tenncc.Text.Trim();
            if (search_tenncc.Text == "")
            {
                Form1_Load(sender, e);
            }
            else
            {
                string query = "Select * from NCC ";
                query += "where TENNCC like  N'%" + name + "%'";
                dataGridView1.DataSource = modify.Table(query);
            }
        }
        //Hàng hóa
        private void deleteTextHH()
        {
            textBox_tenHh.Text = String.Empty;
            textBox_maHH.Text = String.Empty;
            textBox_dvt.Text = String.Empty;
            textBox_giaban.Text = String.Empty;
            textBox_slton.Text = String.Empty;
        }
        private bool CheckTextHH()
        {
            if (textBox_tenHh.Text == String.Empty)
            {
                MessageBox.Show("Chưa nhập tên hàng hóa!");
                return false;
            }
            if (textBox_maHH.Text == String.Empty)
            {
                MessageBox.Show("Chưa nhập mã hàng hóa!");
                return false;
            }
            if (textBox_dvt.Text == String.Empty)
            {
                MessageBox.Show("Chưa nhập đơn vị tính!");
                return false;
            }
            if (textBox_giaban.Text == String.Empty)
            {
                MessageBox.Show("Chưa nhập giá bán!");
                return false;
            }
            if (textBox_slton.Text == String.Empty)
            {
                MessageBox.Show("Chưa nhập số lượng tồn!");
                return false;
            }
            return true;
        }
        private void inputHH()
        {
            string tenHh = textBox_tenHh.Text;
            string maHH = textBox_maHH.Text;
            string dvt = textBox_dvt.Text;
            int giaban = int.Parse(textBox_giaban.Text);
            int slton = int.Parse(textBox_slton.Text);
            QuanLyHangHoa = new QuanLyHangHoa(maHH,tenHh,dvt,giaban,slton);
        }
        private void button_them2_Click(object sender, EventArgs e)
        {
            if (CheckTextHH())
            {
                inputHH();
                string query = "insert into HANGHOA values ('" + QuanLyHangHoa.MaHH + "',N'" + QuanLyHangHoa.TenHH + "',N'" + QuanLyHangHoa.Dvt + "','" + QuanLyHangHoa.Giaban + "','"+QuanLyHangHoa.Slton+"')";
                try
                {
                    if (MessageBox.Show("Có muốn thêm dữ liệu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        modify.Command(query);
                        MessageBox.Show("Thêm thành công!");
                        Form1_Load(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void button_sua2_Click(object sender, EventArgs e)
        {
            if (CheckTextHH())
            {
                inputHH();
                string query = "update HANGHOA set MAHH = '" + QuanLyHangHoa.MaHH + "', TENHH = N'" + QuanLyHangHoa.TenHH + "', DVT = N'" + QuanLyHangHoa.Dvt + "', GIABAN = '" + QuanLyHangHoa.Giaban + "', SLTON = '"+QuanLyHangHoa.Slton+"' ";
                query += "where MAHH ='" + QuanLyHangHoa.MaHH + "';";
                try
                {
                    if (MessageBox.Show("Có muốn sửa dữ liệu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        modify.Command(query);
                        MessageBox.Show("Sửa thành công!");
                        Form1_Load(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void button_xoa2_Click(object sender, EventArgs e)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string choose = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            string query = "delete from HANGHOA ";
            query += "where MAHH='" + choose + "'";
            try
            {
                if (MessageBox.Show("Có muốn xóa dữ liệu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    modify.Command(query);
                    MessageBox.Show("Xóa thành công!");
                    Form1_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox_maHH.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            textBox_tenHh.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
            textBox_dvt.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
            textBox_giaban.Text = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
            textBox_slton.Text = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
        }
        private void textBox_searchHH_TextChanged(object sender, EventArgs e)
        {
            string name = textBox_searchHH.Text.Trim();
            if (textBox_searchHH.Text == "")
            {
                Form1_Load(sender, e);
            }
            else
            {
                string query = "Select * from HANGHOA ";
                query += "where MAHH like  '%" + name + "%'";
                dataGridView2.DataSource = modify.Table(query);
            }
        }
        private void textBox_searchTenHH_TextChanged(object sender, EventArgs e)
        {
            string name = textBox_searchTenHH.Text.Trim();
            if (textBox_searchTenHH.Text == "")
            {
                Form1_Load(sender, e);
            }
            else
            {
                string query = "Select * from HANGHOA ";
                query += "where TENHH like  N'%" + name + "%'";
                dataGridView2.DataSource = modify.Table(query);
            }
        }
        private void textBox_giaban_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void textBox_slton_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        //Phiếu nhập
        private void button1_Click(object sender, EventArgs e)
        {
            PhieuNhap phieuNhap = new PhieuNhap();
            DialogResult dialogResult = phieuNhap.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                Console.WriteLine("OK");
            }
            phieuNhap.Dispose();
        }
        //Hóa đơn
        private void deleteTextHD()
        {
            textBox_maHD.Text = String.Empty;
            dateTimePicker1.Value = DateTime.Now;
            textBox_maKH.Text = String.Empty;
            textBox_maNvien.Text = String.Empty;
        }
        private bool CheckTextHD()
        {
            if (textBox_maHD.Text == String.Empty)
            {
                MessageBox.Show("Chưa nhập mã hóa đơn!");
                return false;
            }
            if (textBox_maKH.Text == String.Empty)
            {
                MessageBox.Show("Chưa nhập mã khách hàng!");
                return false;
            }
            if (textBox_maNvien.Text == String.Empty)
            {
                MessageBox.Show("Chưa nhập mã nhân viên!");
                return false;
            }
            return true;
        }
        private void inputHD()
        {
            string maHD = textBox_maHD.Text;
            string maKH = textBox_maKH.Text;
            DateTime ngay = dateTimePicker1.Value;
            string manv = textBox_maNvien.Text;
            QuanLyHoaDon = new QuanLyHoaDon(maHD,ngay,maKH,manv);
        }
        private void button_them3_Click(object sender, EventArgs e)
        {
            if (CheckTextHD())
            {
                inputHD();
                string query = "insert into HOADON values ('" + QuanLyHoaDon.Mahd + "','" + QuanLyHoaDon.Ngay + "','" + QuanLyHoaDon.Makh + "','" + QuanLyHoaDon.Manv + "')";
                try
                {
                    if (MessageBox.Show("Có muốn thêm dữ liệu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        modify.Command(query);
                        MessageBox.Show("Thêm thành công!");
                        Form1_Load(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void button_sua3_Click(object sender, EventArgs e)
        {
            if (CheckTextHD())
            {
                inputHD();
                string query = "update HOADON set MAHD = '" + QuanLyHoaDon.Mahd + "', NGAY = '" + QuanLyHoaDon.Ngay + "', MAKH = '" + QuanLyHoaDon.Makh + "', MANV = '" + QuanLyHoaDon.Manv + "' ";
                query += "where MAHD ='" + QuanLyHoaDon.Mahd + "';";
                try
                {
                    if (MessageBox.Show("Có muốn sửa dữ liệu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        modify.Command(query);
                        MessageBox.Show("Sửa thành công!");
                        Form1_Load(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void button_xoa3_Click(object sender, EventArgs e)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string choose = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            string query = "delete from HOADON ";
            query += "where MAHD = '" + choose + "'";
            try
            {
                if (MessageBox.Show("Có muốn xóa dữ liệu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    modify.Command(query);
                    MessageBox.Show("Xóa thành công!");
                    Form1_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox_maHD.Text = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
            dateTimePicker1.Text = dataGridView3.SelectedRows[0].Cells[1].Value.ToString();
            textBox_maKH.Text = dataGridView3.SelectedRows[0].Cells[2].Value.ToString();
            textBox_maNvien.Text = dataGridView3.SelectedRows[0].Cells[3].Value.ToString();
        }
        private void textBox_searchHD_TextChanged(object sender, EventArgs e)
        {
            string name = textBox_searchHD.Text.Trim();
            if (textBox_searchHD.Text == "")
            {
                Form1_Load(sender, e);
            }
            else
            {
                string query = "Select * from HOADON ";
                query += "where MAHD like  '%" + name + "%'";
                dataGridView3.DataSource = modify.Table(query);
            }
        }
        private void textBox_searchKH_TextChanged(object sender, EventArgs e)
        {
            string name = textBox_searchKH.Text.Trim();
            if (textBox_searchKH.Text == "")
            {
                Form1_Load(sender, e);
            }
            else
            {
                string query = "Select * from HOADON ";
                query += "where MAKH like  '%" + name + "%'";
                dataGridView3.DataSource = modify.Table(query);
            }
        }
        //Phiếu xuất
        private void button5_Click(object sender, EventArgs e)
        {
            PhieuXuat phieuXuat = new PhieuXuat();
            DialogResult dialogResult = phieuXuat.ShowDialog();
            if(dialogResult == DialogResult.OK)
            {
                Console.WriteLine("OK");
            }
            phieuXuat.Dispose();
        }
        //Lợi nhuận
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string name = textBox2.Text.Trim();
            if (textBox2.Text == "")
            {
                Form1_Load(sender, e);
            }
            else
            {
                string query = "Select sum(CTHD.SL*HANGHOA.GIABAN) ";
                query += "from HOADON,CTHD,HANGHOA ";
                query += "where year(NGAY) = " + name + " and HOADON.MAHD = CTHD.MAHD and CTHD.MAHH = HANGHOA.MAHH";
                dataGridView10.DataSource = modify.Table(query);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            string name = comboBox1.Text;
            if (comboBox1.Text == "")
            {
                Form1_Load(sender, e);
            }
            else
            {
                string query = "Select sum(CTHD.SL*HANGHOA.GIABAN) ";
                query += "from HOADON,CTHD,HANGHOA ";
                query += "where month(HOADON.NGAY) = " + name + " and HOADON.MAHD = CTHD.MAHD and CTHD.MAHH = HANGHOA.MAHH";
                dataGridView7.DataSource = modify.Table(query);
            }
        }
    }
}