using Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Dashboard 
    {
        // field
        private Image[] menu_THA = {Properties.Resources.Task1 , Properties.Resources.Task2 ,
            Properties.Resources.Task3 , Properties.Resources.Task4 , Properties.Resources.Result1 , Properties.Resources.price_all , Properties.Resources.Summit , Properties.Resources.Bill_THA
        };
        private Image[] menu = { Properties.Resources.Task1_eng , Properties.Resources.Task2_eng , Properties.Resources.Task3_eng
                , Properties.Resources.Task4_eng , Properties.Resources.Result_eng  , Properties.Resources.price_all_eng , Properties.Resources.Result_ , Properties.Resources.Bill_Eng
        }; 
        private int[] count_menu = new int[4];
        private string[] menu_name = { "Cocoa  Frappe", "Chocolate Frappe", "Iced green tea", "Iced milk tea" };
        protected int[] menu_price = { 40, 45,  35 , 35 };
        private List<int> all_price = new List<int>();

        // property
        public int All_price
        {
            get => all_price.Sum();
        }
        public Image[] Menu_THA { get => this.menu_THA; }
        public Image[] Menu { get => this.menu; }
        public string[] Menu_name { get => menu_name;}
        public int[] Count_menu { get => this.count_menu; }
        public int[] Menu_price { get => menu_price; }
        public string THA { get => "THA"; } 
        public string ENG { get => "ENG"; }

        // method   
        //get index ของเมนู
        public int Get_indexOf(string val)
        {
            return Menu_name.ToList().IndexOf(val);
        }
        //ฟังก์เพิ่มค่า ที่นับจำนวน count_menu
        
        //ฟังก์ลดค่า ที่นับจำนวน count_menu
        public void Increase(int index)
        {
            this.count_menu[index]++;
        }
        public void Decrease(int index)
        {
            if (this.count_menu[index] > 0)
            {
                this.count_menu[index]--;
            }
        }

        public void Increase_value(int value)
        {
            all_price.Add(value);
        }
        public void Decrease_value(int value)
        {
            all_price.Remove(value);
        }
    }
}
