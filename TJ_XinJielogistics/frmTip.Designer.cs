﻿namespace TJ_XinJielogistics
{
    partial class frmTip
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.noReplaceRadioButton = new System.Windows.Forms.RadioButton();
            this.replaceRadioButton = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.checkColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.shifazhanColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mudizhan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yuandanhao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jianshu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shouhuoren = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dianhua = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Input_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.editColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.deleteColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btdown = new System.Windows.Forms.Button();
            this.newButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.keywordTextBox = new System.Windows.Forms.TextBox();
            this.btfind = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pager1 = new TJ_XinJielogistics.Pager();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.dataGridView);
            this.groupBox1.Controls.Add(this.btdown);
            this.groupBox1.Controls.Add(this.newButton);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.pager1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(672, 367);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.noReplaceRadioButton);
            this.groupBox3.Controls.Add(this.replaceRadioButton);
            this.groupBox3.Location = new System.Drawing.Point(249, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(175, 41);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            // 
            // noReplaceRadioButton
            // 
            this.noReplaceRadioButton.AutoSize = true;
            this.noReplaceRadioButton.Location = new System.Drawing.Point(99, 15);
            this.noReplaceRadioButton.Name = "noReplaceRadioButton";
            this.noReplaceRadioButton.Size = new System.Drawing.Size(73, 17);
            this.noReplaceRadioButton.TabIndex = 19;
            this.noReplaceRadioButton.Text = "手动打印";
            this.noReplaceRadioButton.UseVisualStyleBackColor = true;
            // 
            // replaceRadioButton
            // 
            this.replaceRadioButton.AutoSize = true;
            this.replaceRadioButton.Checked = true;
            this.replaceRadioButton.Location = new System.Drawing.Point(8, 15);
            this.replaceRadioButton.Name = "replaceRadioButton";
            this.replaceRadioButton.Size = new System.Drawing.Size(73, 17);
            this.replaceRadioButton.TabIndex = 20;
            this.replaceRadioButton.TabStop = true;
            this.replaceRadioButton.Text = "自动打印";
            this.replaceRadioButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(6, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 54);
            this.label2.TabIndex = 21;
            this.label2.Text = "标签列表";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(430, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 20;
            this.button1.Text = "打印";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkColumn1,
            this.shifazhanColumn1,
            this.mudizhan,
            this.yuandanhao,
            this.jianshu,
            this.shouhuoren,
            this.dianhua,
            this.Input_Date,
            this.editColumn1,
            this.deleteColumn1});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView.Location = new System.Drawing.Point(0, 145);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowTemplate.Height = 50;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(672, 174);
            this.dataGridView.TabIndex = 18;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            // 
            // checkColumn1
            // 
            this.checkColumn1.HeaderText = "选择";
            this.checkColumn1.Name = "checkColumn1";
            this.checkColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.checkColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // shifazhanColumn1
            // 
            this.shifazhanColumn1.DataPropertyName = "shifazhan";
            this.shifazhanColumn1.HeaderText = "始发站";
            this.shifazhanColumn1.Name = "shifazhanColumn1";
            // 
            // mudizhan
            // 
            this.mudizhan.DataPropertyName = "mudizhan";
            this.mudizhan.HeaderText = "目的站";
            this.mudizhan.Name = "mudizhan";
            // 
            // yuandanhao
            // 
            this.yuandanhao.DataPropertyName = "yuandanhao";
            this.yuandanhao.HeaderText = "原单号";
            this.yuandanhao.Name = "yuandanhao";
            // 
            // jianshu
            // 
            this.jianshu.DataPropertyName = "jianshu";
            this.jianshu.HeaderText = "件数";
            this.jianshu.Name = "jianshu";
            // 
            // shouhuoren
            // 
            this.shouhuoren.DataPropertyName = "shouhuoren";
            this.shouhuoren.HeaderText = "收货人";
            this.shouhuoren.Name = "shouhuoren";
            // 
            // dianhua
            // 
            this.dianhua.DataPropertyName = "dianhua";
            this.dianhua.HeaderText = "电话";
            this.dianhua.Name = "dianhua";
            // 
            // Input_Date
            // 
            this.Input_Date.DataPropertyName = "Input_Date";
            this.Input_Date.HeaderText = "时间";
            this.Input_Date.Name = "Input_Date";
            // 
            // editColumn1
            // 
            this.editColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2, 12, 2, 12);
            this.editColumn1.DefaultCellStyle = dataGridViewCellStyle1;
            this.editColumn1.HeaderText = "";
            this.editColumn1.Name = "editColumn1";
            this.editColumn1.Text = "编辑";
            this.editColumn1.UseColumnTextForButtonValue = true;
            this.editColumn1.Width = 60;
            // 
            // deleteColumn1
            // 
            this.deleteColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(2, 12, 2, 12);
            this.deleteColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            this.deleteColumn1.HeaderText = "";
            this.deleteColumn1.Name = "deleteColumn1";
            this.deleteColumn1.Text = "删除";
            this.deleteColumn1.ToolTipText = "删除";
            this.deleteColumn1.UseColumnTextForButtonValue = true;
            this.deleteColumn1.Width = 60;
            // 
            // btdown
            // 
            this.btdown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btdown.Location = new System.Drawing.Point(510, 19);
            this.btdown.Name = "btdown";
            this.btdown.Size = new System.Drawing.Size(75, 25);
            this.btdown.TabIndex = 17;
            this.btdown.Text = "下载Excel";
            this.btdown.UseVisualStyleBackColor = true;
            this.btdown.Click += new System.EventHandler(this.btdown_Click);
            // 
            // newButton
            // 
            this.newButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newButton.Location = new System.Drawing.Point(591, 19);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(75, 25);
            this.newButton.TabIndex = 16;
            this.newButton.Text = "新建";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dateTimePicker2);
            this.groupBox2.Controls.Add(this.dateTimePicker1);
            this.groupBox2.Controls.Add(this.keywordTextBox);
            this.groupBox2.Controls.Add(this.btfind);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(0, 50);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(666, 89);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(314, 12);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(143, 20);
            this.dateTimePicker2.TabIndex = 12;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(88, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(143, 20);
            this.dateTimePicker1.TabIndex = 11;
            // 
            // keywordTextBox
            // 
            this.keywordTextBox.Location = new System.Drawing.Point(64, 52);
            this.keywordTextBox.Name = "keywordTextBox";
            this.keywordTextBox.Size = new System.Drawing.Size(441, 20);
            this.keywordTextBox.TabIndex = 10;
            // 
            // btfind
            // 
            this.btfind.Location = new System.Drawing.Point(509, 12);
            this.btfind.Name = "btfind";
            this.btfind.Size = new System.Drawing.Size(75, 25);
            this.btfind.TabIndex = 9;
            this.btfind.Text = "查找";
            this.btfind.UseVisualStyleBackColor = true;
            this.btfind.Click += new System.EventHandler(this.btfind_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(253, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "结束日期";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "关键字";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "开始日期";
            // 
            // pager1
            // 
            this.pager1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pager1.AutoSize = true;
            this.pager1.Location = new System.Drawing.Point(6, 325);
            this.pager1.Name = "pager1";
            this.pager1.NMax = 0;
            this.pager1.PageCount = 0;
            this.pager1.PageCurrent = 0;
            this.pager1.PageSize = 5;
            this.pager1.Size = new System.Drawing.Size(666, 30);
            this.pager1.TabIndex = 19;
            // 
            // frmTip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 367);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmTip";
            this.Text = "运单录取";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button btdown;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox keywordTextBox;
        private System.Windows.Forms.Button btfind;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private Pager pager1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton noReplaceRadioButton;
        private System.Windows.Forms.RadioButton replaceRadioButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn shifazhanColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn mudizhan;
        private System.Windows.Forms.DataGridViewTextBoxColumn yuandanhao;
        private System.Windows.Forms.DataGridViewTextBoxColumn jianshu;
        private System.Windows.Forms.DataGridViewTextBoxColumn shouhuoren;
        private System.Windows.Forms.DataGridViewTextBoxColumn dianhua;
        private System.Windows.Forms.DataGridViewTextBoxColumn Input_Date;
        private System.Windows.Forms.DataGridViewButtonColumn editColumn1;
        private System.Windows.Forms.DataGridViewButtonColumn deleteColumn1;
    }
}