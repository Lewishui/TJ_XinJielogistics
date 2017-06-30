using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Microsoft.Win32;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using TJ.Common;
using TJ.DB;

namespace TJ.Buiness
{
    public class clsAllnew
    {
        string connectionString = "mongodb://127.0.0.1";
        string DB_NAME = "XJ_logistics_TJ";
        string orderprint;
        string tisprint;

        #region print
        private List<Stream> m_streams;
        private int m_currentPageIndex;
        List<clsTipsinfo> FilterTIPResults;
        #endregion


        public clsAllnew()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "System\\IP.txt";

            string[] fileText = File.ReadAllLines(path);
            connectionString = "mongodb://" + fileText[0];
            getUserPint();


        }
        public void createUser_Server(List<clsuserinfo> AddMAPResult)
        {
            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("XJ_logistics_TJ_User");
            MongoCollection<BsonDocument> employees1 = db1.GetCollection<BsonDocument>("XJ_logistics_TJ_User");

            //  collection1.RemoveAll();
            if (AddMAPResult == null)
            {
                MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (clsuserinfo item in AddMAPResult)
            {

                QueryDocument query = new QueryDocument("name", item.name);
                collection1.Remove(query);
                BsonDocument fruit_1 = new BsonDocument
                 { 
                 { "name", item.name },
                 { "password", item.password },
                 { "Createdate", DateTime.Now.ToString("yyyy/MM/dd/HH")}, 
                 { "Btype", item.Btype} ,
                  { "denglushijian", item.denglushijian} ,
                   { "jigoudaima", item.jigoudaima} ,
                 { "AdminIS", item.AdminIS} 
                 };
                collection1.Insert(fruit_1);
            }
        }
        public void lock_Userpassword_Server(List<clsuserinfo> AddMAPResult)
        {

            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("XJ_logistics_TJ_User");
            MongoCollection<BsonDocument> employees1 = db1.GetCollection<BsonDocument>("XJ_logistics_TJ_User");

            if (AddMAPResult == null)
            {
                MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (clsuserinfo item in AddMAPResult)
            {
                QueryDocument query = new QueryDocument("name", item.name);
                var update = Update.Set("Btype", item.Btype.Trim());
                collection1.Update(query, update);
            }
        }
        public void deleteUSER(string name)
        {

            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("XJ_logistics_TJ_User");
            MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("XJ_logistics_TJ_User");

            if (name == null)
            {
                MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            QueryDocument query = new QueryDocument("name", name);

            collection1.Remove(query);
        }

        public List<clsuserinfo> ReadUserlistfromServer()
        {

            #region Read  database info server
            try
            {
                List<clsuserinfo> ClaimReport_Server = new List<clsuserinfo>();

                MongoServer server = MongoServer.Create(connectionString);
                MongoDatabase db1 = server.GetDatabase(DB_NAME);
                MongoCollection collection1 = db1.GetCollection("XJ_logistics_TJ_User");
                MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("XJ_logistics_TJ_User");

                foreach (BsonDocument emp in employees.FindAll())
                {
                    clsuserinfo item = new clsuserinfo();

                    #region 数据
                    if (emp.Contains("_id"))
                        item.Order_id = (emp["_id"].ToString());
                    if (emp.Contains("name"))
                        item.name = (emp["name"].AsString);
                    if (emp.Contains("password"))
                        item.password = (emp["password"].ToString());
                    if (emp.Contains("Btype"))
                        item.Btype = (emp["Btype"].AsString);
                    if (emp.Contains("denglushijian"))
                        item.denglushijian = (emp["denglushijian"].AsString);
                    if (emp.Contains("Createdate"))
                        item.Createdate = (emp["Createdate"].AsString);
                    if (emp.Contains("AdminIS"))
                        item.AdminIS = (emp["AdminIS"].AsString);

                    if (emp.Contains("jigoudaima"))
                        item.jigoudaima = (emp["jigoudaima"].AsString);

                    #endregion
                    ClaimReport_Server.Add(item);
                }
                return ClaimReport_Server;

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
                return null;
                throw ex;
            }
            #endregion
        }

        public void changeUserpassword_Server(List<clsuserinfo> AddMAPResult)
        {

            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("XJ_logistics_TJ_User");
            MongoCollection<BsonDocument> employees1 = db1.GetCollection<BsonDocument>("XJ_logistics_TJ_User");

            if (AddMAPResult == null)
            {
                MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (clsuserinfo item in AddMAPResult)
            {
                QueryDocument query = new QueryDocument("name", item.name);
                var update = Update.Set("password", item.password.Trim());
                collection1.Update(query, update);
            }
        }

        public List<clsuserinfo> findUser(string findtext)
        {

            #region Read  database info server
            try
            {
                List<clsuserinfo> ClaimReport_Server = new List<clsuserinfo>();

                MongoServer server = MongoServer.Create(connectionString);
                MongoDatabase db1 = server.GetDatabase(DB_NAME);
                MongoCollection collection1 = db1.GetCollection("XJ_logistics_TJ_User");
                MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("XJ_logistics_TJ_User");

                var query = new QueryDocument("name", findtext);

                foreach (BsonDocument emp in employees.Find(query))
                {
                    clsuserinfo item = new clsuserinfo();

                    #region 数据
                    if (emp.Contains("_id"))
                        item.Order_id = (emp["_id"].ToString());
                    if (emp.Contains("name"))
                        item.name = (emp["name"].AsString);
                    if (emp.Contains("password"))
                        item.password = (emp["password"].ToString());
                    if (emp.Contains("Btype"))
                        item.Btype = (emp["Btype"].AsString);
                    if (emp.Contains("denglushijian"))
                        item.denglushijian = (emp["denglushijian"].AsString);
                    if (emp.Contains("Createdate"))
                        item.Createdate = (emp["Createdate"].AsString);
                    if (emp.Contains("AdminIS"))
                        item.AdminIS = (emp["AdminIS"].AsString);

                    if (emp.Contains("jigoudaima"))
                        item.jigoudaima = (emp["jigoudaima"].AsString);
                    #endregion
                    ClaimReport_Server.Add(item);
                }
                return ClaimReport_Server;

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
                return null;

                throw ex;
            }
            #endregion
        }

        public void updateLoginTime_Server(List<clsuserinfo> AddMAPResult)
        {

            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("XJ_logistics_TJ_User");
            MongoCollection<BsonDocument> employees1 = db1.GetCollection<BsonDocument>("XJ_logistics_TJ_User");

            //  collection1.RemoveAll();
            if (AddMAPResult == null)
            {
                MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (clsuserinfo item in AddMAPResult)
            {
                QueryDocument query = new QueryDocument("name", item.name);
                var update = Update.Set("denglushijian", item.denglushijian.Trim());
                collection1.Update(query, update);
            }
        }

        public void update_OrderServer(List<clsOrderDatabaseinfo> AddMAPResult)
        {

            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("XJ_logistics_TJ_Order");
            MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("XJ_logistics_TJ_Order");

            //  collection1.RemoveAll();
            if (AddMAPResult == null)
            {
                MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (clsOrderDatabaseinfo item in AddMAPResult)
            {

                IMongoQuery query = Query.EQ("_id", new ObjectId(item.Order_id));
                //collection.Remove(query);

                //QueryDocument query = new QueryDocument("name", item.name);
                //var query = Query.And(Query.EQ("fapiaohao", item.fapiaohao), Query.EQ("danganhao", item.danganhao), Query.EQ("jigoudaima", item.jigoudaima));//同时满足多个条件

                #region 集合
                var update = Update.Set("fukuandanwei", item.fukuandanwei.Trim());
                collection1.Update(query, update);
                update = Update.Set("weituoren", item.weituoren);
                collection1.Update(query, update);
                update = Update.Set("dizhi", item.dizhi);
                collection1.Update(query, update);
                update = Update.Set("dianhua", item.dianhua);
                collection1.Update(query, update);
                update = Update.Set("shouji", item.shouji);
                collection1.Update(query, update);
                update = Update.Set("zhongyaotishi3", item.zhongyaotishi3);
                collection1.Update(query, update);
                update = Update.Set("quhuoren3", item.quhuoren3);
                collection1.Update(query, update);
                update = Update.Set("quhuoren_riqi3", item.quhuoren_riqi3);
                collection1.Update(query, update);
                update = Update.Set("weituoren3", item.weituoren3);
                collection1.Update(query, update);
                update = Update.Set("weituoren_riqi3", item.weituoren_riqi3);
                collection1.Update(query, update);
                update = Update.Set("daodaidi2", item.daodaidi2);
                collection1.Update(query, update);
                if (item.jiesuanfangshi2 != null)
                {
                    update = Update.Set("jiesuanfangshi2", item.jiesuanfangshi2);
                    collection1.Update(query, update);
                }
                update = Update.Set("shouhuoren2", item.shouhuoren2);
                collection1.Update(query, update);
                update = Update.Set("danwei2", item.danwei2);
                collection1.Update(query, update);
                update = Update.Set("dizhi2", item.dizhi2);
                collection1.Update(query, update);
                update = Update.Set("dianhua2", item.dianhua2);
                collection1.Update(query, update);
                update = Update.Set("shouji2", item.shouji2);
                collection1.Update(query, update);
                update = Update.Set("huowupinming2", item.huowupinming2);
                collection1.Update(query, update);
                update = Update.Set("shijijianshu2", item.shijijianshu2);
                collection1.Update(query, update);
                update = Update.Set("shijizhongliang2", item.shijizhongliang2);
                collection1.Update(query, update);
                update = Update.Set("tijizhongliang2", item.tijizhongliang2);
                collection1.Update(query, update);
                update = Update.Set("baoxianjin2", item.baoxianjin2);
                collection1.Update(query, update);
                update = Update.Set("baoxianfei2", item.baoxianfei2);
                collection1.Update(query, update);
                update = Update.Set("daishouzafei2", item.daishouzafei2);
                collection1.Update(query, update);
                update = Update.Set("daofukuan2", item.daofukuan2);
                collection1.Update(query, update);
                if (item.luyunbu != null)
                {
                    update = Update.Set("luyunbu", item.luyunbu);
                    collection1.Update(query, update);
                }
                if (item.kongyunbu != null)
                {
                    update = Update.Set("kongyunbu", item.kongyunbu);
                    collection1.Update(query, update);
                }
                if (item.chaxundianhua != null)
                {
                    update = Update.Set("chaxundianhua", item.chaxundianhua);
                    collection1.Update(query, update);
                }
                if (item.toushudianhua != null)
                {
                    update = Update.Set("toushudianhua", item.toushudianhua);
                    collection1.Update(query, update);
                }
                if (item.kongyun4 != null)
                {
                    update = Update.Set("kongyun4", item.kongyun4);
                    collection1.Update(query, update);
                }
                if (item.luyun4 != null)
                {
                    update = Update.Set("luyun4", item.luyun4);
                    collection1.Update(query, update);
                }
                if (item.fahuoshijian4 != null)
                {
                    update = Update.Set("fahuoshijian4", item.fahuoshijian4);
                    collection1.Update(query, update);
                }
                if (item.chicunjizhongliang5 != null)
                {
                    update = Update.Set("chicunjizhongliang5", item.chicunjizhongliang5);
                    collection1.Update(query, update);
                }
                if (item.pansongwanglu5 != null)
                {
                    update = Update.Set("pansongwanglu5", item.pansongwanglu5);
                    collection1.Update(query, update);
                }
                if (item.sijixingming5 != null)
                {
                    update = Update.Set("sijixingming5", item.sijixingming5);
                    collection1.Update(query, update);
                }
                if (item.chepaihao5 != null)
                {
                    update = Update.Set("chepaihao5", item.chepaihao5);
                    collection1.Update(query, update);
                }
                if (item.yewuyuan5 != null)
                {
                    update = Update.Set("yewuyuan5", item.yewuyuan5);
                    collection1.Update(query, update);
                }
                if (item.qianshouren5 != null)
                {
                    update = Update.Set("qianshouren5", item.qianshouren5);
                    collection1.Update(query, update);
                }
                if (item.yundanhao != null)
                {
                    update = Update.Set("yundanhao", item.yundanhao);
                    collection1.Update(query, update);
                }
                if (item.comment != null)
                {
                    update = Update.Set("comment", item.comment);
                    collection1.Update(query, update);
                }

                #endregion

            }
        }
        public void update_TipServer(List<clsTipsinfo> AddMAPResult)
        {

            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("XJ_logistics_TJ_Tip");
            MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("XJ_logistics_TJ_Tip");

            //  collection1.RemoveAll();
            if (AddMAPResult == null)
            {
                MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (clsTipsinfo item in AddMAPResult)
            {
                IMongoQuery query = Query.EQ("_id", new ObjectId(item.tip_id));
                //collection.Remove(query);

                //QueryDocument query = new QueryDocument("name", item.name);
                //var query = Query.And(Query.EQ("fapiaohao", item.fapiaohao), Query.EQ("danganhao", item.danganhao), Query.EQ("jigoudaima", item.jigoudaima));//同时满足多个条件

                #region 集合
                var update = Update.Set("shifazhan", item.shifazhan.Trim());
                collection1.Update(query, update);
                if (item.mudizhan != null)
                {
                    update = Update.Set("mudizhan", item.mudizhan);
                    collection1.Update(query, update);
                }
                if (item.yuandanhao != null)
                {
                    update = Update.Set("yuandanhao", item.yuandanhao);
                    collection1.Update(query, update);
                }
                if (item.jianshu != null)
                {
                    update = Update.Set("jianshu", item.jianshu);
                    collection1.Update(query, update);
                }
                if (item.shouhuoren != null)
                {
                    update = Update.Set("shouhuoren", item.shouhuoren);
                    collection1.Update(query, update);
                }
                if (item.dianhua != null)
                {
                    update = Update.Set("dianhua", item.dianhua);
                    collection1.Update(query, update);
                }
                if (item.Input_Date != null)
                {
                    update = Update.Set("Input_Date", item.Input_Date);
                    collection1.Update(query, update);
                }
                #endregion

            }
        }

        public void create_OrderServer(List<clsOrderDatabaseinfo> AddMAPResult)
        {

            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("XJ_logistics_TJ_Order");
            MongoCollection<BsonDocument> employees1 = db1.GetCollection<BsonDocument>("XJ_logistics_TJ_Order");

            if (AddMAPResult == null)
            {
                MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (clsOrderDatabaseinfo item in AddMAPResult)
            {
                //QueryDocument query = new QueryDocument("name", item.name);

                var dd = Query.And(Query.EQ("fukuandanwei", item.fukuandanwei), Query.EQ("weituoren", item.weituoren), Query.EQ("shouji2", item.shouji2));//同时满足多个条件

                collection1.Remove(dd);
                #region 集合
                BsonDocument fruit_1 = new BsonDocument
                 { 
                 { "fukuandanwei", item.fukuandanwei },
                 { "weituoren", item.weituoren },                 
                 { "dizhi", item.dizhi} ,
                 { "dianhua", item.dianhua} ,
                 { "shouji", item.shouji} ,
                 { "zhongyaotishi3", item.zhongyaotishi3} ,
                 { "quhuoren3", item.quhuoren3} ,
                  { "quhuoren_riqi3", item.quhuoren_riqi3 },                 
                 { "weituoren3", item.weituoren3} ,
                 { "weituoren_riqi3", item.weituoren_riqi3} ,
                 { "daodaidi2", item.daodaidi2} ,
                 { "jiesuanfangshi2", item.jiesuanfangshi2} ,
                 { "shouhuoren2", item.shouhuoren2} ,
                  { "danwei2", item.danwei2 },                 
                 { "dizhi2", item.dizhi2} ,
                 { "dianhua2", item.dianhua2} ,
                 { "shouji2", item.shouji2} ,
                 { "huowupinming2", item.huowupinming2} ,
                 { "shijijianshu2", item.shijijianshu2} ,
                  { "shijizhongliang2", item.shijizhongliang2 },                 
                 { "tijizhongliang2", item.tijizhongliang2} ,
                 { "baoxianjin2", item.baoxianjin2} ,
                 { "baoxianfei2", item.baoxianfei2} ,
                 { "daishouzafei2", item.daishouzafei2} ,
                 { "daofukuan2", item.daofukuan2} ,
                  { "luyunbu", item.luyunbu },                 
                 { "kongyunbu", item.kongyunbu} ,
                 { "chaxundianhua", item.chaxundianhua} ,
                 { "toushudianhua", item.toushudianhua} ,
                 { "kongyun4", item.kongyun4} ,
                 { "luyun4", item.luyun4} ,
                  { "fahuoshijian4", item.fahuoshijian4 },                 
                 { "chicunjizhongliang5", item.chicunjizhongliang5} ,
                 { "pansongwanglu5", item.pansongwanglu5} ,
                 { "sijixingming5", item.sijixingming5} ,
                 { "chepaihao5", item.chepaihao5} ,
                 { "yewuyuan5", item.yewuyuan5} ,
                  { "qianshouren5", item.qianshouren5 }, 
                 { "yundanhao", item.yundanhao },     
                     { "comment", item.comment }, 
                 { "Input_Date", item.Input_Date}
                 };
                #endregion

                collection1.Insert(fruit_1);
            }
        }
        public void create_tipServer(List<clsTipsinfo> AddMAPResult)
        {

            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("XJ_logistics_TJ_Tip");
            MongoCollection<BsonDocument> employees1 = db1.GetCollection<BsonDocument>("XJ_logistics_TJ_Tip");

            if (AddMAPResult == null)
            {
                MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (clsTipsinfo item in AddMAPResult)
            {
                //QueryDocument query = new QueryDocument("name", item.name);
                if (item.shifazhan != null)
                {

                    var dd = Query.And(Query.EQ("shifazhan", item.shifazhan), Query.EQ("mudizhan", item.mudizhan), Query.EQ("yuandanhao", item.yuandanhao), Query.EQ("jianshu", item.jianshu), Query.EQ("shouhuoren", item.shouhuoren), Query.EQ("dianhua", item.dianhua));//同时满足多个条件

                    collection1.Remove(dd);
                }
                #region 集合
                BsonDocument fruit_1 = new BsonDocument
                 { 
                 { "shifazhan", item.shifazhan },
                 { "mudizhan", item.mudizhan },                 
                 { "yuandanhao", item.yuandanhao} ,
                 { "jianshu", item.jianshu} ,
                 { "shouhuoren", item.shouhuoren} ,
                 { "dianhua", item.dianhua} ,                                           
                 { "Input_Date", item.Input_Date}
                 };
                #endregion

                collection1.Insert(fruit_1);
            }
        }

        public List<clsOrderDatabaseinfo> ReadAll_Order()
        {

            #region Read  database info server
            try
            {
                List<clsOrderDatabaseinfo> ClaimReport_Server = new List<clsOrderDatabaseinfo>();

                MongoServer server = MongoServer.Create(connectionString);
                MongoDatabase db1 = server.GetDatabase(DB_NAME);
                MongoCollection collection1 = db1.GetCollection("XJ_logistics_TJ_Order");
                MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("XJ_logistics_TJ_Order");

                foreach (BsonDocument emp in employees.FindAll())
                {
                    clsOrderDatabaseinfo item = new clsOrderDatabaseinfo();

                    #region 数据
                    if (emp.Contains("_id"))
                        item.Order_id = (emp["_id"].ToString());
                    if (emp.Contains("fukuandanwei"))
                        item.fukuandanwei = (emp["fukuandanwei"].ToString());
                    if (emp.Contains("weituoren"))
                        item.weituoren = (emp["weituoren"].ToString());
                    if (emp.Contains("dizhi"))
                        item.dizhi = (emp["dizhi"].ToString());
                    if (emp.Contains("dianhua"))
                        item.dianhua = (emp["dianhua"].AsString);
                    if (emp.Contains("shouji"))
                        item.shouji = (emp["shouji"].AsString);
                    if (emp.Contains("zhongyaotishi3"))
                        item.zhongyaotishi3 = (emp["zhongyaotishi3"].AsString);
                    if (emp.Contains("quhuoren3"))
                        item.quhuoren3 = (emp["quhuoren3"].AsString);

                    if (emp.Contains("quhuoren_riqi3"))
                        item.quhuoren_riqi3 = (emp["quhuoren_riqi3"].ToString());
                    if (emp.Contains("weituoren3"))
                        item.weituoren3 = (emp["weituoren3"].ToString());
                    if (emp.Contains("weituoren_riqi3"))
                        item.weituoren_riqi3 = (emp["weituoren_riqi3"].ToString());
                    if (emp.Contains("daodaidi2"))
                        item.daodaidi2 = (emp["daodaidi2"].ToString());
                    if (emp.Contains("jiesuanfangshi2"))
                        item.jiesuanfangshi2 = (emp["jiesuanfangshi2"].AsString);
                    if (emp.Contains("shouhuoren2"))
                        item.shouhuoren2 = (emp["shouhuoren2"].AsString);
                    if (emp.Contains("danwei2"))
                        item.danwei2 = (emp["danwei2"].AsString);
                    if (emp.Contains("dizhi2"))
                        item.dizhi2 = (emp["dizhi2"].AsString);
                    if (emp.Contains("dianhua2"))
                        item.dianhua2 = (emp["dianhua2"].ToString());
                    if (emp.Contains("shouji2"))
                        item.shouji2 = (emp["shouji2"].ToString());
                    if (emp.Contains("huowupinming2"))
                        item.huowupinming2 = (emp["huowupinming2"].ToString());
                    if (emp.Contains("shijijianshu2"))
                        item.shijijianshu2 = (emp["shijijianshu2"].ToString());
                    if (emp.Contains("shijizhongliang2"))
                        item.shijizhongliang2 = (emp["shijizhongliang2"].AsString);
                    if (emp.Contains("tijizhongliang2"))
                        item.tijizhongliang2 = (emp["tijizhongliang2"].AsString);
                    if (emp.Contains("baoxianjin2"))
                        item.baoxianjin2 = (emp["baoxianjin2"].AsString);
                    if (emp.Contains("baoxianfei2"))
                        item.baoxianfei2 = (emp["baoxianfei2"].AsString);
                    if (emp.Contains("daishouzafei2"))
                        item.daishouzafei2 = (emp["daishouzafei2"].ToString());
                    if (emp.Contains("daofukuan2"))
                        item.daofukuan2 = (emp["daofukuan2"].ToString());
                    if (emp.Contains("luyunbu"))
                        item.luyunbu = (emp["luyunbu"].ToString());
                    if (emp.Contains("kongyunbu"))
                        item.kongyunbu = (emp["kongyunbu"].ToString());
                    if (emp.Contains("chaxundianhua"))
                        item.chaxundianhua = (emp["chaxundianhua"].AsString);
                    if (emp.Contains("toushudianhua"))
                        item.toushudianhua = (emp["toushudianhua"].AsString);
                    if (emp.Contains("kongyun4"))
                        item.kongyun4 = (emp["kongyun4"].AsString);
                    if (emp.Contains("luyun4"))
                        item.luyun4 = (emp["luyun4"].AsString);
                    if (emp.Contains("fahuoshijian4"))
                        item.fahuoshijian4 = emp["fahuoshijian4"].ToString();
                    if (emp.Contains("chicunjizhongliang5"))
                        item.chicunjizhongliang5 = (emp["chicunjizhongliang5"].ToString());
                    if (emp.Contains("pansongwanglu5"))
                        item.pansongwanglu5 = (emp["pansongwanglu5"].ToString());
                    if (emp.Contains("sijixingming5"))
                        item.sijixingming5 = (emp["sijixingming5"].ToString());
                    if (emp.Contains("chepaihao5"))
                        item.chepaihao5 = (emp["chepaihao5"].AsString);
                    if (emp.Contains("yewuyuan5"))
                        item.yewuyuan5 = (emp["yewuyuan5"].AsString);
                    if (emp.Contains("qianshouren5"))
                        item.qianshouren5 = (emp["qianshouren5"].AsString);
                    if (emp.Contains("Input_Date"))
                        item.Input_Date = (emp["Input_Date"].AsString);
                    if (emp.Contains("yundanhao"))
                        item.yundanhao = (emp["yundanhao"].AsString);
                    if (emp.Contains("comment"))
                        item.comment = (emp["comment"].AsString);

                    #endregion

                    ClaimReport_Server.Add(item);
                }
                return ClaimReport_Server;

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
                return null;

                throw ex;
            }
            #endregion
        }

        public List<clsOrderDatabaseinfo> findOrder_Server(string kettext, string start_time, string end_time)
        {

            #region Read  database info server
            try
            {
                List<clsOrderDatabaseinfo> ClaimReport_Server = new List<clsOrderDatabaseinfo>();

                MongoServer server = MongoServer.Create(connectionString);
                MongoDatabase db1 = server.GetDatabase(DB_NAME);
                MongoCollection collection1 = db1.GetCollection("XJ_logistics_TJ_Order");
                MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("XJ_logistics_TJ_Order");

                var query = new QueryDocument("fukuandanwei", kettext);
                //    var dd = Query.And(Query.EQ("jigoudaima", jigoudaima), Query.EQ("fapiaoleixing", fapiaoleixing));//同时满足多个条件
                if (kettext != "")
                {
                    foreach (BsonDocument emp in employees.Find(query))
                    {
                        clsOrderDatabaseinfo item = new clsOrderDatabaseinfo();

                        #region 数据
                        if (emp.Contains("_id"))
                            item.Order_id = (emp["_id"].ToString());
                        if (emp.Contains("fukuandanwei"))
                            item.fukuandanwei = (emp["fukuandanwei"].ToString());
                        if (emp.Contains("weituoren"))
                            item.weituoren = (emp["weituoren"].ToString());
                        if (emp.Contains("dizhi"))
                            item.dizhi = (emp["dizhi"].ToString());
                        if (emp.Contains("dianhua"))
                            item.dianhua = (emp["dianhua"].AsString);
                        if (emp.Contains("shouji"))
                            item.shouji = (emp["shouji"].AsString);
                        if (emp.Contains("zhongyaotishi3"))
                            item.zhongyaotishi3 = (emp["zhongyaotishi3"].AsString);
                        if (emp.Contains("quhuoren3"))
                            item.quhuoren3 = (emp["quhuoren3"].AsString);

                        if (emp.Contains("quhuoren_riqi3"))
                            item.quhuoren_riqi3 = (emp["quhuoren_riqi3"].ToString());
                        if (emp.Contains("weituoren3"))
                            item.weituoren3 = (emp["weituoren3"].ToString());
                        if (emp.Contains("weituoren_riqi3"))
                            item.weituoren_riqi3 = (emp["weituoren_riqi3"].ToString());
                        if (emp.Contains("daodaidi2"))
                            item.daodaidi2 = (emp["daodaidi2"].ToString());
                        if (emp.Contains("jiesuanfangshi2"))
                            item.jiesuanfangshi2 = (emp["jiesuanfangshi2"].AsString);
                        if (emp.Contains("shouhuoren2"))
                            item.shouhuoren2 = (emp["shouhuoren2"].AsString);
                        if (emp.Contains("danwei2"))
                            item.danwei2 = (emp["danwei2"].AsString);
                        if (emp.Contains("dizhi2"))
                            item.dizhi2 = (emp["dizhi2"].AsString);
                        if (emp.Contains("dianhua2"))
                            item.dianhua2 = (emp["dianhua2"].ToString());
                        if (emp.Contains("shouji2"))
                            item.shouji2 = (emp["shouji2"].ToString());
                        if (emp.Contains("huowupinming2"))
                            item.huowupinming2 = (emp["huowupinming2"].ToString());
                        if (emp.Contains("shijijianshu2"))
                            item.shijijianshu2 = (emp["shijijianshu2"].ToString());
                        if (emp.Contains("shijizhongliang2"))
                            item.shijizhongliang2 = (emp["shijizhongliang2"].AsString);
                        if (emp.Contains("tijizhongliang2"))
                            item.tijizhongliang2 = (emp["tijizhongliang2"].AsString);
                        if (emp.Contains("baoxianjin2"))
                            item.baoxianjin2 = (emp["baoxianjin2"].AsString);
                        if (emp.Contains("baoxianfei2"))
                            item.baoxianfei2 = (emp["baoxianfei2"].AsString);
                        if (emp.Contains("daishouzafei2"))
                            item.daishouzafei2 = (emp["daishouzafei2"].ToString());
                        if (emp.Contains("daofukuan2"))
                            item.daofukuan2 = (emp["daofukuan2"].ToString());
                        if (emp.Contains("luyunbu"))
                            item.luyunbu = (emp["luyunbu"].ToString());
                        if (emp.Contains("kongyunbu"))
                            item.kongyunbu = (emp["kongyunbu"].ToString());
                        if (emp.Contains("chaxundianhua"))
                            item.chaxundianhua = (emp["chaxundianhua"].AsString);
                        if (emp.Contains("toushudianhua"))
                            item.toushudianhua = (emp["toushudianhua"].AsString);
                        if (emp.Contains("kongyun4"))
                            item.kongyun4 = (emp["kongyun4"].AsString);
                        if (emp.Contains("luyun4"))
                            item.luyun4 = (emp["luyun4"].AsString);
                        if (emp.Contains("fahuoshijian4"))
                            item.fahuoshijian4 = emp["fahuoshijian4"].ToString();
                        if (emp.Contains("chicunjizhongliang5"))
                            item.chicunjizhongliang5 = (emp["chicunjizhongliang5"].ToString());
                        if (emp.Contains("pansongwanglu5"))
                            item.pansongwanglu5 = (emp["pansongwanglu5"].ToString());
                        if (emp.Contains("sijixingming5"))
                            item.sijixingming5 = (emp["sijixingming5"].ToString());
                        if (emp.Contains("chepaihao5"))
                            item.chepaihao5 = (emp["chepaihao5"].AsString);
                        if (emp.Contains("yewuyuan5"))
                            item.yewuyuan5 = (emp["yewuyuan5"].AsString);
                        if (emp.Contains("qianshouren5"))
                            item.qianshouren5 = (emp["qianshouren5"].AsString);
                        if (emp.Contains("Input_Date"))
                            item.Input_Date = (emp["Input_Date"].AsString);

                        if (emp.Contains("yundanhao"))
                            item.yundanhao = (emp["yundanhao"].AsString);

                        if (emp.Contains("comment"))
                            item.comment = (emp["comment"].AsString);

                        #endregion

                        ClaimReport_Server.Add(item);
                    }
                    query = new QueryDocument("shouji", kettext);
                    foreach (BsonDocument emp in employees.Find(query))
                    {
                        clsOrderDatabaseinfo item = new clsOrderDatabaseinfo();

                        #region 数据
                        if (emp.Contains("_id"))
                            item.Order_id = (emp["_id"].ToString());
                        if (emp.Contains("fukuandanwei"))
                            item.fukuandanwei = (emp["fukuandanwei"].ToString());
                        if (emp.Contains("weituoren"))
                            item.weituoren = (emp["weituoren"].ToString());
                        if (emp.Contains("dizhi"))
                            item.dizhi = (emp["dizhi"].ToString());
                        if (emp.Contains("dianhua"))
                            item.dianhua = (emp["dianhua"].AsString);
                        if (emp.Contains("shouji"))
                            item.shouji = (emp["shouji"].AsString);
                        if (emp.Contains("zhongyaotishi3"))
                            item.zhongyaotishi3 = (emp["zhongyaotishi3"].AsString);
                        if (emp.Contains("quhuoren3"))
                            item.quhuoren3 = (emp["quhuoren3"].AsString);

                        if (emp.Contains("quhuoren_riqi3"))
                            item.quhuoren_riqi3 = (emp["quhuoren_riqi3"].ToString());
                        if (emp.Contains("weituoren3"))
                            item.weituoren3 = (emp["weituoren3"].ToString());
                        if (emp.Contains("weituoren_riqi3"))
                            item.weituoren_riqi3 = (emp["weituoren_riqi3"].ToString());
                        if (emp.Contains("daodaidi2"))
                            item.daodaidi2 = (emp["daodaidi2"].ToString());
                        if (emp.Contains("jiesuanfangshi2"))
                            item.jiesuanfangshi2 = (emp["jiesuanfangshi2"].AsString);
                        if (emp.Contains("shouhuoren2"))
                            item.shouhuoren2 = (emp["shouhuoren2"].AsString);
                        if (emp.Contains("danwei2"))
                            item.danwei2 = (emp["danwei2"].AsString);
                        if (emp.Contains("dizhi2"))
                            item.dizhi2 = (emp["dizhi2"].AsString);
                        if (emp.Contains("dianhua2"))
                            item.dianhua2 = (emp["dianhua2"].ToString());
                        if (emp.Contains("shouji2"))
                            item.shouji2 = (emp["shouji2"].ToString());
                        if (emp.Contains("huowupinming2"))
                            item.huowupinming2 = (emp["huowupinming2"].ToString());
                        if (emp.Contains("shijijianshu2"))
                            item.shijijianshu2 = (emp["shijijianshu2"].ToString());
                        if (emp.Contains("shijizhongliang2"))
                            item.shijizhongliang2 = (emp["shijizhongliang2"].AsString);
                        if (emp.Contains("tijizhongliang2"))
                            item.tijizhongliang2 = (emp["tijizhongliang2"].AsString);
                        if (emp.Contains("baoxianjin2"))
                            item.baoxianjin2 = (emp["baoxianjin2"].AsString);
                        if (emp.Contains("baoxianfei2"))
                            item.baoxianfei2 = (emp["baoxianfei2"].AsString);
                        if (emp.Contains("daishouzafei2"))
                            item.daishouzafei2 = (emp["daishouzafei2"].ToString());
                        if (emp.Contains("daofukuan2"))
                            item.daofukuan2 = (emp["daofukuan2"].ToString());
                        if (emp.Contains("luyunbu"))
                            item.luyunbu = (emp["luyunbu"].ToString());
                        if (emp.Contains("kongyunbu"))
                            item.kongyunbu = (emp["kongyunbu"].ToString());
                        if (emp.Contains("chaxundianhua"))
                            item.chaxundianhua = (emp["chaxundianhua"].AsString);
                        if (emp.Contains("toushudianhua"))
                            item.toushudianhua = (emp["toushudianhua"].AsString);
                        if (emp.Contains("kongyun4"))
                            item.kongyun4 = (emp["kongyun4"].AsString);
                        if (emp.Contains("luyun4"))
                            item.luyun4 = (emp["luyun4"].AsString);
                        if (emp.Contains("fahuoshijian4"))
                            item.fahuoshijian4 = (emp["fahuoshijian4"].ToString());
                        if (emp.Contains("chicunjizhongliang5"))
                            item.chicunjizhongliang5 = (emp["chicunjizhongliang5"].ToString());
                        if (emp.Contains("pansongwanglu5"))
                            item.pansongwanglu5 = (emp["pansongwanglu5"].ToString());
                        if (emp.Contains("sijixingming5"))
                            item.sijixingming5 = (emp["sijixingming5"].ToString());
                        if (emp.Contains("chepaihao5"))
                            item.chepaihao5 = (emp["chepaihao5"].AsString);
                        if (emp.Contains("yewuyuan5"))
                            item.yewuyuan5 = (emp["yewuyuan5"].AsString);
                        if (emp.Contains("qianshouren5"))
                            item.qianshouren5 = (emp["qianshouren5"].AsString);
                        if (emp.Contains("Input_Date"))
                            item.Input_Date = (emp["Input_Date"].AsString);
                        if (emp.Contains("yundanhao"))
                            item.yundanhao = (emp["yundanhao"].AsString);
                        if (emp.Contains("comment"))
                            item.comment = (emp["comment"].AsString);

                        #endregion

                        ClaimReport_Server.Add(item);
                    }
                    query = new QueryDocument("shouji2", kettext);
                    foreach (BsonDocument emp in employees.Find(query))
                    {
                        clsOrderDatabaseinfo item = new clsOrderDatabaseinfo();

                        #region 数据
                        if (emp.Contains("_id"))
                            item.Order_id = (emp["_id"].ToString());
                        if (emp.Contains("fukuandanwei"))
                            item.fukuandanwei = (emp["fukuandanwei"].ToString());
                        if (emp.Contains("weituoren"))
                            item.weituoren = (emp["weituoren"].ToString());
                        if (emp.Contains("dizhi"))
                            item.dizhi = (emp["dizhi"].ToString());
                        if (emp.Contains("dianhua"))
                            item.dianhua = (emp["dianhua"].AsString);
                        if (emp.Contains("shouji"))
                            item.shouji = (emp["shouji"].AsString);
                        if (emp.Contains("zhongyaotishi3"))
                            item.zhongyaotishi3 = (emp["zhongyaotishi3"].AsString);
                        if (emp.Contains("quhuoren3"))
                            item.quhuoren3 = (emp["quhuoren3"].AsString);

                        if (emp.Contains("quhuoren_riqi3"))
                            item.quhuoren_riqi3 = (emp["quhuoren_riqi3"].ToString());
                        if (emp.Contains("weituoren3"))
                            item.weituoren3 = (emp["weituoren3"].ToString());
                        if (emp.Contains("weituoren_riqi3"))
                            item.weituoren_riqi3 = (emp["weituoren_riqi3"].ToString());
                        if (emp.Contains("daodaidi2"))
                            item.daodaidi2 = (emp["daodaidi2"].ToString());
                        if (emp.Contains("jiesuanfangshi2"))
                            item.jiesuanfangshi2 = (emp["jiesuanfangshi2"].AsString);
                        if (emp.Contains("shouhuoren2"))
                            item.shouhuoren2 = (emp["shouhuoren2"].AsString);
                        if (emp.Contains("danwei2"))
                            item.danwei2 = (emp["danwei2"].AsString);
                        if (emp.Contains("dizhi2"))
                            item.dizhi2 = (emp["dizhi2"].AsString);
                        if (emp.Contains("dianhua2"))
                            item.dianhua2 = (emp["dianhua2"].ToString());
                        if (emp.Contains("shouji2"))
                            item.shouji2 = (emp["shouji2"].ToString());
                        if (emp.Contains("huowupinming2"))
                            item.huowupinming2 = (emp["huowupinming2"].ToString());
                        if (emp.Contains("shijijianshu2"))
                            item.shijijianshu2 = (emp["shijijianshu2"].ToString());
                        if (emp.Contains("shijizhongliang2"))
                            item.shijizhongliang2 = (emp["shijizhongliang2"].AsString);
                        if (emp.Contains("tijizhongliang2"))
                            item.tijizhongliang2 = (emp["tijizhongliang2"].AsString);
                        if (emp.Contains("baoxianjin2"))
                            item.baoxianjin2 = (emp["baoxianjin2"].AsString);
                        if (emp.Contains("baoxianfei2"))
                            item.baoxianfei2 = (emp["baoxianfei2"].AsString);
                        if (emp.Contains("daishouzafei2"))
                            item.daishouzafei2 = (emp["daishouzafei2"].ToString());
                        if (emp.Contains("daofukuan2"))
                            item.daofukuan2 = (emp["daofukuan2"].ToString());
                        if (emp.Contains("luyunbu"))
                            item.luyunbu = (emp["luyunbu"].ToString());
                        if (emp.Contains("kongyunbu"))
                            item.kongyunbu = (emp["kongyunbu"].ToString());
                        if (emp.Contains("chaxundianhua"))
                            item.chaxundianhua = (emp["chaxundianhua"].AsString);
                        if (emp.Contains("toushudianhua"))
                            item.toushudianhua = (emp["toushudianhua"].AsString);
                        if (emp.Contains("kongyun4"))
                            item.kongyun4 = (emp["kongyun4"].AsString);
                        if (emp.Contains("luyun4"))
                            item.luyun4 = (emp["luyun4"].AsString);
                        if (emp.Contains("fahuoshijian4"))
                            item.fahuoshijian4 = (emp["fahuoshijian4"].ToString());
                        if (emp.Contains("chicunjizhongliang5"))
                            item.chicunjizhongliang5 = (emp["chicunjizhongliang5"].ToString());
                        if (emp.Contains("pansongwanglu5"))
                            item.pansongwanglu5 = (emp["pansongwanglu5"].ToString());
                        if (emp.Contains("sijixingming5"))
                            item.sijixingming5 = (emp["sijixingming5"].ToString());
                        if (emp.Contains("chepaihao5"))
                            item.chepaihao5 = (emp["chepaihao5"].AsString);
                        if (emp.Contains("yewuyuan5"))
                            item.yewuyuan5 = (emp["yewuyuan5"].AsString);
                        if (emp.Contains("qianshouren5"))
                            item.qianshouren5 = (emp["qianshouren5"].AsString);
                        if (emp.Contains("Input_Date"))
                            item.Input_Date = (emp["Input_Date"].AsString);
                        if (emp.Contains("yundanhao"))
                            item.yundanhao = (emp["yundanhao"].AsString);
                        if (emp.Contains("comment"))
                            item.comment = (emp["comment"].AsString);

                        #endregion

                        ClaimReport_Server.Add(item);
                    }
                    query = new QueryDocument("dianhua", kettext);
                    foreach (BsonDocument emp in employees.Find(query))
                    {
                        clsOrderDatabaseinfo item = new clsOrderDatabaseinfo();

                        #region 数据
                        if (emp.Contains("_id"))
                            item.Order_id = (emp["_id"].ToString());
                        if (emp.Contains("fukuandanwei"))
                            item.fukuandanwei = (emp["fukuandanwei"].ToString());
                        if (emp.Contains("weituoren"))
                            item.weituoren = (emp["weituoren"].ToString());
                        if (emp.Contains("dizhi"))
                            item.dizhi = (emp["dizhi"].ToString());
                        if (emp.Contains("dianhua"))
                            item.dianhua = (emp["dianhua"].AsString);
                        if (emp.Contains("shouji"))
                            item.shouji = (emp["shouji"].AsString);
                        if (emp.Contains("zhongyaotishi3"))
                            item.zhongyaotishi3 = (emp["zhongyaotishi3"].AsString);
                        if (emp.Contains("quhuoren3"))
                            item.quhuoren3 = (emp["quhuoren3"].AsString);

                        if (emp.Contains("quhuoren_riqi3"))
                            item.quhuoren_riqi3 = (emp["quhuoren_riqi3"].ToString());
                        if (emp.Contains("weituoren3"))
                            item.weituoren3 = (emp["weituoren3"].ToString());
                        if (emp.Contains("weituoren_riqi3"))
                            item.weituoren_riqi3 = (emp["weituoren_riqi3"].ToString());
                        if (emp.Contains("daodaidi2"))
                            item.daodaidi2 = (emp["daodaidi2"].ToString());
                        if (emp.Contains("jiesuanfangshi2"))
                            item.jiesuanfangshi2 = (emp["jiesuanfangshi2"].AsString);
                        if (emp.Contains("shouhuoren2"))
                            item.shouhuoren2 = (emp["shouhuoren2"].AsString);
                        if (emp.Contains("danwei2"))
                            item.danwei2 = (emp["danwei2"].AsString);
                        if (emp.Contains("dizhi2"))
                            item.dizhi2 = (emp["dizhi2"].AsString);
                        if (emp.Contains("dianhua2"))
                            item.dianhua2 = (emp["dianhua2"].ToString());
                        if (emp.Contains("shouji2"))
                            item.shouji2 = (emp["shouji2"].ToString());
                        if (emp.Contains("huowupinming2"))
                            item.huowupinming2 = (emp["huowupinming2"].ToString());
                        if (emp.Contains("shijijianshu2"))
                            item.shijijianshu2 = (emp["shijijianshu2"].ToString());
                        if (emp.Contains("shijizhongliang2"))
                            item.shijizhongliang2 = (emp["shijizhongliang2"].AsString);
                        if (emp.Contains("tijizhongliang2"))
                            item.tijizhongliang2 = (emp["tijizhongliang2"].AsString);
                        if (emp.Contains("baoxianjin2"))
                            item.baoxianjin2 = (emp["baoxianjin2"].AsString);
                        if (emp.Contains("baoxianfei2"))
                            item.baoxianfei2 = (emp["baoxianfei2"].AsString);
                        if (emp.Contains("daishouzafei2"))
                            item.daishouzafei2 = (emp["daishouzafei2"].ToString());
                        if (emp.Contains("daofukuan2"))
                            item.daofukuan2 = (emp["daofukuan2"].ToString());
                        if (emp.Contains("luyunbu"))
                            item.luyunbu = (emp["luyunbu"].ToString());
                        if (emp.Contains("kongyunbu"))
                            item.kongyunbu = (emp["kongyunbu"].ToString());
                        if (emp.Contains("chaxundianhua"))
                            item.chaxundianhua = (emp["chaxundianhua"].AsString);
                        if (emp.Contains("toushudianhua"))
                            item.toushudianhua = (emp["toushudianhua"].AsString);
                        if (emp.Contains("kongyun4"))
                            item.kongyun4 = (emp["kongyun4"].AsString);
                        if (emp.Contains("luyun4"))
                            item.luyun4 = (emp["luyun4"].AsString);
                        if (emp.Contains("fahuoshijian4"))
                            item.fahuoshijian4 = (emp["fahuoshijian4"].ToString());
                        if (emp.Contains("chicunjizhongliang5"))
                            item.chicunjizhongliang5 = (emp["chicunjizhongliang5"].ToString());
                        if (emp.Contains("pansongwanglu5"))
                            item.pansongwanglu5 = (emp["pansongwanglu5"].ToString());
                        if (emp.Contains("sijixingming5"))
                            item.sijixingming5 = (emp["sijixingming5"].ToString());
                        if (emp.Contains("chepaihao5"))
                            item.chepaihao5 = (emp["chepaihao5"].AsString);
                        if (emp.Contains("yewuyuan5"))
                            item.yewuyuan5 = (emp["yewuyuan5"].AsString);
                        if (emp.Contains("qianshouren5"))
                            item.qianshouren5 = (emp["qianshouren5"].AsString);
                        if (emp.Contains("Input_Date"))
                            item.Input_Date = (emp["Input_Date"].AsString);
                        if (emp.Contains("yundanhao"))
                            item.yundanhao = (emp["yundanhao"].AsString);
                        if (emp.Contains("comment"))
                            item.comment = (emp["comment"].AsString);

                        #endregion

                        ClaimReport_Server.Add(item);
                    }
                    query = new QueryDocument("dianhua2", kettext);
                    foreach (BsonDocument emp in employees.Find(query))
                    {
                        clsOrderDatabaseinfo item = new clsOrderDatabaseinfo();

                        #region 数据
                        if (emp.Contains("_id"))
                            item.Order_id = (emp["_id"].ToString());
                        if (emp.Contains("fukuandanwei"))
                            item.fukuandanwei = (emp["fukuandanwei"].ToString());
                        if (emp.Contains("weituoren"))
                            item.weituoren = (emp["weituoren"].ToString());
                        if (emp.Contains("dizhi"))
                            item.dizhi = (emp["dizhi"].ToString());
                        if (emp.Contains("dianhua"))
                            item.dianhua = (emp["dianhua"].AsString);
                        if (emp.Contains("shouji"))
                            item.shouji = (emp["shouji"].AsString);
                        if (emp.Contains("zhongyaotishi3"))
                            item.zhongyaotishi3 = (emp["zhongyaotishi3"].AsString);
                        if (emp.Contains("quhuoren3"))
                            item.quhuoren3 = (emp["quhuoren3"].AsString);

                        if (emp.Contains("quhuoren_riqi3"))
                            item.quhuoren_riqi3 = (emp["quhuoren_riqi3"].ToString());
                        if (emp.Contains("weituoren3"))
                            item.weituoren3 = (emp["weituoren3"].ToString());
                        if (emp.Contains("weituoren_riqi3"))
                            item.weituoren_riqi3 = (emp["weituoren_riqi3"].ToString());
                        if (emp.Contains("daodaidi2"))
                            item.daodaidi2 = (emp["daodaidi2"].ToString());
                        if (emp.Contains("jiesuanfangshi2"))
                            item.jiesuanfangshi2 = (emp["jiesuanfangshi2"].AsString);
                        if (emp.Contains("shouhuoren2"))
                            item.shouhuoren2 = (emp["shouhuoren2"].AsString);
                        if (emp.Contains("danwei2"))
                            item.danwei2 = (emp["danwei2"].AsString);
                        if (emp.Contains("dizhi2"))
                            item.dizhi2 = (emp["dizhi2"].AsString);
                        if (emp.Contains("dianhua2"))
                            item.dianhua2 = (emp["dianhua2"].ToString());
                        if (emp.Contains("shouji2"))
                            item.shouji2 = (emp["shouji2"].ToString());
                        if (emp.Contains("huowupinming2"))
                            item.huowupinming2 = (emp["huowupinming2"].ToString());
                        if (emp.Contains("shijijianshu2"))
                            item.shijijianshu2 = (emp["shijijianshu2"].ToString());
                        if (emp.Contains("shijizhongliang2"))
                            item.shijizhongliang2 = (emp["shijizhongliang2"].AsString);
                        if (emp.Contains("tijizhongliang2"))
                            item.tijizhongliang2 = (emp["tijizhongliang2"].AsString);
                        if (emp.Contains("baoxianjin2"))
                            item.baoxianjin2 = (emp["baoxianjin2"].AsString);
                        if (emp.Contains("baoxianfei2"))
                            item.baoxianfei2 = (emp["baoxianfei2"].AsString);
                        if (emp.Contains("daishouzafei2"))
                            item.daishouzafei2 = (emp["daishouzafei2"].ToString());
                        if (emp.Contains("daofukuan2"))
                            item.daofukuan2 = (emp["daofukuan2"].ToString());
                        if (emp.Contains("luyunbu"))
                            item.luyunbu = (emp["luyunbu"].ToString());
                        if (emp.Contains("kongyunbu"))
                            item.kongyunbu = (emp["kongyunbu"].ToString());
                        if (emp.Contains("chaxundianhua"))
                            item.chaxundianhua = (emp["chaxundianhua"].AsString);
                        if (emp.Contains("toushudianhua"))
                            item.toushudianhua = (emp["toushudianhua"].AsString);
                        if (emp.Contains("kongyun4"))
                            item.kongyun4 = (emp["kongyun4"].AsString);
                        if (emp.Contains("luyun4"))
                            item.luyun4 = (emp["luyun4"].AsString);
                        if (emp.Contains("fahuoshijian4"))
                            item.fahuoshijian4 = (emp["fahuoshijian4"].ToString());
                        if (emp.Contains("chicunjizhongliang5"))
                            item.chicunjizhongliang5 = (emp["chicunjizhongliang5"].ToString());
                        if (emp.Contains("pansongwanglu5"))
                            item.pansongwanglu5 = (emp["pansongwanglu5"].ToString());
                        if (emp.Contains("sijixingming5"))
                            item.sijixingming5 = (emp["sijixingming5"].ToString());
                        if (emp.Contains("chepaihao5"))
                            item.chepaihao5 = (emp["chepaihao5"].AsString);
                        if (emp.Contains("yewuyuan5"))
                            item.yewuyuan5 = (emp["yewuyuan5"].AsString);
                        if (emp.Contains("qianshouren5"))
                            item.qianshouren5 = (emp["qianshouren5"].AsString);
                        if (emp.Contains("Input_Date"))
                            item.Input_Date = (emp["Input_Date"].AsString);
                        if (emp.Contains("yundanhao"))
                            item.yundanhao = (emp["yundanhao"].AsString);
                        if (emp.Contains("comment"))
                            item.comment = (emp["comment"].AsString);

                        #endregion

                        ClaimReport_Server.Add(item);
                    }
                }

                //  query = new QueryDocument("guidangrenzhanghao", guidangren);

                var query1 = Query.And(Query.GTE("fahuoshijian4", start_time.Replace("/", "")), Query.LTE("fahuoshijian4", end_time.Replace("/", "")));
                foreach (BsonDocument emp in employees.Find(query1))
                {
                    clsOrderDatabaseinfo item = new clsOrderDatabaseinfo();

                    #region 数据
                    if (emp.Contains("_id"))
                        item.Order_id = (emp["_id"].ToString());
                    if (emp.Contains("fukuandanwei"))
                        item.fukuandanwei = (emp["fukuandanwei"].ToString());
                    if (emp.Contains("weituoren"))
                        item.weituoren = (emp["weituoren"].ToString());
                    if (emp.Contains("dizhi"))
                        item.dizhi = (emp["dizhi"].ToString());
                    if (emp.Contains("dianhua"))
                        item.dianhua = (emp["dianhua"].AsString);
                    if (emp.Contains("shouji"))
                        item.shouji = (emp["shouji"].AsString);
                    if (emp.Contains("zhongyaotishi3"))
                        item.zhongyaotishi3 = (emp["zhongyaotishi3"].AsString);
                    if (emp.Contains("quhuoren3"))
                        item.quhuoren3 = (emp["quhuoren3"].AsString);

                    if (emp.Contains("quhuoren_riqi3"))
                        item.quhuoren_riqi3 = (emp["quhuoren_riqi3"].ToString());
                    if (emp.Contains("weituoren3"))
                        item.weituoren3 = (emp["weituoren3"].ToString());
                    if (emp.Contains("weituoren_riqi3"))
                        item.weituoren_riqi3 = (emp["weituoren_riqi3"].ToString());
                    if (emp.Contains("daodaidi2"))
                        item.daodaidi2 = (emp["daodaidi2"].ToString());
                    if (emp.Contains("jiesuanfangshi2"))
                        item.jiesuanfangshi2 = (emp["jiesuanfangshi2"].AsString);
                    if (emp.Contains("shouhuoren2"))
                        item.shouhuoren2 = (emp["shouhuoren2"].AsString);
                    if (emp.Contains("danwei2"))
                        item.danwei2 = (emp["danwei2"].AsString);
                    if (emp.Contains("dizhi2"))
                        item.dizhi2 = (emp["dizhi2"].AsString);
                    if (emp.Contains("dianhua2"))
                        item.dianhua2 = (emp["dianhua2"].ToString());
                    if (emp.Contains("shouji2"))
                        item.shouji2 = (emp["shouji2"].ToString());
                    if (emp.Contains("huowupinming2"))
                        item.huowupinming2 = (emp["huowupinming2"].ToString());
                    if (emp.Contains("shijijianshu2"))
                        item.shijijianshu2 = (emp["shijijianshu2"].ToString());
                    if (emp.Contains("shijizhongliang2"))
                        item.shijizhongliang2 = (emp["shijizhongliang2"].AsString);
                    if (emp.Contains("tijizhongliang2"))
                        item.tijizhongliang2 = (emp["tijizhongliang2"].AsString);
                    if (emp.Contains("baoxianjin2"))
                        item.baoxianjin2 = (emp["baoxianjin2"].AsString);
                    if (emp.Contains("baoxianfei2"))
                        item.baoxianfei2 = (emp["baoxianfei2"].AsString);
                    if (emp.Contains("daishouzafei2"))
                        item.daishouzafei2 = (emp["daishouzafei2"].ToString());
                    if (emp.Contains("daofukuan2"))
                        item.daofukuan2 = (emp["daofukuan2"].ToString());
                    if (emp.Contains("luyunbu"))
                        item.luyunbu = (emp["luyunbu"].ToString());
                    if (emp.Contains("kongyunbu"))
                        item.kongyunbu = (emp["kongyunbu"].ToString());
                    if (emp.Contains("chaxundianhua"))
                        item.chaxundianhua = (emp["chaxundianhua"].AsString);
                    if (emp.Contains("toushudianhua"))
                        item.toushudianhua = (emp["toushudianhua"].AsString);
                    if (emp.Contains("kongyun4"))
                        item.kongyun4 = (emp["kongyun4"].AsString);
                    if (emp.Contains("luyun4"))
                        item.luyun4 = (emp["luyun4"].AsString);
                    if (emp.Contains("fahuoshijian4"))
                        item.fahuoshijian4 = (emp["fahuoshijian4"].ToString());
                    if (emp.Contains("chicunjizhongliang5"))
                        item.chicunjizhongliang5 = (emp["chicunjizhongliang5"].ToString());
                    if (emp.Contains("pansongwanglu5"))
                        item.pansongwanglu5 = (emp["pansongwanglu5"].ToString());
                    if (emp.Contains("sijixingming5"))
                        item.sijixingming5 = (emp["sijixingming5"].ToString());
                    if (emp.Contains("chepaihao5"))
                        item.chepaihao5 = (emp["chepaihao5"].AsString);
                    if (emp.Contains("yewuyuan5"))
                        item.yewuyuan5 = (emp["yewuyuan5"].AsString);
                    if (emp.Contains("qianshouren5"))
                        item.qianshouren5 = (emp["qianshouren5"].AsString);
                    if (emp.Contains("Input_Date"))
                        item.Input_Date = (emp["Input_Date"].AsString);
                    if (emp.Contains("yundanhao"))
                        item.yundanhao = (emp["yundanhao"].AsString);
                    if (emp.Contains("comment"))
                        item.comment = (emp["comment"].AsString);

                    #endregion

                    ClaimReport_Server.Add(item);
                }

                return ClaimReport_Server;

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
                return null;

                throw ex;
            }
            #endregion
        }
        public List<clsTipsinfo> findTip_Server(string kettext, string start_time, string end_time)
        {

            #region Read  database info server
            try
            {
                List<clsTipsinfo> ClaimReport_Server = new List<clsTipsinfo>();

                MongoServer server = MongoServer.Create(connectionString);
                MongoDatabase db1 = server.GetDatabase(DB_NAME);
                MongoCollection collection1 = db1.GetCollection("XJ_logistics_TJ_Tip");
                MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("XJ_logistics_TJ_Tip");

                var query = new QueryDocument("shifazhan", kettext);
                //    var dd = Query.And(Query.EQ("jigoudaima", jigoudaima), Query.EQ("fapiaoleixing", fapiaoleixing));//同时满足多个条件
                if (kettext != "")
                {
                    foreach (BsonDocument emp in employees.Find(query))
                    {
                        clsTipsinfo item = new clsTipsinfo();

                        #region 数据
                        if (emp.Contains("_id"))
                            item.tip_id = (emp["_id"].ToString());
                        if (emp.Contains("shifazhan"))
                            item.shifazhan = (emp["shifazhan"].ToString());
                        if (emp.Contains("mudizhan"))
                            item.mudizhan = (emp["mudizhan"].ToString());
                        if (emp.Contains("yuandanhao"))
                            item.yuandanhao = (emp["yuandanhao"].ToString());
                        if (emp.Contains("jianshu"))
                            item.jianshu = (emp["jianshu"].AsString);
                        if (emp.Contains("shouhuoren"))
                            item.shouhuoren = (emp["shouhuoren"].AsString);
                        if (emp.Contains("dianhua"))
                            item.dianhua = (emp["dianhua"].AsString);

                        if (emp.Contains("Input_Date"))
                            item.Input_Date = (emp["Input_Date"].AsString);

                        #endregion

                        ClaimReport_Server.Add(item);
                    }
                    query = new QueryDocument("mudizhan", kettext);
                    foreach (BsonDocument emp in employees.Find(query))
                    {
                        clsTipsinfo item = new clsTipsinfo();

                        #region 数据
                        if (emp.Contains("_id"))
                            item.tip_id = (emp["_id"].ToString());
                        if (emp.Contains("shifazhan"))
                            item.shifazhan = (emp["shifazhan"].ToString());
                        if (emp.Contains("mudizhan"))
                            item.mudizhan = (emp["mudizhan"].ToString());
                        if (emp.Contains("yuandanhao"))
                            item.yuandanhao = (emp["yuandanhao"].ToString());
                        if (emp.Contains("jianshu"))
                            item.jianshu = (emp["jianshu"].AsString);
                        if (emp.Contains("shouhuoren"))
                            item.shouhuoren = (emp["shouhuoren"].AsString);
                        if (emp.Contains("dianhua"))
                            item.dianhua = (emp["dianhua"].AsString);

                        if (emp.Contains("Input_Date"))
                            item.Input_Date = (emp["Input_Date"].AsString);

                        #endregion

                        ClaimReport_Server.Add(item);
                    }
                    query = new QueryDocument("yuandanhao", kettext);
                    foreach (BsonDocument emp in employees.Find(query))
                    {
                        clsTipsinfo item = new clsTipsinfo();

                        #region 数据
                        if (emp.Contains("_id"))
                            item.tip_id = (emp["_id"].ToString());
                        if (emp.Contains("shifazhan"))
                            item.shifazhan = (emp["shifazhan"].ToString());
                        if (emp.Contains("mudizhan"))
                            item.mudizhan = (emp["mudizhan"].ToString());
                        if (emp.Contains("yuandanhao"))
                            item.yuandanhao = (emp["yuandanhao"].ToString());
                        if (emp.Contains("jianshu"))
                            item.jianshu = (emp["jianshu"].AsString);
                        if (emp.Contains("shouhuoren"))
                            item.shouhuoren = (emp["shouhuoren"].AsString);
                        if (emp.Contains("dianhua"))
                            item.dianhua = (emp["dianhua"].AsString);

                        if (emp.Contains("Input_Date"))
                            item.Input_Date = (emp["Input_Date"].AsString);

                        #endregion

                        ClaimReport_Server.Add(item);
                    }
                    query = new QueryDocument("dianhua", kettext);
                    foreach (BsonDocument emp in employees.Find(query))
                    {
                        clsTipsinfo item = new clsTipsinfo();

                        #region 数据
                        if (emp.Contains("_id"))
                            item.tip_id = (emp["_id"].ToString());
                        if (emp.Contains("shifazhan"))
                            item.shifazhan = (emp["shifazhan"].ToString());
                        if (emp.Contains("mudizhan"))
                            item.mudizhan = (emp["mudizhan"].ToString());
                        if (emp.Contains("yuandanhao"))
                            item.yuandanhao = (emp["yuandanhao"].ToString());
                        if (emp.Contains("jianshu"))
                            item.jianshu = (emp["jianshu"].AsString);
                        if (emp.Contains("shouhuoren"))
                            item.shouhuoren = (emp["shouhuoren"].AsString);
                        if (emp.Contains("dianhua"))
                            item.dianhua = (emp["dianhua"].AsString);

                        if (emp.Contains("Input_Date"))
                            item.Input_Date = (emp["Input_Date"].AsString);

                        #endregion
                        ClaimReport_Server.Add(item);
                    }
                }
                var query1 = Query.And(Query.GTE("Input_Date", start_time.Replace("/", "")), Query.LTE("Input_Date", end_time.Replace("/", "")));
                foreach (BsonDocument emp in employees.Find(query1))
                {
                    clsTipsinfo item = new clsTipsinfo();

                    #region 数据
                    if (emp.Contains("_id"))
                        item.tip_id = (emp["_id"].ToString());
                    if (emp.Contains("shifazhan"))
                        item.shifazhan = (emp["shifazhan"].ToString());
                    if (emp.Contains("mudizhan"))
                        item.mudizhan = (emp["mudizhan"].ToString());
                    if (emp.Contains("yuandanhao"))
                        item.yuandanhao = (emp["yuandanhao"].ToString());
                    if (emp.Contains("jianshu"))
                        item.jianshu = (emp["jianshu"].AsString);
                    if (emp.Contains("shouhuoren"))
                        item.shouhuoren = (emp["shouhuoren"].AsString);
                    if (emp.Contains("dianhua"))
                        item.dianhua = (emp["dianhua"].AsString);

                    if (emp.Contains("Input_Date"))
                        item.Input_Date = (emp["Input_Date"].AsString);

                    #endregion

                    ClaimReport_Server.Add(item);
                }


                return ClaimReport_Server;

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
                return null;

                throw ex;
            }
            #endregion
        }

        public void delete_OrderServer(clsOrderDatabaseinfo AddMAPResult)
        {

            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("XJ_logistics_TJ_Order");
            MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("XJ_logistics_TJ_Order");

            if (AddMAPResult.Order_id != null && AddMAPResult.Order_id.Length > 0)
            {
                IMongoQuery query = Query.EQ("_id", new ObjectId(AddMAPResult.Order_id));
                collection1.Remove(query);
            }
        }
        public void delete_TipServer(clsTipsinfo AddMAPResult)
        {

            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db1 = server.GetDatabase(DB_NAME);
            MongoCollection collection1 = db1.GetCollection("XJ_logistics_TJ_Tip");
            MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("XJ_logistics_TJ_Tip");

            if (AddMAPResult.tip_id != null && AddMAPResult.tip_id.Length > 0)
            {
                IMongoQuery query = Query.EQ("_id", new ObjectId(AddMAPResult.tip_id));
                collection1.Remove(query);
            }
        }


        #region 打印
        //标签量设置

        public void printTIP(clsAllnew BusinessHelp, List<clsOrderDatabaseinfo> FilterOrderResults)
        {

            //  if (this.checkBox1.Checked == true)
            {
                List<clsTipsinfo> FilterTIPResults = new List<clsTipsinfo>();
                foreach (clsOrderDatabaseinfo temp in FilterOrderResults)
                {
                    for (int i = 1; i <= Convert.ToInt32(temp.shijijianshu2); i++)
                    {
                        clsTipsinfo item = new clsTipsinfo();
                        FilterTIPResults = new List<clsTipsinfo>();

                        item.yuandanhao = temp.yundanhao;
                        item.shifazhan = temp.dizhi;
                        item.mudizhan = temp.daodaidi2;
                        item.jianshu = temp.shijijianshu2 + "-" + i.ToString(); //temp.shijijianshu2;
                        item.shouhuoren = temp.quhuoren3;
                        item.dianhua = temp.dianhua2;
                        item.Input_Date = DateTime.Now.ToString("yyyyMMdd");
                        FilterTIPResults.Add(item);
                        Run2(FilterTIPResults);

                    }
                }



            }
        }
        //单独打印

        public void PrintTIP(clsTipsinfo model, int jianshutotal)
        {
            for (int j = 1; j <= jianshutotal; j++)
            {
                List<clsTipsinfo> FilterOrderResults = new List<clsTipsinfo>();

                clsTipsinfo item = new clsTipsinfo();
                item = model;

                item.jianshu = jianshutotal + "-" + j.ToString();

                FilterOrderResults.Add(item);

                Run2(FilterOrderResults);
            }
        }
        #region 标签打印
        public void Run(List<clsOrderDatabaseinfo> FilterOrderResults)
        {
            LocalReport report = new LocalReport();
            //report.ReportPath = @"C:\mysteap\work_office\ProjectOut\天津信捷物流\TJ_XinJielogistics\TJ_XinJielogistics\Report1.rdlc";
            report.ReportPath = Application.StartupPath + "\\Report1.rdlc";

            //report.DataSources.Add(
            //   new ReportDataSource("Sales", FilterOrderResults));
            report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", FilterOrderResults));

    


            Export(report);
            m_currentPageIndex = 0;

            Print(orderprint, 944, 598);
        }
        public void Run2(List<clsTipsinfo> FilterOrderResults)
        {
            LocalReport report = new LocalReport();
            // report.ReportPath = @"C:\mysteap\work_office\ProjectOut\天津信捷物流\TJ_XinJielogistics\TJ_XinJielogistics\Report1.rdlc";
            report.ReportPath = Application.StartupPath + "\\Report2.rdlc";
            //report.DataSources.Add(
            //   new ReportDataSource("Sales", FilterOrderResults));
            report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", FilterOrderResults));

            Export2(report);
            m_currentPageIndex = 0;
            Print(tisprint, 393, 393);
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

            //string deviceInfo =
            //"<DeviceInfo>" +
            //"  <OutputFormat>EMF</OutputFormat>" +
            //"  <PageWidth>18.9in</PageWidth>" +
            //"  <PageHeight>11.42in</PageHeight>" +
            //"  <MarginTop>0.25in</MarginTop>" +
            //"  <MarginLeft>0.25in</MarginLeft>" +
            //"  <MarginRight>0.25in</MarginRight>" +
            //"  <MarginBottom>0.25in</MarginBottom>" +
            //"</DeviceInfo>";

            string deviceInfo =
        "<DeviceInfo>" +
        "  <OutputFormat>EMF</OutputFormat>" +
        "  <PageWidth>11cm</PageWidth>" +
        "  <PageHeight>11cm</PageHeight>" +
        "  <MarginTop>0.1cm</MarginTop>" +
        "  <MarginLeft>0.1cm</MarginLeft>" +
        "  <MarginRight>0.1cm</MarginRight>" +
        "  <MarginBottom>0.1cm</MarginBottom>" +
        "</DeviceInfo>";

       //     string deviceInfo =
       //"<DeviceInfo>" +
       //"  <OutputFormat>EMF</OutputFormat>" +
       //"  <PageWidth>39.3in</PageWidth>" +
       //"  <PageHeight>39.3in</PageHeight>" +
       //"  <MarginTop>0.4in</MarginTop>" +
       //"  <MarginLeft>0.4in</MarginLeft>" +
       //"  <MarginRight>0.4in</MarginRight>" +
       //"  <MarginBottom>0.4in</MarginBottom>" +
       //"</DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }
        public void Print(string defaultPrinterName, int lenpage, int withpage)
        {

            m_currentPageIndex = 0;
            if (m_streams == null || m_streams.Count == 0)
                return;
            //声明PrintDocument对象用于数据的打印

            PrintDocument printDoc = new PrintDocument();

            //指定需要使用的打印机的名称，使用空字符串""来指定默认打印机

            if (defaultPrinterName == "" || defaultPrinterName == null)
                defaultPrinterName = printDoc.PrinterSettings.PrinterName;

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
            printDoc.DefaultPageSettings.Landscape = false;
            //大小
            printDoc.DefaultPageSettings.PaperSize = new PaperSize("Custom", lenpage, withpage);


            printDoc.Print();

        }
        private Stream CreateStream(string name, string fileNameExtension,

    Encoding encoding, string mimeType, bool willSeek)
        {

            //如果需要将报表输出的数据保存为文件，请使用FileStream对象。

            Stream stream = new MemoryStream();

            m_streams.Add(stream);

            return stream;

        }
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);
            //ev.PageSettings.Landscape = true;
           //
            StringFormat SF = new StringFormat();
            SF.LineAlignment = StringAlignment.Center;
            SF.Alignment = StringAlignment.Center;
            //RectangleF rect = new RectangleF(0, 0, ev.PageBounds.Width, ev.Graphics.MeasureString("Authors Informations", new Font("Times New Roman", 20)).Height);    //其中e.PageBounds属性表示页面全部区域的矩形区域
            //ev.Graphics.MeasureString(string,Font).Heighte.Graphics.DrawString("Authors Informations",new Font("Times New Roman",20),Brushes.Black,rect,SF);
            float left = ev.PageSettings.Margins.Left;//打印区域的左边界
            float top = ev.PageSettings.Margins.Top;//打印区域的上边界
            float width = ev.PageSettings.PaperSize.Width - left - ev.PageSettings.Margins.Right;//计算出有效打印区域的宽度
            float height = ev.PageSettings.PaperSize.Height - top - ev.PageSettings.Margins.Bottom;//计算出有效打印区域的高度

           
            ////
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }
        public void Export(LocalReport report)
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

            //string deviceInfo =
            //"<DeviceInfo>" +
            //"  <OutputFormat>EMF</OutputFormat>" +
            //"  <PageWidth>18.9in</PageWidth>" +
            //"  <PageHeight>11.42in</PageHeight>" +
            //"  <MarginTop>0.25in</MarginTop>" +
            //"  <MarginLeft>0.25in</MarginLeft>" +
            //"  <MarginRight>0.25in</MarginRight>" +
            //"  <MarginBottom>0.25in</MarginBottom>" +
            //"</DeviceInfo>";

            string deviceInfo =
        "<DeviceInfo>" +
        "  <OutputFormat>EMF</OutputFormat>" +
        "  <PageWidth>15.2cm</PageWidth>" +
        "  <PageHeight>26cm</PageHeight>" +
        "  <MarginTop>0.1cm</MarginTop>" +
        "  <MarginLeft>0.1cm</MarginLeft>" +
        "  <MarginRight>0.1cm</MarginRight>" +
        "  <MarginBottom>0.1cm</MarginBottom>" +
        "</DeviceInfo>";


            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }
        #endregion

        //获取打印机名称
        private void getUserPint()
        {
            try
            {
                RegistryKey rkLocalMachine = Registry.LocalMachine;
                RegistryKey rkSoftWare = rkLocalMachine.OpenSubKey(clsConstant.RegEdit_Key_SoftWare);
                RegistryKey rkAmdape2e = rkSoftWare.OpenSubKey(clsConstant.RegEdit_Key_AMDAPE2E);
                if (rkAmdape2e != null)
                {
                    orderprint = clsCommHelp.encryptString(clsCommHelp.NullToString(rkAmdape2e.GetValue(clsConstant.RegEdit_Key_Order)));
                    tisprint = clsCommHelp.encryptString(clsCommHelp.NullToString(rkAmdape2e.GetValue(clsConstant.RegEdit_Key_Tips)));

                    rkAmdape2e.Close();
                }
                rkSoftWare.Close();
                rkLocalMachine.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ex;
            }
        }

        #endregion
    }
}
