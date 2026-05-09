namespace PortalHub.Application.DTOs.Master
{
    public class CountryDto
    {
        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public string CountryCode { get; set; }
    }

     public class CreateCountryDto
    {
        public string CountryName { get; set; }

        public string CountryCode { get; set; }

        public bool IsActive { get; set; }
    }

    public class UpdateCountryDto
    {
        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public string CountryCode { get; set; }

        public bool IsActive { get; set; }
    }

    public class CountryResponseDto
    {
        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public string CountryCode { get; set; }

        public bool IsActive { get; set; }
    }
}