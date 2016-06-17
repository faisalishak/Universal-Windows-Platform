using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace AdaptiveCode
{
    class DeviceFamilyTrigger : StateTriggerBase
    {
        private string _deviceFamily;

        //Public property 
        public string DeviceFamily
        {

            get
            {
                return _deviceFamily;
            }
            set
            {
                _deviceFamily = value;
                var qualifiers = Windows.ApplicationModel.Resources.Core.ResourceContext.
                   GetForCurrentView().QualifierValues;

                if (qualifiers.ContainsKey("DeviceFamily"))
                    SetActive(qualifiers["DeviceFamily"] == _deviceFamily);
                else
                    SetActive(false);
            }
        }

    }
}
