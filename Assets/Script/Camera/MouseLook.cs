using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera/Mouse Look")]
public class MouseLook : MonoBehaviour
{
    //3个枚举
    // 这个表示当前控制模式，分别是
    // MouseXAndY上下左右旋转
    // MouseX只能左右旋转
    // MouseY只能上下旋转
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;

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
    public Camera cameraEye;
    void Start()
    {
        // 冻结刚体的旋转功能
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
        cameraEye = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        //如果是上下左右旋转的模式
        if (axes == RotationAxes.MouseXAndY)
        {
            // 根据鼠标X轴计算摄像机 Y轴旋转角度
            rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            // 根据鼠标Y轴计算摄像机x轴旋转角度
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

            // 检查上下旋转角度不超过 minimumY和maximumY
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            rotationX = Mathf.Clamp(rotationY, minimumX, maximumX);

            // 设置摄像机旋转角度
            cameraEye.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            if(Mathf.Abs(rotationX)>0)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }
        }
        else if (axes == RotationAxes.MouseX)//如果只是左右旋转
        {
            cameraEye.transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
        else//如果只是上下旋转
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            cameraEye.transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }
    }
}