using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTT
{
    internal class Products
    {
        
        public Products(uint ID, string Name, double Price, int Quantity, uint ID_category)
        {
            this.ID = ID;
            this.Name = Name;
            this.Price = Price;
            this.Quantity = Quantity;
            this.ID_category = ID_category;
        }

        

        private uint ID;
        private string Name;
        private double Price;
        private int Quantity;
        private uint ID_category;



        public uint ProductID
        {
            get { return ID; }
            set { ID = value; }
        }

       public string ProductName
        {
            get { return Name; }
            set { Name = value; }
        }

        public double ProductPrice
        {
            get { return Price; }
            set { Price = value; }
        }

        public int ProductQuantity
        {
            get { return Quantity; }
            set { Quantity = value; }
        }

        public uint ProductID_category
        {
            get { return ID_category;}
            set { ID_category = value;}
        }

    }
}
