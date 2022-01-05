using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameCatalog.api.Validations
{
    public class GenericError
    {
        public GenericError(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
