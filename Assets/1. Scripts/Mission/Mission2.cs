using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mission2 : MonoBehaviour
{
    public GameObject bottom;
    public Transform trashTransform;
    public Transform handleTransform;
    public Animator trashAnimator;
    
    private Animator animator;
    private PlayerController playerController;
    private bool isDragHandle = false;
    private Vector2 originPosition;
    private RectTransform rectTransformHandle;
    private bool isStart = false; 

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rectTransformHandle = handleTransform.GetComponent<RectTransform>();
        originPosition = rectTransformHandle.anchoredPosition;
    }

    void Update()
    {
        if (!isStart)
            return;
        
        if (isDragHandle)
        {
            trashAnimator.enabled = true;
            handleTransform.position = Input.mousePosition;
            rectTransformHandle.anchoredPosition = new Vector2(
                    originPosition.x,
                    Mathf.Clamp(rectTransformHandle.anchoredPosition.y, -115, -47));  // 핸들 드래그시 이동하는 y 좌표를 제한함
            if (Input.GetMouseButtonUp(0))
            {
                rectTransformHandle.anchoredPosition = originPosition;
                trashAnimator.enabled = false;
                isDragHandle = false;
            }
        }

        
        // 핸들이 당겨지면 trash 를 떨어트림
        var handIsPulled = rectTransformHandle.anchoredPosition.y <= -114;
        bottom.SetActive(!handIsPulled);

        var childIdx = trashTransform.childCount;
        while (childIdx-- > 0)
        {
            var trash = trashTransform.GetChild(childIdx);
            if (trash.GetComponent<RectTransform>().anchoredPosition.y < -550)
            {
                Destroy(trash.gameObject);
            }
        }

        if (trashTransform.childCount == 0)
        {
            SuccessMission();
        }


        // Rect Mask 2D: 마스크 영역 안에서만 자식 오브젝트가 보임
    }

    public void StartMission()
    {
        Debug.Log("StartMission 2");
        isStart = true;
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
        isStart = false;
        animator.SetBool("IsUp", false);
        playerController.EndMission();

        if (trashTransform.childCount > 0)
        {
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
    }

    public void ClickedHandle()
    {
        isDragHandle = true;
    }

    private void SuccessMission()
    {
        Debug.Log("SuccessMission 2");
        trashAnimator.enabled = false;
        isDragHandle = false;
        rectTransformHandle.anchoredPosition = originPosition;
        ClickedCancel();
    }
}
