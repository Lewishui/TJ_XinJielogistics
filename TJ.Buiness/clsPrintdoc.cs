using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace TJ.Buiness
{
   

    public class clsPrintdoc
    {
        string PaperName = "";//纸张名字
        string PageWidth = "";//纸张宽度
        string PageHeight = "";//纸张高度
        string MarginTop = "";//上边距
        string MarginLeft = "";//左边距
        string MarginRight = "";//右边距
        string MarginBottom = "";//下边距
        string PagePrinter = "";//打印机

        private bool hxdy = false;
        int m_PageIndex = 0;//需要打印的当前页 2012-08-14

     
        /// <summary>
        /// 获取一个值，指示是否为横向打印，默认为纵向
        /// </summary>
        public bool Hxdy
        {
            set { hxdy = value; }
        }

        /// <summary>
        /// 用来记录当前打印到第几页了
        /// </summary>
        private int m_currentPageIndex;

        /// <summary>
        /// //声明一个Stream对象的列表用来保存报表的输出数据  
        ///LocalReport对象的Render方法会将报表按页输出为多个Stream对象。
        /// </summary>
        private IList<Stream> m_streams;//文件流


        ///用来提供Stream对象的函数，用于LocalReport对象的Render方法的第三个参数
        /// 提供 Stream 对象以进行呈现的 CreateStreamCallback 委托指向的方法  
        /// 这里为将报表的每一个页面作为一个EMF图片存放，通常用于报表呈现  
        /// </summary>  
        /// <param name="name">流的名称</param>  
        /// <param name="fileNameExtension">创建文件流时要使用的文件扩展名</param>  
        /// <param name="encoding">指定流的字符编码的 Encoding 枚举器值。如果流不包含字符，则此值可能为 null。</param>  
        /// <param name="mimeType">一个包含流的 MIME 类型的 string</param>  
        /// <param name="willSeek">指示流是否需要支持查找的 Boolean 值。如果值为 false，则流将为只前推，并将按其创建顺序发送到块区中的客户端。如果值为 true，则流可以任何顺序写入。</param>  
        /// <returns>ReportViewer 控件可以写入数据的 Stream 对象</returns>  
        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            //如果需要将报表输出的数据保存为文件，请使用FileStream对象。
            Stream stream = new FileStream(name +"1" + "." + fileNameExtension, FileMode.Create);
            m_streams.Add(stream);
            return stream;
        }


        /// <summary>
        /// 导出报表的每一个页面到一个EMF文件   
        /// </summary>
        /// <param name="report">ReportViewer.LocalReport</param>
        /// <param name="PageName">页面设置</param>
        private void Export(LocalReport report, string PageName)
        {
            string deviceInfo = "";

        
            if (PageName == "")//单据名称
            {
                deviceInfo =
                   "<DeviceInfo>" +
                   "  <OutputFormat>EMF</OutputFormat>" +
                   "  <PageWidth>21cm</PageWidth>" +
                   "  <PageHeight>29.7cm</PageHeight>" +
                   "  <MarginTop>1cm</MarginTop>" +
                   "  <MarginLeft>1cm</MarginLeft>" +
                   "  <MarginRight>1cm</MarginRight>" +
                   "  <MarginBottom>1cm</MarginBottom>" +
                   "</DeviceInfo>";

            }
            else
            {
                deviceInfo =
                    "<DeviceInfo>" +
                    "  <OutputFormat>EMF</OutputFormat>" +
                    "  <PageWidth>" + PageWidth + "</PageWidth>" +
                    "  <PageHeight>" + PageHeight + "</PageHeight>" +
                    "  <MarginTop>" + MarginTop + "</MarginTop>" +
                    "  <MarginLeft>" + MarginLeft + "</MarginLeft>" +
                    "  <MarginRight>" + MarginRight + "</MarginRight>" +
                    "  <MarginBottom>" + MarginBottom + "</MarginBottom>" +
                    "</DeviceInfo>";

            }
            Warning[] warnings;
            m_streams = new List<Stream>();
            try
            {
                //report.DisplayName = PageName;//
                //将报表的内容按照deviceInfo指定的格式输出到CreateStream函数提供的Stream中。
                report.Render("Image", deviceInfo, CreateStream, out warnings);
                report.Dispose();
            }
            catch (Exception ex)
            {
                Exception innerEx = ex.InnerException;//取内异常。因为内异常的信息才有用，才能排除问题。   
                while (innerEx != null)
                {
                    MessageBox.Show(innerEx.Message);
                    innerEx = innerEx.InnerException;
                }
            }

            foreach (Stream stream in m_streams)
            {
                stream.Position = 0;
            }
            

        }

        /// <summary>
        /// 缩略图形的调用函数
        /// </summary>
        /// <returns></returns>
        public bool DelegateGetThumbnailImageAbort()
        {
            return false;
        }

        /// <summary>   
        /// 当前页打印的输出   
        /// </summary>   
        /// <param name="sender"></param>   
        /// <param name="ev"></param>   
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {


            if (m_PageIndex == 0)//没有指定页码打印所有的
            {
                //把数据流转化把EMF矢量图片 
                Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

               //设置高质量插值法
                ev.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                //设置高质量,低速度呈现平滑程度

                ev.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //在指定位置并且按指定大小绘制原图片的指定部分 ev.Graphics.DrawImage(原始图片, new System.Drawing.Rectangle(0, 0, 缩略图片的宽, 缩略图片的高), new System.Drawing.Rectangle(0, 0, 原始图片的宽, 原始图片的高), System.Drawing.GraphicsUnit.Pixel);
                ev.Graphics.DrawImage(pageImage, new System.Drawing.Rectangle(0, 0, ev.PageBounds.Width, ev.PageBounds.Height), new System.Drawing.Rectangle(0, 0, pageImage.Width, pageImage.Height), System.Drawing.GraphicsUnit.Pixel);
               
                m_currentPageIndex++;
                ev.HasMorePages = (m_currentPageIndex < m_streams.Count);

            }
            else//指定页面后只打印指定页码
            {
                if (m_PageIndex > m_streams.Count)
                {
                    ev.HasMorePages = false;//当终止页数大于总页数
                    ev.Cancel = true;
                    MessageBox.Show("自定义页面填写有误,终止页数不能大于总页数，请检查！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                while (m_currentPageIndex + 1 < m_PageIndex)
                {
                    m_currentPageIndex++;
                }
                Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);
                ev.Graphics.DrawImageUnscaledAndClipped(pageImage, new System.Drawing.Rectangle(0, 0, ev.PageBounds.Width, ev.PageBounds.Height));
                ev.HasMorePages = false;
                pageImage.Dispose();
            }
        }

        /// <summary>
        /// //用来记录当前打印到第几页了 
        /// </summary>
        /// <param name="DocumentName"></param>
        /// <param name="DispalyPageSetupDialog"></param>
        private void Print(string DocumentName, bool DispalyPageSetupDialog)//"体检指引单打印", false
        {
            if (m_streams == null || m_streams.Count == 0)
            {
                return;
            }
            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = DocumentName;

            if (!PrinterExists())//检查是否有打印机
            {
                return;
            }
            if (PagePrinter != "")//打印机
            {
                bool IsExists = false;//设置打印机名称是否存在
                for (int x = 0; x < PrinterSettings.InstalledPrinters.Count; x++)
                {
                    if (PrinterSettings.InstalledPrinters[x].ToString() == PagePrinter)
                    {
                        printDoc.PrinterSettings.PrinterName = PagePrinter;//获得打印机的名称
                        IsExists = true;
                        break;
                    }
                }
                if (!IsExists)
                {
                    MessageBox.Show("打印机：" + "【" + PagePrinter + "】不存在，请联系管理员！", "提示",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (PaperName != "")//纸张名称：A4,A3
            {
                //if (!Printer.FormInPrinter(printDoc.PrinterSettings.PrinterName, PaperName))//纸张不存在自己创建
                //{
                //    decimal width = Convert.ToDecimal(PageWidth.Replace("cm", "")) * 10;
                //    decimal height = Convert.ToDecimal(PageHeight.Replace("cm", "")) * 10;
                //    Printer.AddCustomPaperSize(printDoc.PrinterSettings.PrinterName, PaperName, width, height);
                //}

                int index = 0;//纸张的索引
                bool IsExists = false;//打印机纸张是否存在该纸张名称
                for (int j = 0; j < printDoc.PrinterSettings.PaperSizes.Count; j++)
                {
                    if (printDoc.PrinterSettings.PaperSizes[j].PaperName == PaperName)
                    {
                       
                        index = j;
                        IsExists = true;
                        break;
                    }
                }
                if (IsExists)
                {
                    printDoc.DefaultPageSettings.PaperSize = printDoc.PrinterSettings.PaperSizes[index];
                }
                else
                {
                    MessageBox.Show("【" + PaperName + "】自定义纸张不存在，请联系管理员！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            //事件委托
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);

            if (DispalyPageSetupDialog)
            {
                PageSetupDialog pageDialog = new PageSetupDialog();
                pageDialog.Document = printDoc;

                if (pageDialog.ShowDialog() == DialogResult.OK)
                {
                    printDoc.Print();
                }
            }
            else
            {

                if (hxdy)//横向打印
                {
                    printDoc.DefaultPageSettings.Landscape = true;
                }
                printDoc.Print();
           }
        }

        /// <summary>
        /// 打印rdlc文件
        /// </summary>
        /// <param name="report">本地报表</param>
        /// <param name="DocumentName">打印文件名称</param>
        /// <param name="DispalyPageSetupDialog">是否显示打印设置对话框</param>
        /// <param name="PageName">打印单据的名字</param>
        public void Run(LocalReport report, string DocumentName, bool DispalyPageSetupDialog, string PageName)//report, "体检指引单打印", false, "A4"
        {
            // pw.StartThread();
            if (PageName != "")//取自定义纸张大小
            {
                xtBiz xtbiz = new xtBiz();
                DataTable dt = null;// xtbiz.Get_Xt_ggdy(PageName);//获得到设置的纸张的大小.
                if (dt.Rows.Count == 1)
                {
                    PaperName = dt.Rows[0]["PaperName"].ToString();
                    PageWidth = dt.Rows[0]["PageWidth"].ToString();
                    PageHeight = dt.Rows[0]["PageHeight"].ToString();
                    MarginTop = dt.Rows[0]["MarginTop"].ToString();
                    MarginLeft = dt.Rows[0]["MarginLeft"].ToString();
                    MarginRight = dt.Rows[0]["MarginRight"].ToString();
                    MarginBottom = dt.Rows[0]["MarginBottom"].ToString();
                    PagePrinter = dt.Rows[0]["PagePrinter"].ToString();
                    if (PageWidth == "") PageWidth = "0cm";
                    if (PageHeight == "") PageHeight = "0cm";
                    if (MarginTop == "") MarginTop = "0cm";
                    if (MarginLeft == "") MarginLeft = "0cm";
                    if (MarginRight == "") MarginRight = "0cm";
                    if (MarginBottom == "") MarginBottom = "0cm";
                }
                else
                {
                    MessageBox.Show("该单据纸张配置没有设置，请联系管理员！", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            Export(report, PageName);//导出报表的每一个页面到一个EMF文件   
            m_currentPageIndex = 0;
            Print(DocumentName, DispalyPageSetupDialog);// "体检指引单打印", false
         //释放资源
           Dispose();//一定释放资源
        }


        /// <summary>
        /// 打印rdlc文件
        /// </summary>
        /// <param name="report">本地报表</param>
        /// <param name="DocumentName">打印文件名称</param>
        /// <param name="DispalyPageSetupDialog">是否显示打印设置对话框</param>
        /// <param name="PageName">打印单据的名字</param>
        /// <param name="PageFw">打印单据的范围</param>
        public void Run(LocalReport report, string DocumentName, bool DispalyPageSetupDialog, string PageName, string PageFw)
        {
            //pw.StartThread();
            if (PageName != "")//取自定义纸张大小
            {
                xtBiz xtbiz = new xtBiz();
                DataTable dt = null;// xtbiz.Get_Xt_ggdy(PageName);
                if (dt.Rows.Count == 1)
                {
                    PaperName = dt.Rows[0]["PaperName"].ToString();
                    PageWidth = dt.Rows[0]["PageWidth"].ToString();
                    PageHeight = dt.Rows[0]["PageHeight"].ToString();
                    MarginTop = dt.Rows[0]["MarginTop"].ToString();
                    MarginLeft = dt.Rows[0]["MarginLeft"].ToString();
                    MarginRight = dt.Rows[0]["MarginRight"].ToString();
                    MarginBottom = dt.Rows[0]["MarginBottom"].ToString();
                    PagePrinter = dt.Rows[0]["PagePrinter"].ToString();
                    if (PageWidth == "") PageWidth = "0cm";
                    if (PageHeight == "") PageHeight = "0cm";
                    if (MarginTop == "") MarginTop = "0cm";
                    if (MarginLeft == "") MarginLeft = "0cm";
                    if (MarginRight == "") MarginRight = "0cm";
                    if (MarginBottom == "") MarginBottom = "0cm";
                }
                else
                {
                    MessageBox.Show("该单据纸张配置没有设置，请联系管理员！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

　　　　　　　//导出报表的每一个页面到一个EMF文件   
            Export(report, PageName);
            if (PageFw == "")
            {
                m_currentPageIndex = 0;
                Print(DocumentName, DispalyPageSetupDialog);
               
            }
            else
            {
                string[] str_fw = PageFw.Split('-');
                int begin = 0;
                int end = 0;
                try
                {
                    if (str_fw.Length == 1)
                    {
                        begin = Convert.ToInt32(str_fw[0]);
                        end = Convert.ToInt32(str_fw[0]);
                    }
                    else
                    {
                        begin = Convert.ToInt32(str_fw[0]);
                        end = Convert.ToInt32(str_fw[1]);
                    }
                    if (begin > end)
                    {
                        MessageBox.Show("该单据自定义打印页数填写有误，请检查！", "提示",
                           MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("该单据自定义打印页数填写有误，请检查！", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                for (int i = begin; i <= end; i++)
                {
                    m_PageIndex = i;
                    m_currentPageIndex = 0;
                    Print(DocumentName, DispalyPageSetupDialog);
                    //pw.StopThread();
                }
            }
            //释放资源
           Dispose();
        }

        /// <summary>
        /// 释放资料源
        /// </summary>
        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }

        /// <summary>
        /// 判断系统中是否存在打印机
        /// </summary>
        /// <returns>TRUE：存在打印机；FALSE：不存在打印机</returns>
        private bool PrinterExists()
        {
            PrinterSettings.StringCollection snames = PrinterSettings.InstalledPrinters;

            foreach (string s in snames)
            {
                if (s.ToLower().Trim() == s.ToLower().Trim())
                {
                    return true;
                }
            }
          

            MessageBox.Show("没找到打印机！\n只有安装了打印机才能进行打印", "没找到打印机",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
 

    }
}
