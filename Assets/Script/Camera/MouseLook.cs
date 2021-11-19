using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera/Mouse Look")]
[RequireComponent(typeof(CameraZoom))]
public class MouseLook : MonoBehaviour
{
    // 这俩是左右上下旋转时候的灵敏度
    public float sensitivityX = 10F;
    public float sensitivityY = 10F;

    // 左右旋转的最大角度
    public float minimumX = -60F;
    public float maximumX = 60F;

    //上下旋转最大角度
    public float minimumY = -30F;
    public float maximumY = 30F;

    float rotationY = 0F;
    float rotationX = 0F;
    private Camera cameraEye;
    private CameraZoom Zoom;
    void Start()
    {
        // 冻结刚体的旋转功能
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
        cameraEye = GetComponentInChildren<Camera>();
        Zoom = GetComponent<CameraZoom>();
    }

    void Update()
    {
        Zoom.CameraScale();
        // 根据鼠标X轴计算摄像机 Y轴旋转角度
        rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

        // 根据鼠标Y轴计算摄像机x轴旋转角度
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        if (rotationX != 0 && rotationY != 0)
        {
            RotateView();
        }
    }

    private void RotateView()
    {
        // 检查上下旋转角度不超过 minimumY和maximumY
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
        rotationX = Mathf.Clamp(rotationY, minimumX, maximumX);

        // 设置摄像机旋转角度
        cameraEye.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        if (Mathf.Abs(rotationX) > 0)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
    }
}