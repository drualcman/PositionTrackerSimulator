namespace PositionTrackerSimulator.ValueObjects;

internal class TrackedRoute
{
    public PositionTrackerLatLong Origin { get; }
    public PositionTrackerLatLong Destination { get; }
    public double Degree { get; }
    public double TotalDistanceKm { get; }
    public double SpeedKmHr { get; }
    public DateTime TravelStartTime { get; }
    public Action<PositionNotification> Callback { get; }

    public System.Timers.Timer Timer { get; }

    public TrackedRoute(PositionTrackerLatLong origin, PositionTrackerLatLong destination,
        double degree, double totalDistanceKm, double speedKmHr, DateTime travelStartTime,
        Action<PositionNotification> callback, System.Timers.Timer timer)
    {
        Origin = origin;
        Destination = destination;
        Degree = degree;
        TotalDistanceKm = totalDistanceKm;
        SpeedKmHr = speedKmHr;
        TravelStartTime = travelStartTime;
        Callback = callback;
        Timer = timer;
    }
}
