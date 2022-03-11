using System;

namespace Common.StandardInfrastructure
{
    public static class AppServicesHelper
    {
        private static IServiceProvider _services = null;

        /// <summary>
        /// Provides static access to the framework's services provider
        /// </summary>
        public static IServiceProvider Services
        {
            get => _services;
            set
            {
                if (_services != null)
                {
                    throw new Exception("Can't set once a value has already been set.");
                }
                _services = value;
            }
        }

    }
}
