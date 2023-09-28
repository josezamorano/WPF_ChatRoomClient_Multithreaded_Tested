using Autofac;
using DataAccessLayer.IONetwork;
using DataAccessLayer.Utils.Interfaces;
using DomainLayer;
using DomainLayer.Utils.Interfaces;
using PresentationLayer.MVVM.ViewModels;
using PresentationLayer.MVVM.Views;
using PresentationLayer.EventObservers;
using PresentationLayer.Utils.Interfaces;
using ServiceLayer;
using ServiceLayer.Interfaces;
using System;
using System.Windows;

namespace PresentationLayer
{
    public partial class App : Application
    {
        IMainWindowViewModel _mainWindowViewModel;
       
        public App()
        {
            ConfigureDependencyInjectionContainer();
            Console.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindowView mainWindowView = new MainWindowView(_mainWindowViewModel);
            if(mainWindowView != null)
            {
                mainWindowView.Show();
            }
        }


        #region Helper Methods

        private void ConfigureDependencyInjectionContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();

            //Service Layer
            builder.RegisterType<InputValidator>().As<IInputValidator>();
            builder.RegisterType<ObjectCreator>().As<IObjectCreator>();


            //Data Access Layer
            builder.RegisterType<SerializationProvider>().As<ISerializationProvider>();
            builder.RegisterType<StreamProvider>().As<IStreamProvider>();


            //Domain Layer 
            builder.RegisterType<TcpClientProvider>().As<ITcpClientProvider>().SingleInstance(); 
            builder.RegisterType<ClientManager>().As<IClientManager>().SingleInstance();
            builder.RegisterType<MessageFactory>().As<IMessageFactory>();
            builder.RegisterType<ServerAction>().As<IServerAction>().SingleInstance();
            builder.RegisterType<Transmitter>().As<ITransmitter>();
            builder.RegisterType<User>().As<IUser>().SingleInstance(); 
            builder.RegisterType<UserChatRoomAssistant>().As<IUserChatRoomAssistant>().SingleInstance();

            //Presentation Layer

            builder.RegisterType<ChatRoomObserver>().As<IChatRoomObserver>().SingleInstance();
            builder.RegisterType<ServerUserObserver>().As<IServerUserObserver>().SingleInstance();
            builder.RegisterType<ClientLogObserver>().As<IClientLogObserver>().SingleInstance();
            builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>().SingleInstance();
            builder.RegisterType<AllChatRoomsViewModel>().As<IAllChatRoomsViewModel>().SingleInstance();
            builder.RegisterType<CreateChatRoomViewModel>().As<ICreateChatRoomViewModel>().SingleInstance();
            builder.RegisterType<SingleChatRoomViewModel>().As<ISingleChatRoomViewModel>().SingleInstance();
            builder.RegisterType<ConnectionViewModel>().As<IConnectionViewModel>().SingleInstance();
            builder.RegisterType<ContactViewModel>().As<IContactViewModel>().SingleInstance();
            builder.RegisterType<InvitationViewModel>().As<IInvitationViewModel>().SingleInstance();
            
            

            Autofac.IContainer newContainer = builder.Build();

            ILifetimeScope newScope = newContainer.BeginLifetimeScope();

            _mainWindowViewModel = newScope.Resolve<IMainWindowViewModel>();
        
        }

        #endregion Helper Methods
    }
}
