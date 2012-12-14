using System;
using System.Collections.Generic;

namespace EFTest.Models
{
    public class GEOGRAPHICAL_INFORMATION
    {
        public DateTime UPDATE_DATE_TIME { get; set; }
        public decimal LATITUDE { get; set; }
        public decimal LONGITUDE { get; set; }
        public decimal SPEED { get; set; }
        public int ORIENTATION { get; set; }
    }
}
