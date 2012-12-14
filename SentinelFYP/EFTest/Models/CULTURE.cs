using System;
using System.Collections.Generic;

namespace EFTest.Models
{
    public class CULTURE
    {
        public CULTURE()
        {
            this.USERs = new List<USER>();
        }

        public Guid CULTURE_KEY { get; set; }
        public string REGION_CULTURE_CODE { get; set; }
        public string LANGUAGE_CULTURE_CODE { get; set; }
        public virtual ICollection<USER> USERs { get; set; }
    }
}
