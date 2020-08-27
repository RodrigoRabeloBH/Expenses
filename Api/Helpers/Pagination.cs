using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Helpers
{
    public class Pagination<T>
    {
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }
        public double TotalPage { get; set; }

        public Pagination(IEnumerable<T> data, int index, int length)
        {
            Total = data.Count();
            Data = data.Skip((index - 1) * length).Take(length).ToList();
            TotalPage = Math.Ceiling((double)data.Count() / length);
        }
    }
}