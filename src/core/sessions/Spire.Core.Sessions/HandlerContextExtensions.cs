#region

using System;
using System.Threading.Tasks;
using Autofac;
using Spire.Core.Abstractions.Builders.Processing;
using Spire.Core.Abstractions.Processing.Attributes;
using Spire.Core.Abstractions.Processing.Contexts;
using Spire.Core.Processing;
using Spire.Core.Sessions.Abstractions;

#endregion

namespace Spire.Core.Sessions
{
    /// <summary>
    /// Extensions for adding sessions support.
    /// </summary>
    public static class HandlerContextExtensions
    {
        /// <summary>
        /// Gets session, based on <see cref="IHandlerContext{TEntity}"/>.
        /// </summary>
        /// <param name="handlerContext">Handler context.</param>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <returns>A session.</returns>
        /// <exception cref="InvalidOperationException">Throws, if <see cref="ISessionManager"/> wasn't registered in the services container builder.</exception>
        public static ISession<TEntity> Session<TEntity>(this IHandlerContext<TEntity> handlerContext)
        {
            ISessionManager sessionManager = handlerContext.Container.ResolveOptional<ISessionManager>();

            if (sessionManager == null)
            {
                throw new InvalidOperationException(
                    "ISessionManager wasn't found, you should register via AddSessionManagement()");
            }

            if (sessionManager.IsSessionExists<TEntity>(handlerContext.Sender, handlerContext.Update.Type))
            {
                return sessionManager.GetSession<TEntity>(handlerContext.Sender, handlerContext.Update.Type);
            }
            else
            {
                ISession<TEntity> session =
                    sessionManager.CreateSession<TEntity>(handlerContext.Sender, handlerContext.Update.Type);

                sessionManager.AddSession(session);

                return session;
            }
        }

        /// <summary>
        /// Adds session management to the <see cref="ContainerBuilder"/>.
        /// </summary>
        /// <param name="containerBuilder">Container builder.</param>
        /// <returns>Configured <see cref="ContainerBuilder"/> instance.</returns>
        public static ContainerBuilder AddSessionManagement(this ContainerBuilder containerBuilder)
        {
            ISessionManager sessionManager = new SessionManager();

            containerBuilder.RegisterInstance(sessionManager).As<ISessionManager>()
                .IfNotRegistered(typeof(ISessionManager));

            return containerBuilder;
        }

        /// <summary>
        /// Adds handler which will intercepts incoming entities and sends it to a specified entity session if entity was requested. 
        /// </summary>
        /// <param name="updateEntityProcessorBuilder"></param>
        /// <param name="attribute">Attribute to identify handler.</param>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TUpdateEntityHandlerAttribute">Update entity handler attribute.</typeparam>
        /// <returns>Configured update entity processor builder instance.</returns>
        public static IUpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute>
            WithSessionRequestUpdateEntityHandler<TEntity, TUpdateEntityHandlerAttribute>(
                this IUpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute> updateEntityProcessorBuilder,
                TUpdateEntityHandlerAttribute attribute)
            where TUpdateEntityHandlerAttribute : UpdateEntityHandlerAttributeBase
        {
            return updateEntityProcessorBuilder.WithHandlerDescriptor(
                new FuncUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute>(
                    Guid.NewGuid(),
                    new Func<IHandlerContext<TEntity>, ValueTask<bool>>(async (IHandlerContext<TEntity> context) =>
                    {
                        ISession<TEntity> session = context.Session();

                        if (session.IsUpdateEntityRequested)
                        {
                            await session.UpdateEntityChannelWriter.WriteAsync(context.Entity);
                            return false;
                        }

                        return true;
                    }), attribute));
        }
    }
}