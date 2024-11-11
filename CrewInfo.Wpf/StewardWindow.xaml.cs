using CrewInfo.Core.Models;
using CrewInfo.Dto;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace CrewInfo.Wpf
{
    /// <summary>
    /// Логика взаимодействия для StewardWindow.xaml
    /// </summary>
    public partial class StewardWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public StewardWindow()
        {
            InitializeComponent();
            LoadStewards();
        }

        private async void LoadStewards()
        {
            try
            {
                var stewards = await GetAllStewards();
                StewardDataGrid.ItemsSource = stewards;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки стюартов: {ex.Message}");
            }
        }

        private async void SearchSteward_Click(object sender, RoutedEventArgs e)
        {
            var fullName = FullNameFilter.Text;
            var passportNumber = PassportNumberFilter.Text;
            var mobileNumber = MobileNumberFilter.Text;

            try
            {
                if (fullName == "" && passportNumber == "" && mobileNumber == "")
                {
                    LoadStewards();
                    return;
                }
                var steward = await GetSteward(fullName, passportNumber, mobileNumber);
                StewardDataGrid.ItemsSource = new List<Steward> { steward };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения информации о стюарте: {ex.Message}");
            }
        }

        private async void UpdateSteward_Click(object sender, RoutedEventArgs e)
        {
            if (StewardDataGrid.SelectedItem is Steward selectedSteward)
            {
                var request = new StewardRequest
                (
                    selectedSteward.FullName,
                    selectedSteward.ResidenceAddress,
                    selectedSteward.MobileNumber,
                    selectedSteward.PassportNumber,
                    selectedSteward.PassportIssueDate,
                    selectedSteward.PassportIssuedBy,
                    selectedSteward.BirthDate,
                    selectedSteward.RegistrationAddress,
                    selectedSteward.InnNumber,
                    selectedSteward.InsurancePolicyNumber,
                    selectedSteward.MaritalStatus,
                    selectedSteward.CrewNumber
                );
                try
                {
                    await UpdateSteward(selectedSteward.StewardId, request);
                    LoadStewards();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка обновления данных о стюарте: {ex.Message}");
                }
            }
        }

        private async void DeleteSteward_Click(object sender, RoutedEventArgs e)
        {
            if (StewardDataGrid.SelectedItem is Steward selectedSteward)
            {
                try
                {
                    await DeleteSteward(selectedSteward.StewardId);
                    LoadStewards();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления данных о стюарте: {ex.Message}");
                }
            }
        }

        private async void AddStewardButton_Click(object sender, RoutedEventArgs e)
        {
            var addStewardWindow = new AddStewardWindow();
            addStewardWindow.Show();
        }

        private async Task<List<Steward>> GetAllStewards()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7249/api/steward/get-all-stewards");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Steward>>(content);
        }

        private async Task<Steward> GetSteward(string fullName = null, string passportNumber = null, string mobileNumber = null)
        {
            var url = "https://localhost:7249/api/steward/get-steward";

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
                return JsonConvert.DeserializeObject<Steward>(content);
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                var errorResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(errorContent);

                throw new Exception(errorResponse.ContainsKey("error") ? errorResponse["error"] : "Unknown error occurred");
            }
        }

        private async Task UpdateSteward(Guid stewardId, StewardRequest request)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"https://localhost:7249/api/steward/update-steward/{stewardId}", request);
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

        private async Task DeleteSteward(Guid stewardId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"https://localhost:7249/api/steward/delete-steward/{stewardId}");
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

