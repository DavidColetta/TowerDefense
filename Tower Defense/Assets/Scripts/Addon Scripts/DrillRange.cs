using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillRange : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Sprite square;
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        square = (Sprite)Resources.Load("sprites/Square", typeof(Sprite));
    }
    private void OnEnable() {
        StartCoroutine("FixRange");
    }
    IEnumerator FixRange(){
        yield return new WaitForEndOfFrame();
        spriteRenderer.sprite = square;
        transform.localScale = new Vector3(transform.localScale.x/2, 1, 0);
        transform.localPosition = new Vector3(transform.localScale.x/2, 0, 0);
    }
}
