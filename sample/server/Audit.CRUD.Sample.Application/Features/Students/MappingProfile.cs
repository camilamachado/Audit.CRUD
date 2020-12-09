using Audit.CRUD.Sample.Application.Features.Students.Handlers;
using Audit.CRUD.Sample.Domain.Features.Students;
using AutoMapper;

namespace Audit.CRUD.Sample.Application.Features.Students
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<StudentCreate.Command, Student>();

			CreateMap<StudentUpdate.Command, Student>();
		}
	}
}
