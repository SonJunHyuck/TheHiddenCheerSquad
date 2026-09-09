using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ScrollMaterial : MonoBehaviour
{
    private Material material;
    public float scrollSpeed = 1.0f;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        material.SetFloat("_ScrollSpeed", scrollSpeed);
    }
}
