using DataAccess.Base;
using DataAccess.Management;
using DataAccess.System;
using Microsoft.Extensions.DependencyInjection;
using ObjectInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Startups
{
    public class DAStartup
    {
        public static void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped(typeof(IDataAccessBase<>), typeof(DataAccessBase<>));
            services.AddScoped<IDataAccessBase<ProductInfo>, ProductDA>();
            services.AddScoped<IDataAccessBase<FieldInfo>, FieldDA>();
            services.AddScoped<IDataAccessBase<FieldBookingInfo>, FieldBookingDA>();
            services.AddScoped<IDataAccessBase<CustomerInfo>, CustomerDA>();
            services.AddScoped<IDataAccessBase<StaffInfo>, StaffDA>();
            services.AddScoped<IDataAccessBase<BillInfo>, BillDA>();
            services.AddScoped<IDataAccessBase<BillDetailInfo>, BillDetailDA>();
            services.AddScoped<IDataAccessBase<TimeSlotInfo>, TimeSlotDA>();
            services.AddScoped<IDataAccessBase<AllCodeInfo>, AllCodeDA>();

        }
    }
}
