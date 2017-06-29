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
            username = user;
        }
        public void BeginActive()
        {
            InitializeDataSource();
            //pager1.Bind();
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            var form = new frmNewOrder("create", null, username);
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

                var form = new frmNewOrder("Edit", model, username);
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
            {
                MessageBox.Show("请选择要打印的单子，谢谢", "打印", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            else
            {
                int ishaveprint = 0;

                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    if ((bool)dataGridView.Rows[i].Cells[0].EditedFormattedValue == true)
                    {
                        ishaveprint++;

                    }
                   
                }
                if (ishaveprint == 0)
                {
                    MessageBox.Show("请选择要打印的单子，谢谢", "打印", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

            }
            clsAllnew BusinessHelp = new clsAllnew();

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
                    if (this.noReplaceRadioButton.Checked == true)
                    {
                        var form = new frmprint(FilterOrderResults);
                        if (form.ShowDialog() == DialogResult.OK)
                        {

                        }
                    }
                    else if (this.replaceRadioButton.Checked == true)
                    {

                        BusinessHelp.Run(FilterOrderResults);


                    }
                    if (this.checkBox1.Checked == true)
                    {
                        BusinessHelp.printTIP(BusinessHelp, FilterOrderResults);



                    }
                }
            }
            if (this.replaceRadioButton.Checked == true)
                MessageBox.Show("打印完成，请核对打印信息，谢谢", "打印", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return;
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

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RowRemark = e.RowIndex;
            cloumn = e.ColumnIndex;
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
