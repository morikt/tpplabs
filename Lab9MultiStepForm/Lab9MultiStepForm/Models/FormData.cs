namespace Lab9MultiStepForm.Models
{
    public class FormData
    {
        // Страница 1
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public System.DateTime? BirthDate { get; set; }

        // Страница 2
        public string Email { get; set; }
        public string Phone { get; set; }

        // Страница 3
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string FlatNumber { get; set; }
    }
}
