﻿using HeThongQuanLyBanHang.DAO;
using HeThongQuanLyBanHang.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace HeThongQuanLyBanHang
{
    public partial class fmThemHoaDonChiTiet : Form
    {
        public fmThemHoaDonChiTiet()
        {
            InitializeComponent();
            TaiBan();
            TaiMonAn();
        }
        #region Method
        void TaiMonAn()
        {
            List<MonAn> DSMonAn = MonAnDAO.Instance.LayDSMonAn();
            comboBox2.DataSource = DSMonAn;
            comboBox2.DisplayMember = "TenMon";
        }
        void TaiBan()
        {
            dataBan.Controls.Clear();
            List<Ban> dsBan = BanDAO.Instance.TaiDSBan();
            foreach (Ban item in dsBan)
            {
                Button btn = new Button() { Width = BanDAO.BanWidth, Height = BanDAO.BanHeight };
                btn.Text = item.MaBan + Environment.NewLine + item.TrangThai;
                btn.Click += Btn_Click;
                btn.Tag = item;
                switch (item.TrangThai)
                {
                    case "Không":
                        btn.BackColor = Color.White;
                        break;
                    default:
                        btn.BackColor = Color.Tomato;
                        break;
                }
                dataBan.Controls.Add(btn);
            }
        }
        void LayHoaDon(string MaBan)
        {
            listView1.Items.Clear();
            List<HeThongQuanLyBanHang.DTO.Menu> DSHoaDonChiTiet = MenuDAO.Instance.LayDSMenuTheoBan(MaBan);
            decimal TongTien = 0;
            foreach (HeThongQuanLyBanHang.DTO.Menu item in DSHoaDonChiTiet)
            {
                ListViewItem listItem = new ListViewItem(item.TenMA.ToString());
                listItem.SubItems.Add(item.SoLuong.ToString());
                listItem.SubItems.Add(item.DonGia.ToString());
                listItem.SubItems.Add(item.ThanhTien.ToString());
                TongTien += item.ThanhTien;
                listView1.Items.Add(listItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            textBox1.Text = TongTien.ToString("c", culture);

            TaiBan();
        }
        #endregion

        #region Event
        private void Btn_Click(object sender, EventArgs e)
        {
            string MaBan = ((sender as Button).Tag as Ban).MaBan;
            listView1.Tag = (sender as Button).Tag;
            LayHoaDon(MaBan);
        }
        private void btnThemMon_Click(object sender, EventArgs e)
        {
            Ban ban = listView1.Tag as Ban;
            string maHD = HoaDonDAO.Instance.TraHoaDonTheoBanChuaThanhToan(ban.MaBan);
            string maMA = (comboBox2.SelectedItem as MonAn).MaMon;
            int soLuong = (int)numericFood.Value;
            if (maHD == "-1")
            {
                HoaDonDAO.Instance.ThemHoaDon(ban.MaBan);
                HoaDonChiTietDAO.Instance.ThemHoaDonChiTiet(HoaDonDAO.Instance.LayHoaDonLonNhat(),maMA,soLuong);
            }
            else
            {
                HoaDonChiTietDAO.Instance.ThemHoaDonChiTiet(maHD, maMA, soLuong);
            }
            LayHoaDon(ban.MaBan);
        }
        
        private void btnThanhToan_Click(object sender, EventArgs e)
        {

            Ban ban = listView1.Tag as Ban;
            string maHD = HoaDonDAO.Instance.TraHoaDonTheoBanChuaThanhToan(ban.MaBan);
            string TextBox = textBox1.Text; // Giả sử textBox1 là tên của TextBox của bạn
            TextBox = TextBox.Replace(".", "").Replace(",", "").Replace(" ₫", "");
            decimal tongtien = decimal.Parse(TextBox);
            if (maHD != "-1")
            {
                if (MessageBox.Show("Bạn muốn thanh toán cho " + ban.MaBan, "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK) ;
                {
                    HoaDonDAO.Instance.TraHoaDon(maHD, tongtien);
                    LayHoaDon(ban.MaBan);

                    TaiBan();
                }
            }

        }
        private void btnIn_Click(object sender, EventArgs e)
        {
            Ban ban = listView1.Tag as Ban;
            if (MessageBox.Show("Bạn muốn in hóa đơn cho " + ban.MaBan, "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            { }

        }

        #endregion

        #region
        private void fmThemHoaDonChiTiet_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        #endregion

        
    }

}
