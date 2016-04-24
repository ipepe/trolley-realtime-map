using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace Trollley_Realtime_Map
{
    class Notification
    {
        public static void SimpleToast(string header, string body = "")
        {
            // template to load for showing Toast Notification
            var template = "<toast><visual><binding template =\'ToastGeneric\'><text>" + header + "</text><text>" + body + "</text></binding></visual></toast>";

            // load the template as XML document
            var xmlDocument = new Windows.Data.Xml.Dom.XmlDocument();
            xmlDocument.LoadXml(template);

            // create the toast notification and show to user
            var toastNotification = new ToastNotification(xmlDocument);
            var notification = ToastNotificationManager.CreateToastNotifier();
            notification.Show(toastNotification);
        }
}
}
