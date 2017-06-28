using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TJ.Buiness;
using TJ.Common;
using TJ.DB;

namespace TJ_XinJielogistics
{
    public partial class frmNewOrder : Form
    {
        private string ModelId { get; set; }
        public bool istrue;
        string username;
        List<clsOrderDatabaseinfo> Result;
        clsOrderDatabaseinfo item;
        AutoSizeFormClass asc = new AutoSizeFormClass();
        public frmNewOrder(string maintype, clsOrderDatabaseinfo obj, string user)
        {
            InitializeComponent();
            username = user;

            Set_NewMethod(maintype, obj);


        }

        private void Set_NewMethod(string maintype, clsOrderDatabaseinfo obj)
        {
            item = new clsOrderDatabaseinfo();
            item = obj;

            if (maintype == "Edit")
            {
                // label2.Text = "编辑运单";
                this.Text = "编辑运单";
                ModelId = obj.Order_id;
                //1
                this.titleTextBox.Text = item.fukuandanwei;
                textBox1.Text = item.weituoren;
                textBox2.Text = item.dizhi;

                textBox5.Text = item.dianhua;

                textBox4.Text = item.shouji;

                //2
                textBox17.Text = item.daodaidi2;
                if (item.jiesuanfangshi2 == radioButton1.Text)
                    radioButton1.Checked = true;
                else if (item.jiesuanfangshi2 == radioButton2.Text)
                    radioButton2.Checked = true;
                else if (item.jiesuanfangshi2 == radioButton3.Text)
                    radioButton3.Checked = true;

                textBox15.Text = item.shouhuoren2;
                textBox14.Text = item.danwei2;
                textBox13.Text = item.dizhi2;
                textBox12.Text = item.dianhua2;
                textBox11.Text = item.shouji2;
                textBox10.Text = item.huowupinming2;
                numericUpDown1.Text = item.shijijianshu2;
                textBox7.Text = item.shijizhongliang2;
                textBox6.Text = item.tijizhongliang2;
                textBox3.Text = item.baoxianjin2;
                textBox18.Text = item.baoxianfei2;
                textBox16.Text = item.daishouzafei2;
                textBox8.Text = item.daofukuan2;
                //3
                textBox23.Text = item.zhongyaotishi3;
                textBox22.Text = item.quhuoren3;
                dateTimePicker1.Value = Convert.ToDateTime(clsCommHelp.objToDateTime(item.quhuoren_riqi3));
                textBox20.Text = item.weituoren3;


                dateTimePicker2.Value = Convert.ToDateTime(clsCommHelp.objToDateTime(item.weituoren_riqi3));
                //4
                if (item.kongyun4 == "true")
                    this.checkBox1.Checked = true;

                if (item.luyun4 == "true")
                    checkBox2.Checked = true;
                //item.fahuoshijian4 = clsCommHelp.objToDateTime1(dateTimePicker3.Text).Replace("/", ""); //Convert.ToDateTime(dateTimePicker3.Text);
                dateTimePicker3.Value = Convert.ToDateTime(clsCommHelp.objToDateTime((item.fahuoshijian4)));
                //5
                textBox29.Text = item.chicunjizhongliang5;

                textBox28.Text = item.pansongwanglu5;

                textBox26.Text = item.sijixingming5;

                textBox25.Text = item.chepaihao5;

                textBox24.Text = item.yewuyuan5;

                textBox30.Text = item.qianshouren5;

                textBox19.Text = item.yundanhao;
            }
            else
            {
                textBox24.Text = username;
            }
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            item = new clsOrderDatabaseinfo();


            #region 读取信息
            NewMethod(item);
            #endregion
            save();

        }

        private void save()
        {
            Result = new List<clsOrderDatabaseinfo>();
          
            if (ModelId != null && ModelId != "")
                item.Order_id = ModelId;

            Result.Add(item);

            clsAllnew BusinessHelp = new clsAllnew();
            if (ModelId == null || ModelId == "")
                BusinessHelp.create_OrderServer(Result);
            else
                BusinessHelp.update_OrderServer(Result);
        }

        private void NewMethod(clsOrderDatabaseinfo item)
        {

            //1
            item.fukuandanwei = this.titleTextBox.Text;
            item.weituoren = textBox1.Text;
            item.dizhi = textBox2.Text;
            item.dianhua = textBox5.Text;
            item.shouji = textBox4.Text;
            //2
            item.daodaidi2 = textBox17.Text;
            if (radioButton1.Checked == true)
                item.jiesuanfangshi2 = radioButton1.Text;
            else if (radioButton2.Checked == true)
                item.jiesuanfangshi2 = radioButton2.Text;
            else if (radioButton3.Checked == true)
                item.jiesuanfangshi2 = radioButton3.Text;

            item.shouhuoren2 = textBox15.Text;
            item.danwei2 = textBox14.Text;
            item.dizhi2 = textBox13.Text;
            item.dianhua2 = textBox12.Text;
            item.shouji2 = textBox11.Text;
            item.huowupinming2 = textBox10.Text;
            item.shijijianshu2 = numericUpDown1.Text;
            item.shijizhongliang2 = textBox7.Text;
            item.tijizhongliang2 = textBox6.Text;
            item.baoxianjin2 = textBox3.Text;
            item.baoxianfei2 = textBox18.Text;
            item.daishouzafei2 = textBox16.Text;
            item.daofukuan2 = textBox8.Text;
            //3
            item.zhongyaotishi3 = textBox23.Text;
            item.quhuoren3 = textBox22.Text;
            item.quhuoren_riqi3 = clsCommHelp.objToDateTime1(dateTimePicker1.Text).Replace("/", "");
            item.weituoren3 = textBox20.Text;
            item.weituoren_riqi3 = clsCommHelp.objToDateTime1(dateTimePicker2.Text).Replace("/", "");
            //4
            if (this.checkBox1.Checked == true)
                item.kongyun4 = "true";
            if (checkBox2.Checked == true)
                item.luyun4 = "true";
            item.fahuoshijian4 = clsCommHelp.objToDateTime1(dateTimePicker3.Text).Replace("/", ""); //Convert.ToDateTime(dateTimePicker3.Text);
            //5
            item.chicunjizhongliang5 = textBox29.Text;
            item.pansongwanglu5 = textBox28.Text;
            item.sijixingming5 = textBox26.Text;
            item.chepaihao5 = textBox25.Text;
            item.yewuyuan5 = textBox24.Text;
            item.qianshouren5 = textBox30.Text;
            item.Input_Date = DateTime.Now.ToString("yyyyMMdd");
            item.yundanhao = textBox19.Text.Trim();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            #region 读取信息
            NewMethod(item);
            clear_NewMethod();
            #endregion
            Result.Add(item);
            clsAllnew BusinessHelp = new clsAllnew();
            BusinessHelp.create_OrderServer(Result);
        }
        private void clear_NewMethod()
        {
            //1
            this.titleTextBox.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox5.Text = "";
            textBox4.Text = "";
            textBox19.Text = "";
            //2
            textBox17.Text = "";
            radioButton1.Checked = true;

            textBox15.Text = "";
            textBox14.Text = "";
            textBox13.Text = "";
            textBox12.Text = "";
            textBox11.Text = "";
            textBox10.Text = "";
            numericUpDown1.Text = "";
            textBox7.Text = "";
            textBox6.Text = "";
            textBox3.Text = "";
            textBox18.Text = "";
            textBox16.Text = "";
            textBox8.Text = "";
            //3
            textBox23.Text = "";
            textBox22.Text = "";

            this.checkBox1.Checked = false;

            checkBox2.Checked = false;


            //5
            textBox29.Text = "";
            textBox28.Text = "";
            textBox26.Text = "";
            textBox25.Text = "";
            textBox24.Text = "";
            textBox30.Text = "";
        }

        #region 窗体调整
        class AutoSizeFormClass
        {
            //(1).声明结构,只记录窗体和其控件的初始位置和大小。
            public struct controlRect
            {
                public int Left;
                public int Top;
                public int Width;
                public int Height;
            }
            //(2).声明 1个对象
            //注意这里不能使用控件列表记录 List nCtrl;，因为控件的关联性，记录的始终是当前的大小。
            //      public List oldCtrl= new List();//这里将西文的大于小于号都过滤掉了，只能改为中文的，使用中要改回西文
            public List<controlRect> oldCtrl = new List<controlRect>();
            int ctrlNo = 0;//1;
            //(3). 创建两个函数
            //(3.1)记录窗体和其控件的初始位置和大小,
            public void controllInitializeSize(Control mForm)
            {
                controlRect cR;
                cR.Left = mForm.Left; cR.Top = mForm.Top; cR.Width = mForm.Width; cR.Height = mForm.Height;
                oldCtrl.Add(cR);//第一个为"窗体本身",只加入一次即可
                AddControl(mForm);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
                //this.WindowState = (System.Windows.Forms.FormWindowState)(2);//记录完控件的初始位置和大小后，再最大化
                //0 - Normalize , 1 - Minimize,2- Maximize
            }
            private void AddControl(Control ctl)
            {
                foreach (Control c in ctl.Controls)
                {  //**放在这里，是先记录控件的子控件，后记录控件本身
                    //if (c.Controls.Count > 0)
                    //    AddControl(c);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
                    controlRect objCtrl;
                    objCtrl.Left = c.Left; objCtrl.Top = c.Top; objCtrl.Width = c.Width; objCtrl.Height = c.Height;
                    oldCtrl.Add(objCtrl);
                    //**放在这里，是先记录控件本身，后记录控件的子控件
                    if (c.Controls.Count > 0)
                        AddControl(c);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
                }
            }
            //(3.2)控件自适应大小,
            public void controlAutoSize(Control mForm)
            {
                if (ctrlNo == 0)
                { //*如果在窗体的Form1_Load中，记录控件原始的大小和位置，正常没有问题，但要加入皮肤就会出现问题，因为有些控件如dataGridView的的子控件还没有完成，个数少
                    //*要在窗体的Form1_SizeChanged中，第一次改变大小时，记录控件原始的大小和位置,这里所有控件的子控件都已经形成
                    controlRect cR;
                    //  cR.Left = mForm.Left; cR.Top = mForm.Top; cR.Width = mForm.Width; cR.Height = mForm.Height;
                    cR.Left = 0; cR.Top = 0; cR.Width = mForm.PreferredSize.Width; cR.Height = mForm.PreferredSize.Height;

                    oldCtrl.Add(cR);//第一个为"窗体本身",只加入一次即可
                    AddControl(mForm);//窗体内其余控件可能嵌套其它控件(比如panel),故单独抽出以便递归调用
                }
                float wScale = (float)mForm.Width / (float)oldCtrl[0].Width;//新旧窗体之间的比例，与最早的旧窗体
                float hScale = (float)mForm.Height / (float)oldCtrl[0].Height;//.Height;
                ctrlNo = 1;//进入=1，第0个为窗体本身,窗体内的控件,从序号1开始
                AutoScaleControl(mForm, wScale, hScale);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
            }
            private void AutoScaleControl(Control ctl, float wScale, float hScale)
            {
                int ctrLeft0, ctrTop0, ctrWidth0, ctrHeight0;
                //int ctrlNo = 1;//第1个是窗体自身的 Left,Top,Width,Height，所以窗体控件从ctrlNo=1开始
                foreach (Control c in ctl.Controls)
                { //**放在这里，是先缩放控件的子控件，后缩放控件本身
                    //if (c.Controls.Count > 0)
                    //   AutoScaleControl(c, wScale, hScale);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
                    ctrLeft0 = oldCtrl[ctrlNo].Left;
                    ctrTop0 = oldCtrl[ctrlNo].Top;
                    ctrWidth0 = oldCtrl[ctrlNo].Width;
                    ctrHeight0 = oldCtrl[ctrlNo].Height;
                    //c.Left = (int)((ctrLeft0 - wLeft0) * wScale) + wLeft1;//新旧控件之间的线性比例
                    //c.Top = (int)((ctrTop0 - wTop0) * h) + wTop1;
                    c.Left = (int)((ctrLeft0) * wScale);//新旧控件之间的线性比例。控件位置只相对于窗体，所以不能加 + wLeft1
                    c.Top = (int)((ctrTop0) * hScale);//
                    c.Width = (int)(ctrWidth0 * wScale);//只与最初的大小相关，所以不能与现在的宽度相乘 (int)(c.Width * w);
                    c.Height = (int)(ctrHeight0 * hScale);//
                    ctrlNo++;//累加序号
                    //**放在这里，是先缩放控件本身，后缩放控件的子控件
                    if (c.Controls.Count > 0)
                        AutoScaleControl(c, wScale, hScale);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用

                    if (ctl is DataGridView)
                    {
                        DataGridView dgv = ctl as DataGridView;
                        Cursor.Current = Cursors.WaitCursor;

                        int widths = 0;
                        for (int i = 0; i < dgv.Columns.Count; i++)
                        {
                            dgv.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);  // 自动调整列宽  
                            widths += dgv.Columns[i].Width;   // 计算调整列后单元列的宽度和                       
                        }
                        if (widths >= ctl.Size.Width)  // 如果调整列的宽度大于设定列宽  
                            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;  // 调整列的模式 自动  
                        else
                            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;  // 如果小于 则填充  

                        Cursor.Current = Cursors.Default;
                    }
                }


            }
        }
        #endregion

        private void frmNewOrder_Load(object sender, EventArgs e)
        {
            AutoSizeFormClass asc = new AutoSizeFormClass();
        }

        private void frmNewOrder_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            item = new clsOrderDatabaseinfo();

            clsAllnew BusinessHelp = new clsAllnew();

            NewMethod(item);
            List<clsOrderDatabaseinfo> FilterOrderResults = new List<clsOrderDatabaseinfo>();
            FilterOrderResults.Add(item);
            BusinessHelp.Run(FilterOrderResults);
            save();
        }



        private void button4_Click(object sender, EventArgs e)
        {
            clsAllnew BusinessHelp = new clsAllnew();
            item = new clsOrderDatabaseinfo();

            NewMethod(item);
            List<clsOrderDatabaseinfo> FilterOrderResults = new List<clsOrderDatabaseinfo>();
            FilterOrderResults.Add(item);
            BusinessHelp.Run(FilterOrderResults);
            BusinessHelp.printTIP(BusinessHelp, FilterOrderResults);
            save();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            clsAllnew BusinessHelp = new clsAllnew();
            item = new clsOrderDatabaseinfo();

            NewMethod(item);
            List<clsOrderDatabaseinfo> FilterOrderResults = new List<clsOrderDatabaseinfo>();
            FilterOrderResults.Add(item);

           BusinessHelp. printTIP(BusinessHelp, FilterOrderResults);

           save();
        }
    }
}
