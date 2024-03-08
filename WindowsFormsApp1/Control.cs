using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class Control
    {
        public int change = 0;
        public int Change(int price , int pay)
        {
            return change;
        }
        public bool Buy(int price , int pay)

        {
            if (pay >= price)
            {
                this.change = pay - price;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool index_check(int index)
        {
            if (index >= 0 && index < 4)
            {
                return true;
            }
            return false;
        }

        public bool index_check(int index, object[] arr)
        {
            if (index >= arr.Length && index <= 0)
            {
                return true;
            }
            return false;
        }
    }
}
