namespace PositionTrackerSimulator.Models;
public class RouteInfo
{
    public int RouteId { get; set; }
    public PositionTrackerLatLong Destination { get; }
    public DateTime TravelStartTime { get; }
    public double SpeedKmHr { get; }
    public double RouteDistanceKm { get; }
    public long NotificationIntervalInSeconds { get; }
    public Action<PositionNotification> CallBack { get;  }

    public RouteInfo(int routeId, PositionTrackerLatLong destination, DateTime travelStartTime, 
        double speedKmHr, double routeDistanceKm, long notificationIntervalInSeconds, 
        Action<PositionNotification> callBack)
    {
        RouteId = routeId;
        Destination = destination;
        TravelStartTime = travelStartTime;
        SpeedKmHr = speedKmHr;
        RouteDistanceKm = routeDistanceKm;
        NotificationIntervalInSeconds = notificationIntervalInSeconds;
        CallBack = callBack;
    }
}
