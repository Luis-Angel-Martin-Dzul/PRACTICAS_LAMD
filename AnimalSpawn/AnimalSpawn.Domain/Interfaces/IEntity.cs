using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalSpawn.Domain.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
        bool Status { get; set; }
        DateTime CreateAt { get; set; }
        int CreatedBy { get; set; }
        DateTime? UpdateAt { get; set; }
        int? UpdateBy { get; set; }
    }
}
