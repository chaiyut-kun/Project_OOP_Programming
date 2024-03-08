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
        Dashboard dashboard = new Dashboard();
        Bill bill = new Bill();
        Control control = new Control();
        Label[] all_m , label_layer1 ;
        // all pic เอาไว้สำหรับ เก็บ obj ของรูปภาพ layer 1 
        PictureBox[] all_pic , pic_layer2;
        List<string> new_orders = new List<string>();
        string[] orders = new string[] {"" , "" , "", ""};
        int pay;
        bool can , change_lang = false;
        


        private Label[] Get_Label()
        {
            all_m = new Label[] { First_m, Sec_m, Third_m, Fourth_m };
            return all_m;
        }
        private PictureBox[] Get_picture()
        {
            all_pic = new PictureBox[] {Menu1 , Menu2 , Menu3  , Menu4 , Result_pic , PriceAll_pic , Summit_result };
            return all_pic;
        }
        private bool Check_index(int index)
        {
            can = control.index_check(index);
            if (!can)
            {
                MessageBox.Show("โปรแกรมมีปัญหา");
                return false;
            }
            return true;
        }
        private void Manage_In(int index)
        {
            if (!Check_index(index))
            {
                return;
            }
            //Increase count of menu 
            dashboard.Increase(index);
            //Reset value of count menu
            Get_Label()[index].Text = dashboard.Count_menu[index].ToString();
            //Increase sum of menu
            dashboard.All_price = dashboard.Menu_price[index];
            orders[index] = dashboard.menu_name[index];
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
            if (!Check_index(index))
            {
                return;
            }
            //Decrease count of menu 
            dashboard.Decrease(index);
            //Reset value of count menu
            Get_Label()[index].Text = dashboard.Count_menu[index].ToString();
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
        private void Mange_pic(bool change)
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
            }
            else
            {
                Language_pic.Image = Properties.Resources.icons8_english_100;
                label1.Text = dashboard.ENG;
                for (int i = 0; i < Get_picture().Length; i++)
                {
                    Get_picture()[i].Image = dashboard.Menu_THA[i];
                }
            }
        }
        private void to_eng(bool change)
        {
            Mange_pic(change);

        }
        private void to_tha(bool change)
        {
            Mange_pic(change);
        }
        private void Language_Click(object sender, EventArgs e)
        {
            change_lang = !change_lang;
            if (change_lang)
            {
                to_eng(change_lang);   
            }
            else
            {
                to_tha(change_lang);
            }
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
            Show_layer2(false);
            Show_layer1(true);
        }
        private void Save_CSV_Click(object sender, EventArgs e)
        {
            if (!bill.Write_file(new_orders, dashboard.All_price, Recieve_Box.Text , Change_label.Text , Datetime_order.Text , Queue_order.Text))
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
                
                Queue_order.Text = (dashboard.getCurrent_queue()+1).ToString();
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
            Array.ForEach(Get_Label().Concat(label_layer1).ToArray(), label => { label.Visible = visible; });
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
            List<string> new_orders = orders.Where(v => v != "").ToList();
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

