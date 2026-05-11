using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ClassStore.Domain.Entities
{
    public class CartLine
    {
       public Class Class { get; set; }
       public int Quantity { get; set; }
    }

    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }

        public void AddItem(Class myclass, int myquantity)
        {
            CartLine line = lineCollection
                .Where(c => c.Class.ClassID == myclass.ClassID)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Class = myclass,
                    Quantity = myquantity
                });
            }
            else
            {
                line.Quantity += myquantity;
            }
        }
            public void RemoveLine(Class myclass)
            {
                lineCollection.RemoveAll(l => l.Class.ClassID == myclass.ClassID);
            }

            public decimal ComputeTotalValue()
            {
                return lineCollection.Sum(e => e.Class.Price * e.Quantity);
            }
            public void Clear()
            {
                lineCollection.Clear();
            }
    }
}
