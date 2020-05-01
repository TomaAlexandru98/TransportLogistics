using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Data;

namespace TransportLogistics.ApplicationLogic.Models
{
    public class Trailer : TrailerData
    {

        protected Trailer()
        {
        }
        public static Trailer Create(string model,int maximumWeightKg, int capacity, int numberAxles, decimal height, decimal width, decimal length)
        {
            if (string.IsNullOrEmpty(model))
                throw new ArgumentException("");
            if(maximumWeightKg <=0)
                throw new ArgumentException("");
            if (capacity <= 0)
                throw new ArgumentException("");
            if (numberAxles <= 0)
                throw new ArgumentException("");
            if (height <= 0)
                throw new ArgumentException("");
            if (width <= 0)
                throw new ArgumentException("");
            if (length <= 0)
                throw new ArgumentException("");
            var trailer = new Trailer
            {
                Id = Guid.NewGuid(),
                Model = model,
                MaximWeightKg = maximumWeightKg,
                Capacity = capacity,
                NumberAxles = numberAxles,
                Height = height,
                Width = width,
                Length = length
            };
            return trailer;
        }
       public void Modify(int aux)
        {
            
        }
    }
}
