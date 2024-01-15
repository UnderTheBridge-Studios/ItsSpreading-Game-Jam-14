using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialBlockGrowth : MonoBehaviour
{
        private MaterialPropertyBlock materialBlock;
        private MeshRenderer meshRenderer;
        //public float ticks = 5;
        public float growthTime = 1;
        public float initGrowth = 1;
        public float goalGrowth = 0;
        public bool DoGrow = false;
        static float interpolation = 0.0f;
        

        void Start()
        {
            materialBlock = new MaterialPropertyBlock();
            meshRenderer = GetComponent<MeshRenderer>();
        }
        void Update()
    {
        //breaks the game ATM :)
            if((Input.GetKey("e") && !DoGrow)) {
                    ChangePropertyBlock(Mathf.Lerp(initGrowth, goalGrowth, interpolation));
                    interpolation += growthTime * Time.deltaTime;
                    if(interpolation <= goalGrowth) 
                    {
                        float temp = goalGrowth;
                        goalGrowth = initGrowth;
                        interpolation = 0.0f;
                        DoGrow = true;
                    }
            }
        }
        
        private void ChangePropertyBlock(float amount)
        {
            //for(int i = 0; i < ticks; i++) {
            //amount = map(i, 0, ticks, 0, 1);
            //print("Growth: " + amount);
            materialBlock.SetFloat("_Growth", amount);
            meshRenderer.SetPropertyBlock(materialBlock);
            }

        float map(float s, float a1, float a2, float b1, float b2)
        {
            return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
        }
}