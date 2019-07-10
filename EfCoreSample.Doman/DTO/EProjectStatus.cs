
using System.ComponentModel;

namespace EfCoreSample.Doman.DTO
{
    public enum EProjectStatus
    {       
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
