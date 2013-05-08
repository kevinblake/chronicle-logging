using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chronicle.Logging.Business.Entities
{
    public class Log
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public string Thread { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string Level { get; set; }

        public string Logger { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength]
        public string Message { get; set; }

        public string Exception { get; set; }

        public string UserName { get; set; }

        public string Custom { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string Category { get; set; }
    }
}