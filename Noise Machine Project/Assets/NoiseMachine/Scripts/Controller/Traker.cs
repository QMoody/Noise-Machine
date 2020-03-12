﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Traker : MonoBehaviour
{
    //Initializations for positions used in calculations
    float YSPos;
    float ZSPos;

    float YCPos;
    float ZCPos;

    float YPOSroc;
    float ZPOSroc;

    float totalDis;
    public float goalDis = 250;

    //UI references
    public GameObject distance;
    public GameObject winText;
    public GameObject speedometer;

    void Start()
    {
        YSPos = transform.position.y;
        ZSPos = transform.position.z;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CurrentSpeed();
        DistanceTravelled();

        if (totalDis > goalDis)
        {
            winText.SetActive(true); //Shows text that you have won
            Invoke("Win", 3); //Restarts the game after 3 seconds

        }
    }

    void CurrentSpeed() 
    {
        //UI update for players current speed in km/h
        speedometer.GetComponent<Text>().text = (Mathf.FloorToInt(GetComponent<Rigidbody>().velocity.magnitude * 3.6F)).ToString() + " km/h";

        //Will be a speedometer arrow that shows that same thing
    }

    void DistanceTravelled()
    {
        //calculates total distance travelled
        YCPos = transform.position.y;
        ZCPos = transform.position.z;

        YPOSroc = (YSPos - YCPos);
        ZPOSroc = (ZCPos - ZSPos);

        totalDis = Mathf.FloorToInt(Mathf.Sqrt((YPOSroc * YPOSroc) + (ZPOSroc * ZPOSroc)));


        //UI update that shows the elevation (distance travelled) of the player moving towards the goal by moving the text displaying the elevation from the start until the goal (happens in the position.y)
        distance.GetComponent<RectTransform>().position = new Vector3(distance.GetComponent<RectTransform>().position.x, 500 - ( 500 * (totalDis / goalDis)), distance.GetComponent<RectTransform>().position.z);

        //UI update that shows the elevation (distance travelled) in text
        distance.GetComponent<Text>().text = (totalDis).ToString() + "m"; 
    }

    void Win()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}