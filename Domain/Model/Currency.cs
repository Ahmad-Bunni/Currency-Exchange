using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model
{
    public class Currency
    {
        public string Logo { get; set; }
        public double Rate { get; set; }
        public string Amount { get; set; } = "0.00";
        public string Abbreviation { get; set; }
        public bool IsBase { get; set; }
    }
}
