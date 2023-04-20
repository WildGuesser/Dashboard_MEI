using API_MEI.DTOs;
using API_MEI.DTOs.Tabela_Docente;
using API_MEI.Models;
using AutoMapper;

namespace API_MEI.Mappings
{
    public class EntitiesToDTOMappingProfile : Profile
    {
        public EntitiesToDTOMappingProfile()
        {
            CreateMap<Trabalhos, TrabalhoDTO>().ReverseMap();
            CreateMap<Juri, JuriDTO>().ReverseMap();
            CreateMap<Alunos, AlunosDTO>().ReverseMap();
            CreateMap<Docentes, DocentesDTO>().ReverseMap();
            CreateMap<Especialistas, EspecialistasDTO>().ReverseMap();
            CreateMap<Empresas, EmpresasDTO>().ReverseMap();
            CreateMap<Membros, MembrosDTO_D>().ReverseMap();
        
        }
    }
}
