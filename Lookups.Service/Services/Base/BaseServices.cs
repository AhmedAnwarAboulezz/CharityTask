using AutoMapper;
using Orders.DataAccess;
using Microsoft.Extensions.Localization;

namespace Orders.Service.Services.Base
{
    public class BaseServices
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMapper Mapper;
        protected readonly IStringLocalizer<Common.StandardInfrastructure.Resources.Common> CommonLocalize;
        protected readonly IStringLocalizer<Service.Resources.Orders> OrdersLocalize;

        public BaseServices(IMapper mapper, IUnitOfWork unitOfWork, IStringLocalizer<Service.Resources.Orders> ordersLocalize, IStringLocalizer<Common.StandardInfrastructure.Resources.Common> commonLocalize)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;

            OrdersLocalize = ordersLocalize;
            CommonLocalize = commonLocalize;
        }
    }
}
