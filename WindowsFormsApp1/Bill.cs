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
    public class Bill : Dashboard
    {
        private DateTime datetime = new DateTime();
        private StringBuilder content = new StringBuilder();
        private string path = @"D:\Programming\C#\Project\service Program\WindowsFormsApp1\Data.csv";

        //method
        //ฟังก์ชันจะคืนค่าเวลาณปัจจุบัน
        public DateTime Get_now()
        {
            datetime = DateTime.Now;
            return datetime;
        }
        //ฟังก์ชันสำหรับเขียนไฟล์
        public bool Write_file(List<string> menu, int total , string receive , string change )
        {
            for (int i = 0; i < menu.Count; i++)
            {
                content.Append(string.Format("{0} :", menu[i]));
            }
            content.Append(string.Format(",{0} , {1} , {2} , {3} , {4}\n", total, receive, change, $"{this.datetime.ToString("dd/MM/yyyy")} : {this.datetime.ToString("HH/mm/ss")}", this.getCurrent_queue() + 1));

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
        //ฟังก์ชันคืนค่า คิวล่าสุด
        public int getCurrent_queue()
        {
            string[] content = File.ReadAllLines(path);
            int current = int.Parse(content[content.Length - 1][content[content.Length - 1].Length - 1].ToString());
            return current;
            
        }

    }
}
