using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Glow : MonoBehaviour
{
    [ColorUsage(true, true)]
    public Color glowColor;
    private MaterialPropertyBlock _propBlock;
    private SpriteRenderer _renderer;
    void Awake() {
        _propBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<SpriteRenderer>();
    }
    private void Start() {
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetColor("_GlowColor",glowColor);
        _renderer.SetPropertyBlock(_propBlock);
    }
}
