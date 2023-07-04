using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    public int popUpIndex = 0;
    private bool isScreenTouched = false;
    private bool shouldRun = false;
    public GameObject ButtonTutorial;

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
        // if (shouldRun) {
        //     if (Input.touchCount > 0)
        //     {
        //         Touch touch = Input.GetTouch(0);
        //         if (touch.phase == TouchPhase.Began)
        //         {
        //             isScreenTouched = true;
        //             UpdatePopUpIndex();
        //         } else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
        //             isScreenTouched = false;
        //         }
        //     }
        //     else
        //     {
        //         if (isScreenTouched) 
        //         {
        //             UpdatePopUpIndex();
        //         }
        //         isScreenTouched = false;
        //     }
        // }
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
                ButtonTutorial.SetActive(false);
                Time.timeScale = 1f;
                shouldRun = false;
                popUpIndex = 0;
        } 
    }

    public void StartRunning()
    {
        shouldRun = true;
        ButtonTutorial.SetActive(true);
    }

    public void StopRunning()
    {
        shouldRun = false;
    }

    public void ResetTutorial()
    {
        popUpIndex = 0;
        popUps[0].SetActive(false);
        popUps[1].SetActive(false);
        popUps[2].SetActive(false);
        StopRunning();
    }

    public bool TutorialIsActive()
    {
        return shouldRun;
    }

    public void ButtonClicked() {
        if (shouldRun) {
            UpdatePopUpIndex();
        }
    }
}
