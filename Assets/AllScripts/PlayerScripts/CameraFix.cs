using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFix : MonoBehaviour
{
    private Vector3 _startPosition;
    private Ray _ray;
    private float _rayDistance = 4;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        var pos = transform.position;
        _ray = new Ray(pos + transform.forward * _rayDistance, -transform.forward);
        if (Physics.Raycast(_ray, out hit, _rayDistance + 1))
        {
            transform.position = hit.point;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _startPosition, 0.5f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(_ray);
    }
}