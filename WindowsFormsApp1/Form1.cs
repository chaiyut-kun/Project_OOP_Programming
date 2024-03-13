using Service;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Show_layer2(false);
        }
    //asset
        private Dashboard dashboard = new Dashboard();
        private Bill bill = new Bill();
        private Control control = new Control();
        private Label[]  label_layer1 , all_m;
         // all pic เอาไว้สำหรับ เก็บ obj ของรูปภาพ layer 1 
        private PictureBox[] pic_layer2 , all_pic;
        private List<string> new_orders = new List<string>();
        private string[] orders = new string[] {"" , "" , "", ""};
        private int pay;
        private bool can , change_lang = false;



        private Label[] Get_label()
        {
            all_m = new Label[] { First_m, Sec_m, Third_m, Fourth_m };
            return all_m;
        }
        private PictureBox[] Get_picture()
        {
            all_pic = new PictureBox[] { Menu1, Menu2, Menu3, Menu4, Result_pic, PriceAll_pic, Summit_result, Bill_pic , Previous_page , Save_csv};
            return all_pic;
        }
        private void Manage_In(int index)
        {

            can = control.index_check(index);
            if (!can)
            {

                MessageBox.Show("Program is Error");
                return;
            }
            //Increase count of menu 
            dashboard.Increase(index);
            //Reset value of count menu
            Get_label()[index].Text = dashboard.Count_menu[index].ToString();
            //Increase sum of menu
            dashboard.Increase_value(dashboard.Menu_price[index]);
            orders[index] = dashboard.Menu_name[index];
            //Reset total price
            Re_price();



            /*
            index = 0
            dashboard.Increase(0);
            First_m.Text = dashboard.Count_menu[0].ToString();
            dashboard.All_price = dashboard.Menu_price[0];
            Re_price();
            */
        }
        private void Manage_De(int index)
        {
            can = control.index_check(index);
            if (!can)
            {
                MessageBox.Show("Program is Error");
                return;
            }
            //Decrease count of menu 
            dashboard.Decrease(index);
            //Reset value of count menu
            Get_label()[index].Text = dashboard.Count_menu[index].ToString();
            //Decrease sum of menu
            dashboard.Decrease_value(dashboard.Menu_price[index]);
            //Reset total price
            if (dashboard.Count_menu[index] == 0)
            {
                orders[index] = "";
            }
            Re_price();
        }
        private void Re_price()
        {
            Allprice_change_label.Text = dashboard.All_price.ToString();
        }
        private void Manage_pic(bool change)
        {

            // if change = TO _eng
            if (change)
            {
                Language_pic.Image = Properties.Resources.icons8_thailand_100;
                label1.Text = dashboard.THA;
                for (int i = 0; i < Get_picture().Length; i++)
                {
                    Get_picture()[i].Image = dashboard.Menu[i];
                }
                Datetime_order.Location = new Point(Datetime_order.Location.X + 31, Datetime_order.Location.Y);
            }
            else
            {
                Language_pic.Image = Properties.Resources.icons8_english_100;
                label1.Text = dashboard.ENG;
                for (int i = 0; i < Get_picture().Length; i++)
                {
                    Get_picture()[i].Image = dashboard.Menu_THA[i];
                }
                Datetime_order.Location = new Point(Datetime_order.Location.X - 31, Datetime_order.Location.Y);
            }
        }
        private void Language_Click(object sender, EventArgs e)
        {
            change_lang = !change_lang;
            
            Manage_pic(change_lang);
        }
    //end asset 


        private void Menu1_Click(object sender, EventArgs e)
        {
            Manage_In(0);
            
        }
        private void First_D_Click(object sender, EventArgs e)
        {
            Manage_De(0);
            
        }
        private void Menu2_Click(object sender, EventArgs e)
        {
            Manage_In(1);
        }
        private void Sec_D_Click(object sender, EventArgs e)
        {
            Manage_De(1);
        }
        private void Menu3_Click(object sender, EventArgs e)
        {
            Manage_In(2);
        }
        private void Third_D_Click(object sender, EventArgs e)
        {
            Manage_De(2);
        }
        private void Menu4_Click(object sender, EventArgs e)
        {
            Manage_In(3);
        }
        private void Fourth_D_Click(object sender, EventArgs e)
        {
            Manage_De(3);
        }
        private void Previous_page_Click(object sender, EventArgs e)
        {
            Show_layer1(true);
            Show_layer2(false);
        }
        private void Save_CSV_Click(object sender, EventArgs e) 
        {
            if (!bill.Write_file(new_orders, dashboard.All_price , Recieve_Box.Text , Change_label.Text ))
            {
                MessageBox.Show("การเขียนไฟล์เกิดปัญหา", "ไม่สามารถเขียนไฟล์ได้");
            }
            else
            {
                MessageBox.Show("ทำการเขียนไฟล์สำเร็จ", "การเขียนไฟล์สำเร็จ");
            }
        }
        private void Summit_result_Click(object sender, EventArgs e)
        {
            can = int.TryParse(Recieve_Box.Text, out pay);
            if (dashboard.All_price == 0)
            {
                MessageBox.Show("ยังไม่ได้ทำรายการกรุณาทำรายการก่อนครับ", "กรุณาทำรายการ");
            }
            else if (!can || !control.Buy(dashboard.All_price , pay))
            {
                MessageBox.Show("กรุณาป้อนจำนวนเงินเป็นตัวเลข หรือ กรุณาป้อนจำนวนเงินให้พอกับราคารวม", "กรุณาป้อนจำนวนเงิน");
                return;
            }
            else
            {
                Show_layer1(false);
                Show_layer2(true);
                Mange_bill();
                
                Queue_order.Text = (bill.getCurrent_queue()+1).ToString();
                Datetime_order.Text = string.Format("{0} : {1}", bill.Get_now().ToString("dd/MM/yyyy"), bill.Get_now().ToString("H:mm:ss"));
            }
            

        }
        private void Show_layer1(bool visible)
        {

            label_layer1 = new Label[] { Allprice_change_label, First_D, Sec_D, Third_D, Fourth_D };
            foreach (PictureBox pic in Get_picture())
            {
                pic.Visible = visible;
            }
            /*foreach (Label la in Get_Label().Concat(label_layer1).ToArray())
            {
                la.Visible = visible;
            }*/
            Array.ForEach(Get_label().Concat(label_layer1).ToArray(), label => { label.Visible = visible; });
            Recieve_Box.Visible = visible;
            label2.Visible = visible;
        }
        private void Show_layer2(bool visible)
        {
            pic_layer2 = new PictureBox[] { Previous_page, Save_csv };
            Group_Bill.Visible = visible;
            Bill_pic.Visible = visible;
            Array.ForEach(pic_layer2 , pic => { pic.Visible = visible; }); 
           
        }
        private void Mange_bill()
        {
            new_orders = orders.Where(v => v != "").ToList();
            List<int> indexing = new List<int>();
            foreach (string d in new_orders)
            {
                indexing.Add(dashboard.Get_indexOf(d));
            }

            
            
            Label[][] Bill_label = new Label[][]
            {
                new Label[] {Bill_menu1 ,  Bill_menu2 , Bill_menu3 , Bill_menu4 },
                new Label[] {Qty_1,  Qty_2 , Qty_3, Qty_4 },
                new Label[] {Bill_price1,  Bill_price2 , Bill_price3 , Bill_price4 },
                new Label[] {Bill_total1 ,  Bill_total2 , Bill_total3 , Bill_total4 },
            };
            for (int i = 0; i < new_orders.Count; i++)
            {
                
                Bill_label[0][i].Text = new_orders[i].ToString();
                Bill_label[1][i].Text = dashboard.Count_menu[indexing[i]].ToString();
                Bill_label[2][i].Text = dashboard.Menu_price[indexing[i]].ToString();
                Bill_label[3][i].Text = (dashboard.Count_menu[indexing[i]] * dashboard.Menu_price[indexing[i]]).ToString();
                
                
                Bill_total_result.Text = dashboard.All_price.ToString();
                Recieve_label.Text = Recieve_Box.Text;
                Change_label.Text = (control.Change(dashboard.All_price , pay)).ToString();
                    
            }
        }
    }

}

