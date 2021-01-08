using System;
using System.Collections.Generic;

namespace CSharp.Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Customer { get; set; }
        public List<string> Items { get; set; }
    }
}