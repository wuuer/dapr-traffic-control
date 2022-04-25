using System;
using System.Collections.Generic;
using System.Linq;
using FineCollectionService.Models;
using System.Threading.Tasks;
using ServiceInvocation.DaprClient.Attributes;

namespace FineCollectionService.Proxies
{
    [DaprClientProxy("vehicleregistrationservice")]
    public interface IVehicleRegistrationService
    {
        [GetMethod("vehicleinfo/{licenseNumber}")]
        public Task<VehicleInfo> GetVehicleInfo([PathString]string licenseNumber);


    }
}
