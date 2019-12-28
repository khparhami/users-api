using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace zip.api.integration
{
    public abstract class ClientBase
    {
        public StringContent GetJsonStringContent(object request)
        {
            var json = JsonConvert.SerializeObject(request);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
