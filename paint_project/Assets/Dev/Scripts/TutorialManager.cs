using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    public int popUpIndex = 0;
    private bool isScreenTouched = false;

    // Start is called before the first frame update
    void Start()
    {
        popUps[0].SetActive(true);
        popUps[1].SetActive(false);
        popUps[2].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                isScreenTouched = true;
                UpdatePopUpIndex();
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isScreenTouched = false;
            }
        }
        else
        {
            isScreenTouched = false;
        }
    }

    void UpdatePopUp()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }
    }

    void UpdatePopUpIndex()
    {
        if (popUpIndex == 0)
        {
                popUpIndex++;
                UpdatePopUp();
        } else if (popUpIndex == 1) {
                popUpIndex++;
                UpdatePopUp();
        } else if (popUpIndex == 2) {
                popUpIndex++;
                UpdatePopUp();
        }
    }
}
