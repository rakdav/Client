using ClientProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientProject.ViewModel
{
    class SdelkaViewModel:BaseViewModel
    {
        private HttpClient httpClient;
        public SdelkaViewModel()
        {
            httpClient = new HttpClient();
            Load();
        }
        private ObservableCollection<Model.Sdelka>? sdelkas;
        public ObservableCollection<Model.Sdelka>? Sdelkas
        {
            get { return sdelkas; }
            set
            {
                sdelkas = value;
                OnPropertyChanged(nameof(Sdelkas));
            }
        }

        private void Load()
        {
            Sdelkas = null;
            Task<ObservableCollection<Model.Sdelka>> task = Task.Run(() => getSdelkas());
            Sdelkas = task.Result;
        }
        private async Task<ObservableCollection<Model.Sdelka>> getSdelkas()
        {
            StringContent content = new StringContent("getSdelkas");
            using var request = new HttpRequestMessage(HttpMethod.Get, "http://127.0.0.1:8888/connection/");
            request.Headers.Add("table", "sdelka");
            request.Content = content;
            HttpResponseMessage response = await httpClient.SendAsync(request);
            string responseText = await response.Content.ReadAsStringAsync();
            List<Model.Sdelka> list = JsonSerializer.Deserialize<List<Model.Sdelka>>(responseText)!;
            return new ObservableCollection<Model.Sdelka>(list);
        }
    }
}
