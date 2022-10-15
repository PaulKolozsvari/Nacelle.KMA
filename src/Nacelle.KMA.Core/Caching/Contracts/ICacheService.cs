using System;
using System.Threading.Tasks;

namespace Nacelle.KMA.Core.Caching
{
    public interface ICacheService
    {
        void ClearAll();
		void Clear(string key);
        void ClearWhereKeyPrefix(string key);
        void ClearExpired();
		T GetValue<T>(string key);
		Task<T> GetOrUpdateValue<T>(string key, Func<Task<T>> fetchFunc, int expiryDays = 7, bool forceRefresh = false);
		void SetValue<T>(string key, T value, int expiryDays = 7);
    }
}