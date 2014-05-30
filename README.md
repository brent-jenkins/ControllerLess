ControllerLess
==============

Enable ASP.NET MVC controller-less applications

Background
==========

Using the default MVC HTTP handler means that regardless of whether you actually need server-side code 
for rendering each view, you must have a corresponding controller.

In many cases these controllers have a single action (e.g. Index) which simply returns the view. This 
seems inefficient and adds to the maintenance cost of the application.

So how can we get around this? Wouldn't it be great if we could just have a single re-usable controller 
with a single action instead of creating multiple copies of the same thing? The good news is that it's easy with MVC!

And if you need a controller for a specific view, you can still add it in the normal way and it will work as expected.

Configuration
=============

Once installed, simply update your route config to use the DynamicRouteHandler.

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
			new DynamicRouteHandler());

		routes.Add(route);
	}
}
```