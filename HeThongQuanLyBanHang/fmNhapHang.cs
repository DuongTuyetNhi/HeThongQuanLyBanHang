﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeThongQuanLyBanHang
{
    public partial class fmNhapHang : Form
    {
        public fmNhapHang()
        {
            InitializeComponent();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            fmSuaNguyenLieuNhap snl = new fmSuaNguyenLieuNhap();
            snl.Show();
        }
    }
}
