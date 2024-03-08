#### ความเป็นมาของโปรแกรม
โปรแกรมได้รับแรงบันดาลใจจากร้าน NobiCha จากบิ๊กซีอัศวรรณ ผู้ทำจัดสร้างขึ้นเพื่อสร้างเป็นต้นแบบโปรแกรมสำหรับผู้ขายโดยเฉพาะ ซึ่งสามารถใช้เพื่อเป็นตัวช่วยการขายให้กับผู้ค้าได้

### วัตถุประสงค์ของโปรแกรม
เป็นตัวช่วยการขายให้กับผู้ค้าของร้านชานมไข่มุก

### โครงสร้างของโปรแกรม
```mermaid
classDiagram
    Dashboard <|-- Control
    Dashboard <|-- Bill
    Form1 -- Dashboard
    class Bill{
        -Datetime datetime
        -StringBuilder content
        -String path 
        +Get_now()
        +Write_file(List<string> menu , int total , string receive , string change , string datetime)
        +getCurrent_queue()
    }
    class Dashboard{
        -Image[] menu_THA
        -Image[] menu
        -int[] count_menu
        -int[] menu_price
        #String[] menu_name
        -List<int> all_price
        +Menu_THA() 
        +Menu() 
        +Menu_name() 
        +Count_menu() 
        +Menu_price() 
        +THA() 
        +ENG() 
        +Get_indexOf(string val)    
        +Increase(int index) 
        +Decrease(int index)
        +Increase_value(int val)
        +Decrease_value(int val) 
    }
class Control{
    -int change
    +Change(int price , int pay) 
    +Buy(int price , int pay) 
    +index_check(int index) 
}
class Form1{
    -Dashboard dashboard
    -Bill bill
    -Control control
    -Label[] all_m
    -PictureBox[] all_pic
    -PictureBox[] pic_layer2
    -List<string> new_orders
    -string[] orders
    -int pay
    -bool can
    -bool change_lang
    +Fom1()
    -Get_label() 
    -Get_picture() 
    -Check_index(int index) 
    -Mange_In(int index) 
    -Manage_De(int index) 
    -Re_price() 
    -Mange_pic(bool change)
    -to_eng(bool change)
    -to_tha(bool change)
    -Langeuage_Click(object sender , EventArgs e) 
    -Menu1_Click(object sender , EventArgs e) 
    -First_D_Click(object sender , EventArgs e) 
    -Menu2_Click(object sender , EventArgs e) 
    -Sec_D_Click(object sender , EventArgs e) 
    -Menu3_Click(object sender , EventArgs e) 
    -Third_D_Click(object sender , EventArgs e) 
    -Menu4_Click(object sender , EventArgs e) 
    -Fourth_D_Click(object sender , EventArgs e) 
    -Previous_page_Click(object sender , EventArgs e) 
    -Save_CSV_Click(object sender , EventArgs e) 
    -Summmit_result_Click(object sender , EventArgs e) 
    -Show_layer1(bool visible) 
    -Show_layer2(bool visible) 
    -Mange_bill() 
} 
```

### ผู้พัฒนาโปรแกรม
นายชัยยุตม์ ถาวร 663450037-1