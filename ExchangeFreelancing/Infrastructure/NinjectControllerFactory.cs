using ExchangeFreelancing.Domain.Abstract;
using ExchangeFreelancing.Domain.Concrete;

using Ninject;
using System;
using System.Web.Mvc;

namespace ExchangeFreelancing.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        private void AddBindings()
        {
            ninjectKernel.Bind<IOrder>().To<EFOrderRepository>();
            ninjectKernel.Bind<ICategory>().To<EFCategoryRepository>();
            ninjectKernel.Bind<IRequest>().To<EFRequestRepository>();
            ninjectKernel.Bind<IFile>().To<EFFileRepository>();
            ninjectKernel.Bind<IMessage>().To<EFMessageRepository>();
            ninjectKernel.Bind<IClaim>().To<EFClaimRepository>();
            ninjectKernel.Bind<IComment>().To<EFCommentRepository>();
        }
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

    }
}