namespace PositionTrackerSimulator;
public class GoToDestinationSimulator : IDisposable
{
    readonly Dictionary<int, TrackedRoute> TrackedRoutes = new();
    readonly Dictionary<int, RouteCache> RoutesCache = new();
    public Task<PositionTrackerLatLong> SubscribeAcync(RouteInfo routeInfo)
    {
        TrackedRoute trackedRoute = GetTrackedRouteFake(routeInfo);
        TrackedRoutes.TryAdd(routeInfo.RouteId, trackedRoute);
        NotifyPosition(routeInfo.RouteId);
        return Task.FromResult(trackedRoute.Origin);
    }

    private void NotifyPosition(int routeId)
    {
        TrackedRoute trackedOrder = TrackedRoutes[routeId];
        PositionNotification notification = GetPositionNotification(routeId);
        trackedOrder.Callback(notification);
        if(notification.Finished)
            UnSubscribe(routeId);
    }

    private PositionNotification GetPositionNotification(int routeId)
    {     
        TrackedRoute trackedRoute = TrackedRoutes[routeId]; 
        PositionNotification notification;
        double elapsetTimeHours = (DateTime.Now - trackedRoute.TravelStartTime).TotalHours;
        if(elapsetTimeHours > 0)
        {
            double currentDistanceInKm = trackedRoute.SpeedKmHr * elapsetTimeHours;
            if (currentDistanceInKm >= trackedRoute.TotalDistanceKm)
            {
                currentDistanceInKm = trackedRoute.TotalDistanceKm;
            }
            PositionTrackerLatLong currentPosition = trackedRoute.Origin.AddKm(trackedRoute.Degree, currentDistanceInKm);
            notification =  new PositionNotification(routeId, currentPosition, currentDistanceInKm, 
                currentDistanceInKm >= trackedRoute.TotalDistanceKm);              
        }
        else
        {
           notification =  new PositionNotification(routeId, trackedRoute.Origin, 0, false);
        }
        return notification;
    }

    private TrackedRoute GetTrackedRouteFake(RouteInfo routeInfo)
    {
        RouteCache route;
        if(!RoutesCache.TryGetValue(routeInfo.RouteId, out route))
        {        
            route = new RouteCache();
            double degree = new Random().Next(0, 360);
            double distanceInMetter = routeInfo.RouteDistanceKm * 1000.0;
            PositionTrackerLatLong origin = routeInfo.Destination.AddMetters(degree, -distanceInMetter);
            route.Origin = origin;
            route.Degree = degree;
            RoutesCache.Add(routeInfo.RouteId, route);
        }
        System.Timers.Timer timer = new System.Timers.Timer(routeInfo.NotificationIntervalInSeconds * 1000);
        timer.Elapsed += (sender, e) => NotifyPosition(routeInfo.RouteId);
        timer.Start();
        return new TrackedRoute(route.Origin, routeInfo.Destination, route.Degree, routeInfo.RouteDistanceKm, 
            routeInfo.SpeedKmHr, routeInfo.TravelStartTime, routeInfo.CallBack, timer);
    }

    public void UnSubscribe(int routeId)
    {
        if(TrackedRoutes.TryGetValue(routeId, out TrackedRoute trackedOrder))
        {
            trackedOrder.Timer.Dispose();
            TrackedRoutes.Remove(routeId);
        }
    }

    public void Dispose()
    {
        foreach(var route in TrackedRoutes)
        {
            route.Value.Timer.Dispose();
        }
        TrackedRoutes.Clear();
    }
}
