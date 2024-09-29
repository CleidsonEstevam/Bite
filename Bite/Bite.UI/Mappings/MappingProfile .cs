using AutoMapper;
using Bite.Model;
using Bite.UI.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UsuarioVM, Usuario>()
            .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true)); // Definindo valores padrão
    }
}
