using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Zurvan.ClientApp.ViewModels;

namespace Zurvan.ClientApp
{
    public class Bootstrapper:BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();
        private ShellViewModel shellViewModel;
        public Bootstrapper() { Initialize();}

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
             shellViewModel = new ShellViewModel();
            _container.Singleton<ShellViewModel>();
            GetWindowManager().ShowDialogAsync(shellViewModel);

        }
        protected override void Configure()
        {
            _container = new SimpleContainer();
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.PerRequest<ShellViewModel>();
        }
        protected override object GetInstance(Type serviceType, string key)
        {
            return _container.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        public IWindowManager GetWindowManager()
        {
            return (IWindowManager)_container.GetInstance(typeof(IWindowManager), null);
        }

    }
}
