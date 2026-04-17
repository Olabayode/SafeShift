using AutoMapper;
using SafeShift.Models;
using SafeShift.Models.DTOs.Incidents;
using SafeShift.Models.DTOs.Inspections;
using SafeShift.Models.DTOs.Shifts;
using SafeShift.Models.DTOs.Users;

namespace SafeShift.BLL.Mappings;

public class SafeShiftMappingProfile : Profile
{
    public SafeShiftMappingProfile()
    {
        CreateMap<User, UserReadDto>();

        CreateMap<Incident, IncidentReadDto>();
        CreateMap<IncidentCreateDto, Incident>();
        CreateMap<IncidentUpdateDto, Incident>();

        CreateMap<Inspection, InspectionReadDto>();
        CreateMap<InspectionCreateDto, Inspection>();
        CreateMap<InspectionUpdateDto, Inspection>();

        CreateMap<Shift, ShiftReadDto>();
        CreateMap<ShiftCreateDto, Shift>();
        CreateMap<ShiftUpdateDto, Shift>();
    }
}
