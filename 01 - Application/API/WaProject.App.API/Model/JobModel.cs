using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaProject.App.API.Model
{
    public class JobModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Completed { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
