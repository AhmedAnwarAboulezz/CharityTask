using System;

namespace Common.StandardInfrastructure.Interface
{
    public interface IDateModel
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }
}
