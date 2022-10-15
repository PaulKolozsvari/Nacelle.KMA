using System;
using System.Linq;
using System.Threading.Tasks;
using MonkeyCache;
using Newtonsoft.Json;

namespace Nacelle.KMA.Core.Caching
{
    public class MonkeyCacheService : ICacheService
	{
		private readonly IBarrel _barrel;

		public MonkeyCacheService(IBarrel barrel)
		{
			_barrel = barrel;
		}

		public void ClearAll()
		{
			_barrel.EmptyAll();
		}

		public void Clear(string key)
		{
			_barrel.Empty(key);
		}

        public void ClearWhereKeyPrefix(string keyPrefix)
        {
            var allKeeys = _barrel.GetKeys();
            var matchingKeys = allKeeys.Where(x => x.StartsWith(keyPrefix, StringComparison.InvariantCultureIgnoreCase));
            foreach(var key in matchingKeys)
            {
                Clear(key);
            }
        }

        public void ClearExpired()
        {
            _barrel.EmptyExpired();
        }

		public T GetValue<T>(string key)
		{
			if (_barrel.IsExpired(key))
			{
				return default(T);
			}

			var json = _barrel.Get<string>(key);

            return string.IsNullOrWhiteSpace(json) ? default(T) : JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<T> GetOrUpdateValue<T>(string key, Func<Task<T>> fetchFunc, int expiryDays = 7, bool forceRefresh = false)
		{
			var data = string.Empty;

			if (!forceRefresh && !_barrel.IsExpired(key))
			{
				data = _barrel.Get<string>(key);
			}

			try
			{
                if (string.IsNullOrWhiteSpace(data) || forceRefresh)
                {
                    var res = await fetchFunc();
                    // Only persist non default value (null), don't want to obliterate an existing value with a null
                    if (res != null)
                    {
                        SetValue<T>(key, res, expiryDays);
                        return res;
                    }
                }


                // Still don't have a value value, so try get last known value
                if (!_barrel.IsExpired(key))
                {
                    data = _barrel.Get<string>(key);
                }

                // Cache has expired and/or couldn't fetch a new data via force refresh
                if (!string.IsNullOrWhiteSpace(data))
                {
                    return JsonConvert.DeserializeObject<T>(data);
                }

                return default(T);
            }
			catch (Exception ex)
			{
				Console.WriteLine($"Unable to get information from server {ex}");

				throw;
			}
		}

		public void SetValue<T>(string key, T value, TimeSpan timeSpan)
		{
            if (value != null)
            {
                var json = JsonConvert.SerializeObject(value);


                _barrel.Add(key, json, timeSpan);
            }
        }

        public void SetValue<T>(string key, T value, int expiryDays = 7)
        {
            SetValue(key, value, TimeSpan.FromDays(expiryDays));
        }


    }
}
