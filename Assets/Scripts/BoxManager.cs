using UnityEngine;

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

    private void Start()
    {
        // Example setup (You can remove or modify this as needed)
        boxTypes = new BoxType[] { BoxType.Rare, BoxType.Legendary, BoxType.Mythic, BoxType.Youtuber };
        boxRarityWeights = new float[] { 0.5f, 0.3f, 0.15f, 0.05f }; 
    }

    public BoxType GetRandomBoxType(string levelName)
    {
        // Log the level name for debugging
        Debug.Log($"Getting box type for level: {levelName}");

        // Adjust weights based on the level
        AdjustRarityWeights(levelName);

        float randomValue = Random.value;
        float cumulativeWeight = 0f;

        for (int i = 0; i < boxRarityWeights.Length; i++)
        {
            cumulativeWeight += boxRarityWeights[i];
            if (randomValue <= cumulativeWeight)
            {
                return boxTypes[i];
            }
        }

        return boxTypes[0]; // Default to the first box type if something goes wrong
    }

    private void AdjustRarityWeights(string levelName)
    {
        // Adjust the rarity weights based on the level name
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
}
