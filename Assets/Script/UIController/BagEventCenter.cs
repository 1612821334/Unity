using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagEventCenter : MonoBehaviour
{
    private Toggle[] pages;
    private List<GameObject> bagPages = new List<GameObject>();
    private void OnEnable()
    {
        SetLister();
        Initialization();
    }

    private void Initialization()
    {
        pages[0].isOn = true;
        for (int totalIndex = 1; totalIndex < pages.Length; totalIndex++)
        {
            pages[totalIndex].isOn = false;
        }
        bagPages[0].SetActive(true);
    }

    private void SetLister()
    {
        bagPages.Clear();
        pages = GetComponentsInChildren<Toggle>();
        for (int totalIndex = 0; totalIndex < pages.Length; totalIndex++)
        {
            int activeIndex = totalIndex;
            bagPages.Add(pages[totalIndex].transform.GetChild(0).gameObject);
            pages[totalIndex].onValueChanged.AddListener((isOn => OnToggle(isOn, activeIndex)));
        }
    }

    public void OnToggle(bool isOn,int activeIndex)
    {
        for (int indexTotal = 0; indexTotal < bagPages.Count; indexTotal++) 
        {
            bagPages[indexTotal].SetActive(false);
        }
        if(isOn)
        {
            bagPages[activeIndex].SetActive(true);
        }
    }
}
