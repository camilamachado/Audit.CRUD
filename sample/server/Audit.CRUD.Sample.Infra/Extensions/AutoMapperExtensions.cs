using AutoMapper;
using System;

namespace Audit.CRUD.Sample.Infra.Extensions
{
    public static class AutoMapperExtensions
    {
        public static void ConfigureProfiles(this object any, params Type[] types)
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                foreach (Type type in types)
                {
                    cfg.AddProfiles(type);
                }
            });
        }
    }
}
