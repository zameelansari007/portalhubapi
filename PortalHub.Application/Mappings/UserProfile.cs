using AutoMapper;
using PortalHub.Application.DTOs.Portal;
using PortalHub.Domain.Models.Portal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PortalHub.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(d => d.FullName,
                    o => o.MapFrom(s => $"{s.FirstName} {s.LastName}"));

            CreateMap<CreateUserDto, User>()
                .ForMember(d => d.UserId, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.MapFrom(_ => DateTime.UtcNow))
                .ForMember(d => d.IsActive, o => o.MapFrom(_ => true))
                .ForMember(d => d.IsEmailVerified, o => o.MapFrom(_ => false))
                .ForMember(d => d.IsPhoneVerified, o => o.MapFrom(_ => false))
                 //.ForMember(d => d.IsSupplier, o => o.MapFrom(_ => false))
                .ForMember(d => d.Password, o => o.Ignore())
                .ForMember(d => d.Role, o => o.Ignore())
                .ForMember(d => d.SupplierProfile, o => o.Ignore());

            CreateMap<UpdateUserDto, User>()
                .ForMember(d => d.UserId, o => o.Ignore())
                .ForMember(d => d.Email, o => o.Ignore())
                .ForMember(d => d.RoleId, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.Password, o => o.Ignore());
        }
    }
}
