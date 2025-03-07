using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using ClientProject.Infrastructure.Command;
using ClientProject.Model;
using ClientProject.View;

namespace ClientProject.ViewModel
{
    internal class ClientViewModel:BaseViewModel
    {
        private HttpClient httpClient;
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(async obj =>
                {
                    ClientWindow cw = new ClientWindow(new Model.Client());
                    if (cw.ShowDialog() == true)
                    {
                        await Task.Run(()=>SendClient(cw.Client));
                    }
                }));
            }
        }
        private async Task SendClient(Model.Client client)
        {
            JsonContent content = JsonContent.Create(client);
            content.Headers.Add("table", "client");
            using var response = await httpClient.PostAsync("http://127.0.0.1:8888/connection/", content);
            string responseText = await response.Content.ReadAsStringAsync();
            MessageBox.Show(responseText);
        }
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
