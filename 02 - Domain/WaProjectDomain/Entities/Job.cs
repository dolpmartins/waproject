using System;

namespace WaProject.Domain.Entities
{
    public class Job : BaseEntity
    {
        public string Descricao { get; set; }
        public bool Completed { get; set; }
        public DateTime? CompletedDate { get; set; }

    }
}
