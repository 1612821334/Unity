using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ¾µÍ·Ëõ·Å£¬¸´Ô­
/// </summary>
public class CameraZoom : MonoBehaviour
{
    public float[] zoomLevel;
    private Camera cameraSign;
    private int index;
    private void Awake()
    {
        cameraSign = GetComponentInChildren<Camera>();
    }
    public void CameraScale()
    {
        if(Input.GetMouseButtonDown(2))
        {
            index = index < zoomLevel.Length - 1 ? index + 1 : 0;
        }
        cameraSign.fieldOfView = Mathf.Lerp(cameraSign.fieldOfView, zoomLevel[index], 0.1f);
        if (Mathf.Abs(cameraSign.fieldOfView - zoomLevel[index]) < 0.1f) 
        {
            cameraSign.fieldOfView = zoomLevel[index];
        }
    }
}
