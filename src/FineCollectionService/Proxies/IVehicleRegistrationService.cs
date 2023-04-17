using System;
using System.Collections.Generic;
using System.Linq;
using FineCollectionService.Models;
using System.Threading.Tasks;

namespace FineCollectionService.Proxies
{
    public interface IVehicleRegistrationService
    {
        public Task<VehicleInfo> GetVehicleInfo(string licenseNumber);


    }
}
