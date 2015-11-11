using System;
using System.Collections.Generic;

namespace IntelliSun.ComponentModel.Composition
{
    public interface IServiceLocator : IServiceProvider
    {
        IEnumerable<object> GetServices(Type serviceType);

        bool HasService(Type serviceType);
    }
}
