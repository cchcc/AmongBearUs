using UnityEngine;

public class NPCControl : MonoBehaviour
{
    public Sprite[] idle;
    public Sprite[] dead;
    private SpriteRenderer spriteRenderer;
    private int idx;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        idx = Random.Range(0, idle.Length);
        spriteRenderer.sprite = idle[idx];
    }

    public void Dead()
    {
        spriteRenderer.sprite = dead[idx];
        spriteRenderer.sortingOrder = -1;
    }
}
