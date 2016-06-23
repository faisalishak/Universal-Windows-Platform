using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace Sqlite_UWP.Common
{
    class ShowToast
    {
        public ShowToast(int timeoutInSeconds, string text)
        {
            var toastTemplate = ToastTemplateType.ToastImageAndText01;
            var toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
            var toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(text));

            ////Andreas Hammar 2014-10-08 08:39: note! does not work on windows phone
            //var toastImageAttributes = toastXml.GetElementsByTagName("image");
            //((XmlElement)toastImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/jay_transparent.png");
            //((XmlElement)toastImageAttributes[0]).SetAttribute("alt", "jay");

            var toast = new ToastNotification(toastXml);
            if (timeoutInSeconds >= 0)
                toast.ExpirationTime = DateTime.Now.AddSeconds(timeoutInSeconds);

            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
