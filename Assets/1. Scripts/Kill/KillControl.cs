using System.Collections.Generic;
using UnityEngine;

public class KillControl : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject killAnim;
    public GameObject successText;
    public GameObject mainMenuUI;

    private int killCount;
    
    public void ResetKill()
    {
        killAnim.SetActive(false);
        killCount = 0;
        SpawnNPC();
    }

    public void SpawnNPC()
    {
        foreach (var point in spawnPoints)
        {
            if (point.childCount != 0)
                Destroy(point.GetChild(0).gameObject);
        }
        
        
        var randomIndices = new HashSet<int>();
        do
        {
            randomIndices.Add(Random.Range(0, 10));
        } while (randomIndices.Count < 5);

        foreach (var i in randomIndices)
        {
            Instantiate(Resources.Load("NPC"), spawnPoints[i]);
        }
        
    }

    public void Kill()
    {
        killCount++;

        if (killCount == 5)
        {
            successText.SetActive(true);
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
