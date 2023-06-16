using CRMS.Core.Contracts;
using System;
using Unity;
using CRMS.Services;
using CRMS.DataAccess.SQL;
using CRMS.Core.Models;
using CRMS.Core;
using CRMS.Services.Services;
using CRMS.Core.ServiceInterface;
using CRMS.Core.RepositoryInterface;
using CRMS.DataAccess.SQL.Repository;

namespace CRMS.WebUI
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType< IRepository<Role>, SqlRepository<Role>>();     
            
            container.RegisterType<IRoleRepository, RoleRepository>();
            container.RegisterType<IRoleService, RoleService>();

            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IUserService, UserService>();

            container.RegisterType<IUserRoleRepository, UserRoleRepository>();
            container.RegisterType<IUserRoleService, UserRoleService>();

            container.RegisterType<IConferenceRoomRepository, ConferenceRoomRepository>();
            container.RegisterType<IConferenceRoomService, ConferenceRoomService>();

            container.RegisterType<IRepository<CommonLookUp>, SqlRepository<CommonLookUp>>();
            container.RegisterType<ICommonLookUpService, CommonLookUpService>();

            container.RegisterType<IFormMstRepository, FormMstRepository>();
            container.RegisterType<IFormMstService, FormMstService>();

            container.RegisterType<IFormRoleMappingRepository, FormRoleMappingRepository>();
            container.RegisterType<IFormRoleMappingService, FormRoleMappingService>();

            container.RegisterType<ITicketRepository, TicketRepository>();
            container.RegisterType<ITicketService, TicketService>();

            container.RegisterType<ITicketAttachmentRepository, TicketAttachmentRepository>();
            container.RegisterType<ITicketAttachmentService, TicketAttachmentService>();

            container.RegisterType<ITicketCommentRepository, TicketCommentRepository>();
            container.RegisterType<ITicketCommentService, TicketCommentService>();
            
            container.RegisterType<IAuditLogsRepository, AuditLogsRepository>();
            container.RegisterType<IAuditLogsService, AuditLogsService>();

            container.RegisterType<IDashBordRepository, DashBordRepository>();
            container.RegisterType<IDashBordService, DashBordService>();


            //container.RegisterType<IUserRepository, UserService>(); 
        }
    }
}