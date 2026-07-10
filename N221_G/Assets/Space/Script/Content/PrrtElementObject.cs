using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrrtElementObject : MonoBehaviour
{
    //인덱스 정보
    public int index = -1;
    public SpriteRenderer spriteRenderer;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsUserTableAddElementObject(collision)) {
            Debug.Log("충돌 처리 확인 ");
            UserTableAddElementObject userTableAddElementObject =
                collision.GetComponent<UserTableAddElementObject>();
            if (userTableAddElementObject.index.Equals(index)) {
                DOTween.To(() => spriteRenderer.color,
                    color => spriteRenderer.color = color, 
                    Color.white, 
                    0.5f);
            }
        }
    }

    private bool IsUserTableAddElementObject(Collider2D collision) {
        return collision.GetComponent<UserTableAddElementObject>();
    }

}
