using GradBootcamp_Tolgahaninan.Models;

namespace GradBootcamp_Tolgahaninan.Data.Redis.IRedis
{
    public interface IRedisHelper // Redis Helper Interface
    {
        Task<bool> SetKeyAsync(string Key, string value);
        Task<string> GetKeyAsync(string Key);
    }
}
