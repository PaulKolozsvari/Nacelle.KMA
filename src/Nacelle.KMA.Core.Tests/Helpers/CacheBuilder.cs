using Nacelle.KMA.Core.Tests.Models;

namespace Nacelle.KMA.Core.Tests.Helpers
{
    public class CacheBuilder
    {
        private string _item1;
        private string _item2;

        public CacheBuilder WithItem1(string item1)
        {
            _item1 = item1;
            return this;
        }

        public CacheBuilder WithItem2(string item2)
        {
            _item2 = item2;
            return this;
        }

        public CacheBuilder WithDefaults() => WithItem1("Value1").WithItem2("Value2");

        public Cache Build()
        {
            return new Cache
            {
                Item1 = _item1,
                Item2 = _item2
            };
        }
    }
}
