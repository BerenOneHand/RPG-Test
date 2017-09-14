using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layers[] layerPriorities = {
        Layers.OutOfBounds,
        Layers.Unit,
        Layers.Pathable
    };

    float distanceToBackground = 250f;
    Camera viewCamera;

    RaycastHit raycastHit;
    public RaycastHit hit
    {
        get { return raycastHit; }
    }

    Layers layerHit;
    public Layers currentLayerHit
    {
        get { return layerHit; }
    }

    public delegate void OnLayerChange(Layers newLayer); // declare new delegate type
    public event OnLayerChange onLayerChange; // instantiate an observer set

    void Start() // TODO Awake?
    {
        viewCamera = Camera.main;
    }

    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layers layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                raycastHit = hit.Value;
                if (layerHit != layer) // if layer has changed
                {
                    layerHit = layer;
                    onLayerChange(layer); // call the delegates
                }
                layerHit = layer;
                return;
            }
        }

        // Otherwise return background hit
        raycastHit.distance = distanceToBackground;
        layerHit = Layers.RaycastEndStop;
    }

    RaycastHit? RaycastForLayer(Layers layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
