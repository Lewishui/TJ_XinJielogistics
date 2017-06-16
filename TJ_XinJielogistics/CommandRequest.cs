using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TJ_XinJielogistics
{
    public enum CommandRequestEnum { TripList, EditTripDays, ScenicList, CustomerTripList, EditCustomerTripDays }

    public class CommandRequestEventArgs : EventArgs
    {
        private CommandRequestEnum command;

        public int ModelId { get; set; }

        public CommandRequestEventArgs(CommandRequestEnum command, int modelId = 0)
        {
            this.command = command;
            this.ModelId = modelId;
        }
        public CommandRequestEnum Command
        {
            get { return command; }
        }
    }
}
