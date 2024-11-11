using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using CrewInfo.Core.Models;
using CrewInfo.Dto;
using Newtonsoft.Json;

namespace CrewInfo.Wpf
{
    /// <summary>
    /// Логика взаимодействия для PilotWindow.xaml
    /// </summary>
    public partial class PilotWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public PilotWindow()
        {
            InitializeComponent();
            LoadPilots();
        }

        private async void LoadPilots()
        {
            try
            {
                var pilots = await GetAllPilots();
                PilotDataGrid.ItemsSource = pilots;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки пилотов: {ex.Message}");
            }
        }

        private async void SearchPilot_Click(object sender, RoutedEventArgs e)
        {
            var fullName = FullNameFilter.Text;
            var passportNumber = PassportNumberFilter.Text;
            var mobileNumber = MobileNumberFilter.Text;

            try
            {
                if (fullName == "" && passportNumber == "" && mobileNumber == "")
                {
                    LoadPilots();
                    return;
                }
                var pilot = await GetPilot(fullName, passportNumber, mobileNumber);
                PilotDataGrid.ItemsSource = new List<Pilot> { pilot };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения информации о пилоте: {ex.Message}");
            }
        }

        private async void UpdatePilot_Click(object sender, RoutedEventArgs e)
        {
            if (PilotDataGrid.SelectedItem is Pilot selectedPilot)
            {
                var request = new PilotRequest
                (
                    selectedPilot.FullName,
                    selectedPilot.ResidenceAddress,
                    selectedPilot.MobileNumber,
                    selectedPilot.PassportNumber,
                    selectedPilot.PassportIssueDate,
                    selectedPilot.PassportIssuedBy,
                    selectedPilot.BirthDate,
                    selectedPilot.RegistrationAddress,
                    selectedPilot.InnNumber,
                    selectedPilot.InsurancePolicyNumber,
                    selectedPilot.MaritalStatus,
                    selectedPilot.FlightHours,
                    selectedPilot.Qualification,
                    selectedPilot.LastTrainingLocation,
                    selectedPilot.LastTrainingDate,
                    selectedPilot.CrewNumber
                );
                try
                {
                    await UpdatePilot(selectedPilot.PilotId, request);
                    LoadPilots();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка обновления данных о пилоте: {ex.Message}");
                }
            }
        }

        private async void DeletePilot_Click(object sender, RoutedEventArgs e)
        {
            if (PilotDataGrid.SelectedItem is Pilot selectedPilot)
            {
                try
                {
                    await DeletePilot(selectedPilot.PilotId);
                    LoadPilots();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления данных о пилоте: {ex.Message}");
                }
            }
        }

        private async void AddPilotButton_Click(object sender, RoutedEventArgs e)
        {
            var addPilotWindow = new AddPilotWindow();
            addPilotWindow.Show();
        }

        private async Task<List<Pilot>> GetAllPilots()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7249/api/pilot/get-all-pilots");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Pilot>>(content);
        }

        private async Task<Pilot> GetPilot(string fullName = null, string passportNumber = null, string mobileNumber = null)
        {
            var url = "https://localhost:7249/api/pilot/get-pilot";

            var parameters = new List<string>();
            if (!string.IsNullOrEmpty(fullName))
                parameters.Add($"fullName={Uri.EscapeDataString(fullName)}");
            if (!string.IsNullOrEmpty(passportNumber))
                parameters.Add($"passportNumber={Uri.EscapeDataString(passportNumber)}");
            if (!string.IsNullOrEmpty(mobileNumber))
                parameters.Add($"mobileNumber={Uri.EscapeDataString(mobileNumber)}");

            if (parameters.Any())
                url += "?" + string.Join("&", parameters);

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Pilot>(content);
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                var errorResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(errorContent);

                throw new Exception(errorResponse.ContainsKey("error") ? errorResponse["error"] : "Unknown error occurred");
            }
        }

        private async Task UpdatePilot(Guid pilotId, PilotRequest request)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"https://localhost:7249/api/pilot/update-pilot/{pilotId}", request);
            if (response.IsSuccessStatusCode)
            {
                return; 
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                var errorResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(errorContent);

                throw new Exception(errorResponse.ContainsKey("error") ? errorResponse["error"] : "Unknown error occurred");
            }
        }

        private async Task DeletePilot(Guid pilotId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"https://localhost:7249/api/pilot/delete-pilot/{pilotId}");
            if (response.IsSuccessStatusCode)
            {
                return; 
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                var errorResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(errorContent);

                throw new Exception(errorResponse.ContainsKey("error") ? errorResponse["error"] : "Unknown error occurred");
            }
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}

