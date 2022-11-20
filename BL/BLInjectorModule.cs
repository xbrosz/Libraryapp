using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Module = Autofac.Module;
using AutoMapper;
using Infrastructure.EFCore;
using BL.Services.Services;
using BL.Services.IServices;
using BL.QueryObjects.QueryObjects;
using BL.QueryObjects.IQueryObject;
using BL.DTOs.Author;
using BL.DTOs.Branch;
using BL.DTOs.User;
using BL.DTOs;

namespace BL
{
    public class BLInjectorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping)))
                .As<IMapper>()
                .SingleInstance();

            // ToDo register remaining query objects
            builder.RegisterType<AuthorQueryObject>().As<IQueryObject<AuthorFilterDto, AuthorDto>>().InstancePerDependency();
            builder.RegisterType<BranchQueryObject>().As<IQueryObject<BranchFilterDto, BranchDto>>().InstancePerDependency();
            builder.RegisterType<UserQueryObject>().As<IQueryObject<UserFilterDto, UserDetailDto>>().InstancePerDependency();
            builder.RegisterType<BookQueryObject>().As<IQueryObject<BookFilterDto, BookGridDto>>().InstancePerDependency();
            builder.RegisterType<BookPrintQueryObject>().As<IQueryObject<BookPrintFilterDto, BookPrintDto>>().InstancePerDependency();

            // ToDo register remaining services
            builder.RegisterType<AuthorService>().As<IAuthorService>().InstancePerDependency();
            builder.RegisterType<BranchService>().As<IBranchService>().InstancePerDependency();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerDependency();
            builder.RegisterType<ReservationService>().As<IReservationService>().InstancePerDependency();
            builder.RegisterType<BookService>().As<IBookService>().InstancePerDependency();
            builder.RegisterType<BookPrintService>().As<IBookPrintService>().InstancePerDependency();

            
        }
    }
}
