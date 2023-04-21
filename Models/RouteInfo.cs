namespace PositionTrackerSimulator.Models;
public class RouteInfo
{
    public int RouteId { get; set; }
    public PositionTrackerLatLong Destination { get; }
    public DateTime TravelStartTime { get; }
    public double SpeedKmHr { get; }
    public double RouteDistanceKm { get; }
    public double NotificationIntervalInSeconds { get; }
    public Action<PositionNotification> CallBack { get;  }

    public RouteInfo(int routeId, PositionTrackerLatLong destination, DateTime travelStartTime, 
        double speedKmHr, double routeDistanceKm, double notificationIntervalInSeconds, 
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
