using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using umbraco.BusinessLogic;

namespace NewsletterStudioContrib.Factory
{

    public class BootstrapNewsletterStudio : ApplicationBase
    {
        public BootstrapNewsletterStudio()
        {
            // This code will run on startup since the class inheris from ApplicationBase. 
            // ApplicationBase is an Umbraco base class that will execute its constructor app start up.

            NewsletterStudio.Infrastucture.GlobalFactory.SetGlobalFactory(new CustomGlobalFactory());
        }

    }
}
