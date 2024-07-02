// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude Nikula
// Created          : 03-13-2024
//
// Last Modified By : Claude Nikula
// Last Modified On : 05-08-2024
// ***********************************************************************
// <copyright file="CommonModel.cs" company="MITJobTracker">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>
//     Common Model Class. This properties are used in all the tables.
// </summary>
// ***********************************************************************
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
            IsDeleted = false;
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

        [Comment(" IsDeleted, Is record deleted.")]
        [DataMember]
        public bool IsDeleted { get; set; }

        [Comment("External foreign.")]
        [DataMember]
        [MaxLength(150)]
        public string? DeleteByID { get; set; }

        [Comment("Date and Time record was create.")]
        [DataMember]
        public DateTime? DeleteDate { get; set; }
    }
}
