using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialBlockGrowth : MonoBehaviour
{
    //Material instance properties
    public MaterialPropertyBlock materialBlock;
    private MeshRenderer meshRenderer;
    [SerializeField] public float currentPosition = 1;
    [SerializeField] private float growthSpeed = 1;

    private bool isShrinking = false;
    private bool isGrowing = false;
    private float speedModifier = 1f;
    private float interpolation = 0.0f;
    private float initialPosition = 0;


    void Start()
        {
            print("O to grow, P to shrink");
            materialBlock = new MaterialPropertyBlock();
            meshRenderer = GetComponent<MeshRenderer>();
        }

    void Update()
    {
        if (Input.GetKeyDown("o") && currentPosition != 0 && !isShrinking)
        {
            //There is a delay between currentPosition and the function tick
            initialPosition = currentPosition;
            isShrinking = true;
            isGrowing = false;
        }
        else if (Input.GetKeyDown("p") && currentPosition != 1 && !isGrowing)
        {
            //There is a delay between currentPosition and the function tick
            initialPosition = currentPosition;
            isGrowing = true;
            isShrinking = false;
        }

        if (isShrinking)
        {
            Shrink();
        }
        else if (isGrowing)
        {
            Grow();
        }
    }

    private void Shrink()
    {
        currentPosition = Mathf.Lerp(initialPosition, 0, interpolation);
        ChangePropertyBlock(currentPosition);
        interpolation += (growthSpeed*speedModifier) * Time.deltaTime;
        if (currentPosition <= 0)
        {
            isShrinking = false;
            interpolation = 0.0f;
            //currentPosition = 0;
        }
    }

    private void Grow()
    {
        currentPosition = Mathf.Lerp(initialPosition, 1, interpolation);
        ChangePropertyBlock(currentPosition);
        interpolation += (growthSpeed*speedModifier) * Time.deltaTime;
        if (currentPosition >= 1)
        {
            isGrowing = false;
            interpolation = 0.0f;
            //currentPosition = 1;
        }
    } 
    private void ChangePropertyBlock(float amount)
        {
            materialBlock.SetFloat("_Growth", amount);
            meshRenderer.SetPropertyBlock(materialBlock);
        }
}
