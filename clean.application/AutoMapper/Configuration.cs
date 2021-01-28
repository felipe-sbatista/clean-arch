using System;
using AutoMapper;

namespace clean.application.AutoMapper
{
    public static class Configuration
    {
        public static MapperConfiguration RegisterConfigs()
        {
            return new MapperConfiguration(cf =>
            {
                cf.AllowNullCollections = true;
                cf.AllowNullDestinationValues = true;
                cf.DisableConstructorMapping();

                cf.ForAllMaps((m, e) => e.MaxDepth(1));

                cf.AddProfile(new DtoProfile());
            });
        }
    }
}
