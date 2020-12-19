using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platypus.Extensions {

    public static class IHttpContextAccessorExtension {

        public static Guid UserId(this IHttpContextAccessor context) {
            if (!Guid.TryParse(context.HttpContext.User.Identity.Name, out Guid userId)) {
                throw new ArgumentException("Cannot identify requesting user");
            }

            return userId;
        }
    }
}