using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using ClientProject.Model;

namespace ClientProject.ViewModel
{
    internal class ClientViewModel:BaseViewModel
    {
        private HttpClient httpClient;
        public ObservableCollection<Model.Client>? Clients { get; set; }
        public ClientViewModel()
        {
            httpClient = new HttpClient();
            Task.Run(() => Load());
        }
        private void Load()
        {
            Task<List<Model.Client>> task = getClients();
            List<Model.Client> clients = task.Result;
            Clients = new ObservableCollection<Model.Client>(clients);
        }
        private async Task<List<Model.Client>> getClients()
        {
            StringContent content = new StringContent("getClients");
            using var request = new HttpRequestMessage(HttpMethod.Get, "http://127.0.0.1:8888/connection/");
            request.Headers.Add("table", "client");
            request.Content = content;
            HttpResponseMessage response = await httpClient.SendAsync(request);
            string responseText = await response.Content.ReadAsStringAsync();
            List<Model.Client> clients = JsonSerializer.Deserialize<List<Model.Client>>(responseText)!;
            return clients;
        }

    }
}
