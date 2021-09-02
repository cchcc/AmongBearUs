using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mission2 : MonoBehaviour
{
    public Transform trashTransform;
    private Animator animator;
    private PlayerController playerController;
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void StartMission()
    {
        animator.SetBool("IsUp", true);
        playerController = FindObjectOfType<PlayerController>();

        // spawn trash
        var randomPosX = new Func<int>(() => Random.Range(-180, 180));
        var randomPosY = new Func<int>(() => Random.Range(-180, 180));
        var randomRotZ = new Func<int>(() => Random.Range(-180, 180));
        var spawnTrash = new Action<string, int>((resourcePath, size) =>
        {
            while (size-- > 0)
            {
                var trash = Instantiate(Resources.Load(resourcePath), trashTransform) as GameObject;
                var rectTransform = trash!.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(randomPosX(), randomPosY());
                rectTransform.eulerAngles = new Vector3(0, 0, randomRotZ());
            }
        });

        spawnTrash("Trash/Trash1", 3);
        spawnTrash("Trash/Trash2", 3);
        spawnTrash("Trash/Trash3", 3);
        spawnTrash("Trash/Trash4", 10);
        spawnTrash("Trash/Trash5", 10);
    }

    public void ClickedCancel()
    {
        animator.SetBool("IsUp", false);
        playerController.EndMission();
        
        IEnumerator DestroyTrash()
        {
            yield return new WaitForSeconds(1f);
            var childIdx = trashTransform.childCount;
            while (childIdx-- > 0)
            {
                var child = trashTransform.GetChild(childIdx);
                Destroy(child.gameObject);
            }
        }
        
        StartCoroutine(DestroyTrash());
    }

    private void SuccessMission()
    {
        ClickedCancel();
    }
}
