using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClientProject.Model
{
    public class Sdelka : INotifyPropertyChanged
    {
        [JsonPropertyName("sdelkaid")]
        private int sdelkaid;
        public int Sdelkaid
        {
            get => sdelkaid;
            set
            {
                sdelkaid = value;
                OnProperyChanged(nameof(Sdelkaid));
            }
        }
        [JsonPropertyName("count")]
        private int count;
        public int Count
        {
            get => count;
            set
            {
                count = value;
                OnProperyChanged(nameof(Count));
            }
        }
        [JsonPropertyName("data")]
        private DateOnly data;
        public DateOnly Data
        {
            get => data;
            set
            {
                data = value;
                OnProperyChanged(nameof(Data));
            }
        }
        [JsonPropertyName("productid")]
        private int productid;
        public int Productid
        {
            get => productid;
            set
            {
                productid = value;
                OnProperyChanged(nameof(Productid));
            }
        }
        [JsonPropertyName("clientid")]
        private int clientid;
        public int Clientid
        {
            get => clientid;
            set
            {
                clientid = value;
                OnProperyChanged(nameof(Clientid));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnProperyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
