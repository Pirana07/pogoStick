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

        // Initialize box counts if not already set
        InitializeBoxCounts();
    }

    private void InitializeBoxCounts()
    {
        foreach (BoxType boxType in boxTypes)
        {
            if (!PlayerPrefs.HasKey(boxType.ToString()))
            {
                PlayerPrefs.SetInt(boxType.ToString(), 0);
            }
        }
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
        // Get the current count of the awarded box type
        int currentCount = PlayerPrefs.GetInt(boxType.ToString(), 0);

        // Increment the count
        PlayerPrefs.SetInt(boxType.ToString(), currentCount + 1);
    }

    public Dictionary<BoxType, int> GetAwardedBoxCounts()
    {
        Dictionary<BoxType, int> boxCounts = new Dictionary<BoxType, int>();

        foreach (BoxType boxType in boxTypes)
        {
            int count = PlayerPrefs.GetInt(boxType.ToString(), 0);
            boxCounts[boxType] = count;
        }

        return boxCounts;
    }

    public string GetAwardedBoxes()
    {
        var boxCounts = GetAwardedBoxCounts();
        string awardedBoxes = "";

        foreach (var box in boxCounts)
        {
            if (box.Value > 0)
            {
                awardedBoxes += $"{box.Key.ToString()} - {box.Value}, ";
            }
        }

        return string.IsNullOrEmpty(awardedBoxes) ? "No boxes awarded yet." : awardedBoxes.TrimEnd(',', ' ');
    }

    public void RemoveBox(BoxType boxType)
    {
        // Get the current count of the awarded box type
        int currentCount = PlayerPrefs.GetInt(boxType.ToString(), 0);

        // Decrement the count, ensuring it does not go below zero
        PlayerPrefs.SetInt(boxType.ToString(), Mathf.Max(0, currentCount - 1));
    }
}
