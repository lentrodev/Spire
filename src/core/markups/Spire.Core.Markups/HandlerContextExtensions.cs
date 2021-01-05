using Autofac;
using Autofac.Builder;
using Autofac.Core.Registration;
using Spire.Core.Abstractions.Processing.Contexts;
using Spire.Core.Markups.Inline;
using Spire.Core.Markups.Reply;
using Telegram.Bot.Types.ReplyMarkups;

namespace Spire.Core.Markups
{
    public static class HandlerContextExtensions
    {
        public static InlineKeyboardMarkupBuilder InlineMarkupBuilder<TEntity>(this IHandlerContext<TEntity> handlerContext)
        {
            return handlerContext.Container.Resolve<InlineKeyboardMarkupBuilder>();
        }
        
        public static ReplyKeyboardMarkupBuilder ReplyMarkupBuilder<TEntity>(this IHandlerContext<TEntity> handlerContext)
        {
            return handlerContext.Container.Resolve<ReplyKeyboardMarkupBuilder>();
        }
        
        public static ContainerBuilder RegisterMarkupBuilders(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<InlineKeyboardMarkupBuilder>()
                .AsSelf()
                .As<KeyboardMarkupBuilderBase<InlineKeyboardMarkup, InlineKeyboardButton,
                    InlineKeyboardMarkupRowBuilder, InlineKeyboardMarkupBuildOptions>>()
                .AsImplementedInterfaces();
            
            containerBuilder.RegisterType<ReplyKeyboardMarkupBuilder>()
                .AsSelf()
                .As<KeyboardMarkupBuilderBase<ReplyKeyboardMarkup, KeyboardButton,
                    ReplyKeyboardMarkupRowBuilder, ReplyKeyboardMarkupBuildOptions>>()
                .AsImplementedInterfaces();
            
            return containerBuilder;
        }
    }
}