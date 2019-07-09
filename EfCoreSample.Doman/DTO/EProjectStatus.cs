using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EfCoreSample.Doman.DTO
{
    public enum EProjectStatus
    {
        None,
        [Description("Pending")]
        Pending,
        [Description("InProgress")]
        InProgress,
        [Description("Completed")]
        Completed,
        [Description("Cancelled")]
        Cancelled
    }
}
