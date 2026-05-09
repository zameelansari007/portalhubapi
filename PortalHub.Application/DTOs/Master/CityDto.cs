namespace PortalHub.Application.DTOs.Master
{
    public class CityDto
    {
        public int CityId { get; set; }

        public int StateId { get; set; }

        public string CityName { get; set; }
    }
    public class CreateCityDto
    {
        public int StateId { get; set; }

        public string CityName { get; set; }

        public bool IsActive { get; set; } = true;
    }
    public class UpdateCityDto
    {
        public int CityId { get; set; }

        public int StateId { get; set; }

        public string CityName { get; set; }

        public bool IsActive { get; set; }
    }
     public class CityResponseDto
    {
        public int CityId { get; set; }

        public int StateId { get; set; }

        public string CityName { get; set; }

        public bool IsActive { get; set; }
    }
}