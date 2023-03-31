using API_MEI.DTOs;
using API_MEI.Models;
using AutoMapper;

namespace API_MEI.Mappings
{
    public class EntitiesToDTOMappingProfile : Profile
    {
        public EntitiesToDTOMappingProfile()
        {
            CreateMap<Trabalho, TrabalhoDTO>().ReverseMap();
            CreateMap<Trabalho, TrabalhoDTOwID>().ReverseMap(); //Criei isto para colocar o HttpPut com ID
            CreateMap<Juri, JuriDTO>().ReverseMap();
            CreateMap<Alunos, AlunosDTO>().ReverseMap();
            CreateMap<Docentes, DocentesDTO>().ReverseMap();
            CreateMap<Especialistas, EspecialistasDTO>().ReverseMap();
            CreateMap<Empresas, EmpresasDTO>().ReverseMap();
            CreateMap<Membros, MembrosDTO>().ReverseMap();  
        }
    }
}
