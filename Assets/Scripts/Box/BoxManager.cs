using UnityEngine;
using System.Collections.Generic;

public class BoxManager : MonoBehaviour
{
  public int keysCollected;

    void Start()
    {
        // Load saved keys at the start of the game or when the scene loads
        keysCollected = PlayerPrefs.GetInt("KeysCollected", 0);
    }

    // Call this method when you collect a key
    public void CollectKey()
    {
        
        keysCollected++;
        // Save the updated key count
        PlayerPrefs.SetInt("KeysCollected", keysCollected);
        PlayerPrefs.Save();  // Ensure data is saved to disk
        Debug.Log("KeysCollected");
        Debug.Log(keysCollected);

    }

    // Get the current number of keys collected
    public int GetKeyCount()
    {
        return keysCollected;
    }

    // Call this to reset keys if needed (e.g., for debugging or new game start)
    public void ResetKeys()
    {
        keysCollected = 0;
        PlayerPrefs.SetInt("KeysCollected", keysCollected);
        PlayerPrefs.Save();
    }


}
//     public enum BoxType
//     {
//         Rare,
//         Legendary,
//         Mythic,
//         Youtuber
//     }

//     [SerializeField] private BoxType[] boxTypes;
//     [SerializeField] private float[] boxRarityWeights;

//     private const string AwardedBoxesKey = "AwardedBoxes";

//     private void Start()
//     {
        
//         boxTypes = new BoxType[] { BoxType.Rare, BoxType.Legendary, BoxType.Mythic, BoxType.Youtuber };
//         boxRarityWeights = new float[] { 0.5f, 0.3f, 0.15f, 0.05f };

//         InitializeBoxCounts();
//     }

//     private void InitializeBoxCounts()
//     {
//         foreach (BoxType boxType in boxTypes)
//         {
//             if (!PlayerPrefs.HasKey(boxType.ToString()))
//             {
//                 PlayerPrefs.SetInt(boxType.ToString(), 0);
//             }
//         }
//     }

//     public BoxType GetRandomBoxType(string levelName)
//     {
//         Debug.Log($"Getting box type for level: {levelName}");

//         AdjustRarityWeights(levelName);

//         float randomValue = Random.value;
//         float cumulativeWeight = 0f;

//         for (int i = 0; i < boxRarityWeights.Length; i++)
//         {
//             cumulativeWeight += boxRarityWeights[i];
//             if (randomValue <= cumulativeWeight)
//             {
//                 BoxType awardedBox = boxTypes[i];
//                 SaveAwardedBox(awardedBox);
//                 Debug.Log($"Awarded a {awardedBox} box at level {levelName}!");
//                 return awardedBox;
//             }
//         }

//         return boxTypes[0]; // Default to the first box type if something goes wrong
//     }

//     private void AdjustRarityWeights(string levelName)
//     {
//         switch (levelName.ToLower())
//         {
//             case "stage1":
//                 boxRarityWeights = new float[] { 0.7f, 0.15f, 0f, 0f };
//                 break;
//             case "stage2":
//                 boxRarityWeights = new float[] { 0.7f, 0.4f, 0.05f, 0.0f };
//                 break;
//             case "stage3":
//                 boxRarityWeights = new float[] { 0.4f, 0.35f, 0.2f, 0.05f };
//                 break;
//             case "stage4":
//                 boxRarityWeights = new float[] { 0.2f, 0.35f, 0.25f, 0.1f };
//                 break;
//             default:
//                 Debug.LogWarning($"Unknown level name: {levelName}. Using default rarity weights.");
//                 boxRarityWeights = new float[] { 0.5f, 0.3f, 0.15f, 0.05f };
//                 break;
//         }
//     }

//     public void SaveAwardedBox(BoxType boxType)
//     {
//         // Get the current count of the awarded box type
//         int currentCount = PlayerPrefs.GetInt(boxType.ToString(), 0);

//         // Increment the count
//         PlayerPrefs.SetInt(boxType.ToString(), currentCount + 1);

//         // Debug log to confirm the increment
//         Debug.Log($"Saved awarded box: {boxType}. New count: {PlayerPrefs.GetInt(boxType.ToString(), 0)}");
//     }

//     public Dictionary<BoxType, int> GetAwardedBoxCounts()
//     {
//         Dictionary<BoxType, int> boxCounts = new Dictionary<BoxType, int>();

//         foreach (BoxType boxType in boxTypes)
//         {
//             int count = PlayerPrefs.GetInt(boxType.ToString(), 0);
//             boxCounts[boxType] = count;
//         }

//         return boxCounts;
//     }

//     public string GetAwardedBoxes()
//     {
//         var boxCounts = GetAwardedBoxCounts();
//         string awardedBoxes = "";

//         foreach (BoxType boxType in boxTypes)
//         {
//             int count = boxCounts.ContainsKey(boxType) ? boxCounts[boxType] : 0;
//             awardedBoxes += $"{boxType.ToString()} - {count}, ";
//         }

//         return awardedBoxes.TrimEnd(',', ' ');
//     }

//     public void RemoveBox(BoxType boxType)
//     {
//         // Get the current count of the awarded box type
//         int currentCount = PlayerPrefs.GetInt(boxType.ToString(), 0);

//         // Debug log to check the current count
//         Debug.Log($"Attempting to remove one {boxType}. Current count: {currentCount}");

//         // Decrement the count, ensuring it does not go below zero
//         PlayerPrefs.SetInt(boxType.ToString(), Mathf.Max(0, currentCount - 1));

//         // Debug log to confirm the updated count
//         Debug.Log($"Updated count for {boxType}: {PlayerPrefs.GetInt(boxType.ToString(), 0)}");
//     }
// }
