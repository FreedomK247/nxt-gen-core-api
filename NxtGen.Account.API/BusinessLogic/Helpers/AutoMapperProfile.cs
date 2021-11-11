using AutoMapper;
using NxtGen.Account.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NxtGen.Account.API.BusinessLogic.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Data.Entities.Account, AccountResponseViewModel>();

            CreateMap<Data.Entities.Account, AuthenticateResponseViewModel>();

            CreateMap<RegisterRequestViewModel, Data.Entities.Account>();

            CreateMap<AccountRequestViewModel, Data.Entities.Account>();

            CreateMap<AccountUpdateRequestViewModel, Data.Entities.Account>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {

                        // ignore if nuyll & empty string properties.
                        if (prop == null) return false;

                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        // ignore a null role.
                        if (x.DestinationMember.Name == "Role" && src.Role == null) return false;

                        return true;

                    }));
        }

    }
}
