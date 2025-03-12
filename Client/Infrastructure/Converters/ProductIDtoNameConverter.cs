using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ClientProject.Infrastructure.Converters
{
    public class ProductIDtoNameConverter : IValueConverter
    {
        private HttpClient httpClient = new HttpClient();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           return Task.Run(() => getFIO((int)value)).Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        private async Task<string> getFIO(int id)
        {
            StringContent content = new StringContent(id.ToString());
            using var request = new HttpRequestMessage(HttpMethod.Get, "http://127.0.0.1:8888/connection/");
            request.Headers.Add("table", "client");
            request.Content = content;
            HttpResponseMessage response = await httpClient.SendAsync(request);
            string responseText = await response.Content.ReadAsStringAsync();
            Model.Client client = JsonSerializer.Deserialize<Model.Client>(responseText)!;
            return client.Firstname + " " + client.Lastname + " " + client.Surname;
        }
    }
}
