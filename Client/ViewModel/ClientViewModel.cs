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
using System.Windows.Threading;
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
                        await SendClient(cw.Client);
                    }
                }));
            }
        }
        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(async (selectedItem) =>
                {
                    Model.Client? client = selectedItem as Model.Client;
                    if (client == null) return;
                    if (MessageBox.Show("Вы действительно хотите удалить элемент?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                    {
                       await RemoveClient(client);
                    }
                }));
            }
        }
        
        private async Task RemoveClient(Model.Client client)
        {
            JsonContent content = JsonContent.Create(client);
            using var request = new HttpRequestMessage(HttpMethod.Delete, "http://127.0.0.1:8888/connection/");
            request.Headers.Add("table", "client");
            request.Content = content;
            HttpResponseMessage response = await httpClient.SendAsync(request);
            string responseText = await response.Content.ReadAsStringAsync();
            if (responseText == "Yes") Load();
        }
        private async Task SendClient(Model.Client client)
        {
            try
            {
                JsonContent content = JsonContent.Create(client);
                content.Headers.Add("table", "client");
                using var response = await httpClient.PostAsync("http://127.0.0.1:8888/connection/", content);
                Load();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private ObservableCollection<Model.Client>? clients;
        public ObservableCollection<Model.Client>? Clients
        {
            get { return clients; }
            set
            {
                clients = value;
                OnPropertyChanged(nameof(Clients));
            }
        }
        private Model.Client? selectedClient;
        public Model.Client? SelectedClient
        {
            get => selectedClient;
            set
            {
                selectedClient = value;
                OnPropertyChanged(nameof(SelectedClient));
            }
        }
        public ClientViewModel()
        {
            httpClient = new HttpClient();
            Load();
        }
        private void Load()
        {
            Clients = null;
            Task<ObservableCollection<Model.Client>> task = Task.Run(() => getClients());
            Clients = task.Result;
        }
        private async Task<ObservableCollection<Model.Client>> getClients()
        {
            StringContent content = new StringContent("getClients");
            using var request = new HttpRequestMessage(HttpMethod.Get, "http://127.0.0.1:8888/connection/");
            request.Headers.Add("table", "client");
            request.Content = content;
            HttpResponseMessage response = await httpClient.SendAsync(request);
            string responseText = await response.Content.ReadAsStringAsync();
            List<Model.Client> clients = JsonSerializer.Deserialize<List<Model.Client>>(responseText)!;
            return new ObservableCollection<Model.Client>(clients);
        }

    }
}
