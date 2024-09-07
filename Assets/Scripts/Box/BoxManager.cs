using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BoxManager : MonoBehaviour
{
    public enum BoxType
    {
        Rare,
        Legendary,
        Mythic,
        Youtuber
    }

    [SerializeField] private BoxType[] boxTypes;
    [SerializeField] private float[] boxRarityWeights;

    private const string AwardedBoxesKey = "AwardedBoxes";

    private void Start()
    {
        // Example setup (You can remove or modify this as needed)
        boxTypes = new BoxType[] { BoxType.Rare, BoxType.Legendary, BoxType.Mythic, BoxType.Youtuber };
        boxRarityWeights = new float[] { 0.5f, 0.3f, 0.15f, 0.05f };
    }

    public BoxType GetRandomBoxType(string levelName)
    {
        Debug.Log($"Getting box type for level: {levelName}");

        AdjustRarityWeights(levelName);

        float randomValue = Random.value;
        float cumulativeWeight = 0f;

        for (int i = 0; i < boxRarityWeights.Length; i++)
        {
            cumulativeWeight += boxRarityWeights[i];
            if (randomValue <= cumulativeWeight)
            {
                BoxType awardedBox = boxTypes[i];
                SaveAwardedBox(awardedBox);
                Debug.Log($"Awarded a {awardedBox} box at level {levelName}!");
                return awardedBox;
            }
        }

        return boxTypes[0]; // Default to the first box type if something goes wrong
    }

    private void AdjustRarityWeights(string levelName)
    {
        switch (levelName.ToLower())
        {
            case "stage1":
                boxRarityWeights = new float[] { 0.6f, 0.25f, 0.1f, 0.05f };
                break;
            case "stage2":
                boxRarityWeights = new float[] { 0.5f, 0.3f, 0.15f, 0.05f };
                break;
            case "stage3":
                boxRarityWeights = new float[] { 0.4f, 0.35f, 0.2f, 0.05f };
                break;
            case "stage4":
                boxRarityWeights = new float[] { 0.3f, 0.4f, 0.25f, 0.05f };
                break;
            default:
                Debug.LogWarning($"Unknown level name: {levelName}. Using default rarity weights.");
                boxRarityWeights = new float[] { 0.5f, 0.3f, 0.15f, 0.05f };
                break;
        }
    }

    private void SaveAwardedBox(BoxType boxType)
    {
        // Get the current list of awarded boxes from PlayerPrefs
        string awardedBoxes = PlayerPrefs.GetString(AwardedBoxesKey, "");

        // Add the new box type to the list
        awardedBoxes += boxType.ToString() + ",";

        // Save the updated list back to PlayerPrefs
        PlayerPrefs.SetString(AwardedBoxesKey, awardedBoxes);
    }

    public Dictionary<BoxType, int> GetAwardedBoxCounts()
    {
        string awardedBoxes = PlayerPrefs.GetString(AwardedBoxesKey, "");
        string[] boxes = awardedBoxes.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

        Dictionary<BoxType, int> boxCounts = new Dictionary<BoxType, int>();
        foreach (BoxType boxType in boxTypes)
        {
            boxCounts[boxType] = boxes.Count(box => box.Equals(boxType.ToString()));
        }

        return boxCounts;
    }

    public string GetAwardedBoxes()
    {
        return PlayerPrefs.GetString(AwardedBoxesKey, "No boxes awarded yet.");
    }

    public void RemoveBox(BoxType boxType)
    {
        string awardedBoxes = PlayerPrefs.GetString(AwardedBoxesKey, "");
        string[] boxes = awardedBoxes.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

        List<string> boxList = new List<string>(boxes);
        if (boxList.Contains(boxType.ToString()))
        {
            boxList.Remove(boxType.ToString()); // Remove only the first occurrence
        }

        awardedBoxes = string.Join(",", boxList);

        PlayerPrefs.SetString(AwardedBoxesKey, awardedBoxes);
    }
}
