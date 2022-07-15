
using GradBootcamp_Tolgahaninan.Data.Redis.IRedis;
using GradBootcamp_Tolgahaninan.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace GradBootcamp_Tolgahaninan.Data.Redis
{

      public class RedisHelper : IRedisHelper // Redis helper class implements redis helper interface
      {
          private readonly IConnectionMultiplexer _redisConnectionMultiplexer; // To create connection multiplexer instance
          private  IDatabase _redisDbAsync; // To create database instance

        public RedisHelper(IConnectionMultiplexer redisConnectionMultiplexer) // Constructor for dependency injection
          {
              _redisConnectionMultiplexer = redisConnectionMultiplexer; 
              _redisDbAsync = _redisConnectionMultiplexer.GetDatabase();   // To assign IDatabase instance to redis database
        }
          public async Task<string> GetKeyAsync(string Key) // To get key with given paremeter
          {

              var value = await _redisDbAsync.StringGetAsync(Key); // Assigned database to reach redis keys


              return await Task.FromResult((string)value);
          }

          public async Task<bool> SetKeyAsync(string Key,  string value) // To set key with given paremeters
        {

              return await _redisDbAsync.StringSetAsync(Key, value);// Assigned database to set redis keys
        }


      }

}
