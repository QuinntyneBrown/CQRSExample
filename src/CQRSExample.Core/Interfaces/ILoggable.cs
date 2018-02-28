using System;

namespace CQRSExample.Core.Interfaces
{
    public interface ILoggable
    {
        string CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        string LastModifiedBy { get; set; }
        DateTime LastModifiedOn { get; set; }
    }
}
