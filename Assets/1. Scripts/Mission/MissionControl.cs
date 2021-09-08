using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionControl : MonoBehaviour
{
    public Slider missionGauge;
    public GameObject textSuccess;
    public GameObject mainMenuUI;
    
    private readonly List<GameObject> successMissions = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetMissions()
    {
        textSuccess.SetActive(false);
        missionGauge.value = 0f;
        successMissions.ForEach(o => o.SetActive(true));
        successMissions.Clear();
    }

    public void SuccessMission(GameObject mission)
    {
        mission.SetActive(false);
        successMissions.Add(mission);
        missionGauge.value = successMissions.Count / 7f;

        var isComplete = successMissions.Count == 7;
        if (isComplete)
        {
            textSuccess.SetActive(true);
            Invoke(nameof(GoBackToMainMenu), 1f);
        }
    }
    
    public void GoBackToMainMenu()
    {
        mainMenuUI.SetActive(true);
        gameObject.SetActive(false);  // mission
        FindObjectOfType<PlayerController>().DestroyPlayer();
    }
}
