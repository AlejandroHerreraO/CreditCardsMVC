using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TarjetasMVC.Models {
    public class Card {
        public String CardNumber { get; set; }
        public enum CardTypes {
            Visa = 1,
            MasterCard = 2,
            AmericanExpress = 3
        }
        public CardTypes CardType { get; set; }
        public int CardBalance { get; set; }
        public int CardLimit { get; set; }
        public int CustomerID { get; set; }

        public bool Valid() {
            switch(innerSum()) {
                case 10: if (CardType == CardTypes.Visa) return true;
                    break;
                case 15: if (CardType == CardTypes.MasterCard) return true;
                    break;
                case 20: if (CardType == CardTypes.AmericanExpress) return true;
                    break;
            }
            return false;
        }

        private int innerSum() {
            int r = Int32.Parse(CardNumber);
            int r1 = r / 1000;
            int r2 = (r % 1000) / 100;
            int r3 = (r % 100) / 10;
            int r4 = (r % 10);

            return r1 + r2 + r3 + r4;
        }
    }
}