using System;
using System.Linq;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Microsoft.AspNet.SignalR;
using NUnit.Framework;

namespace SignalR.Castle.Tests
{
    [TestFixture]
    public class CastleDependencyResolverTests
    {
        [Test]
        public void current_property_exposes_the_correct_resolver()
        {
            var kernel = new DefaultKernel();
            var resolver = new CastleDependencyResolver(kernel);

            GlobalHost.DependencyResolver = resolver;

            Assert.That(CastleDependencyResolver.Current, Is.EqualTo(GlobalHost.DependencyResolver));
        }

        [Test]
        public void null_container_scope_throws_exception()
        {
            var exception = Assert.Throws<ArgumentNullException>(
                () => new CastleDependencyResolver(null));
            Assert.That(exception.ParamName, Is.EqualTo("kernel"));
        }

        [Test]
        public void get_service_returns_null_for_unregistered_service()
        {
            var kernel = new DefaultKernel();
            var resolver = new CastleDependencyResolver(kernel);

            var service = resolver.GetService(typeof(object));

            Assert.That(service, Is.Null);
        }

        [Test]
        public void get_service_returns_registered_service()
        {
            var kernel = new DefaultKernel();
            kernel.Register(Component.For<object>().Instance(new object()));

            var resolver = new CastleDependencyResolver(kernel);

            var service = resolver.GetService(typeof(object));

            Assert.That(service, Is.Not.Null);
        }

        [Test]
        public void get_services_returns_empty_enumerable_for_unregistered_service()
        {
            var kernel = new DefaultKernel();
            var resolver = new CastleDependencyResolver(kernel);

            var services = resolver.GetServices(typeof(object));

            Assert.That(services.Count(), Is.EqualTo(0));
        }

        [Test]
        public void get_services_returns_registered_service()
        {
            var kernel = new DefaultKernel();
            kernel.Register(Component.For<object>().Instance(new object()));

            var resolver = new CastleDependencyResolver(kernel);

            var services = resolver.GetServices(typeof(object));

            Assert.That(services.Count(), Is.EqualTo(1));
        }
    }
}