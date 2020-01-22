using System;

namespace DAL.Domain
{
    public class Menu : BaseDomain
    {
        public int InstitutionId { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
