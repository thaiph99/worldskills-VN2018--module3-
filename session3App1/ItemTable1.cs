using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace session3App1
{
    class ItemTable1
    {
        string col1, col2, col3, col4, col5, col6, col7;

        public ItemTable1(string col1, string col2, string col3, string col4, string col5, string col6, string col7)
        {
            this.col1 = col1;
            this.col2 = col2;
            this.col3 = col3;
            this.col4 = col4;
            this.col5 = col5;
            this.col6 = col6;
            this.col7 = col7;
        }

        public ItemTable1()
        {
        }

        public string Col1 { get => col1; set => col1 = value; }
        public string Col2 { get => col2; set => col2 = value; }
        public string Col3 { get => col3; set => col3 = value; }
        public string Col4 { get => col4; set => col4 = value; }
        public string Col5 { get => col5; set => col5 = value; }
        public string Col6 { get => col6; set => col6 = value; }
        public string Col7 { get => col7; set => col7 = value; }



    }
}
