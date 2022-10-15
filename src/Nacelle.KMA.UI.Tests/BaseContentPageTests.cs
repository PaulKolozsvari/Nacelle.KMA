using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;
using Nacelle.KMA.UI.Pages;
using System.Linq;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace Nacelle.KMA.UI.Tests
{
    [TestClass]
    public class BaseContentPageTests
    {
        [TestMethod, Ignore]
        public void XamlCompilationAttributeTest()
        {
            Assembly assembly = typeof(App).GetTypeInfo().Assembly;

            var allTypes = from x in assembly.GetTypes()
                                let y = x.BaseType
                                where !x.IsAbstract && !x.IsInterface &&
                                y != null && y.IsGenericType &&
                                y.GetGenericTypeDefinition() == typeof(BaseContentPage<>)
                                select x;

            foreach (var type in allTypes)
            {
                var found = false;
                var attributes = type.GetTypeInfo().GetCustomAttributes();
                foreach(var attr in attributes)
                {
                    if (attr is XamlCompilationAttribute)
                    {
                        found = true;
                        break;
                    }
                }

                Assert.IsTrue(found, "XamlCompilation attribute not found on class " + type.Name);
            }
        }
    }
}
