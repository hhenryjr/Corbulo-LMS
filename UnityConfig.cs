using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;

using Sabio.Web.Core.Injection;
using System.Web.Http;
using Sabio.Web.Services.Tests;
using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sabio.Web.Models;
using System.Data.Entity;
using System.Web;
using Microsoft.Owin.Security;
using Sabio.Web.Services.Interfaces;
using Sabio.Web.Services;
using Sabio.Web.Classes;


namespace Sabio.Web
{
    public static class UnityConfig
    {
        private static UnityContainer _container;

        public static UnityContainer GetContainer()
        {
            return _container;
        }

        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers                       

            //  use this way if you only want to register a single instance of any interface
            //container.RegisterType<IEmployeeService, EmployeeService>();

            //  use this way if you want to bind multiple types of the same interface -  see http://stackoverflow.com/a/27588327
            var resolver = container.Resolve<Func<IEmployeeService>>();

            // Register mappings for the IRepository interface to appropriate concrete types
            //  see http://stackoverflow.com/a/27902226 for example on getting named instances to work

            //allow read from Web.config
            container.RegisterType<ISiteConfig, SiteConfig>();

            container.RegisterType<IEmployeeService, TestEmployeeService>("EmployeeService");
            container.RegisterType<IEmployeeService, TestBossEmployeeService>("BossService");

            container.RegisterType<ITestService, TestService>();

            container.RegisterType<IAddressService, AddressService>();
            container.RegisterType<IAttendanceService, AttendanceService>();
            container.RegisterType<IBlogService, BlogService>();
            container.RegisterType<ICommentsService, CommentsServices>();
            container.RegisterType<ICoursesService, CoursesService>();
            container.RegisterType<IEthnicitiesService, EthnicitiesService>();
            container.RegisterType<IFaqService, FaqService>();
            container.RegisterType<IFileService, FileService>();
            container.RegisterType<IInstructorServices, InstructorsServices>();
            container.RegisterType<IMeetingService, MeetingsService>();
            container.RegisterType<IMessagingService, MessagingService>();
            container.RegisterType<IModuleService, ModuleService>();
            container.RegisterType<ISectionModuleService, SectionModuleService>();
            container.RegisterType<IOfficeHourServices, OfficeHourServices>();
            container.RegisterType<ISectionService, SectionService>();
            container.RegisterType<ITagsService, TagsService>();
            container.RegisterType<ITrackCourseService, TrackCourseService>();
            container.RegisterType<ITrackService, TrackService>();
            container.RegisterType<IPaymentService, PaymentService>();
            container.RegisterType<IUserDataService, UserDataService>();
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            container.RegisterType<IWikiContentService, WikiContentService>();
            container.RegisterType<IWikiSpaceService, WikiSpaceService>();
            container.RegisterType<IWikiService, WikiService>();
            container.RegisterType<IUserOnboardService, UserOnboardService>();
            container.RegisterType<ITestimonialsService, TestimonialsService>();
            container.RegisterType<UserManager<ApplicationUser>>();
            container.RegisterType<DbContext, ApplicationDbContext>();
            container.RegisterType<ApplicationUserManager>();
           
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            //  this line is needed so that the resolver can be used by api controllers 
            config.DependencyResolver = new UnityResolver(container);

            _container = container;
        }
    }
}
