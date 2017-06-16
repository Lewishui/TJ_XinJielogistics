using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using TJ.DB;

namespace TJ_XinJielogistics
{
    public partial class frmprint : Form
    {
        List<clsOrderDatabaseinfo> Result;
        public log4net.ILog ProcessLogger;
        public log4net.ILog ExceptionLogger;

        #region print

        //private string _reportname = "";
        //private DataSet _datasource = null;
        private string _autoprint = "";
        private IList<Stream> m_streams;
        private int m_currentPageIndex;


        #endregion
        public frmprint(List<clsOrderDatabaseinfo> Result1)
        {
            InitializeComponent();
            InitializeReportEvent();

            Result = new List<clsOrderDatabaseinfo>();
            Result = Result1;
            InitialSystemInfo();
            ProcessLogger.Fatal("print Initial" + DateTime.Now.ToString());
            //   reportViewer1.PrintDialog();
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

                reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Report1.rdlc";
              //  reportViewer1.LocalReport.ReportPath = @"C:\mysteap\work_office\ProjectOut\天津信捷物流\TJ_XinJielogistics\TJ_XinJielogistics\Report1.rdlc";

                ProcessLogger.Fatal("109723 load file" + DateTime.Now.ToString());
                //指定数据集,数据集名称后为表,不是DataSet类型的数据集
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", Result));
                ProcessLogger.Fatal("109724 Add file" + DateTime.Now.ToString());
                //显示报表
                this.reportViewer1.RefreshReport();
                ProcessLogger.Fatal("109724 display file" + DateTime.Now.ToString());
                //240×340   一般文件袋
                //2835  X  1713

              //  this.reportViewer1.RenderingComplete += new RenderingCompleteEventHandler(PrintLabelDirectly);

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


            //this.reportViewer1.Width = 29;
            //this.reportViewer1.Height = 21;
            PageSettings pageset = new PageSettings();
            pageset.Landscape = true;
            //var pageSettings = this.reportViewer1.GetPageSettings();
            pageset.PaperSize = new PaperSize()
            {
                //Width = 210,
                //Height = 297
                //
                Width = 827,
                Height = 1169


                //Width = 340,
                //Height = 240
            };
            pageset.Margins = new Margins() { Left = 10, Top = 10, Bottom = 10, Right = 10 };
            reportViewer1.SetPageSettings(pageset);

        }
        public void PrintLabelDirectly(object sender, RenderingCompleteEventArgs e)
        {
            try
            {

                reportViewer1.PrintDialog();
                reportViewer1.Clear();
                reportViewer1.LocalReport.ReleaseSandboxAppDomain();
                this.Close();
            }
            catch (Exception ex)
            {
            }
        }

        #region auto

        public void PPrint()
        {
            try
            {
                LocalReport report = this.reportViewer1.LocalReport;
                Export(report);
                m_currentPageIndex = 0;
                NBPrint();
                if (m_streams != null)
                {
                    foreach (Stream stream in m_streams)
                        stream.Close();
                    m_streams = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("在打印过程中出现异常! " + ex.Message.ToString());
            }
        }
        private void Export(LocalReport report)
        {
            //7.5in 3.66in 0 0 0 0 当前设置为A4纵向
            string deviceInfo =
              "<DeviceInfo>" +
              "  <OutputFormat>EMF</OutputFormat>" +
                //"  <PageWidth>20.7cm</PageWidth>" +
                //"  <PageHeight>28cm</PageHeight>" +
              "  <PageWidth>21cm</PageWidth>" +
              "  <PageHeight>12.7cm</PageHeight>" +
              "  <MarginTop>0in</MarginTop>" +
              "  <MarginLeft>0in</MarginLeft>" +
              "  <MarginRight>0in</MarginRight>" +
              "  <MarginBottom>0in</MarginBottom>" +
              "</DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            //report.Render("Image", deviceInfo, CreateStream, out warnings);//PDF
            report.Render("PDF", deviceInfo, CreateStream, out warnings);

            foreach (Stream stream in m_streams)
            {
                stream.Position = 0;
            }
        }
        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            string filenameext = DateTime.Now.Year.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            Stream stream = new FileStream(name + "." + fileNameExtension, FileMode.Create);
            m_streams.Add(stream);
            return stream;
        }

        private void NBPrint()
        {
            if (m_streams == null || m_streams.Count == 0)
                return;
            PrintDocument printDoc = new PrintDocument();

            if (!printDoc.PrinterSettings.IsValid)
            {
                string msg = String.Format("Can't find printer \"{0}\".", "默认打印机!");
                MessageBox.Show(msg, "找不到默认打印机");
                return;
            }
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            printDoc.Print();
            this.Close();
        }
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }
        #endregion

    }
}
