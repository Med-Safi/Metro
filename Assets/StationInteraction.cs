using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StationInteraction : MonoBehaviour, IPointerClickHandler
{
   

    public SubwayPathFinder pathFinder;
    public Text stationInfoText;

    
    public Material defaultMaterial;
    public Material selectedMaterial;

    
    public static GameObject startStation = null;
    public static GameObject endStation = null;

    
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
       
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material = defaultMaterial;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (startStation == null || startStation == gameObject)
        {
            if (startStation == gameObject) 
            {
                startStation = null;
                spriteRenderer.material = defaultMaterial; 
                UpdateStationInfo(""); 
            }
            else
            {
                startStation = gameObject;
                spriteRenderer.material = selectedMaterial; 
                UpdateStationInfo("Start Station: " + gameObject.name);
            }
        }
        else if (endStation == null || endStation == gameObject) 
        {
            if (endStation == gameObject) 
            {
                endStation = null;
                spriteRenderer.material = defaultMaterial; 
                UpdateStationInfo(""); 
            }
            else
            {
                endStation = gameObject;
                spriteRenderer.material = selectedMaterial; 
                UpdateStationInfo("End Station: " + gameObject.name);
            }
        }
       
        if (startStation != null && endStation != null)
        {
          
            string startStationName = startStation.name;
            string endStationName = endStation.name;

            
            pathFinder.ExecutePathfinding(startStationName, endStationName);

            
            ResetStations();
        }

    }

    private void ResetStations()
    {
        if (startStation != null)
        {
            startStation.GetComponent<SpriteRenderer>().material = defaultMaterial;
            startStation = null;
        }

        if (endStation != null)
        {
            endStation.GetComponent<SpriteRenderer>().material = defaultMaterial;
            endStation = null;
        }
        UpdateStationInfo("");
    }

    
    void UpdateStationInfo(string info)
    {
        if (stationInfoText != null)
        {
            stationInfoText.text = info;
        }
    }
}
