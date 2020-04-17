﻿using System;
using System.Collections.Generic;
using System.Text;
using Tools.Domain.Abstractions;
using Tools.Domain.Helpers;

namespace Tools.GeoLocation.Domain.Entities
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