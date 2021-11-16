using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera/Mouse Look")]
public class MouseLook : MonoBehaviour
{
    //3��ö��
    // �����ʾ��ǰ����ģʽ���ֱ���
    // MouseXAndY����������ת
    // MouseXֻ��������ת
    // MouseYֻ��������ת
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;

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
    public Camera cameraEye;
    void Start()
    {
        // ����������ת����
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
        cameraEye = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        //���������������ת��ģʽ
        if (axes == RotationAxes.MouseXAndY)
        {
            // �������X���������� Y����ת�Ƕ�
            rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            // �������Y����������x����ת�Ƕ�
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

            // ���������ת�ǶȲ����� minimumY��maximumY
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            rotationX = Mathf.Clamp(rotationY, minimumX, maximumX);

            // �����������ת�Ƕ�
            cameraEye.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            if(Mathf.Abs(rotationX)>0)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }
        }
        else if (axes == RotationAxes.MouseX)//���ֻ��������ת
        {
            cameraEye.transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
        else//���ֻ��������ת
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            cameraEye.transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }
    }
}