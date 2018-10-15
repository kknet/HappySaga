// DestroyPath.cs is part of 2D Physics Draw asset for Unity 3D
// Made by Tianguosong
// Date: 2019.9.19

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace com.zonglv.minigame.draw
{

    public class DestroySpritePath : MonoBehaviour
    {

        public bool isPermanent;
        private List<Vector2> newVerticies = new List<Vector2>();
        public float destroyCounter;
        private bool canDestroy = false;
        private Vector2 centerOfMass = Vector2.zero;
        private DrawingSprite managerScript;
        private bool dynamicMass;
        private float massScale;

        void Start()
        {

            managerScript = GameObject.Find("DrawingSprite").GetComponent<DrawingSprite>();
            newVerticies = managerScript.newVerticies;
            destroyCounter = managerScript.lifeTime;
            isPermanent = managerScript.isPermanent;
        }

        void Update()
        {

            if (Input.GetMouseButtonUp(0) && this.name.Equals("Drawing" + (DrawingSprite.cloneNumber - 1)))
            {

                foreach (Vector2 positions in newVerticies)
                {
                    centerOfMass += positions;

                }
                centerOfMass /= newVerticies.Count;
                canDestroy = true;
            }

            if (destroyCounter > 0 && isPermanent == false && canDestroy == true)
            {
                destroyCounter -= Time.deltaTime;
                if (destroyCounter <= 0)
                {
                    Destroy(this.gameObject);
                }
            }

        }
    }
}
