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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
            loadData();
        }

        public MainWindow mainWin = ((MainWindow)Application.Current.MainWindow);
        public Window1 win1 = (Window1)Application.Current.Windows[1];
        public Random random = new Random();

        public void loadData()
        {
            var ite1 = (ItemTable1)mainWin.dgvOutbound.SelectedItem;
            var ite2 = (ItemTable1)mainWin.dgvReturn.SelectedItem;

            int cost = int.Parse(ite1.Col6.Replace("$", "").Replace(",", ""));
            if (mainWin.rdbtnreturn.IsChecked == true)
                cost += int.Parse(ite2.Col6.Replace("$", "").Replace(",", ""));
            lbCostTotal.Content = (cost * int.Parse(mainWin.txtPassenger.Text)).ToString("C0");
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public char getRanC()
        {
            string del = "";
            for (char i = 'A'; i <= 'Z'; i++)
                del += i;
            for (char i = '1'; i <= '9'; i++)
                del += i;
            int num = random.Next(0, del.Length - 1);
            char let = del[num];
            return let;
        }

        public string createBookRef()
        {
            string s = "";
            for (int i = 0; i < 6; i++)
                s += getRanC();
            return s;
        }

        int cnt = 0;

        private void BtnIssue_Click(object sender, RoutedEventArgs e)
        {
            var db = new Session3Entities();
            string bookRef = "";
            while (true)
            {
                bookRef = createBookRef();
                var data = db.Tickets.Where(p => p.BookingReference == bookRef).ToList();
                if (data.Count == 0)
                    break;
            }

            var listPass = win1.dgvPassList.Items.OfType<ItemTable2>().ToList();
            var x = (ItemTable1)mainWin.dgvOutbound.SelectedItem;
            var x1 = (ItemTable1)mainWin.dgvReturn.SelectedItem;
            DateTime dt = DateTime.ParseExact(x.Col3, "MM/dd/yyyy", null);
            string[] xx = x.Col5.Split('-');
            string[] xx1 = x1.Col5.Split('-');
            foreach (var xi in xx)
                insertTicket(bookRef, listPass, dt, xi);
            foreach (var xi in xx1)
                insertTicket(bookRef, listPass, dt, xi);
            db.SaveChangesAsync();
            MessageBox.Show("Bạn đã đặt vé thành công", "Thông báo");
        }

        private void insertTicket(string bookRef, List<ItemTable2> listPass, DateTime dt, string flightNum)
        {
            var db = new Session3Entities();
            Ticket tIns = new Ticket();
            tIns.UserID = 1;
            tIns.ScheduleID = db.Schedules.Where(p => p.Date >= dt && p.FlightNumber == flightNum).Select(p => p.ID).FirstOrDefault();
            tIns.CabinTypeID = db.CabinTypes.Where(p => p.Name == mainWin.cbbCabinType.Text).Select(p => p.ID).FirstOrDefault();
            tIns.BookingReference = bookRef;
            tIns.Email = null;
            for (int i = 0; i < listPass.Count; i++)
            {
                tIns.Firstname = listPass[i].Col1;
                tIns.Lastname = listPass[i].Col2;
                string tmp1 = listPass[i].Col5;
                tIns.PassportCountryID = db.Countries.Where(p => p.Name == tmp1).Select(p => p.ID).FirstOrDefault();
                tIns.Phone = listPass[i].Col6;
                tIns.PassportNumber = listPass[i].Col4;
                tIns.Confirmed = true;
                db.Tickets.Add(tIns);
                db.SaveChanges();
                cnt++;
            }

        }
    }
}
