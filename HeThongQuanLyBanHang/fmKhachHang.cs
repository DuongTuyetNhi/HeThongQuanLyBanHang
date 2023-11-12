﻿using HeThongQuanLyBanHang.DAO;
using System;
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
    public partial class fmKhachHang : Form
    {
        public fmKhachHang()
        {
            InitializeComponent();
            LoadKhachhangList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        void LoadKhachhangList()
        {
            string query = "select * from dbo.KHACHHANG";
            dataGridView1.DataSource = dataProvider.Instance.ExecuteQuery(query);
        }
    }
}
