using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBG : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Material material;

    [SerializeField] private float speedX, speedY;

    private bool canMove = true;
    public bool CanMove { get { return canMove; } set { canMove = value; } }

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
    }

    void Update()
    {
        if (canMove)
        {
            MoveBackground(); 
        }
    }

    private void MoveBackground()
    {
        Vector2 offset = material.mainTextureOffset;

        offset.x -= speedX * Time.deltaTime;
        offset.y -= speedY * Time.deltaTime;

        material.mainTextureOffset = offset;
    }
}
