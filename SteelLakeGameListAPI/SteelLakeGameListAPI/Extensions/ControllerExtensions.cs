using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteelLakeGameListAPI.Extensions
{
    public static class ControllerExtensions
    {
        public static ActionResult Either<T, F>(this Controller controller, bool condition)
                where T : ActionResult, new()
                where F : ActionResult, new()
        {
            if (condition)
            {
                return new T();
            }
            else
            {
                return new F();
            }
        }
    }
}
