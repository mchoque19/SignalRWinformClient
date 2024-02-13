using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Device
    {
        private int id;
        private string name;
        private string mac;
        private string ip;
        private string version;
        private bool active;
        private DateTime date;


        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Mac { get => mac; set => mac = value; }
        public string Ip { get => ip; set => ip = value; }
        public string Version { get => version; set => version = value; }
        public bool Active { get => active; set => active = value; }
        public DateTime Date { get => date; set => date = value; }
    }
}
