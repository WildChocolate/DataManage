using Autofac;
using Manage.BLL;
using Manage.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.BLLContainer
{
    public class Container
    {
        public static IUserService  GetUserService(){
            IUserService service = new UserService();
            return service;
        }
        static IContainer container = null;
        public static T GetService<T>()
        {
            try
            {
                if (container == null)
                {
                    Initialize();
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception("IOC实例化出错!" + ex.Message);
            }
            return container.Resolve<T>();
        }
        private static void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UserService>().As<IBLL.IUserService>().InstancePerDependency();
            builder.RegisterType<DataTypeService>().As<IBLL.IDataTypeService>().InstancePerDependency();
            builder.RegisterType<FlowStepService>().As<IBLL.IFlowStepService>().InstancePerDependency();
            builder.RegisterType<MenuService>().As<IBLL.IMenuService>().InstancePerDependency();
            builder.RegisterType<NameValueDataService>().As<IBLL.INameValueDataService>().InstancePerDependency();
            builder.RegisterType<RoleMenuService>().As<IBLL.IRoleMenuService>().InstancePerDependency();
            builder.RegisterType<RoleService>().As<IBLL.IRoleService>().InstancePerDependency();
            builder.RegisterType<UserRoleService>().As<IBLL.IUserRoleService>().InstancePerDependency();
            builder.RegisterType<VerifyFlowService>().As<IBLL.IVerifyFlowService>().InstancePerDependency();
            builder.RegisterType<RoleVerifyService>().As<IBLL.IRoleVerifyService>().InstancePerDependency();
            builder.RegisterType<DataService>().As<IBLL.IDataService>().InstancePerDependency();
            builder.RegisterType<DataVerifyStepService>().As<IBLL.IDataVerifyStepService>().InstancePerDependency();
            container = builder.Build();
        }
    }
}
