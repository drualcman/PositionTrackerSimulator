namespace PositionTrackerSimulator.Models;
public class PositionNotification
{
    public int RouteId { get; }
    public PositionTrackerLatLong CurrentPosition { get; }
    public double CurrentDistanceInMetters { get; }
    public bool Finished { get; }

    public PositionNotification(int routeId, PositionTrackerLatLong currentPosition,
        double currentDistanceInMetters, bool finished)
    {
        RouteId = routeId;
        CurrentPosition = currentPosition;
        CurrentDistanceInMetters = currentDistanceInMetters;
        Finished = finished;
    }
}
