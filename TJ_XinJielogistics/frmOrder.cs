using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using TJ.Buiness;
using TJ.Common;
using TJ.DB;

namespace TJ_XinJielogistics
{
    public partial class frmOrder : Form
    {
        List<clsOrderDatabaseinfo> FilterOrderResults;
        public event EventHandler CommandRequestEvent;
        List<clsOrderDatabaseinfo> OrderResults;
        private SortableBindingList<clsOrderDatabaseinfo> sortablePendingOrderList;
        int RowRemark = 0;
        int cloumn = 0;
        string Useramin;
        string username;
        #region print
        private List<Stream> m_streams;
        private int m_currentPageIndex;
        List<clsTipsinfo> FilterTIPResults;
        #endregion
        public frmOrder(string user, string isadmin)
        {
            InitializeComponent();
            Useramin = isadmin;
            username =user ;
        }
        public void BeginActive()
        {
            InitializeDataSource();
            //pager1.Bind();
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            var form = new frmNewOrder("create", null,username);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {

                InitializeDataSource();
                //InitializeDataGridView();
            }

        }

        public class SortableBindingList<T> : BindingList<T>
        {
            private bool isSortedCore = true;
            private ListSortDirection sortDirectionCore = ListSortDirection.Ascending;
            private PropertyDescriptor sortPropertyCore = null;
            private string defaultSortItem;

            public SortableBindingList() : base() { }

            public SortableBindingList(IList<T> list) : base(list) { }

            protected override bool SupportsSortingCore
            {
                get { return true; }
            }

            protected override bool SupportsSearchingCore
            {
                get { return true; }
            }

            protected override bool IsSortedCore
            {
                get { return isSortedCore; }
            }

            protected override ListSortDirection SortDirectionCore
            {
                get { return sortDirectionCore; }
            }

            protected override PropertyDescriptor SortPropertyCore
            {
                get { return sortPropertyCore; }
            }

            protected override int FindCore(PropertyDescriptor prop, object key)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (Equals(prop.GetValue(this[i]), key)) return i;
                }
                return -1;
            }

            protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
            {
                isSortedCore = true;
                sortPropertyCore = prop;
                sortDirectionCore = direction;
                Sort();
            }

            protected override void RemoveSortCore()
            {
                if (isSortedCore)
                {
                    isSortedCore = false;
                    sortPropertyCore = null;
                    sortDirectionCore = ListSortDirection.Ascending;
                    Sort();
                }
            }

            public string DefaultSortItem
            {
                get { return defaultSortItem; }
                set
                {
                    if (defaultSortItem != value)
                    {
                        defaultSortItem = value;
                        Sort();
                    }
                }
            }

            private void Sort()
            {
                List<T> list = (this.Items as List<T>);
                list.Sort(CompareCore);
                ResetBindings();
            }

            private int CompareCore(T o1, T o2)
            {
                int ret = 0;
                if (SortPropertyCore != null)
                {
                    ret = CompareValue(SortPropertyCore.GetValue(o1), SortPropertyCore.GetValue(o2), SortPropertyCore.PropertyType);
                }
                if (ret == 0 && DefaultSortItem != null)
                {
                    PropertyInfo property = typeof(T).GetProperty(DefaultSortItem, BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.IgnoreCase, null, null, new Type[0], null);
                    if (property != null)
                    {
                        ret = CompareValue(property.GetValue(o1, null), property.GetValue(o2, null), property.PropertyType);
                    }
                }
                if (SortDirectionCore == ListSortDirection.Descending) ret = -ret;
                return ret;
            }

            private static int CompareValue(object o1, object o2, Type type)
            {
                if (o1 == null) return o2 == null ? 0 : -1;
                else if (o2 == null) return 1;
                else if (type.IsPrimitive || type.IsEnum) return Convert.ToDouble(o1).CompareTo(Convert.ToDouble(o2));
                else if (type == typeof(DateTime)) return Convert.ToDateTime(o1).CompareTo(o2);
                else return String.Compare(o1.ToString().Trim(), o2.ToString().Trim());
            }
        }

        private void btfind_Click(object sender, EventArgs e)
        {

            InitializeDataSource();

        }

        private void InitializeDataSource()
        {

            clsAllnew BusinessHelp = new clsAllnew();
            string start_time = clsCommHelp.objToDateTime1(dateTimePicker1.Text);
            string end_time = clsCommHelp.objToDateTime1(dateTimePicker2.Text);

            OrderResults = BusinessHelp.findOrder_Server(this.keywordTextBox.Text, start_time, end_time);

            //if (Result)
            this.dataGridView.AutoGenerateColumns = false;
            sortablePendingOrderList = new SortableBindingList<clsOrderDatabaseinfo>(OrderResults);
            this.bindingSource1.DataSource = sortablePendingOrderList;
            this.dataGridView.DataSource = this.bindingSource1;
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewColumn column = dataGridView.Columns[e.ColumnIndex];

            if (column == editColumn1)
            {
                var row = dataGridView.Rows[e.RowIndex];

                var model = row.DataBoundItem as clsOrderDatabaseinfo;

                var form = new frmNewOrder("Edit", model,username);
                if (form.ShowDialog() == DialogResult.Yes)
                {
                    BeginActive();
                }
            }
            else if (column == deleteColumn1)
            {
                if (Useramin == "true")
                {
                    var row = dataGridView.Rows[e.RowIndex];
                    var model = row.DataBoundItem as clsOrderDatabaseinfo;
                    string msg = string.Format("确定删除餐厅<{0}>？", model.fukuandanwei);

                    if (MessageHelper.DeleteConfirm(msg))
                    {
                        clsAllnew BusinessHelp = new clsAllnew();

                        BusinessHelp.delete_OrderServer(model);
                        BeginActive();
                    }
                }
                else
                {
                    MessageBox.Show("亲，非管理员不可删除～", "删除", MessageBoxButtons.OK, MessageBoxIcon.Error);


                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView.RowCount == 0 || dataGridView.RowCount < RowRemark)
                return;

            FilterOrderResults = new List<clsOrderDatabaseinfo>();

            var row = dataGridView.Rows[RowRemark];
            var model = row.DataBoundItem as clsOrderDatabaseinfo;
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                if ((bool)dataGridView.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    FilterOrderResults = new List<clsOrderDatabaseinfo>();

                    row = dataGridView.Rows[i];
                    model = row.DataBoundItem as clsOrderDatabaseinfo;
                    FilterOrderResults.Add(model);

                    //FilterOrderResults.Add(model);
                    if (this.noReplaceRadioButton.Checked == true)
                    {
                        var form = new frmprint(FilterOrderResults);
                        if (form.ShowDialog() == DialogResult.OK)
                        {

                        }
                    }
                    else if (this.replaceRadioButton.Checked == true)
                    {
                        //LocalReport report = new LocalReport();
                        //report.ReportPath = @"C:\mysteap\work_office\ProjectOut\天津信捷物流\TJ_XinJielogistics\TJ_XinJielogistics\Report1.rdlc";
                        //report.EnableExternalImages = true;
                        //var qtyTable = new DataTable();
                        //qtyTable.TableName = "order";
                        //qtyTable.Columns.Add("卖场", System.Type.GetType("System.String"));//0
                        //ReportDataSource source = new ReportDataSource("order", qtyTable);
                        //report.DataSources.Add(source);
                        //report.Refresh();
                        Run();
                        //var form = new frmprint(FilterOrderResults);
                        //form.PPrint();

                    }
                    if (this.checkBox1.Checked == true)
                    {
                        FilterTIPResults = new List<clsTipsinfo>();
                        foreach (clsOrderDatabaseinfo temp in FilterOrderResults)
                        {
                            clsTipsinfo item = new clsTipsinfo();

                            item.yuandanhao = temp.yundanhao;
                            item.shifazhan = temp.dizhi;
                            item.mudizhan = temp.daodaidi2;
                            item.jianshu = temp.shijijianshu2;
                            item.shouhuoren = temp.quhuoren3;
                            item.dianhua = temp.dianhua2;
                            item.Input_Date = DateTime.Now.ToString("yyyyMMdd");
                            FilterTIPResults.Add(item);

                        }
                        Run2();


                    }
                }
            }
            MessageBox.Show("打印完成，请核对打印信息，谢谢", "打印", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return;
        }
        private void Run()
        {
            LocalReport report = new LocalReport();
            //report.ReportPath = @"C:\mysteap\work_office\ProjectOut\天津信捷物流\TJ_XinJielogistics\TJ_XinJielogistics\Report1.rdlc";
            report.ReportPath = Application.StartupPath + "\\Report1.rdlc";

            //report.DataSources.Add(
            //   new ReportDataSource("Sales", FilterOrderResults));
            report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", FilterOrderResults));

            Export(report);
            m_currentPageIndex = 0;
            Print();
        }
        private DataTable LoadSalesData()
        {
            // Create a new DataSet and read sales data file 
            //    data.xml into the first DataTable.
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(@"..\..\data.xml");

            return dataSet.Tables[0];
        }
        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }
        private void Export(LocalReport report)
        {
            //A4    21*29.7厘米（210mm×297mm）
            // A5的尺寸:148毫米*210毫米
            //string deviceInfo =
            //  "<DeviceInfo>" +
            //  "  <OutputFormat>EMF</OutputFormat>" +
            //  "  <PageWidth>8.5in</PageWidth>" +
            //  "  <PageHeight>11in</PageHeight>" +
            //  "  <MarginTop>0.25in</MarginTop>" +
            //  "  <MarginLeft>0.25in</MarginLeft>" +
            //  "  <MarginRight>0.25in</MarginRight>" +
            //  "  <MarginBottom>0.25in</MarginBottom>" +
            //  "</DeviceInfo>";

            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>EMF</OutputFormat>" +
            "  <PageWidth>18.9in</PageWidth>" +
            "  <PageHeight>11.42in</PageHeight>" +
            "  <MarginTop>0.25in</MarginTop>" +
            "  <MarginLeft>0.25in</MarginLeft>" +
            "  <MarginRight>0.25in</MarginRight>" +
            "  <MarginBottom>0.25in</MarginBottom>" +
            "</DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RowRemark = e.RowIndex;
            cloumn = e.ColumnIndex;
        }
        private Stream CreateStream(string name, string fileNameExtension,

    Encoding encoding, string mimeType, bool willSeek)
        {

            //如果需要将报表输出的数据保存为文件，请使用FileStream对象。

            Stream stream = new MemoryStream();

            m_streams.Add(stream);

            return stream;

        }

        private void Print()
        {

            m_currentPageIndex = 0;



            if (m_streams == null || m_streams.Count == 0)

                return;

            //声明PrintDocument对象用于数据的打印

            PrintDocument printDoc = new PrintDocument();

            //指定需要使用的打印机的名称，使用空字符串""来指定默认打印机
            string defaultPrinterName = printDoc.PrinterSettings.PrinterName;
            printDoc.PrinterSettings.PrinterName = defaultPrinterName;

            //判断指定的打印机是否可用

            if (!printDoc.PrinterSettings.IsValid)
            {

                MessageBox.Show("Can't find printer");

                return;

            }

            //声明PrintDocument对象的PrintPage事件，具体的打印操作需要在这个事件中处理。

            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);

            //执行打印操作，Print方法将触发PrintPage事件。
            printDoc.DefaultPageSettings.Landscape = true;
            printDoc.Print();

        }
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);
            //ev.PageSettings.Landscape = true;
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        //private void PrintPage(object sender, PrintPageEventArgs ev)
        //{

        //    //Metafile对象用来保存EMF或WMF格式的图形，

        //    //我们在前面将报表的内容输出为EMF图形格式的数据流。

        //    m_streams[m_currentPageIndex].Position = 0;

        //    Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

        //    //指定是否横向打印

        //    ev.PageSettings.Landscape = true;

        //    //这里的Graphics对象实际指向了打印机

        //    ev.Graphics.DrawImage(pageImage, 0, 0);

        //    m_streams[m_currentPageIndex].Close();

        //    m_currentPageIndex++;

        //    //设置是否需要继续打印

        //    ev.HasMorePages = (m_currentPageIndex < m_streams.Count);

        //}
        #region 标签打印
        private void Run2()
        {
            LocalReport report = new LocalReport();
            // report.ReportPath = @"C:\mysteap\work_office\ProjectOut\天津信捷物流\TJ_XinJielogistics\TJ_XinJielogistics\Report1.rdlc";
            report.ReportPath = Application.StartupPath + "\\Report2.rdlc";
            //report.DataSources.Add(
            //   new ReportDataSource("Sales", FilterOrderResults));
            report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", FilterOrderResults));

            Export2(report);
            m_currentPageIndex = 0;
            Print();
        }
        private void Export2(LocalReport report)
        {
            //A4    21*29.7厘米（210mm×297mm）
            // A5的尺寸:148毫米*210毫米
            //string deviceInfo =
            //  "<DeviceInfo>" +
            //  "  <OutputFormat>EMF</OutputFormat>" +
            //  "  <PageWidth>8.5in</PageWidth>" +
            //  "  <PageHeight>11in</PageHeight>" +
            //  "  <MarginTop>0.25in</MarginTop>" +
            //  "  <MarginLeft>0.25in</MarginLeft>" +
            //  "  <MarginRight>0.25in</MarginRight>" +
            //  "  <MarginBottom>0.25in</MarginBottom>" +
            //  "</DeviceInfo>";

            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>EMF</OutputFormat>" +
            "  <PageWidth>18.9in</PageWidth>" +
            "  <PageHeight>11.42in</PageHeight>" +
            "  <MarginTop>0.25in</MarginTop>" +
            "  <MarginLeft>0.25in</MarginLeft>" +
            "  <MarginRight>0.25in</MarginRight>" +
            "  <MarginBottom>0.25in</MarginBottom>" +
            "</DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        #endregion

        private void btdown_Click(object sender, EventArgs e)
        {
            {
                if (this.dataGridView.Rows.Count == 0)
                {
                    MessageBox.Show("Sorry , No Data Output !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = ".csv";
                saveFileDialog.Filter = "csv|*.csv";
                string strFileName = "System  Info" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                saveFileDialog.FileName = strFileName;
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    strFileName = saveFileDialog.FileName.ToString();
                }
                else
                {
                    return;
                }
                FileStream fa = new FileStream(strFileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fa, Encoding.Unicode);
                string delimiter = "\t";
                string strHeader = "";
                for (int i = 0; i < this.dataGridView.Columns.Count; i++)
                {
                    strHeader += this.dataGridView.Columns[i].HeaderText + delimiter;
                }
                sw.WriteLine(strHeader);

                //output rows data
                for (int j = 0; j < this.dataGridView.Rows.Count; j++)
                {
                    string strRowValue = "";

                    for (int k = 0; k < this.dataGridView.Columns.Count; k++)
                    {
                        if (this.dataGridView.Rows[j].Cells[k].Value != null)
                        {
                            strRowValue += this.dataGridView.Rows[j].Cells[k].Value.ToString().Replace("\r\n", " ").Replace("\n", "") + delimiter;
                            if (this.dataGridView.Rows[j].Cells[k].Value.ToString() == "LIP201507-35")
                            {

                            }

                        }
                        else
                        {
                            strRowValue += this.dataGridView.Rows[j].Cells[k].Value + delimiter;
                        }
                    }
                    sw.WriteLine(strRowValue);
                }
                sw.Close();
                fa.Close();
                MessageBox.Show("Dear User, Down File  Successful ！", "System", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

    }
}
