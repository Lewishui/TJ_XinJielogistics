using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TJ_XinJielogistics
{
    public partial class frmWrokflow : Form
    {
        frmOrder orderControl;
        frmTip TipControl;
        string Useramin;
        string username;

        public frmWrokflow(string user,string isadmin)
        {
            InitializeComponent();
            Useramin = isadmin;
            username = user;
            InitUserControls();
        

        }

        private void bookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (orderControl == null)
            //{
            //    orderControl = new frmOrder();
            //    orderControl.Dock = DockStyle.Fill;
            //}
            //this.orderControl.BeginActive();
            //this.mainPanel.Controls.Clear();
            //this.mainPanel.Controls.Add(orderControl);


            if (orderControl == null)
            {
                orderControl = new frmOrder(username,Useramin);
                orderControl.Dock = DockStyle.Fill;
                orderControl.TopLevel = false; //重要的一个步骤
            }
            this.mainPanel.Controls.Clear();
            orderControl.Parent = this.mainPanel;
            orderControl.Show();
        }

        private void InitUserControls()
        {
            if (orderControl == null)
            {
                orderControl = new frmOrder(username, Useramin);
                orderControl.Dock = DockStyle.Fill;
                orderControl.TopLevel = false; //重要的一个步骤
            }
            this.mainPanel.Controls.Clear();
            orderControl.Parent = this.mainPanel;
            orderControl.Show();
        }

        private void tripsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TipControl == null)
            {
                TipControl = new frmTip(username, Useramin);
                TipControl.Dock = DockStyle.Fill;
                TipControl.TopLevel = false; //重要的一个步骤
            }
            this.mainPanel.Controls.Clear();
            TipControl.Parent = this.mainPanel;
            TipControl.Show();

            //if (TipControl == null)
            //{
            //    TipControl = new frmTip();
            //    TipControl.Dock = DockStyle.Fill;
            //    this.TipControl.CommandRequestEvent += new EventHandler(OnCommandRequest);

            //}
            //this.TipControl.BeginActive();
            //this.mainPanel.Controls.Clear();
            //this.mainPanel.Controls.Add(TipControl);

        }
        void OnCommandRequest(object sender, EventArgs e)
        {
            var commandEventArgs = e as CommandRequestEventArgs;
            //if (commandEventArgs.Command == CommandRequestEnum.EditTripDays)
            //{

            //    this.mainPanel.Controls.Clear();
            //    this.mainPanel.Controls.Add(editTripControl);
            //    editTripControl.ModelId = commandEventArgs.ModelId;
            //    editTripControl.BeginActive();
            //}

            //if (commandEventArgs.Command == CommandRequestEnum.TripList)
            //{
            //    tripsControl.BeginActive();
            //    this.mainPanel.Controls.Clear();
            //    this.mainPanel.Controls.Add(tripsControl);
            //}

            //if (commandEventArgs.Command == CommandRequestEnum.CustomerTripList)
            //{
            //    bookControl.BeginActive();
            //    this.mainPanel.Controls.Clear();
            //    this.mainPanel.Controls.Add(bookControl);
            //}


            //if (commandEventArgs.Command == CommandRequestEnum.EditCustomerTripDays)
            //{
            //    this.mainPanel.Controls.Clear();
            //    this.mainPanel.Controls.Add(editCustomerTripControl);
            //    editCustomerTripControl.ModelId = commandEventArgs.ModelId;
            //    editCustomerTripControl.BeginActive();
            //}



            Console.WriteLine("Sub1 receives the OnCommandRequest event.");
        }

    }
}
