using CommonLib;
using System.Text.Json;

namespace FieldBookingService.Memory
{
    public static class  DefErrorMem
    {
        private static Dictionary<string, string> errorDictionary = new Dictionary<string, string>();


        public static void LoadFromJson()
        {
            try
            {
                string jsonString = File.ReadAllText(CommonData.LinkFileDefError);
                var jsonData = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);

                if (jsonData != null)
                {
                    foreach (var item in jsonData)
                    {
                        errorDictionary[item.Key] = item.Value;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                // Handle file not found error
            }
            catch (JsonException)
            {
                // Handle JSON parsing error
            }
            // Add more catch blocks if needed
        }

        public static string GetErrorDesc(long errorCode)
        {
            if (errorDictionary.ContainsKey(errorCode.ToString()))
            {
                return errorDictionary[errorCode.ToString()];
            }
            else
            {
                return "Unknown error code";
            }
        }
    }
}
