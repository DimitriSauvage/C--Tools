using DimitriSauvageTools.Domain.Abstractions;

namespace DimitriSauvageTools.GeoLocation.Domain.Entities
{
    /// <summary>
    /// GPS Coordinate
    /// </summary>
    public class LatLng : ValueObject
    {
        /// <summary>
        /// Latitude
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        public double Longitude { get; set; }
    }
}