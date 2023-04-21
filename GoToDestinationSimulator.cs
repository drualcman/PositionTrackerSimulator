namespace PositionTrackerSimulator;
public class GoToDestinationSimulator : IDisposable
{
    readonly Dictionary<int, TrackedRoute> TrackedRoutes = new();
    public Task<PositionTrackerLatLong> SubscribeAcync(RouteInfo routeInfo)
    {
        TrackedRoute trackedRoute = GetTrackedRouteFake(routeInfo, routeInfo.CallBack);
        TrackedRoutes.TryAdd(routeInfo.RouteId, trackedRoute);
        NotifyPosition(routeInfo.RouteId);
        return Task.FromResult(trackedRoute.Origin);
    }

    private void NotifyPosition(int routeId)
    {
        TrackedRoute trackedOrder = TrackedRoutes[routeId];
        PositionTrackerLatLong currentPosition = trackedOrder.Destination;
        double distance;
        bool finished = false;

        double elapsetTimeHours = (DateTime.Now - trackedOrder.TravelStartTime).TotalSeconds / 3600;
        distance = trackedOrder.SpeedKmHr * elapsetTimeHours;
        if(distance >= trackedOrder.TotalDistanceKm)
        {
            distance = trackedOrder.TotalDistanceKm;
        }
        currentPosition = currentPosition.AddKm(trackedOrder.Degree, distance);

        PositionNotification notification = new PositionNotification(routeId, currentPosition, distance, finished);
        trackedOrder.Callback(notification);
        if(finished)
            UnSubscribe(routeId);
    }

    private TrackedRoute GetTrackedRouteFake(RouteInfo routeInfo, Action<PositionNotification> callBack)
    {
        throw new NotImplementedException();
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
