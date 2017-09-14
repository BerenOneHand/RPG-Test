using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;
    public float closeDistance = 0.15f;
        
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            switch (cameraRaycaster.currentLayerHit)
            {
                case Layers.Pathable:
                    currentClickTarget = cameraRaycaster.hit.point;
                    break;
                case Layers.Unit:
                    print("Not moving to enemy");
                    break;
                case Layers.OutOfBounds:
                    print("Not moving to out of bounds area");
                    break;
                default:
                    print("Unexpected layer found");
                    return;
            }
        }
        var movingVector = currentClickTarget - transform.position;
        if (movingVector.magnitude > closeDistance) m_Character.Move(movingVector, false, false);
        else m_Character.Move(new Vector3(0,0,0), false, false);
    }
}

