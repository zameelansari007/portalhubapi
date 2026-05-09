namespace PortalHub.Application.DTOs.Master
{
    public class StateDto
    {
        public int StateId { get; set; }

        public int CountryId { get; set; }

        public string StateName { get; set; }
    }

    public class CreateStateDto
    {
        public int CountryId { get; set; }

        public string StateName { get; set; }

        public bool IsActive { get; set; } = true;
    }
    public class UpdateStateDto
    {
        public int StateId { get; set; }

        public int CountryId { get; set; }

        public string StateName { get; set; }

        public bool IsActive { get; set; }
    }
    public class StateResponseDto
    {
        public int StateId { get; set; }

        public int CountryId { get; set; }

        public string StateName { get; set; }

        public bool IsActive { get; set; }
    }
}