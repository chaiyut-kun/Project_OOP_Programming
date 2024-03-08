using Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class Dashboard : Bill
    {
        private Image[] menu_THA = {Properties.Resources.Task1 , Properties.Resources.Task2 ,
            Properties.Resources.Task3 , Properties.Resources.Task4 , Properties.Resources.Result1 , Properties.Resources.price_all , Properties.Resources.Summit , Properties.Resources.Bill_THA
        };
        public Image[] Menu_THA { get => this.menu_THA; }
        private Image[] menu = { Properties.Resources.Task1_eng , Properties.Resources.Task2_eng , Properties.Resources.Task3_eng
                , Properties.Resources.Task4_eng , Properties.Resources.Result_eng  , Properties.Resources.price_all_eng , Properties.Resources.Result_ , Properties.Resources.Bill_Eng
        }; 
        public Image[] Menu { get => this.menu; }
        private int[] count_menu = new int[4];
        public string[] menu_name = { "Cocoa  Frappe", "Chocolate Frappe", "Iced green tea", "Iced milk tea"};
        public int[] Count_menu { get => this.count_menu; }

        //ฟังก์เพิ่มค่า ที่นับจำนวน count_menu
        public void Increase(int index)
        {
            this.count_menu[index]++;
        }
        //get index ของเมนู
        public int Get_indexOf(string val)
        {
            return menu_name.ToList().IndexOf(val);
        }
        //ฟังก์ลดค่า ที่นับจำนวน count_menu
        public void Decrease(int index)
        {
            if (this.count_menu[index] > 0)
            {
                this.count_menu[index]--;
            }
        }
        private int[] menu_price = { 40, 45,  35 , 35 }; 
        public int[] Menu_price { get => menu_price; }
        public string THA { get => "THA"; } 
        public string ENG { get => "ENG"; }
    }
}
