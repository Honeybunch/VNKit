using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VNKit
{
    public class Background : MonoBehaviour
    {
        public bool AutoSize = true;

        private Vector3 lastPosition;
        private Resolution lastScreenRes;

        public void Start() 
        {
            lastPosition = transform.position;
            lastScreenRes = Screen.currentResolution;

            if (AutoSize)
                SizeToFitScreen();
        }

        /// <summary>
        /// Called frequently; used to make sure that the background is updated with the size of the screen
        /// </summary>
        public void Update() 
        {
            if (AutoSize)
            {
                if (transform.position != lastPosition)
                {
                    SizeToFitScreen();
                    lastPosition = transform.position;
                }
                else if (Screen.currentResolution.width != lastScreenRes.width || Screen.currentResolution.height != lastScreenRes.height)
                {
                    SizeToFitScreen();
                    lastScreenRes = Screen.currentResolution;
                }
            }
        }

        /// <summary>
        /// Automatically scale the game object so that its sprite will fill the screen size
        /// </summary>
        public void SizeToFitScreen() 
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
                return;

            transform.localScale = new Vector3(1, 1, 1);

            float width = spriteRenderer.sprite.bounds.size.x;
            float height = spriteRenderer.sprite.bounds.size.y;

            float distanceToCamera = Vector3.Distance(transform.position, Camera.main.transform.position);

            float worldScreenHeight = 2.0f * Mathf.Tan(0.5f * Camera.main.fieldOfView * Mathf.Deg2Rad) * distanceToCamera;
            float worldScreenWidth = worldScreenHeight * Camera.main.aspect;

            transform.localScale = new Vector3(worldScreenWidth/width, worldScreenHeight/height, 1);
        }
    }

}
