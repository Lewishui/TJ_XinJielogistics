using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TJ.DB
{
    public class clsOrderDatabaseinfo
    {
        public string Order_id { get; set; }
        //1
        public string fukuandanwei { get; set; }
        public string weituoren { get; set; }
        public string dizhi { get; set; }
        public string dianhua { get; set; }
        public string shouji { get; set; }
        //3
        public string zhongyaotishi3 { get; set; }
        public string quhuoren3 { get; set; }
        public string quhuoren_riqi3 { get; set; }
        public string weituoren3 { get; set; }
        public string weituoren_riqi3 { get; set; }
        //2

        public string daodaidi2 { get; set; }
        public string jiesuanfangshi2 { get; set; }
        public string shouhuoren2 { get; set; }
        public string danwei2 { get; set; }
        public string dizhi2 { get; set; }
        public string dianhua2 { get; set; }
        public string shouji2 { get; set; }
        public string huowupinming2 { get; set; }
        public string shijijianshu2 { get; set; }
        public string shijizhongliang2 { get; set; }
        public string tijizhongliang2 { get; set; }
        public string baoxianjin2 { get; set; }
        public string baoxianfei2 { get; set; }
   
        public string daishouzafei2 { get; set; }
        public string daofukuan2 { get; set; }
        //底部
        public string luyunbu { get; set; }
        public string kongyunbu { get; set; }
        public string chaxundianhua { get; set; }
        public string toushudianhua { get; set; }
        //4

        public string kongyun4 { get; set; }
        public string luyun4 { get; set; }
        public string fahuoshijian4 { get; set; }
        //5
        public string chicunjizhongliang5 { get; set; }
        public string pansongwanglu5 { get; set; }
        public string sijixingming5 { get; set; }
        public string chepaihao5 { get; set; }
        public string yewuyuan5 { get; set; }
        public string qianshouren5 { get; set; }
        public string Input_Date { get; set; }

        public string yundanhao { get; set; }
        public string comment { get; set; }
    }
    public class clsuserinfo
    {
        public string Order_id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string Btype { get; set; }
        public string denglushijian { get; set; }
        public string Createdate { get; set; }
        public string AdminIS { get; set; }
        public string jigoudaima { get; set; }
    }
    public class clsTipsinfo
    {
        public string tip_id { get; set; }
        public string shifazhan { get; set; }
        public string mudizhan { get; set; }
        public string yuandanhao { get; set; }
        public string jianshu { get; set; }
        public string shouhuoren { get; set; }
        public string dianhua { get; set; }
        public string Input_Date { get; set; }
  
    }

}
