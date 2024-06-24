
using Business.Interfaces;
using BusinessLayer.Base;
using DataAccess;
using DataAccess.Management;
using Microsoft.Extensions.DependencyInjection;
using ObjectInfo;


namespace Business.Startups
{
    public static class BLStartup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            //

            services.AddScoped<IBusinessBase<ProductInfo>, ProductBL>();
            services.AddScoped<IBusinessBase<BillInfo>, BillBL>();
            services.AddScoped<IBusinessBase<StaffInfo>, StaffBL>();
            services.AddScoped<IBusinessBase<FieldInfo>, FieldInfoBL>();
            services.AddScoped<IBusinessBase<FieldBookingInfo>, FieldBookingBL>();
            services.AddScoped<IBusinessBase<CustomerInfo>, CustomerBL>();
            services.AddScoped<IBusinessBase<TimeSlotInfo>, TimeSlotBL>();
        }
    }
}
