using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Potions
{
    public class Cauldren : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public List<Sprite> idleSprites = new List<Sprite>();
        public List<Sprite> cookingSprites = new List<Sprite>();

        List<Sprite> selectedSprites = new List<Sprite>();

        [HideInInspector] public bool isCooking = false;
        int frameRate = 12;
        float idleTime;

        private void Update()
        {
            idleTime = Time.time;

            if (isCooking)
                selectedSprites = cookingSprites;
            else
                selectedSprites = idleSprites;

            PlayAnimation();
        }

        void PlayAnimation()
        {
            if (!animationCoroutineRunning)
                StartCoroutine("AnimationCoroutine", selectedSprites);
        }

        bool animationCoroutineRunning = false;
        IEnumerator AnimationCoroutine(List<Sprite> selectedSprites)
        {
            animationCoroutineRunning = true;
            selectedSprites = selectedSprites.OrderBy(sprite => sprite.name).ToList();

            for (int i = 0; i < selectedSprites.Count; i++)
            {
                spriteRenderer.sprite = selectedSprites[i];
                yield return new WaitForSeconds(0.2f);
            }
            animationCoroutineRunning = false;
            isCooking = false;
            yield break;
        }
    }
}
