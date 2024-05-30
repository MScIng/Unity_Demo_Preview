using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Niantic.Lightship.AR.LocationAR;
using Niantic.Lightship.AR.PersistentAnchors;
using Niantic.Lightship.AR.VpsCoverage;
using Niantic.Lightship.Maps.Core.Coordinates;
using MapsLatLng = Niantic.Lightship.Maps.Core.Coordinates.LatLng;
using Niantic.Lightship.Maps.MapLayers.Components;
using Niantic.Lightship.Maps;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif


public class VPSCoverageManager : MonoBehaviour
{
    [SerializeField]
    private CoverageClientManager coverageClientManager;
    // private CoverageClientManager coverageClientManager;
    [SerializeField]
    private LayerGameObjectPlacement _publicLocationSign;
    // private LatLng curLatLng;
    // private LatLng prevLatLng;
    private double _lastGpsUpdateTime;
    private static bool IsLocationServiceInitializing
            => Input.location.status == LocationServiceStatus.Initializing;

    public GameObject player;
    [SerializeField]
    private LightshipMapView _lightshipMapView;
    private List<AreaTarget> _curSign = new List<AreaTarget>();
    private MapsLatLng lastPlayerCoord;

    void Start()
    {
        StartCoroutine(UpdateGpsLocation());
        lastPlayerCoord = _lightshipMapView.SceneToLatLng(player.transform.position);
        QueryAroundUser();
    }

    void Update()
    {
        // update VPS public location
        MapsLatLng playerCoord = _lightshipMapView.SceneToLatLng(player.transform.position);
        if (CalculateDistance(playerCoord, lastPlayerCoord) > 0.3)
        {
            Debug.Log("called");
            QueryAroundUser();
            lastPlayerCoord = _lightshipMapView.SceneToLatLng(player.transform.position);
            
        }
    }

    private IEnumerator UpdateGpsLocation()
    {
        yield return null;

        if (Application.isEditor)
        {
            while (isActiveAndEnabled)
            {
                // UpdateEditorInput();
                yield return null;
            }
        }
        else
        {
#if UNITY_ANDROID
                // Request location permission for Android
                if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
                {
                    Permission.RequestUserPermission(Permission.FineLocation);
                    while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
                    {
                        // Wait until permission is enabled
                        yield return new WaitForSeconds(1.0f);
                    }
                }
#endif
            // Check if the user has location service enabled.
            if (!Input.location.isEnabledByUser)
            {
                // OnGpsError?.Invoke("Location permission not enabled");
                yield break;
            }

            // Starts the location service.
            Input.location.Start();

            // Waits until the location service initializes
            int maxWait = 20;
            while (IsLocationServiceInitializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            // If the service didn't initialize in 20
            // seconds, this cancels location service use.
            if (maxWait < 1)
            {
                // OnGpsError?.Invoke("GPS initialization timed out");
                yield break;
            }

            // If the connection failed this cancels location service use.
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                // OnGpsError?.Invoke("Unable to determine device location");
                yield break;
            }

            while (isActiveAndEnabled)
            {
                var gpsInfo = Input.location.lastData;
                if (gpsInfo.timestamp > _lastGpsUpdateTime)
                {
                    _lastGpsUpdateTime = gpsInfo.timestamp;
                    var location = new MapsLatLng(gpsInfo.latitude, gpsInfo.longitude);
                    Debug.Log(location);
                    // UpdatePlayerLocation(location);
                }

                yield return null;
            }

            // Stops the location service if there is no
            // need to query location updates continuously.
            Input.location.Stop();
        }
    }

    public void QueryAroundUser()
    {
        // If not provided, create one on the current gameobject
        if (!coverageClientManager)
        {
            coverageClientManager = gameObject.AddComponent<CoverageClientManager>();
        }

        // Use the current location of the device to query coverage. The query radius is set to 1000 meters.
#if UNITY_EDITOR
        coverageClientManager.UseCurrentLocation = false;
        coverageClientManager.QueryRadius = 1000;
        Debug.Log(coverageClientManager.QueryLatitude);
        Debug.Log(coverageClientManager.QueryLongitude);
#endif
        coverageClientManager.QueryLatitude = (float)_lightshipMapView.SceneToLatLng(player.transform.position).Latitude;
        coverageClientManager.QueryLongitude = (float)_lightshipMapView.SceneToLatLng(player.transform.position).Longitude;
        coverageClientManager.TryGetCoverage(OnCoverageResult);
    }

    private void OnCoverageResult(AreaTargetsResult result)
    {
        // Detect all the VPS results
        if (result.Status == ResponseStatus.Success)
        {
            List<AreaTarget> locations = result.AreaTargets;
            List<AreaTarget> addList = ExceptList(locations, _curSign);
            List<AreaTarget> removeList = ExceptList(_curSign, locations);
            foreach (var location in addList)
            {
                // add
                MapsLatLng coordinates = new MapsLatLng(location.Area.Centroid.Latitude, location.Area.Centroid.Longitude);
                var marker = _publicLocationSign.PlaceInstance(coordinates, "VPS Marker");
                Debug.Log(marker);
                locations.Add(location);
            }
            foreach (var location in removeList)
            {
                // remove
                MapsLatLng coordinates = new MapsLatLng(location.Area.Centroid.Latitude, location.Area.Centroid.Longitude);
                // Vector3 position = _lightshipMapView.LatLngToScene(coordinates);
                // // Debug.log
                // RemovePrefab(position, "VPS Marker");
                // _publicLocationSign.PlaceInstance(coordinates, "VPS Marker");
                locations.Remove(location);
            }
            _curSign = locations;
        }
    }

    private List<AreaTarget> ExceptList(List<AreaTarget> setA, List<AreaTarget> setB)
    {
        // Function:
        // ExceptList(setA, setB) ==> only setA's difference between setB
        List<AreaTarget> result = new List<AreaTarget>();
        foreach (var targetA in setA)
        {
            bool existsInB = false;
            foreach (var targetB in setB)
            {
                if (targetA.Target.Identifier == targetB.Target.Identifier)
                {
                    existsInB = true;
                    break;
                }
            }
            if (!existsInB)
            {
                result.Add(targetA);
            }
        }
        return result;
    }


    public static double CalculateDistance(MapsLatLng point1, MapsLatLng point2)
    {
        double earthRadiusKm = 6371;
        double dLat = DegreesToRadians(point2.Latitude - point1.Latitude);
        double dLon = DegreesToRadians(point2.Longitude - point1.Longitude);

        double lat1 = DegreesToRadians(point1.Latitude);
        double lat2 = DegreesToRadians(point2.Latitude);

        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return earthRadiusKm * c;
    }

    private static double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }


    
}
