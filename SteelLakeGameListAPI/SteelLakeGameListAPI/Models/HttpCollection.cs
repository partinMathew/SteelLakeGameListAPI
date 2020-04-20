using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteelLakeGameListAPI.Models
{
    public class HttpCollection<T>
    {
        public List<T> Data { get; set; }
    }
}
