using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace JopOffere.Models
{
	public class Jop
	{
		public int Id { get; set; }
		[Required]
		[Display(Name ="Job Name")]
		public string JopTitle { get; set; }

		[AllowHtml]
		[Required]
		[Display(Name = "Job Description")]
		public string JopDescription { get; set; }

		[Display(Name = "Category Type")]
		public  int CategoryId { get; set; }

		public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }


        public virtual Category Category { get; set; }
	}
}