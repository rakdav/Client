using ClientProject.ViewModel;
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
    public class Client: INotifyPropertyChanged
    {
        private int clientid;
        [JsonPropertyName("clientid")]
        public int Clientid
        {
            get => clientid;
            set
            {
                clientid = value;
                OnProperyChanged(nameof(Clientid));
            }
        }
        private string firstname;
        [JsonPropertyName("firstname")]
        public string Firstname
        {
            get => firstname;
            set
            {
                firstname = value;
                OnProperyChanged(nameof(Firstname));
            }
        }
        private string surname;
        [JsonPropertyName("surname")]
        public string Surname
        {
            get => surname;
            set
            {
                surname = value;
                OnProperyChanged(nameof(Surname));
            }
        }
        private string lastname;
        [JsonPropertyName("lastname")]
        public string Lastname
        {
            get => lastname;
            set
            {
                lastname = value;
                OnProperyChanged(nameof(Lastname));
            }
        }
        private string company;
        [JsonPropertyName("company")]
        public string Company
        {
            get => company;
            set
            {
                company = value;
                OnProperyChanged(nameof(Company));
            }
        }
        private string phone;
        [JsonPropertyName("phone")]
        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                OnProperyChanged(nameof(Phone));
            }
        }
        private string city;
        [JsonPropertyName("city")]
        public string City
        {
            get => city;
            set
            {
                city = value;
                OnProperyChanged(nameof(City));
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
