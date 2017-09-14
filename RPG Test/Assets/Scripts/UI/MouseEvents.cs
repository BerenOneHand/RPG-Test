using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEvents : MonoBehaviour {

    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D attackCursor = null;
    [SerializeField] Texture2D noActionCursor = null;

    [SerializeField] Vector2 cursorHotSpot = new Vector2(10, 10);

    private int numberOfClicks = 0;
    private float timer = 0.0f;
    public float doubleClickTimeWindow = 0.3f;
    CameraRaycaster cameraRaycaster;

    public void Start()
    {
        cameraRaycaster = GetComponent<CameraRaycaster>();
        cameraRaycaster.onLayerChange += OnLayerChanged; // registering
    }

    public bool IsDoubleClick()
    {
        var isDoubleClick = numberOfClicks == 2;
        if (isDoubleClick)
            numberOfClicks = 0;
        return isDoubleClick;
    }

    public void Update()
    {
        Cursor.SetCursor(walkCursor, cursorHotSpot, CursorMode.Auto);
        timer += Time.deltaTime;

        if (timer > doubleClickTimeWindow)
        {
            numberOfClicks = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            numberOfClicks++;
            timer = 0.0f;
        }
    }

    void OnLayerChanged(Layers newLayer)
    {
        //print("Cursor over new layer");
        switch (newLayer)
        {
            case Layers.Pathable:
                Cursor.SetCursor(walkCursor, cursorHotSpot, CursorMode.Auto);
                break;
            case Layers.RaycastEndStop:
                Cursor.SetCursor(noActionCursor, cursorHotSpot, CursorMode.Auto);
                break;
            case Layers.OutOfBounds:
                Cursor.SetCursor(noActionCursor, cursorHotSpot, CursorMode.Auto);
                break;
            case Layers.Unit:
                Cursor.SetCursor(attackCursor, cursorHotSpot, CursorMode.Auto);
                break;
            default:
                Debug.LogError("Don't know what cursor to show");
                return;
        }
    }
}
