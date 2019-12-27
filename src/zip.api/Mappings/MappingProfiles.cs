using AutoMapper;
using zip.api.Entities;
using zip.api.Requests;

namespace zip.api.Mappings
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateUserRequest, User>();
            CreateMap<CreateUserAccountRequest, Account>();
        }
    }
}
