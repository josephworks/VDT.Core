﻿using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using VDT.Core.DependencyInjection.Tests.ConventionServiceTargets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class ServiceCollectionConventionExtensionsTests {
        protected readonly ServiceCollection services = new ServiceCollection();

        [Fact]
        public void AddServices_Uses_ServiceLifetimeProvider() {
            services.AddServices(options => {
                options.Assemblies.Add(typeof(NamedService).Assembly);
                options.ServiceTypeFinders.Add(new ServiceTypeFinderOptions(t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface))) {
                    ServiceLifetimeProvider = (serviceType, implementationType) => ServiceLifetime.Singleton
                });
            });

            Assert.Equal(ServiceLifetime.Singleton, services.Single(s => s.ServiceType == typeof(INamedService)).Lifetime);
        }

        [Fact]
        public void AddServices_Uses_DefaultServiceLifetime_If_No_ServiceLifetime_Found() {
            services.AddServices(options => {
                options.Assemblies.Add(typeof(NamedService).Assembly);
                options.ServiceTypeFinders.Add(new ServiceTypeFinderOptions(t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface))) {
                    ServiceLifetimeProvider = (serviceType, implementationType) => null
                });
                options.DefaultServiceLifetime = ServiceLifetime.Singleton;
            });

            Assert.Equal(ServiceLifetime.Singleton, services.Single(s => s.ServiceType == typeof(INamedService)).Lifetime);
        }

        [Fact]
        public void AddServices_Uses_DefaultServiceLifetime_If_No_ServiceLifetimeProvider_Supplied() {
            services.AddServices(options => {
                options.Assemblies.Add(typeof(NamedService).Assembly);
                options.ServiceTypeFinders.Add(new ServiceTypeFinderOptions(t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface))));
                options.DefaultServiceLifetime = ServiceLifetime.Singleton;
            });

            Assert.Equal(ServiceLifetime.Singleton, services.Single(s => s.ServiceType == typeof(INamedService)).Lifetime);
        }

        [Fact]
        public void AddServices_Uses_ServiceRegistrar_If_Provided() {
            services.AddServices(options => {
                options.Assemblies.Add(typeof(NamedService).Assembly);
                options.ServiceTypeFinders.Add(new ServiceTypeFinderOptions(t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface))));
                options.DefaultServiceLifetime = ServiceLifetime.Singleton;
                options.ServiceRegistrar = (services, serviceType, implementationType, serviceLifetime) => {
                    services.Add(new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Scoped));
                    return services;
                };
            });

            Assert.Equal(ServiceLifetime.Scoped, Assert.Single(services, s => s.ServiceType == typeof(INamedService)).Lifetime);
        }

        [Fact]
        public void AddServices_Creates_ServiceRegistrations_If_No_ServiceRegistrar_Supplied() {
            services.AddServices(options => {
                options.Assemblies.Add(typeof(NamedService).Assembly);
                options.ServiceTypeFinders.Add(new ServiceTypeFinderOptions(t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface))));
                options.DefaultServiceLifetime = ServiceLifetime.Scoped;
            });

            var service = Assert.Single(services, s => s.ServiceType == typeof(INamedService));

            Assert.Equal(typeof(NamedService), service.ImplementationType);
            Assert.Equal(ServiceLifetime.Scoped, service.Lifetime);
        }

        [Fact]
        public void AddServices_Adds_Services_From_All_ServiceTypeFinders() {
            services.AddServices(options => {
                options.Assemblies.Add(typeof(NamedService).Assembly);
                options.ServiceTypeFinders.Add(new ServiceTypeFinderOptions(t => t.GetInterfaces().Where(i => i == typeof(IGenericInterface))));
                options.ServiceTypeFinders.Add(new ServiceTypeFinderOptions(t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface))));
            });

            Assert.Single(services, s => s.ServiceType == typeof(INamedService));
            Assert.Single(services, s => s.ServiceType == typeof(IGenericInterface));
        }

        [Fact]
        public void AddServices_Adds_No_Services_When_No_Assemblies_Supplied() {
            services.AddServices(options => {
                options.ServiceTypeFinders.Add(new ServiceTypeFinderOptions(t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface))));
            });

            Assert.Empty(services);
        }

        [Fact]
        public void AddServices_Adds_No_Services_When_No_ServiceTypeFinders_Supplied() {
            services.AddServices(options => {
                options.Assemblies.Add(typeof(NamedService).Assembly);
            });

            Assert.Empty(services);
        }
    }
}
