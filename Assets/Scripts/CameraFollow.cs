using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float speedMove, speedRotate;
    public static CameraFollow CF;

    private void Awake()
    {
        CF = this;
    }
    private void Update()
    {
        if (!GameManager.ins._isPause)
        {
            if (!GameManager.ins._isEndGame)
            {
                transform.position = target.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * speedRotate);
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(4f, 12.6f, -12.6f), Time.deltaTime * 2);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(40.665f, -19.6211f, -1.161f), Time.deltaTime * 2);
            }
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0f, 2.63f, -6.96f), Time.deltaTime * 2);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(15.804f, 1.024f, 0.069f), Time.deltaTime * 2);
        }
    }
}
