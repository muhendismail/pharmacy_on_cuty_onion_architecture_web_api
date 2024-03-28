namespace PharmacyOnDuty.Application.Dtos
{
    public class CacheData
    {
        public CacheData() { }
        public CacheData(string cityName, int pharmacyLength)
        {
            CityName = cityName;
            PharmacyLength = pharmacyLength;
        }

        public string CityName {  get; set; }
        public int PharmacyLength { get; set; }
    }
}
