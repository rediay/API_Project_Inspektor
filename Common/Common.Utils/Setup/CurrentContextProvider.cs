using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Common.Entities.Auth;
using Common.Services.Infrastructure;

namespace Common.Utils.Setup
{
    public class CurrentContextProvider : ICurrentContextProvider
    {
        private readonly IHttpContextAccessor _accessor;
        public CurrentContextProvider(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public ContextSession GetCurrentContext()
        {
            if (_accessor.HttpContext.User != null && _accessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var currentUserId = _accessor.HttpContext.User.GetUserId();

                if (currentUserId > 0)
                {
                    return new ContextSession { UserId = currentUserId };
                }
            }

            return null;
        }
    }
}
