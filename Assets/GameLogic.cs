using Assets.Potions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
    public class GameLogic : MonoBehaviour
    {
        [HideInInspector] public List<PotionType> potionsRequiredForCurrentBrew = new List<PotionType>();

        public UserInterface userInterface;
        public List<GameObject> potionPrefabs = new List<GameObject>();
        public List<GameObject> spawnPoints = new List<GameObject>();

        List<AvailablePotionPot> availablePotionPots = new List<AvailablePotionPot>();
        List<AvailableSpawnPoint> avilableSpawnPoints = new List<AvailableSpawnPoint>();

        BrewLevel brewLevel = BrewLevel.One;


        private void Start()
        {
            SetAvailablePotionPots();
            SetAvailableSpawnPoints();
            userInterface.InitialisePotionsList();
            GetPotionsRequiredForBrew(1).ForEach(x => potionsRequiredForCurrentBrew.Add(x));
            AddPotionsToMap();
            userInterface.SetPotionsCount(potionsRequiredForCurrentBrew.Count);
        }

        void SetAvailablePotionPots()
        {
            foreach (var potionPot in potionPrefabs)
            {
                var potionForCurrentPot = Instantiate(potionPot).GetComponent<Potion>();
                availablePotionPots.Add(new AvailablePotionPot(potionForCurrentPot.potionType, potionPot));
                Destroy(potionForCurrentPot.gameObject);
            }
        }

        void SetAvailableSpawnPoints()
        {
            var exclusionSet = new HashSet<int>();
            var range = Enumerable.Range(0, spawnPoints.Count).ToList();
            var rng = new System.Random();

            foreach (var spawnPoint in spawnPoints)
            {
                var currentRange = range.Where(i => !exclusionSet.Contains(i)).ToList();
                int indexToAssign = currentRange[rng.Next(currentRange.Count)];

                exclusionSet.Add(indexToAssign);
                avilableSpawnPoints.Add(new AvailableSpawnPoint(indexToAssign, true, spawnPoint));
            }
        }

        public void SetNextBrewLevel()
        {
            switch (brewLevel)
            {
                case BrewLevel.One:
                    potionsRequiredForCurrentBrew = new List<PotionType>();
                    GetPotionsRequiredForBrew(2).ForEach(x => potionsRequiredForCurrentBrew.Add(x));
                    brewLevel = BrewLevel.Two;
                    break;
                case BrewLevel.Two:
                    potionsRequiredForCurrentBrew = new List<PotionType>();
                    GetPotionsRequiredForBrew(3).ForEach(x => potionsRequiredForCurrentBrew.Add(x));
                    brewLevel = BrewLevel.Three;
                    break;
                case BrewLevel.Three:
                    // Show torches next to exit and open door
                    brewLevel = BrewLevel.Complete;
                    break;
                case BrewLevel.Complete:
                    // Complete level and show score
                    break;
                default:
                    break;
            }
            userInterface.SetPotionsCount(potionsRequiredForCurrentBrew.Count);
            AddPotionsToMap();
        }

        private List<PotionType> GetPotionsRequiredForBrew(int amountRequired)
        {
            List<PotionType> potionTypesForCurrentBrew = new List<PotionType>();

            for (int i = 1; i <= amountRequired; i++)
            {
                var potionTypeToAdd = (PotionType)new System.Random().Next(2, 5); // This needs to be adapted based on the final PotionType enum

                while (potionTypesForCurrentBrew.Contains(potionTypeToAdd))
                    potionTypeToAdd = (PotionType)new System.Random().Next(2, 5); // This needs to be adapted based on the final PotionType enum

                potionTypesForCurrentBrew.Add(potionTypeToAdd);
            }

            return potionTypesForCurrentBrew;
        }        

        public void AddPotionsToMap()
        {
            foreach (var potionRequiredForCurrentBrew in potionsRequiredForCurrentBrew)
            {
                var spawnPointIndex = new System.Random().Next(0, spawnPoints.Count - 1);

                while (!avilableSpawnPoints.Find(x => x.index == spawnPointIndex).available)
                {
                    spawnPointIndex = new System.Random().Next(0, spawnPoints.Count - 1);
                }

                var newPotion = Instantiate(
                    availablePotionPots.First(x => x.potionType == potionRequiredForCurrentBrew).potionPrefabObject,
                    avilableSpawnPoints.Single(x => x.index == spawnPointIndex).spawnPointObject.transform.position,
                    avilableSpawnPoints.Single(x => x.index == spawnPointIndex).spawnPointObject.transform.rotation
                );

                avilableSpawnPoints.Single(x => x.index == spawnPointIndex).available = false;
            }

            foreach (var spawn in avilableSpawnPoints)
            {
                spawn.available = true;
            }
        }
    }

    public enum BrewLevel
    {
        One,
        Two,
        Three,
        Complete
    }

    public class AvailableSpawnPoint
    {
        public int index;
        public bool available;
        public GameObject spawnPointObject;

        public AvailableSpawnPoint(int indexToSet, bool availableToSet, GameObject spawnPointObjectToSet)
        {
            index = indexToSet;
            available = availableToSet;
            spawnPointObject = spawnPointObjectToSet;
        }
    }

    public class AvailablePotionPot
    {
        public PotionType potionType;
        public GameObject potionPrefabObject;

        public AvailablePotionPot(PotionType potionTypeToSet, GameObject potionPrefabObjectToSet)
        {
            potionType = potionTypeToSet;
            potionPrefabObject = potionPrefabObjectToSet;
        }
    }
}
