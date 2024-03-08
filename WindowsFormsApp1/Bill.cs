using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WindowsFormsApp1;

namespace Service
{
    internal class Bill 
    {
        private string queue;
        private DateTime datetime = new DateTime();
        protected List<int> all_price = new List<int>();
        private StringBuilder content = new StringBuilder();
        private string path = @"D:\Programming\C#\Project\service Program\WindowsFormsApp1\Data.csv";

        // all_price property
        public int All_price 
        { 
            get => all_price.Sum();
            set => all_price.Add(value);
        }

        //ลบ ค่าราคาเมื่อเรียกใช้ ฟังก์ชันนี้
        public void Decrease_value(int price)
        {
             all_price.Remove(price);
        }
        //ฟังก์ชันจะคืนค่าเวลาณปัจจุบัน
        public DateTime Get_now()
        {
            return DateTime.Now;
        }

        public bool Write_file(List<string> menu , int total , string receive , string change , string datetime , string queue )
        {
            for (int i = 0; i < menu.Count; i++)
            {
                content.Append(string.Format("{0} ,", menu[i]));
            }
            content.Append(string.Format("{0} , {1} , {2} , {3} , {4}\n",total , receive , change , datetime , queue));
            try
            {
                File.AppendAllText(path, content.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public int getCurrent_queue()
        {
            string[] content = File.ReadAllLines(path);
            int current = int.Parse(content[content.Length - 1][content[content.Length - 1].Length - 1].ToString());
            return current;
            
        }
    }
}
