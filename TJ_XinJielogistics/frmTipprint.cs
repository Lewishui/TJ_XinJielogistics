using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TJ.DB;

namespace TJ_XinJielogistics
{
    public partial class frmTipprint : Form
    {
        List<clsTipsinfo> Result;
        public log4net.ILog ProcessLogger;
        public log4net.ILog ExceptionLogger;
        public frmTipprint(List<clsTipsinfo> Result1)
        {
            InitializeComponent();
            InitializeReportEvent();

            Result = new List<clsTipsinfo>();
            Result = Result1;
            InitialSystemInfo();
            ProcessLogger.Fatal("print Initial" + DateTime.Now.ToString());

        }
        private void InitialSystemInfo()
        {
            #region 初始化配置
            ProcessLogger = log4net.LogManager.GetLogger("ProcessLogger");
            ExceptionLogger = log4net.LogManager.GetLogger("SystemExceptionLogger");

            #endregion
        }
        private void frmprint_Load(object sender, EventArgs e)
        {

            //this.reportViewer1.RefreshReport();


            try
            {

                reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Report2.rdlc";
               // reportViewer1.LocalReport.ReportPath = @"C:\mysteap\work_office\ProjectOut\天津信捷物流\TJ_XinJielogistics\TJ_XinJielogistics\Report2.rdlc";

                ProcessLogger.Fatal("109723 load file" + DateTime.Now.ToString());
                //指定数据集,数据集名称后为表,不是DataSet类型的数据集
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", Result));
                ProcessLogger.Fatal("109724 Add file" + DateTime.Now.ToString());
                //显示报表
                this.reportViewer1.RefreshReport();
                ProcessLogger.Fatal("109724 display file" + DateTime.Now.ToString());
                //240×340   一般文件袋
                //2835  X  1713



            }
            catch (Exception ex)
            {
                MessageBox.Show("异常" + ex);
                return;

                throw;
            }
        }
        private void InitializeReportEvent()
        {
            //this.reportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
            this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);

            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            this.reportViewer1.ZoomPercent = 100;


            //this.reportViewer1.Width = 10;
            //this.reportViewer1.Height = 10;
            PageSettings pageset = new PageSettings();
            pageset.Landscape = true;
            //var pageSettings = this.reportViewer1.GetPageSettings();
            pageset.PaperSize = new PaperSize()
            {
                //Width = 210,
                //Height = 297
                //
                //Width = 100,
                //Height = 100

                Width = 380,
                Height = 380
                //Width = 340,
                //Height = 240
            };
             pageset.Margins = new Margins() { Left = 10, Top = 10, Bottom = 10, Right = 10 };
            reportViewer1.SetPageSettings(pageset);

        }

    }
}
