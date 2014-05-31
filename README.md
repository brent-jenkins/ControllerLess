ControllerLess
==============

Using the default MVC HTTP handler means that regardless of whether you actually need server-side code 
for rendering each view, you must have a corresponding controller.

In many cases these controllers have a single action (e.g. Index) which simply returns the view. This 
seems inefficient and adds to the maintenance cost of the application.

So how can we get around this? Wouldn't it be great if we could just have a single re-usable controller 
with a single action instead of creating multiple copies of the same thing? The good news is that it's easy with ControllerLess!

And if you need a controller for a specific view, you can still add it in the normal way and it will work as expected.

Configuration
=============

Once installed, simply update your route config to use the ControllerLessRouteHandler.

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

Advanced Configuration (optional)
=================================

You can configure ControllerLess to redirect requests to your own controllers and actions on a URL by URL basis in the Web.config file.

```XML
<controllerLessSettings>
  <routes>
    <clear />
    <add url="/Hello" controller="Door" action="Enter"/>
    <add url="/Hello/Cutiepie" controller="Panic" action="Run"/>
    ...
  </routes>
</controllerLessSettings>
```

You can also configure your own controller and action to receive all requests, replacing the ControllerLess default behaviour.

```XML
<controllerLessSettings defaultController="Default" defaultAction="Show">
  <routes>
    ...
  </routes>
</controllerLessSettings>
```

In this example any view that doesn't have a controller will be routed to the "Show" action on the "Default" controller.
