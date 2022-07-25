using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TarjetasMVC.Models {
    public class Data {
        public IList<Customer> customerList { get; set; } 
        public IList<Card> cardList { get; set; }
        public String Amount { get; set; }
        public String SelectedCard { get; set; }
    }
}