using System;
using Aliases.Common.Shared.Exceptions;

namespace Aliases.Common.Shared.Extensions
{
    public static class PageExtensions
    {
        public static TPageOut PerformAction<TPageIn, TPageOut>(this TPageIn page, Func<TPageIn, TPageOut> action) where TPageIn : WebPage where TPageOut : WebPage
        {
            return action(page);
        }

        public static TPage ExpectFail<TPage>(this TPage page, Type exceptionType, Action<TPage> action) where TPage : WebPage
        {
            if (!typeof(Exception).IsAssignableFrom(exceptionType))
            {
                throw new ArgumentException("Exception type must be a type of exception.", nameof(exceptionType));
            }

            bool didThrow = false;
            try
            {
                action(page);
            }
            catch (Exception e)
            {
                if (e.GetType() == exceptionType)
                {
                    didThrow = true;
                }
                else
                {
                    throw;
                }
            }

            if (!didThrow)
            {
                throw new ActionFailedException($"Exected exception did not occur. Expected: { exceptionType }");
            }

            return page;
        }

    }
}
