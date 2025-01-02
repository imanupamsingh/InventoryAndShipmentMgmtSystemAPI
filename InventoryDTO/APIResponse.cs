using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryDTO
{
    public class APIResponseModel<T>
    {
        public int StatusCode { get; set; }
        public bool Status { get; set; }
        public string ResponseMessage { get; set; }
        public T Data { get; set; }
    }
}
