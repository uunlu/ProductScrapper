using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductScrapper.AppServices
{
    public interface ISmtpClientSettings
    {
        SmtpDeliveryMethodEnum Smtp_DeliveryMethod { get; }
        string Smtp_PickupDirectoryLocation { get; }
        string Smtp_From { get; }
        string Smtp_Host { get; }
        int Smtp_Port { get; }
        bool Smtp_EnableSsl { get; }
        string Smtp_Username { get; }
        string Smtp_Password { get; }
    }

    public enum SmtpDeliveryMethodEnum
    {
        Network = 1,
        SpecifiedPickupDirectory = 2
    }
}
