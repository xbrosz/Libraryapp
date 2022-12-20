using Autofac;
using AutoMapper;
using BL.DTOs;
using BL.DTOs.Author;
using BL.DTOs.BookGenre;
using BL.DTOs.Branch;
using BL.DTOs.Reservation;
using BL.DTOs.User;
using BL.Facades.Facades;
using BL.Facades.IFacades;
using BL.QueryObjects.IQueryObject;
using BL.QueryObjects.QueryObjects;
using BL.Services.IServices;
using BL.Services.Services;
using Module = Autofac.Module;

namespace BL
{
    public class BLInjectorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping))).As<IMapper>().SingleInstance();

            builder.RegisterType<AuthorQueryObject>().As<IQueryObject<AuthorFilterDto, AuthorDto>>().InstancePerDependency();
            builder.RegisterType<BranchQueryObject>().As<IQueryObject<BranchFilterDto, BranchDto>>().InstancePerDependency();
            builder.RegisterType<UserQueryObject>().As<IQueryObject<UserFilterDto, UserDetailDto>>().InstancePerDependency();
            builder.RegisterType<BookQueryObject>().As<IQueryObject<BookFilterDto, BookGridDto>>().InstancePerDependency();
            builder.RegisterType<BookPrintQueryObject>().As<IQueryObject<BookPrintFilterDto, BookPrintDto>>().InstancePerDependency();
            builder.RegisterType<RatingQueryObject>().As<IQueryObject<RatingFilterDto, RatingDto>>().InstancePerDependency();
            builder.RegisterType<ReservationQueryObject>().As<IQueryObject<ReservationFilterDto, ReservationsDto>>().InstancePerDependency();
            builder.RegisterType<BookGenreQueryObject>().As<IQueryObject<BookGenreFilterDto, BookGenreDto>>().InstancePerDependency();

            builder.RegisterType<AuthorService>().As<IAuthorService>().InstancePerDependency();
            builder.RegisterType<BranchService>().As<IBranchService>().InstancePerDependency();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerDependency();
            builder.RegisterType<ReservationService>().As<IReservationService>().InstancePerDependency();
            builder.RegisterType<BookService>().As<IBookService>().InstancePerDependency();
            builder.RegisterType<BookPrintService>().As<IBookPrintService>().InstancePerDependency();
            builder.RegisterType<RatingService>().As<IRatingService>().InstancePerDependency();

            builder.RegisterType<BookFacade>().As<IBookFacade>().InstancePerDependency();
            builder.RegisterType<UserFacade>().As<IUserFacade>().InstancePerDependency();
            builder.RegisterType<ReservationFacade>().As<IReservationFacade>().InstancePerDependency();
            builder.RegisterType<RatingFacade>().As<IRatingFacade>().InstancePerDependency();
        }
    }
}
