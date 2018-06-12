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

namespace QuanLyPhongMachTu
{
    public partial class KeToaThuoc : Form
    {
        private int MaPK;
        public KeToaThuoc(int mapk) : this()
        {
            MaPK = mapk;
            txb_MaPK.Text = MaPK.ToString();

        }
        public KeToaThuoc()
        {
            InitializeComponent();
        }
        // them thuoc
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void KeToaThuoc_Load(object sender, EventArgs e)
        {
           

        }

        public void TaiDuLieuVaoDataGirdView()
        {
            //List<CTTT> LoadCTTT(int MaPK)
            List<CTTT> dsBN = KeToaThuoc_BUS.LoadCTTT(int.Parse(txb_MaPK.Text.ToString()));

            dgv_KeToaThuoc.DataSource = dsBN;
        }

        }
    }
