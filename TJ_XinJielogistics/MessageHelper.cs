using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TJ_XinJielogistics
{
    class MessageHelper
    {
        public static bool DeleteConfirm(string msg)
        {

            return MessageBox.Show(msg, "刪除确认", MessageBoxButtons.YesNo) == DialogResult.Yes;
        }

        public static DialogResult ErrorBox(string msg)
        {
            return MessageBox.Show(msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
