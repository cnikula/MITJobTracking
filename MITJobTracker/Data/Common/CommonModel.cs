using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;

namespace MITJobTracker.Data.Common
{

    public class CommonModel
    {
        public CommonModel()
        {
            CreatedDate = DateTime.Now;
            CreatedById = "cnikula";
        }

        [Comment("External foreign.")]
        [DataMember]
        [MaxLength(150)]
        [Required]
        public string CreatedById { get; set; }

        [Comment("Date and Time record was create.")]
        [DataMember]
        public DateTime? CreatedDate { get; set; }

        [Comment("External foreign.")]
        [DataMember]
        [MaxLength(150)]
        public string? ModifiedById { get; set; }

        [Comment("Date and Time record was create.")]
        [DataMember]
        public DateTime? ModifiedDate { get; set; }

        [Comment("External foreign.")]
        [DataMember]
        [MaxLength(150)]
        public string? DeleteByID { get; set; }

        [Comment("Date and Time record was create.")]
        [DataMember]
        public DateTime? DeleteDate { get; set; }
    }
}
