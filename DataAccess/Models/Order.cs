using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Order
    {
        private int id;
        private string softwareVers;
        private int madiCustNo;
        private int compNo;
        private int storeNo;
        private long transNo;
        private int termNo;
        private int operNo;
        private string operName;
        private string tbNum;
        private int pax;
        private string tableType;
        private DateTime startTime;


        public int Id { get => id; set => id = value; }
        public string SoftwareVers { get => softwareVers; set => softwareVers = value; }
        public int MadiCustNo { get => madiCustNo; set => madiCustNo = value; }
        public int CompNo { get => compNo; set => compNo = value; }
        public int StoreNo { get => storeNo; set => storeNo = value; }
        public long TransNo { get => transNo; set => transNo = value; }
        public int TermNo { get => termNo; set => termNo = value; }
        public int OperNo { get => operNo; set => operNo = value; }
        public string OperName { get => operName; set => operName = value; }
        public string TbNum { get => tbNum; set => tbNum = value; }
        public int Pax { get => pax; set => pax = value; }
        public string TableType { get => tableType; set => tableType = value; }
        public DateTime StartTime { get => startTime; set => startTime = value; }


        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}
