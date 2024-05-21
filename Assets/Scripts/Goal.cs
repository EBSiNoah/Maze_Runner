using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    GameObject PlayerObj;
    GameObject RobotObj;
    public GameObject Win;
    public GameObject Defeat;
    public GameObject PauseButton;

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerObj = other.gameObject;
            Win.SetActive(true);
            Time.timeScale = 0;
            PauseButton.SetActive(false);
        }
        else if(other.tag=="Robot")
        {
            RobotObj = other.gameObject;
            Defeat.SetActive(true);
            Time.timeScale = 0;
            PauseButton.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerObj = null;
        }
        else if (other.tag == "Robot")
        {
            RobotObj = null;
        }
    }
}
