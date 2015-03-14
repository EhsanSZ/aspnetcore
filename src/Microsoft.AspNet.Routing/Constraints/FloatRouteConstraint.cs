// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNet.Http;
using Microsoft.Framework.Internal;

namespace Microsoft.AspNet.Routing.Constraints
{
    /// <summary>
    /// Constrains a route parameter to represent only 32-bit floating-point values.
    /// </summary>
    public class FloatRouteConstraint : IRouteConstraint
    {
        /// <inheritdoc />
        public bool Match([NotNull] HttpContext httpContext,
                          [NotNull] IRouter route,
                          [NotNull] string routeKey,
                          [NotNull] IDictionary<string, object> values,
                          RouteDirection routeDirection)
        {
            object value;
            if (values.TryGetValue(routeKey, out value) && value != null)
            {
                if (value is float)
                {
                    return true;
                }

                float result;
                var valueString = Convert.ToString(value, CultureInfo.InvariantCulture);
                return Single.TryParse(valueString,
                                       NumberStyles.Float | NumberStyles.AllowThousands,
                                       CultureInfo.InvariantCulture,
                                       out result);
            }

            return false;
        }
    }
}