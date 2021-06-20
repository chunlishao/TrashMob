﻿namespace TrashMob.Shared.Persistence
{
    using AzureMapsToolkit.Spatial;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Threading.Tasks;

    public class MapRepository : IMapRepository
    {
        private readonly IConfiguration configuration;
        private const string AzureMapKeyName = "AzureMapsKey";
        private const int MetersPerKilometer = 1000;
        private const int MetersPerMile = 1609;

        public MapRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetMapKey()
        {
            return configuration[AzureMapKeyName];
        }

        public async Task<double> GetDistanceBetweenTwoPoints(Tuple<double, double> pointA, Tuple<double, double> pointB, bool IsMetric = true)
        {
            var azureMaps = new AzureMapsToolkit.AzureMapsServices(GetMapKey());
            var distanceRequest = new GreatCircleDistanceRequest
            {
                Start = new Coordinate() { Lat = pointA.Item1, Lon = pointA.Item2 },
                End = new Coordinate() { Lat = pointB.Item1, Lon = pointB.Item2 }
            };

            var response = await azureMaps.GetGreatCircleDistance(distanceRequest).ConfigureAwait(false);

            var distanceInMeters = (long)response.Result.Result.DistanceInMeters;

            if (IsMetric)
            {
                return distanceInMeters / MetersPerKilometer;
            }
            else
            {
                return distanceInMeters / MetersPerMile;
            }
        }
    }
}
