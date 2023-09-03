using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Linq;
public class FilteredPlanes : MonoBehaviour
{
    private ARPlaneManager ARPlaneManager;

    private List<ARPlane> arPlanes;

    private void OnEnable()
    {
        arPlanes = new List<ARPlane>();
        ARPlaneManager = FindObjectOfType<ARPlaneManager>();
        ARPlaneManager.planesChanged += OnPlanesChanged;
    }

    private void OnDisable()
    {
        ARPlaneManager.planesChanged -= OnPlanesChanged;
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        if (args.added != null && args.added.Count > 0)
            arPlanes.AddRange(args.added);

        foreach(ARPlane plane in arPlanes.Where(Plane => Plane.extents.x * Plane.extents.y>= 0.1f))
        {
            if (plane.alignment.IsHorizontal())
            {
                plane.gameObject.SetActive(true);
            }
            else
            {
                plane.gameObject.SetActive(false);
            }
        }

        
    }
}
