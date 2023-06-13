using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using hoclaicshape.Modal;

namespace hoclaicshape
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        QLBanHang1206Context db = new QLBanHang1206Context();
        List<LoaiSp> DML = new List<LoaiSp>();
        public MainWindow()
        {
            InitializeComponent();
        }
        public void refresh()
        {
            var query = (from dml in db.LoaiSp
                         select dml);
            DML = query.ToList();
            cbbLoaiSP.ItemsSource = query.ToList().Select(e => e.TenLoai);
            var data = (from sanPham in db.SanPham
                        join dml in db.LoaiSp on sanPham.MaLoai equals dml.MaLoai
                        select new
                        {
                            MaSp=sanPham.MaSp,
                            TenSp=sanPham.TenSp,
                            TenLoai=dml.TenLoai,
                            SoLuong=sanPham.SoLuong,
                            DonGia=sanPham.DonGia
                        }
                      );
            DSXuat.ItemsSource = data.ToList();
            TXTMaSP.Focus();
            TXTMaSP.Text = "";
            TXTTenSP.Text = "";
            TXTSoLuong.Text = "";
            TXTDonGia.Text = "";
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void window_load(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        private void select_item(object sender, SelectedCellsChangedEventArgs e)
        {
            dynamic selectItem = DSXuat.SelectedItem;
            if (selectItem != null)
            {
               
                TXTMaSP.Text = selectItem.MaSp;
                TXTTenSP.Text = selectItem.TenSp;
                TXTSoLuong.Text = selectItem.SoLuong.ToString();
                TXTDonGia.Text = selectItem.DonGia.ToString();
                cbbLoaiSP.Text = selectItem.TenLoai;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string maSP = TXTMaSP.Text;
            string tenSP = TXTTenSP.Text;
            string tenLoai = cbbLoaiSP.Text;
            string soLuong = TXTSoLuong.Text;
            string donGia = TXTDonGia.Text;
            if (maSP == "" || tenSP == "" || soLuong == "" || donGia == "") {
                MessageBox.Show("vui long nhap day du thong tin!!!");
            }
            else
            {
                var data = (from sanPham in db.SanPham
                            where sanPham.MaSp.Equals(TXTMaSP.Text)
                            select sanPham).SingleOrDefault();
                if (data != null)
                {
                    MessageBox.Show("san pham nay da ton tai");
                }
                else
                {
                    string ml = "";
                    foreach (LoaiSp t in DML)
                    {
                        if (t.TenLoai == tenLoai)
                        {
                            ml = t.MaLoai;
                        }
                    }
                    SanPham a = new SanPham();
                    a.MaSp = maSP;
                    a.TenSp = tenSP;
                    a.MaLoai = ml;
                    a.SoLuong = int.Parse(soLuong);
                    a.DonGia = float.Parse(donGia);
                    db.SanPham.Add(a);
                    db.SaveChanges();
                    refresh();
                    MessageBox.Show("them thanh cong");
                }
             
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (DSXuat.SelectedValue == null) {
                MessageBox.Show("vui long chon san pham de sua");
            }
            else
            {
                string maSP = TXTMaSP.Text;
                string tenSP = TXTTenSP.Text;
                string tenLoai = cbbLoaiSP.Text;
                string soLuong = TXTSoLuong.Text;
                string donGia = TXTDonGia.Text;

                foreach(SanPham sp in db.SanPham)
                {
                    if (sp.MaSp == maSP)
                    {
                        db.SanPham.Remove(sp);
                    }
                }

                string ml = "";
                foreach (LoaiSp t in DML)
                {
                    if (t.TenLoai == tenLoai)
                    {
                        ml = t.MaLoai;
                    }
                }
                SanPham a = new SanPham();
                a.MaSp = maSP;
                a.TenSp = tenSP;
                a.MaLoai = ml;
                a.SoLuong = int.Parse(soLuong);
                a.DonGia = float.Parse(donGia);
                db.SanPham.Add(a);
                db.SaveChanges();
                refresh();
                MessageBox.Show("sau thanh cong");
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (DSXuat.SelectedValue == null)
            {
                MessageBox.Show("vui long chon san pham de xoa");
            }
            else
            {
                string maSP = TXTMaSP.Text;
                foreach (SanPham sp in db.SanPham)
                {
                    if (sp.MaSp == maSP)
                    {
                        db.SanPham.Remove(sp);
                    }
                }
                db.SaveChanges();
                refresh();
                MessageBox.Show("xoa thanh cong");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
