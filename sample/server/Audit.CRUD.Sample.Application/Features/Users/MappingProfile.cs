using Audit.CRUD.Sample.Application.Features.Users.Handlers;
using Audit.CRUD.Sample.Domain.Users;
using AutoMapper;

namespace Audit.CRUD.Sample.Application.Features.Users
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<UserCreate.Command, User>();
		}
	}
}
