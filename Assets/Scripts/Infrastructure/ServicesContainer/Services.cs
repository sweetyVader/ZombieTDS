using System;
using System.Collections.Generic;
using UnityEngine;

namespace TDS.Infrastructure
{
    public class Services
    {
        private const string Tag = nameof(Services);
        private readonly Dictionary<Type, IService> _services = new();
        private static Services _container;

        public static Services Container => _container ?? (_container = new Services());

        public void Register<TService>(TService implementation) where TService : class, IService
        {
            Type key = typeof(TService);

            if (_services.ContainsKey(key))
            {
                Debug.LogError($"{Tag}:{nameof(Register)}: try add service with key {key}, that already exist.");
                return;
            }

            _services.Add(key, implementation);
        }

        public TService Get<TService>() where TService : class, IService
        {
            Type key = typeof(TService);
            if (_services.ContainsKey(key))
            {
                return _services[key] as TService;
            }
            Debug.LogError($"{Tag}:{nameof(Get)}: there is no service with key {key}");
            return default;
        }

        public void UnRegister<TService>() where TService : IService
        {
            Type key = typeof(TService);
            if (!_services.ContainsKey(key))
            {
                Debug.LogError($"{Tag}:{nameof(UnRegister)}: try remove service with key {key}, that already removed.");
                return;
            }

            _services.Remove(key);
        }
    }
}