using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharp.Models 
{
    [Table("Orders")]
    public class Orders 
    {
        public int Id { get; set; }
        public Customer customer { get; set; }
        public List<string> items { get; set; }
        public List<int> quantity { get; set; }
        public DateTime orderTimestamp { get; set; }
        public OrderStatus orderStatus { get; set; }
    }
}