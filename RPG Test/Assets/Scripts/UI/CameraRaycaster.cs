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

    RaycastHit m_hit;
    public RaycastHit hit
    {
        get { return m_hit; }
    }

    Layers m_layerHit;
    public Layers layerHit
    {
        get { return m_layerHit; }
    }

    void Start() // TODO Awake?
    {
        viewCamera = Camera.main;
    }

    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layers layer in layerPriorities)
        {
            var hit = RaycastForLayers(layer);
            if (hit.HasValue)
            {
                m_hit = hit.Value;
                m_layerHit = layer;
                return;
            }
        }

        // Otherwise return background hit
        m_hit.distance = distanceToBackground;
        m_layerHit = Layers.RaycastEndStop;
    }

    RaycastHit? RaycastForLayers(Layers layer)
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
