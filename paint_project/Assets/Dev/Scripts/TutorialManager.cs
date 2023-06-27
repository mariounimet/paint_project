using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    public int popUpIndex = 0;
    private bool isScreenTouched = false;
    private bool shouldRun = false;

    // Start is called before the first frame update
    public void Start()
    {
        popUps[0].SetActive(false);
        popUps[1].SetActive(false);
        popUps[2].SetActive(false);
    }

    // Update is called once per frame
    public void Update()
    {
        if (shouldRun) {
            if (Input.touchCount > 0)
            {
                isScreenTouched = true;
            }
            else
            {
                if (isScreenTouched) 
                {
                    UpdatePopUpIndex();
                }
                isScreenTouched = false;
            }
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

    public void firstPopUp()
    {
        popUps[0].SetActive(true);
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
                Time.timeScale = 1f;
                shouldRun = false;
                popUpIndex = 0;
        } 
    }

    public void StartRunning()
    {
        shouldRun = true;
    }

    public void StopRunning()
    {
        shouldRun = false;
    }
}
