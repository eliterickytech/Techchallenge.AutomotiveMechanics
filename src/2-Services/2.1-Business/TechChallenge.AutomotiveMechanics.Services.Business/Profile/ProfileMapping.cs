using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Profile
{
    public class ProfileMapping : AutoMapper.Profile
    {
        public ProfileMapping()
        {
            CreateMap<Car, CarResult>().ReverseMap();
            CreateMap<CarInsertInput, Car>().ReverseMap();
            CreateMap<CarUpdateInput, Car>().ReverseMap();

            CreateMap<Model, ModelResult>().ReverseMap();
            CreateMap<ModelUpdateInput, Model>().ReverseMap();
            CreateMap<ModelInsertInput, Model>().ReverseMap();

            CreateMap<Manufacturer, ManufacturerResult>().ReverseMap();
            CreateMap<ManufacturerInsertInput, Manufacturer>().ReverseMap();
            CreateMap<ManufacturerUpdateInput, Manufacturer>().ReverseMap();

            CreateMap<Service, ServiceResult>().ReverseMap();
            CreateMap<ServiceInsertInput, Service>().ReverseMap();
            CreateMap<ServiceUpdateInput, Service>().ReverseMap();

            CreateMap<User, UserResult>().ReverseMap();
            CreateMap<UserRegisterInput, User>().ReverseMap();
        }
    }
}
