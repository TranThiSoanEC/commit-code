using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;
using System.Text.RegularExpressions;

namespace QuanLyPhongMachTu
{
    public partial class frmKiemTraHoSoBenhAn : Form
    {
        private KiemTraHoSoBenhAnBUS ktBus;
        public frmKiemTraHoSoBenhAn()
        {
            InitializeComponent();
        }

        private void frmKiemTraHoSoBenhAn_Load(object sender, EventArgs e)
        {
            this.ktBus = new KiemTraHoSoBenhAnBUS();

        }

        private static bool IsNumber(string val)
        {
            if (val != "")
                return Regex.IsMatch(val, @"^[0-9]\d*\.?[0]*$");
            else return true;
        }

        private void loadXuatLSK()
        {
            //1. Load Accounts from DB
            List<KiemTraHoSoBenhAnDTO> LichSuKB = new List<KiemTraHoSoBenhAnDTO>();

            KiemTraHoSoBenhAnDTO k = new KiemTraHoSoBenhAnDTO();
            if (IsNumber(txtMaBenhNhan.Text) == false)
            {
                MessageBox.Show("Mã bệnh nhân không được trống hoặc là chữ. Vui lòng nhập lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            k.Mabn = int.Parse(txtMaBenhNhan.Text);
            if (ktBus.selectByMaBN(LichSuKB, k) == false || LichSuKB.Count == 0)
            {
                MessageBox.Show("Lịch sử khám bệnh on DB are not fetched.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }



            //2. Binding data to Grid view
            this.dgvLichSuKhamBenh.Columns.Clear();
            this.dgvLichSuKhamBenh.DataSource = null;


            this.dgvLichSuKhamBenh.AutoGenerateColumns = false;
            this.dgvLichSuKhamBenh.AllowUserToAddRows = false;
            this.dgvLichSuKhamBenh.DataSource = LichSuKB;


            DataGridViewTextBoxColumn mapkCol = new DataGridViewTextBoxColumn();
            mapkCol.Name = "MaPK";
            mapkCol.HeaderText = "Mã phiếu khám";
            mapkCol.DataPropertyName = "MaPK";
            mapkCol.Width = 120;
            this.dgvLichSuKhamBenh.Columns.Add(mapkCol);


            DataGridViewTextBoxColumn ngaykhamCol = new DataGridViewTextBoxColumn();
            ngaykhamCol.Name = "NgayKham";
            ngaykhamCol.HeaderText = "Ngày Khám";
            ngaykhamCol.DataPropertyName = "NgayKham";
            ngaykhamCol.Width = 130;
            this.dgvLichSuKhamBenh.Columns.Add(ngaykhamCol);

            CurrencyManager myCurrencyManager = (CurrencyManager)this.BindingContext[this.dgvLichSuKhamBenh.DataSource];
            myCurrencyManager.Refresh();
        }

        private void loadCTTT()
        {
            //1. Load Accounts from DB
            List<KiemTraHoSoBenhAnDTO> CTTT = new List<KiemTraHoSoBenhAnDTO>();

            KiemTraHoSoBenhAnDTO k = new KiemTraHoSoBenhAnDTO();
            DataGridViewRow dr = dgvLichSuKhamBenh.SelectedRows[0];
            k.Mapk = int.Parse(dr.Cells["MaPK"].Value.ToString());

            if (ktBus.selectCTTT(CTTT, k) == false)
            {
                MessageBox.Show("Chi tiết toa thuốc on DB are not fetched.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }



            //2. Binding data to Grid view
            this.dgvThongTinToaThuoc.Columns.Clear();
            this.dgvThongTinToaThuoc.DataSource = null;


            this.dgvThongTinToaThuoc.AutoGenerateColumns = false;
            this.dgvThongTinToaThuoc.AllowUserToAddRows = false;
            this.dgvThongTinToaThuoc.DataSource = CTTT;


            DataGridViewTextBoxColumn tenthuocCol = new DataGridViewTextBoxColumn();
            tenthuocCol.Name = "TenThuoc";
            tenthuocCol.HeaderText = "Tên thuốc";
            tenthuocCol.DataPropertyName = "TenThuoc";
            tenthuocCol.Width = 130;
            this.dgvThongTinToaThuoc.Columns.Add(tenthuocCol);

            DataGridViewTextBoxColumn soluongCol = new DataGridViewTextBoxColumn();
            soluongCol.Name = "SoLuong";
            soluongCol.HeaderText = "Số lượng";
            soluongCol.DataPropertyName = "SoLuong";
            soluongCol.Width = 80;
            this.dgvThongTinToaThuoc.Columns.Add(soluongCol);

            DataGridViewTextBoxColumn donvitinhCol = new DataGridViewTextBoxColumn();
            donvitinhCol.Name = "DonViTinh";
            donvitinhCol.HeaderText = "Đơn vị tính";
            donvitinhCol.DataPropertyName = "DonViTinh";
            this.dgvThongTinToaThuoc.Columns.Add(donvitinhCol);

            DataGridViewTextBoxColumn dongiaCol = new DataGridViewTextBoxColumn();
            dongiaCol.Name = "DonGia";
            dongiaCol.HeaderText = "Đơn giá";
            dongiaCol.DataPropertyName = "DonGia";
            this.dgvThongTinToaThuoc.Columns.Add(dongiaCol);

            DataGridViewTextBoxColumn cachdungCol = new DataGridViewTextBoxColumn();
            cachdungCol.Name = "CachDung";
            cachdungCol.HeaderText = "Cách dùng";
            cachdungCol.DataPropertyName = "CachDung";
            cachdungCol.Width = 500;
            this.dgvThongTinToaThuoc.Columns.Add(cachdungCol);


            CurrencyManager myCurrencyManager = (CurrencyManager)this.BindingContext[this.dgvThongTinToaThuoc.DataSource];
            myCurrencyManager.Refresh();
        }

        private void loadCTPK()
        {
            //1. Load Accounts from DB
            KiemTraHoSoBenhAnDTO k = new KiemTraHoSoBenhAnDTO();
            DataGridViewRow dr = dgvLichSuKhamBenh.SelectedRows[0];
            k.Mapk = int.Parse(dr.Cells["MaPK"].Value.ToString());
            if (ktBus.selectCTPK(k) == null)
            {
                MessageBox.Show("Chi tiết phiếu khám on DB are not fetched.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dtpNgayKham.Value = ktBus.selectCTPK(k).Ngaykham;
            txtTenBN.Text = ktBus.selectCTPK(k).Tenbn;
            dtpNgaySinh.Value = ktBus.selectCTPK(k).Ngaysinh;
            if (ktBus.selectCTPK(k).Gioitinh == "Nam")
            {
                rdbNam.Checked = true;
            }
            else
            {
                rdbNu.Checked = true;
            }
            txtBacSiKham.Text = ktBus.selectCTPK(k).Bacsi;
            txtTrieuChung.Text = ktBus.selectCTPK(k).Trieuchung;
            txtChuanDoan.Text = ktBus.selectCTPK(k).Chuandoan;
        }



        private void btnTim_Click(object sender, EventArgs e)
        {
            this.loadXuatLSK();
        }

        private void dgvLichSuKhamBenh_DoubleClick(object sender, EventArgs e)
        {
            this.loadCTTT();
            this.loadCTPK();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn có thực sự muốn thoát không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dlr == DialogResult.Yes)
            {

                Form_Chinh x = new Form_Chinh();
                this.Hide();
                x.ShowDialog();

            }

        }

        private void frmKiemTraHoSoBenhAn_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát tìm kiếm?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
