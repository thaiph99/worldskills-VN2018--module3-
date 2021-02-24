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
using System.Windows.Shapes;

namespace session3App1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            loadCombobox();
            loadData();
        }

        public MainWindow mainWin = ((MainWindow)Application.Current.MainWindow);

        public void loadCombobox()
        {

            using (var db = new Session3Entities())
            {
                var select = db.Countries.Select(p => p.Name);
                foreach (var i in select)
                    cbbPassCT.Items.Add(i);
            }
        }

        public void loadData()
        {
            ItemTable1 t1 = new ItemTable1();
            t1 = (ItemTable1)mainWin.dgvOutbound.SelectedItem;
            lbFrom.Content = t1.Col1;
            lbTo.Content = t1.Col2;
            lbCabin.Content = mainWin.cbbCabinType.Text;
            lbDate.Content = t1.Col3;
            lbFlightNum.Content = t1.Col5;
            if (mainWin.rdbtnreturn.IsChecked == true)
            {
                t1 = (ItemTable1)mainWin.dgvReturn.SelectedItem;
                lbFrom2.Content = t1.Col1;
                lbTo2.Content = t1.Col2;
                lbCabin2.Content = mainWin.cbbCabinType.Text;
                lbDate2.Content = t1.Col3;
                lbFlightNum2.Content = t1.Col5;
            }
            else
            {
                var v = Visibility.Hidden;
                lbFrom2.Visibility = v;
                lbTo2.Visibility = v;
                lbCabin2.Visibility = v;
                lbDate2.Visibility = v;
                lbFlightNum2.Visibility = v;
                v1.Visibility = v;
                v2.Visibility = v;
                v3.Visibility = v;
                v4.Visibility = v;
                v5.Visibility = v;
                v6.Visibility = v;
            }
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dgvPassList.SelectedItem;
            if (selectedItem != null)
                dgvPassList.Items.Remove(selectedItem);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!checkPassDetail())
            {
                MessageBox.Show("Thông tin chưa đúng quy định", "Cảnh báo!");
                return;
            }
            ItemTable2 row = new ItemTable2();
            row.Col1 = txtFiName.Text;
            row.Col2 = txtLaName.Text;
            row.Col3 = dpBirth.Text;
            row.Col4 = txtPassNum.Text;
            row.Col5 = cbbPassCT.Text;
            row.Col6 = txtPhone.Text;
            var listPass = dgvPassList.Items.OfType<ItemTable2>().ToList();
            var checkCoin = listPass.Where(p => p.Col4 == row.Col4).ToList();
            if (checkCoin.Count != 0)
            {
                MessageBox.Show("Passport number bị trùng", "Cảnh báo!");
                return;
            }
            if (listPass.Count == int.Parse(mainWin.txtPassenger.Text))
            {
                MessageBox.Show("Đã điền đủ thông tin cho số vé bạn đặt", "Cảnh báo!");
                return;
            }
            dgvPassList.Items.Add(row);
        }

        public bool checkPassDetail()
        {
            if (txtFiName.Text == "")
                return false;
            if (txtLaName.Text == "")
                return false;
            if (txtPassNum.Text == "")
                return false;
            if (txtPhone.Text == "")
                return false;
            if (dpBirth.Text == "")
                return false;
            if (cbbPassCT.Text == "")
                return false;
            return true;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            var listPass = dgvPassList.Items.OfType<ItemTable2>().ToList();
            if (listPass.Count != int.Parse(mainWin.txtPassenger.Text))
            {
                MessageBox.Show("Bạn chưa điền thông tin cho đủ số vé đã đặt", "Cảnh báo!");
                return;
            }
            Window2 wd2 = new Window2();
            wd2.ShowDialog();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
