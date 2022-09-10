using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _sensitivity;
        private Vector3 _mouseReference;
        private Vector3 _mouseOffset;
        private Vector3 _rotation;    
        private bool _isRotating { get; set; }
      
        public void UpdateMove()
        {
            if (_isRotating)
            {
                _mouseOffset = (Input.mousePosition - _mouseReference);
                _rotation.y = -(_mouseOffset.x) * _sensitivity;

                transform.eulerAngles += _rotation;

                var angle = WrapAngle(transform.eulerAngles.y);
                angle = Mathf.Clamp(angle, -90, 90);

                transform.eulerAngles = new Vector3(0, angle, 0);

                _mouseReference = Input.mousePosition;
            }         
        }
        public void StartMove()
        {
                _isRotating = true;
                _mouseReference = Input.mousePosition;                    
        }
        public void EndMove()
        {
            _isRotating = false;
        }

        private float WrapAngle(float angle)
        {
            angle %= 360;
            if (angle > 180)
                return angle - 360;

            return angle;
        }
    }
}
