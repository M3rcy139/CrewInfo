using CrewInfo.Dto;
using System.Net.Http;
using System.Windows;
using System.Net.Http.Json;
using Newtonsoft.Json;


namespace CrewInfo.Wpf
{
    /// <summary>
    /// Логика взаимодействия для AddPilotWindow.xaml
    /// </summary>
    public partial class AddPilotWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public AddPilotWindow()
        {
            InitializeComponent();
        }

        private async void AddPilot_Click(object sender, RoutedEventArgs e)
        {
            // Валидация обязательных полей
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

            if (string.IsNullOrWhiteSpace(QualificationInput.Text))
            {
                MessageBox.Show("Поле 'Квалификация' обязательно для заполнения.");
                return;
            }

            if (string.IsNullOrWhiteSpace(LastTrainingLocationInput.Text))
            {
                MessageBox.Show("Поле 'Место последнего обучения' обязательно для заполнения.");
                return;
            }

            if (LastTrainingDateInput.SelectedDate == null || LastTrainingDateInput.SelectedDate > DateTime.Now)
            {
                MessageBox.Show("Введите корректную дату последнего обучения (не позже текущей даты).");
                return;
            }

            if (!int.TryParse(FlightHoursInput.Text, out var hours) || hours < 0)
            {
                MessageBox.Show("Введите корректное количество налетанных часов (целое число, не меньше 0).");
                return;
            }

            if (!int.TryParse(CrewNumberInput.Text, out var crewNumber) || crewNumber < 0)
            {
                MessageBox.Show("Введите корректный номер экипажа (целое число, не меньше 0).");
                return;
            }

            var request = new PilotRequest
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
                hours,
                QualificationInput.Text,
                LastTrainingLocationInput.Text,
                LastTrainingDateInput.SelectedDate?.ToUniversalTime() ?? DateTime.MinValue,
                crewNumber
            );

            try
            {
                await AddPilot(request);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при добавлении пилота: {ex.Message}");
            }
        }

        private async Task AddPilot(PilotRequest request)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:7249/api/pilot/add-pilot", request);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Пилот успешно добавлен!");
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
