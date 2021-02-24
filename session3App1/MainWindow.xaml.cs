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

namespace session3App1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            rdbtnreturn.IsChecked = true;
            buildGraph();
            loadCbbWindow();
            loadDataDefaul();
        }

        public void loadDataDefaul()
        {
            dgvOutbound.Items.Clear();
            dgvReturn.Items.Clear();

            using (var db = new Session3Entities())
            {
                var data = db.Schedules;
                foreach (var i in data)
                {
                    ItemTable1 tb = new ItemTable1();
                    int rID = i.RouteID;
                    int airID1 = db.Routes.Where(p => p.ID == rID).Select(p => p.DepartureAirportID).FirstOrDefault();
                    tb.Col1 = db.Airports.Where(p => p.ID == airID1).Select(p => p.IATACode).FirstOrDefault();
                    int airID2 = db.Routes.Where(p => p.ID == rID).Select(p => p.ArrivalAirportID).FirstOrDefault();
                    tb.Col2 = db.Airports.Where(p => p.ID == airID2).Select(p => p.IATACode).FirstOrDefault();
                    tb.Col3 = i.Date.ToString("MM/dd/yyyy");
                    tb.Col4 = i.Time.ToString();
                    tb.Col5 = i.FlightNumber;
                    int idCabinType = db.CabinTypes.Where(p => p.Name == cbbCabinType.Text).Select(p => p.ID).FirstOrDefault();
                    double cost = double.Parse(i.EconomyPrice.ToString());
                    if (idCabinType == 2)
                        cost = cost + (cost * 0.35);
                    else if (idCabinType == 3)
                        cost = cost + (cost + (cost * 0.35)) * 0.3;
                    tb.Col6 = Convert.ToDecimal(cost).ToString("C0");
                    tb.Col7 = "0";
                    dgvOutbound.Items.Add(tb);
                    dgvReturn.Items.Add(tb);
                }
            }
        }

        public void loadCbbWindow()
        {
            using (var db = new Session3Entities())
            {
                var select = from s in db.CabinTypes select s;
                foreach (var data in select)
                    cbbCabinType.Items.Add(data.Name);

                var select1 = from s in db.Airports select s;
                foreach (var data in select1)
                {
                    cbbFrom.Items.Add(data.IATACode);
                    cbbTo.Items.Add(data.IATACode);
                }
            }
        }

        public List<int>[] graph;

        //add data from sql to graph
        public void buildGraph()
        {
            using (var db = new Session3Entities())
            {
                int n = db.Airports.Max(p => p.ID) + 1;
                graph = new List<int>[n];
                for (int i = 0; i < n; i++)
                    graph[i] = new List<int>();
                var select = from s in db.Routes select s;
                foreach (var data in select)
                {
                    int index = data.DepartureAirportID;
                    int val = data.ArrivalAirportID;
                    graph[index].Add(val);
                }
                for (int i = 0; i < n; i++)
                    graph[i] = new HashSet<int>(graph[i]).ToList<int>();
            }
        }

        //show graph into txtPassenger
        public void showgraph()
        {
            for (int i = 0; i < graph.Count(); i++)
            {
                txtPassenger.Text += i + ": ";
                foreach (var j in graph[i])
                {
                    txtPassenger.Text += j + "-";
                }
                txtPassenger.Text += "\n";
            }
        }

        List<int> onePath = new List<int>();
        List<List<int>> listPath = new List<List<int>>();

        // create intermediate list 
        public List<int> addPath(List<int> localpath)
        {
            var localbuilPath = new List<int>();
            foreach (var item in localpath)
                localbuilPath.Add(item);
            return localbuilPath;
        }

        // death first search + backtracking to find all path
        public void dfs(int u, int end)
        {
            if (u == end)
            {
                listPath.Add(addPath(onePath));
                return;
            }
            foreach (var i in graph[u])
                if (!onePath.Contains(i))
                {
                    onePath.Add(i);
                    dfs(i, end);
                    onePath.Remove(i);
                }
            return;
        }

        // get infor path to tho datagrid
        public int getIdRouter(int x, int y)
        {
            using (var db = new Session3Entities())
            {
                var listIdRou = db.Routes.Where(p => p.DepartureAirportID == x && p.ArrivalAirportID == y).Select(p => p.ID).ToList();
                for (int i = 0; i < listIdRou.Count; i++)
                {
                    int ii = listIdRou[i];
                    if (db.Schedules.Any(p => p.RouteID == ii))
                        return listIdRou[i];
                }
                return 0;
            }
        }

        private ItemTable1 get1Path(List<int> path, int id, ComboBox cb1, ComboBox cb2)
        {
            var db = new Session3Entities();
            var row = db.Schedules.Where(p => p.ID == id);
            double cost = 0;
            ItemTable1 x = new ItemTable1();
            x.Col1 = cb1.Text;
            x.Col2 = cb2.Text;
            x.Col3 = row.Select(p => p.Date).FirstOrDefault().ToString("MM/dd/yyyy");
            DateTime dt = new DateTime();
            dt = DateTime.ParseExact(x.Col3, "MM/dd/yyyy", null);
            x.Col4 = row.Select(p => p.Time).FirstOrDefault().ToString();
            x.Col5 += row.Select(p => p.FlightNumber).FirstOrDefault().ToString();
            cost += double.Parse(row.Select(p => p.EconomyPrice).FirstOrDefault().ToString());
            for (int i = 1; i < path.Count - 1; i++)
            {
                int idRoute = getIdRouter(path[i], path[i + 1]);
                id = db.Schedules.Where(p => (p.Date >= dt && p.RouteID == idRoute)).Select(p => p.ID).FirstOrDefault();
                if (id == 0)
                    return null;
                dt = db.Schedules.Where(p => p.ID == id).Select(p => p.Date).FirstOrDefault();
                x.Col5 += "-" + db.Schedules.Where(p => p.ID == id).Select(p => p.FlightNumber).FirstOrDefault();
                cost += double.Parse(db.Schedules.Where(p => p.ID == id).Select(p => p.EconomyPrice).FirstOrDefault().ToString());
            }
            int idCabinType = db.CabinTypes.Where(p => p.Name == cbbCabinType.Text).Select(p => p.ID).FirstOrDefault();
            if (idCabinType == 2)
                cost = cost + (cost * 0.35);
            else if (idCabinType == 3)
                cost = cost + (cost + (cost * 0.35)) * 0.3;
            x.Col6 = Convert.ToDecimal(cost).ToString("C0");
            x.Col7 = x.Col5.Count(p => p == '-').ToString();
            return x;
        }

        public void getPath(List<int> path, string dt, DataGrid dg, ComboBox cb1, ComboBox cb2, int chose)
        {
            var db = new Session3Entities();
            DateTime cmp = DateTime.ParseExact(dt, "MM/dd/yyyy", null);
            int idrou = getIdRouter(path[0], path[1]);
            List<int> listIdSchedules = new List<int>();
            if (chose == 1)
                listIdSchedules = db.Schedules.Where(p => p.Date >= cmp && p.RouteID == idrou).Select(p => p.ID).ToList();
            else
                listIdSchedules = db.Schedules.Where(p => p.Date == cmp && p.RouteID == idrou).Select(p => p.ID).ToList();
            foreach (int id in listIdSchedules)
            {
                ItemTable1 newItem = get1Path(path, id, cb1, cb2);
                if (newItem != null)
                    dg.Items.Add(newItem);
            }
        }

        public void pushPathToDg(DataGrid dg, string date, ComboBox cb1, ComboBox cb2, int chose)
        {
            int posBegin = 0, posEnd = 0;
            using (var db = new Session3Entities())
            {
                posBegin = db.Airports.Where(p => p.IATACode == cb1.Text).Select(p => p.ID).FirstOrDefault();
                posEnd = db.Airports.Where(p => p.IATACode == cb2.Text).Select(p => p.ID).FirstOrDefault();
            }
            listPath.Clear();
            onePath.Clear();
            onePath.Add(posBegin);
            dfs(posBegin, posEnd);
            dg.Items.Clear();
            foreach (var path in listPath)
                getPath(path, date, dg, cb1, cb2, chose);
        }

        public void BtnApply_Click(object sender, RoutedEventArgs e)
        {
            if (!checkSearchPara())
            {
                MessageBox.Show("Thông tin nhập chưa đúng", "Cảnh báo!");
                return;
            }
            pushPathToDg(dgvOutbound, dpOutbound.Text, cbbFrom, cbbTo, 0);
            if (rdbtnreturn.IsChecked == true)
                pushPathToDg(dgvReturn, dpReturn.Text, cbbTo, cbbFrom, 0);
        }

        public void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void Rdbtnreturn_Checked(object sender, RoutedEventArgs e)
        {
            Visibility v = Visibility.Visible;
            dgvReturn.Visibility = Visibility.Visible;
            cbDisplay2.Visibility = v;
            lbReturn2.Visibility = v;
            lbReturn.Visibility = v;
            dpReturn.Visibility = v;
            img1.Visibility = v;
        }

        public void Rdbtn1way_Checked(object sender, RoutedEventArgs e)
        {
            Visibility v = Visibility.Hidden;
            dgvReturn.Visibility = v;
            cbDisplay2.Visibility = v;
            lbReturn2.Visibility = v;
            lbReturn.Visibility = v;
            dpReturn.Visibility = v;
            img1.Visibility = v;
        }

        // booking

        public bool checkPassNum()
        {
            if (txtPassenger.Text == "")
                return false;
            var isNumberic = int.TryParse(txtPassenger.Text, out _);
            if (!isNumberic)
                return false;
            var db = new Session3Entities();
            int passNum = int.Parse(txtPassenger.Text);
            string CabinType = cbbCabinType.Text;

            var seatNum1 = db.Aircrafts.Where(p => p.ID == 1);
            var seatNum2 = db.Aircrafts.Where(p => p.ID == 1);

            int seatNum11 = 0;
            int seatNum21 = 0;
            if (CabinType == "Economy")
            {
                seatNum11 = seatNum1.Select(p => p.EconomySeats).FirstOrDefault();
                seatNum21 = seatNum2.Select(p => p.EconomySeats).FirstOrDefault();
            }
            else if (CabinType == "Business")
            {
                seatNum11 = seatNum1.Select(p => p.BusinessSeats).FirstOrDefault();
                seatNum21 = seatNum2.Select(p => p.BusinessSeats).FirstOrDefault();
            }
            else
            {
                seatNum11 = seatNum1.Select(p => p.TotalSeats - p.EconomySeats - p.BusinessSeats).FirstOrDefault();
                seatNum21 = seatNum2.Select(p => p.TotalSeats - p.EconomySeats - p.BusinessSeats).FirstOrDefault();
            }
            return passNum <= Math.Min(seatNum11, seatNum21) && 0 < passNum;
        }

        public bool checkDataRoute()
        {
            ItemTable1 x1 = new ItemTable1();
            ItemTable1 x2 = new ItemTable1();

            var x01 = dgvOutbound.SelectedItems.OfType<ItemTable1>().ToList();
            var x02 = dgvOutbound.SelectedItems.OfType<ItemTable1>().ToList();

            x1 = (ItemTable1)dgvOutbound.SelectedItem;
            x2 = (ItemTable1)dgvReturn.SelectedItem;
            if (x01.Count == 0 || (x02.Count == 0 && rdbtnreturn.IsChecked == true))
                return false;
            var t1 = DateTime.ParseExact(x1.Col3, "MM/dd/yyyy", null);
            var t2 = DateTime.ParseExact(x2.Col3, "MM/dd/yyyy", null);
            return t1 < t2;
        }

        public bool checkSearchPara()
        {
            if (cbbFrom.Text == "")
                return false;
            if (cbbTo.Text == "")
                return false;
            if (dpOutbound.Text == "")
                return false;
            if (rdbtnreturn.IsChecked == true && dpReturn.Text == "")
                return false;
            if (cbbFrom.Text == cbbTo.Text)
                return false;
            return true;
        }

        public void btnBook_click(object sender, RoutedEventArgs e)
        {
            if (!checkPassNum())
            {
                MessageBox.Show("Mời nhập lại số lượng vé", "Cảnh báo!");
                return;
            }

            if (!(rdbtnreturn.IsChecked == true && checkDataRoute() || rdbtn1way.IsChecked == true))
            {
                MessageBox.Show("Thời gian của các chuyến bay bạn đặt không khả dụng", "Cảnh báo!");
                return;
            }

            Window1 wd1 = new Window1();
            wd1.ShowDialog();
        }

        private void cbDis1_Checked(object sender, RoutedEventArgs e)
        {
            tickCheckBox(dgvOutbound, dpOutbound, cbbFrom, cbbTo);
        }

        public void tickCheckBox(DataGrid dg, DatePicker dp, ComboBox c1, ComboBox c2)
        {
            if (!checkSearchPara())
            {
                MessageBox.Show("Thông tin nhập chưa đúng", "Cảnh báo!");
                return;
            }
            TimeSpan tp = new TimeSpan();
            tp = TimeSpan.FromDays(3);
            DateTime dt = DateTime.ParseExact(dp.Text, "MM/dd/yyyy", null);
            DateTime dt0 = dt.Subtract(tp);
            pushPathToDg(dg, dt0.ToString("MM/dd/yyy"), c1, c2, 1);
            removeByDate(dg, dt);
        }

        public void removeByDate(DataGrid dg, DateTime date)
        {
            TimeSpan tp = new TimeSpan();
            tp = TimeSpan.FromDays(3);
            for (int i = 0; i < dg.Items.Count; i++)
            {
                ItemTable1 x = new ItemTable1();
                x = (ItemTable1)dg.Items[i];
                var d = DateTime.ParseExact(x.Col3, "MM/dd/yyyy", null);
                if (date < d.Subtract(tp))
                {
                    dg.Items.RemoveAt(i);
                    i--;
                }
            }
        }

        private void cbDis2_Checked(object sender, RoutedEventArgs e)
        {
            tickCheckBox(dgvReturn, dpReturn, cbbTo, cbbFrom);
        }

        private void cbDis1_UnChecked(object sender, RoutedEventArgs e)
        {
            if (!checkSearchPara())
            {
                MessageBox.Show("Thông tin nhập chưa đúng", "Cảnh báo!");
                return;
            }
            pushPathToDg(dgvOutbound, dpOutbound.Text, cbbFrom, cbbTo, 0);
        }

        private void cbDis2_UnChecked(object sender, RoutedEventArgs e)
        {
            if (!checkSearchPara())
            {
                MessageBox.Show("Thông tin nhập chưa đúng", "Cảnh báo!");
                return;
            }
            pushPathToDg(dgvReturn, dpReturn.Text, cbbTo, cbbFrom, 0);
        }
    }
}
