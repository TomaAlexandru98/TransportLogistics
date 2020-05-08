using System;
using System.Collections.Generic;
using System.Text;


namespace TransportLogistics.Model
{
    public class Trailer : DataEntity
    {
        public int MaximWeightKg { get; protected set; }
        public string Model { get; protected set; }
        public int Capacity { get; protected set; }
        public int NumberAxles { get; protected set; }
        public decimal Height { get; protected set; }
        public decimal Width { get; protected set; }
        public decimal Length { get; protected set; }

        protected Trailer()
        {
        }
        public static Trailer Create(string model, int maximumWeightKg, int capacity, int numberAxles, decimal height, decimal width, decimal length)
        {
            if (string.IsNullOrEmpty(model))
                throw new ArgumentException("");
            if (maximumWeightKg <= 0)
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

        public void Modify(Trailer trailer, string model, int maximumWeightKg, int capacity, int numberAxles, decimal height, decimal width, decimal length)
        {
            trailer.Model = model;
            trailer.MaximWeightKg = maximumWeightKg;
            trailer.Capacity = capacity;
            trailer.NumberAxles = numberAxles;
            trailer.Height = height;
            trailer.Width = width;
            trailer.Length = length;
            
        }
    }
}
