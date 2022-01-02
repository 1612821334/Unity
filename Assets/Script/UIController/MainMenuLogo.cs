using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuLogo : MonoBehaviour
{
    public GameObject logo;
    private RawImage logoImage;
    public float timer = 0;
    public float speed = 10f;
    public float loadTime = 0.1f;
    void Start()
    {
        logoImage = logo.GetComponent<RawImage>();
        logoImage.uvRect = new Rect(new Vector2(-1, 0), new Vector2(1, 1));
    }
    void Update()
    {
        if(Time.time>timer)
        {
            if (logoImage.uvRect.x <=1) 
            {
                logoImage.uvRect = 
                    new Rect(new Vector2(logoImage.uvRect.x + speed * Time.deltaTime, 0), new Vector2(1, 1));
            }
            else
            {
                logoImage.uvRect = new Rect(new Vector2(-1, 0), new Vector2(1, 1));
            }
            timer = Time.time + loadTime;
        }
    }
}
