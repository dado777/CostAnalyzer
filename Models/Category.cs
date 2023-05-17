using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostAnalyzer.Models
{
    public class Category
    {
        [Key]
        public Int32 CategoryId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public String Title { get; set; }

        [Column(TypeName = "nvarchar(5)")]
        public String Icon { get; set; } = String.Empty;

        [Column(TypeName = "nvarchar(10)")]
        public String Type { get; set; } = "Расход";

        [NotMapped]
        public String? TitleWithIcon
        {
            get
            {
                return Icon + " " + Title;
            }
        }
    }
}