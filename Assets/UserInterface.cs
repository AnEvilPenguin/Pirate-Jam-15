using Assets.Potions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

namespace Assets
{
    public class UserInterface : MonoBehaviour
    {
        public Dictionary<PotionType, Sprite> PotionList;
        public Sprite emptyPotion;
        public Sprite redPotion;
        public Sprite bluePotion;
        public Sprite brownPotion;
        public Sprite greenPotion;
        public Sprite orangePotion;

        public GameObject potionSlot1;
        public GameObject potionSlot2;
        public GameObject potionSlot3;

        public void InitialisePotionsList()
        {
            PotionList = new Dictionary<PotionType, Sprite>()
            {
                { PotionType.Empty, emptyPotion },
                { PotionType.Red, redPotion },
                { PotionType.Blue, bluePotion },
                { PotionType.Brown, brownPotion },
                { PotionType.Green, greenPotion },
                { PotionType.Orange, orangePotion },
            };
        }

        public void SetPotionsCount(int count)
        {
            switch (count)
            {
                case 1:
                    potionSlot2.SetActive(false);
                    potionSlot3.SetActive(false);
                    break;
                case 2:
                    potionSlot2.SetActive(true);
                    potionSlot3.SetActive(false); 
                    break;
                case 3:
                    potionSlot2.SetActive(true);
                    potionSlot3.SetActive(true);
                    break;
                default:
                    break;
            }

            var spriteForEmptyPotion = PotionList.First(x => x.Key == PotionType.Empty).Value;
            potionSlot1.GetComponent<SpriteRenderer>().sprite = spriteForEmptyPotion;
            potionSlot2.GetComponent<SpriteRenderer>().sprite = spriteForEmptyPotion;
            potionSlot3.GetComponent<SpriteRenderer>().sprite = spriteForEmptyPotion;
        }

        public void SetPotionToSlot(Potion potionToSet)
        {
            var spriteToSet = PotionList.First(x => x.Key == potionToSet.potionType).Value;
            var spriteForEmptyPotion = PotionList.First(x => x.Key == PotionType.Empty).Value;

            if (potionSlot1.GetComponent<SpriteRenderer>().sprite == spriteForEmptyPotion)
                potionSlot1.GetComponent<SpriteRenderer>().sprite = spriteToSet;
            else if (potionSlot2.GetComponent<SpriteRenderer>().sprite == spriteForEmptyPotion)
                potionSlot2.GetComponent<SpriteRenderer>().sprite = spriteToSet;
            else if (potionSlot3.GetComponent<SpriteRenderer>().sprite == spriteForEmptyPotion)
                potionSlot3.GetComponent<SpriteRenderer>().sprite = spriteToSet;
        }
    }
}
