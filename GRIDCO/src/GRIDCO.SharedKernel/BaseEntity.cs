using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations.Schema;

namespace GRIDCO.SharedKernel
{
    // This can be modified to BaseEntity<TId> to support multiple key types (e.g. Guid)
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public string Created_By { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? Created_On { get; set; }
        public string Modified_By { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Modified_On { get; set; }
        public bool? Is_Active { get; set; } = true;
        public bool? Is_Deleted { get; set; } = false;

        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}