using System;

namespace Common.StandardInfrastructure.Interface
{
    public interface IBaseEntityModel
    {
        Guid Id { get; set; }
        bool IsDelete { get; set; }
    }
}
