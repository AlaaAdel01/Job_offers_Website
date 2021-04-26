using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JopOffere.Models
{
	public class Category
	{
		public int Id { get; set; }

		[Required]
		[Display(Name="Jop Type")]
		public string CategoryName { get; set; }
		[Required]
		[Display(Name ="Jop Description")]
		public string CategoryDescription { get; set; }
		public virtual ICollection<Jop> Jops { get; set; }
	}
}