using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Application.Services.Authtentication;
using BuberDinner.Contracts.Authentication;
using Mapster;

namespace BuberDinner.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<LoginRequest, LoginQuery>(); // redundante
            config.NewConfig<RegisterRequest, RegisterCommand>(); // redundante
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Id, src => src.User.Id)
                .Map(dest => dest, src => src.User);
        }
    }
}