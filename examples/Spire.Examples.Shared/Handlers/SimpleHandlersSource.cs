#region

using System.Threading.Tasks;
using Spire.Core.Abstractions.Processing.Contexts;
using Spire.Examples.Shared.Attributes;
using Telegram.Bot.Types;

#endregion

namespace Spire.Examples.Shared.Handlers
{
    public class SimpleHandlersSource
    {
        [MessageUpdateEntityHandler(2, Defaults.DefaultProcessorId)]
        public bool SimpleSynchronousHandler(IHandlerContext<Message> handlerContext)
        {
            return true;
        }

        [MessageUpdateEntityHandler(3, Defaults.DefaultProcessorId)]
        public Task<bool> SimpleAsynchronousTaskHandler(IHandlerContext<Message> handlerContext)
        {
            return Task.FromResult(true);
        }

        [MessageUpdateEntityHandler(4, Defaults.DefaultProcessorId)]
        public ValueTask<bool> SimpleAsynchronousValueTaskHandler(IHandlerContext<Message> handlerContext)
        {
            return new ValueTask<bool>(true);
        }
    }
}