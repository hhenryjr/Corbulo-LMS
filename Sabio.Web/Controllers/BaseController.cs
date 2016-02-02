using System.Web.Mvc;
using Sabio.Web.Models.ViewModels;
using Sabio.Web.Classes;
using Sabio.Web.Services;
using Sabio.Web.Domain;
using Sabio.Web.Services.Interfaces;
using Microsoft.Practices.Unity;

namespace Sabio.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public ISiteConfig SiteConfig { get; private set; }

        private UnityContainer _container;

        public BaseController()
        {
            _container = UnityConfig.GetContainer(); 

            SiteConfig = _container.Resolve<ISiteConfig>();
        }

        public BaseController(ISiteConfig config)
        {
            SiteConfig = config;

            _container = UnityConfig.GetContainer();
        }

        public new ViewResult View()
        {
            BaseViewModel model = GetViewModel<BaseViewModel>();
            return base.View(model);
        }

        public new ViewResult View(string viewString)
        {
            BaseViewModel model = GetViewModel<BaseViewModel>();
            return base.View(viewString, model);
        }

        public ViewResult View(BaseViewModel baseVM)
        {
            return base.View(DecorateViewModel(baseVM));
        }

        //Strongly Typed Layout Views
        //Sabio.layout.model needs to move out to layout
        protected T GetViewModel<T>() where T : BaseViewModel, new()
        {
            T model = new T();
            return DecorateViewModel(model);
        }

        protected T DecorateViewModel<T>(T model) where T : BaseViewModel
        {            
            IUserDataService svc = _container.Resolve<IUserDataService>();

            model.BrandName = SiteConfig.BrandName;
            model.BrandTagline = SiteConfig.BrandTagline;
            model.BrandLogo = SiteConfig.BrandLogo;
            model.BrandDescription = SiteConfig.BrandDescription;

            if (UserService.IsLoggedIn())
            {
                model.IsLoggedIn = true;
                model.CurrentUser = UserService.GetCurrentUser();                
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (controllerName != "UserOnboard" && model.UserProfile != null && model.UserProfile.OnboardStatus == 0)
                {
                    HttpContext.Response.Redirect("/user/onboard/" + model.UserProfile.Id);
                }
                model.UserProfile = svc.GetByUserId(model.CurrentUser.Id);
            }
            else
            {
                model.IsLoggedIn = false;
            }
            return model;
        }
    }
}