using Microsoft.AspNetCore.Mvc;
using Plandemic.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plandemic.App.Extensions
{
    public static class MvcExtensions
    {
        public static IActionResult CreateResult(this ApiResponse response)
        {
            var statusCode = (int)response.Status;

            if (statusCode >= 400)
            {
                return new ObjectResult(response)
                {
                    StatusCode = statusCode
                };
            }

            return new StatusCodeResult(statusCode);
        }

        public static IActionResult CreateResult<T>(this ApiResponse<T> response)
        {
            var statusCode = (int)response.Status;

            if(statusCode >= 400)
            {
                return new ObjectResult(response)
                {
                    StatusCode = statusCode
                };
            }

            return new ObjectResult(response.Source)
            {
                StatusCode = statusCode
            };
        }
    }
}
