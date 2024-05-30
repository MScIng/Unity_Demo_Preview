using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Niantic.Lightship.Maps;

public class MapTileTracker : MonoBehaviour
{
    private LightshipMapView _lightshipMapView;
    public GameObject player;

    void Start()
    {
        // Get the LightshipMapView component from the GameObject this script is attached to
        _lightshipMapView = GetComponent<LightshipMapView>();

        if (_lightshipMapView != null)
        {
            // Subscribe to the MapTileAdded event
            // _lightshipMapView.MapTileAdded += OnMapTileAdded;
        }
        else
        {
            Debug.LogError("LightshipMapView component not found on this GameObject.");
        }
    }

    private void OnMapTileAdded()
    {
        // Custom logic to execute when a map tile is added
        Debug.Log("A new map tile has been added.");
    }

    void OnDestroy()
    {
        // Unsubscribe from the event when this script is destroyed
        if (_lightshipMapView != null)
        {
            // _lightshipMapView.MapTileAdded -= OnMapTileAdded;
        }
    }
}
