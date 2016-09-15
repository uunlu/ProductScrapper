using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductScrapper.AppServices
{
    public interface IBootstrapperSettings : ISmtpClientSettings
    {
        string Database_Main_ConnectionString { get; }
        ApplicationTypeEnum ApplicationType { get; }
        //    <add key = "Web_Url" value="http://localhost:4128"/>
        //<add key = "Web_Action_Resset_Password" value="/Accounts/ResetPassword"/>
        //<add key = "Password_Hours_Expiration" value="24"/>
    }

    public enum ApplicationTypeEnum
    {
        WebApplication = 1,
        ConsoleApplication = 2
    }
}
