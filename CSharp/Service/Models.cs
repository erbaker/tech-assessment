using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp.Service
{
    public class Models
    {
        public class CreateOrderModel
        {
            public string customer { get; set; }
            public List<string> items { get; set; }
        }

        public class UpdateOrderModel
        {
            public Guid id { get; set; }
            public List<string> items { get; set; }
        }
    }
}
