using AutoMapper;
using EventPlanner.Domain.Entities;

namespace EventPlanner.Application.Certificates.Dtos;

public class CertificatesProfile : Profile
{
    public CertificatesProfile()
    {
        CreateMap<Certificate, CertificateDto>();
    }
}
