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

    public BoxType[] boxTypes;
    public float[] boxRarityWeights; 

    void Start()
    {
        // Example setup
        boxTypes = new BoxType[] { BoxType.Rare, BoxType.Legendary, BoxType.Mythic, BoxType.Youtuber };
        boxRarityWeights = new float[] { 0.5f/*50%*/, 0.3f/*30%*/, 0.15f/*15%*/, 0.05f /*5%*/}; 
    }

    public BoxType GetRandomBoxType()
    {
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

        return boxTypes[0]; 
    }
}
