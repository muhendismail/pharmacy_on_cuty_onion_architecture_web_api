namespace PharmacyOnDuty.Aplication.Exceptions
{
    public class RedisDataNotFoundException : Exception
    {
        public RedisDataNotFoundException(string message) : base(message)
        {
        }
    }
}
