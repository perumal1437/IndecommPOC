using IndecommPOC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndecommPOC.BL
{
    interface IBLProperties
    {

        List<ViewModelProperties> GetPropertiesfromService();
        string SubmitRequestToService(string url);
        void UpdateProperty(ViewModelProperties requestProperty);
        void SaveProperty(ViewModelProperties requestProperty);
        void DeleteProperty(ViewModelProperties requestProperty);
    }
}
