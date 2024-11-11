using CrewInfo.Dto;
using Newtonsoft.Json;
using System.Net.Http;
using System.Windows;
using System.Net.Http.Json;

namespace CrewInfo.Wpf
{
    /// <summary>
    /// Логика взаимодействия для AddStewardWindow.xaml
    /// </summary>
    public partial class AddStewardWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public AddStewardWindow()
        {
            InitializeComponent();
        }

        private async void AddSteward_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullNameInput.Text))
            {
                MessageBox.Show("Поле 'ФИО' обязательно для заполнения.");
                return;
            }

            if (string.IsNullOrWhiteSpace(ResidenceAddressInput.Text))
            {
                MessageBox.Show("Поле 'Адрес проживания' обязательно для заполнения.");
                return;
            }

            if (string.IsNullOrWhiteSpace(MobileNumberInput.Text) || MobileNumberInput.Text.Length < 10)
            {
                MessageBox.Show("Введите корректный номер телефона (не менее 10 символов).");
                return;
            }

            if (string.IsNullOrWhiteSpace(PassportNumberInput.Text) || PassportNumberInput.Text.Length < 11)
            {
                MessageBox.Show("Поле 'Номер паспорта' обязательно для заполнения.");
                return;
            }

            if (PassportIssueDateInput.SelectedDate == null || PassportIssueDateInput.SelectedDate > DateTime.Now)
            {
                MessageBox.Show("Введите корректную дату выдачи паспорта.");
                return;
            }

            if (string.IsNullOrWhiteSpace(PassportIssuedByInput.Text))
            {
                MessageBox.Show("Поле 'Кем выдан паспорт' обязательно для заполнения.");
                return;
            }

            if (BirthDateInput.SelectedDate == null || BirthDateInput.SelectedDate > DateTime.Now.AddYears(-18))
            {
                MessageBox.Show("Введите корректную дату рождения (не моложе 18 лет).");
                return;
            }

            if (string.IsNullOrWhiteSpace(RegistrationAddressInput.Text))
            {
                MessageBox.Show("Поле 'Адрес регистрации' обязательно для заполнения..");
                return;
            }

            if (string.IsNullOrWhiteSpace(InnNumberInput.Text) || InnNumberInput.Text.Length != 12)
            {
                MessageBox.Show("ИНН должен состоять из 12 цифр.");
                return;
            }

            if (string.IsNullOrWhiteSpace(InsurancePolicyNumberInput.Text) || InsurancePolicyNumberInput.Text.Length < 12)
            {
                MessageBox.Show("Полис страхования должен содержать не менее 12 символов.");
                return;
            }

            if (string.IsNullOrWhiteSpace(MaritalStatusInput.Text))
            {
                MessageBox.Show("Поле 'Семейное положение' обязательно для заполнения.");
                return;
            }

            if (!int.TryParse(CrewNumberInput.Text, out var crewNumber) || crewNumber < 0)
            {
                MessageBox.Show("Введите корректный номер экипажа (целое число, не меньше 0).");
                return;
            }

            var request = new StewardRequest
            (
                FullNameInput.Text,
                ResidenceAddressInput.Text,
                MobileNumberInput.Text,
                PassportNumberInput.Text,
                PassportIssueDateInput.SelectedDate?.ToUniversalTime() ?? DateTime.MinValue,
                PassportIssuedByInput.Text,
                BirthDateInput.SelectedDate?.ToUniversalTime() ?? DateTime.MinValue,
                RegistrationAddressInput.Text,
                InnNumberInput.Text,
                InsurancePolicyNumberInput.Text,
                MaritalStatusInput.Text,
                crewNumber
            );

            try
            {
                await AddSteward(request);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при добавлении стюарта: {ex.Message}");
            }
        }

        private async Task AddSteward(StewardRequest request)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:7249/api/steward/add-steward", request);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Стюарт успешно добавлен!");
                Close();
                return;
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                var errorResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(errorContent);

                throw new Exception(errorResponse.ContainsKey("error") ? errorResponse["error"] : "Unknown error occurred");
            }
        }
    }
}
