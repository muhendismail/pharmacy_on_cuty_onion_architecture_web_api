using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using PharmacyOnDuty.Aplication.Interfaces.Repository;
using PharmacyOnDuty.Application.Cache;
using PharmacyOnDuty.Application.Dtos;
using PharmacyOnDuty.Application.External.Api;
using PharmacyOnDuty.Domain.Entities;
using System.Linq;


namespace PharmacyOnDuty.Application.Services
{
    public class PharmacyService
    {
        private readonly IExternalApi _externalApiService;
        private readonly ICacheService _cacheService;
        private readonly IPharmacyRepository _pharmacyRepository;
        public PharmacyService(IExternalApi externalApiService, IPharmacyRepository pharmacyRepository, ICacheService cacheService)
        {
            _pharmacyRepository = pharmacyRepository;
            _externalApiService = externalApiService;
            _cacheService = cacheService;
        }

        public async Task<List<Pharmacy>> GetPharmacyList(string city)
        {
            var list=await  _pharmacyRepository.GetAllAsync(x => 
            x.City.ToLower()==city.ToLower());
            return list?.ToList() ?? [];
        }

        public async Task SavePharmacyList(List<Pharmacy> pharmacies)
        {
            foreach (var item in pharmacies)
            {
                await _pharmacyRepository.AddAsync(item);
            }
        }

        public async Task DeletePharmacyList()
        {
            await _pharmacyRepository.DeleteAllAsync();
        }

        public async Task<List<Pharmacy>?> GetPharmacyFromExternalApiAndSaveToDB(string city)
        {
            var pharmacyList = await _externalApiService.Send<List<Pharmacy>>(HttpMethod.Get, $"/health/dutyPharmacy?il={city}");

            if (pharmacyList == null) return null;

            var newList = pharmacyList.Select(x => {
                x.City = city;
                return x;
            }).ToList() ?? [];

            await _cacheService.SetAsync<CacheData>(city.ToLower(), new CacheData
            {
                CityName = city,
                PharmacyLength = newList.Count,
            }, expiry: GetTimeSpanDiffBetweenToNowFromTarget());

            await _pharmacyRepository.DeleteAllWithCityParams(city);
            await SavePharmacyList(newList);

            return newList;
        }

        private TimeSpan GetTimeSpanDiffBetweenToNowFromTarget()
        {
            // Hedef saat (örneğin, günün 17:00'ı)
            int targetHour = 17;
            int targetMinute = 0;

            // Şu anki zaman ve hedef zaman
            DateTime now = DateTime.Now;
            //DateTime targetTime = new DateTime(now.Year, now.Month, now.Day, targetHour, targetMinute, 0);
            DateTime targetTime = DateTime.Now.AddMinutes(1);

            // Eğer hedef zaman geçmişse, hedef zamanı bir sonraki güne ayarla
            if (now > targetTime)
            {
                targetTime = targetTime.AddDays(1);
            }

            // Zaman farkını hesapla
            TimeSpan timeDifference = targetTime - now;

            return timeDifference;
        }
    }
}
