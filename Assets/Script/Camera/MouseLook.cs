using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera/Mouse Look")]
[RequireComponent(typeof(CameraZoom))]
public class MouseLook : MonoBehaviour
{
    // ����������������תʱ���������
    public float sensitivityX = 10F;
    public float sensitivityY = 10F;

    // ������ת�����Ƕ�
    public float minimumX = -60F;
    public float maximumX = 60F;

    //������ת���Ƕ�
    public float minimumY = -30F;
    public float maximumY = 30F;

    float rotationY = 0F;
    float rotationX = 0F;
    private Camera cameraEye;
    private CameraZoom Zoom;
    void Start()
    {
        // ����������ת����
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
        cameraEye = GetComponentInChildren<Camera>();
        Zoom = GetComponent<CameraZoom>();
    }

    void Update()
    {
        Zoom.CameraScale();
        // �������X���������� Y����ת�Ƕ�
        rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

        // �������Y����������x����ת�Ƕ�
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        if (rotationX != 0 && rotationY != 0)
        {
            RotateView();
        }
    }

    private void RotateView()
    {
        // ���������ת�ǶȲ����� minimumY��maximumY
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
        rotationX = Mathf.Clamp(rotationY, minimumX, maximumX);

        // �����������ת�Ƕ�
        cameraEye.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        if (Mathf.Abs(rotationX) > 0)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
    }
}