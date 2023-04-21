namespace PositionTrackerSimulator.ValueObjects;
public record struct PositionTrackerLatLong(double Latitude, double Longitude)
{
    public PositionTrackerLatLong AddMetters(double angle, double distanceInMetters)
    {
        CoordinatesCalculatesHelper calculatesHelper = new CoordinatesCalculatesHelper();
        double latitude = calculatesHelper.GetLatitudeFromDegreesPerMetter(Latitude, angle, distanceInMetters);
        double longitude = calculatesHelper.GetLongitudeFromDegreesPerMetter(Latitude, Longitude, angle, distanceInMetters);
        return new PositionTrackerLatLong(latitude, longitude);
    }

    public PositionTrackerLatLong AddKm(double angle, double distanceInKm) =>
        AddMetters(angle, distanceInKm * 1000);

    public PositionTrackerLatLong AddCm(double angle, double distanceInKm) =>
        AddMetters(angle, distanceInKm / 100);
}
