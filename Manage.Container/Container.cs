using Manage.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manage.DAL;
using Autofac;

namespace Manage.DALContainer
{
    public class Container
    {
        public static IUserRepo GetUserRepository()
        {
            IUserRepo repo = new UserRepo();
            return repo;
        }
        static IContainer container = null;
        public static T GetRepository<T>()
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
            builder.RegisterType<UserRepo>().As<IUserRepo>().InstancePerDependency() ;
            builder.RegisterType<DataTypeRepo>().As<IDateTypeRepo>().InstancePerDependency();
            builder.RegisterType<FlowStepRepo>().As<IFlowStepRepo>().InstancePerDependency();
            builder.RegisterType<MenuRepo>().As<IMenuRepo>().InstancePerDependency();
            builder.RegisterType<NameValueInfoRepo>().As<INameValueInfoRepo>().InstancePerDependency();
            builder.RegisterType<RoleMenuRepo>().As<IRoleMenuRepo>().InstancePerDependency();
            builder.RegisterType<RoleRepo>().As<IRoleRepo>().InstancePerDependency();
            builder.RegisterType<UserRoleRepo>().As<IUserRoleRepo>().InstancePerDependency();
            builder.RegisterType<VerifyFlowRepo>().As<IVerifyFlowRepo>().InstancePerDependency();
            container = builder.Build();
        }

    }
}
