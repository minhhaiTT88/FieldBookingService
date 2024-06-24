using ObjectInfo;

namespace Business.Interfaces
{
    public interface ICustomerBL
    {
        CustomerInfo GetByPhoneNumber(string requestId, string phoneNumber);
    }
}
