using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxModel
{
    public class Box
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime LastBoughtDate { get; set; }
        public Box(double width, double height)
        {
            Width = width;
            Height = height;
            ManufacturingDate = DateTime.Now;
            ExpirationDate = ManufacturingDate.AddSeconds(5);
            Quantity = 1;
        }
    }
}
