using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication4.Models;

namespace JopOffere.Models
{
    public class ApplyForJob
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime ApplyDate { get; set; }

        public int JobId { get; set; }

        public string userId { get; set; }
        public virtual Jop job { get; set; }
        public virtual ApplicationUser user { get; set; }

    }
}