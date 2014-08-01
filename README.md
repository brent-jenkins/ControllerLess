#ControllerLess ASP.NET MVC [![Build Status](https://api.travis-ci.org/brentj73/ControllerLess.svg)](https://travis-ci.org/brentj73/ControllerLess)

Microsoft ASP.NET MVC is arguably one of the most flexible frameworks for building modern web applications.

However, using the default ASP.NET MVC functionality means that regardless of whether or not you really need server-side code for rendering each view, you must have a corresponding controller and action.

Often, view controllers have a single action (e.g. Index) which simply returns the view. This has the potential to break [DRY principle](http://en.wikipedia.org/wiki/Don't_repeat_yourself) and adds to the overall maintenance cost of ASP.NET MVC applications.

So how can we get around this? Wouldn't it be great if we could just have a single re-usable controller with a single re-usable action instead of creating multiple copies of the same code?

The good news is that it's easy with the ControllerLess plug-in! And if you need a controller for a specific view or action, you can still add it and it will work as expected.

Installing
=============

The easiest way to install the ControllerLess plug-in is by downloading and installing the package [using Nuget package manager](https://www.nuget.org/packages/ControllerLess).

Alternatively, you can manually download the latest release directly from the [ControllerLess release page at Github](https://github.com/brentj73/ControllerLess/releases) then add a reference in your ASP.NET MVC project to the Anterec.ControllerLess.dll library from Visual Studio.

Configuration
=============

Once you have the Anterec.ControllerLess.dll library referenced in your ASP.NET MVC project, all you need to do is modify your route configuration to set the default route handler.

```C#
using Anterec.ControllerLess.Mvc;

public class RouteConfig
{
	public static void RegisterRoutes(RouteCollection routes)
	{
		routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

		var route = new Route(
			"{controller}/{action}/{id}",
			new RouteValueDictionary(new { controller = "Home", action = "Index", id = UrlParameter.Optional }),
			new ControllerLessRouteHandler());

		routes.Add(route);
	}
}
```

Further Reading
===============

See the [ControllerLess Wiki](wiki) pages for more detailed information on configuring and extending the ControllerLess plug-in.
