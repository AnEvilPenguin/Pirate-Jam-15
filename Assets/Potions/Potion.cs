using UnityEngine;

namespace Assets.Potions
{
    public class Potion : MonoBehaviour
    {
        public PotionType potionType;
    }

    public enum PotionType
    {
        Empty = 0,
        Red = 1,
        Blue = 2,
        Brown = 3,
        Green = 4,
        Orange = 5
    }
}
