namespace FieldBookingService.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class ApiAuthorizeFunctionConfigAttribute : Attribute
    {
        public ApiAuthorizeFunctionConfigAttribute(string functionId)
        {
            FunctionId = functionId;
        }

        public string FunctionId { get; init; } = string.Empty;
    }
}
