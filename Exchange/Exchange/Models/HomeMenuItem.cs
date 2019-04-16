using System;
using System.Collections.Generic;
using System.Text;

namespace Exchange.Models
{
    public enum MenuItemType
    {
        Rates,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
