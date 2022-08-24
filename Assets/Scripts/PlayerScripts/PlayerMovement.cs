using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{  
    [SerializeField]private float _sensitivity = 0.7f;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private bool _isRotating;    
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_isRotating)
            {
                _mouseOffset = (Input.mousePosition - _mouseReference);
                _rotation.y = (_mouseOffset.x) * _sensitivity;

                transform.eulerAngles += _rotation;

                var angle = WrapAngle(transform.eulerAngles.y);
                angle = Mathf.Clamp(angle, -90, 90);

                transform.eulerAngles = new Vector3(0, angle, 0); 

               _mouseReference = Input.mousePosition;                
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            _isRotating = true;
            _mouseReference = Input.mousePosition;

        }
        if (Input.GetMouseButtonUp(0))
        {
            _isRotating = false;
        }

    }
    private float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }

}
