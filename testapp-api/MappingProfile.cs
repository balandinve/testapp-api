using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testapp_api.Models;

namespace testapp_api
{
    public class MappingProfile: AutoMapper.Profile
    {
        public override string ProfileName
        {
            get { return "MappingProfile"; }
        }
        public MappingProfile()
        {
            CreateMap<StoreUserRegisterView, StoreUser>();
            CreateMap<StoreUser, StoreUserView>();

            CreateMap<Product, ProductView>();
        }
    }
}
