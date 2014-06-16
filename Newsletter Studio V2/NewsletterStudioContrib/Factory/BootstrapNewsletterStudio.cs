using Umbraco.Core;

namespace NewsletterStudioContrib.Factory
{

    public class BootstrapNewsletterStudio : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            // This code will run on startup since the class inheris from ApplicationBase. 
            // ApplicationBase is an Umbraco base class that will execute its constructor app start up.

            NewsletterStudio.Infrastucture.GlobalFactory.SetGlobalFactory(new CustomGlobalFactory());
        }

    }
}
