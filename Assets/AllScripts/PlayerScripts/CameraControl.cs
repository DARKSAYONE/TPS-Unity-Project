using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] public float RotationSpeed = 10.0f;
    [SerializeField] public Transform CamAxis;
    [SerializeField] public float MinAngle;
    [SerializeField] public float MaxAngle;


    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        CameraRotation();
    }

    void CameraRotation()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y + Time.deltaTime * RotationSpeed * Input.GetAxis("Mouse X"),0);
        var newAngleX = CamAxis.localEulerAngles.x + Time.deltaTime * RotationSpeed * Input.GetAxis("Mouse Y");
        if (newAngleX > 180)
            newAngleX -= 360;
        newAngleX = Mathf.Clamp(newAngleX, MinAngle, MaxAngle);
        CamAxis.localEulerAngles = new Vector3(newAngleX, 0, 0);
    }
}
