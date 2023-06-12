// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class TutorialManager : MonoBehaviour
// {
//     public GameObject[] popUps;
//     public int popUpIndex = 0;

//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         UpdatePopUpIndex();
//         UpdatePopUp();
//     }

//     void UpdatePopUp()
//     {
//         for (int i = 0; i < popUps.Length; i++)
//         {
//             if (i == popUpIndex)
//             {
//                 popUps[i].SetActive(true);
//             }
//             else
//             {
//                 popUps[i].SetActive(false);
//             }
//         }
//     }

//     void UpdatePopUpIndex()
//     {
//         if (popUpIndex == 0)
//         {
//             if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
//             {
//                 popUpIndex++;
//             }
//         } else if (popUpIndex == 1) {
//             if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
//             {
//                 popUpIndex++;
//             }
//         } else if (popUpIndex == 2) {
//             if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
//             {
//                 popUpIndex++;
//                 //spawner
//             }
//         }
//     }
// }
