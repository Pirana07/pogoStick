using UnityEngine;
using System.Collections.Generic;

public class SkinManager : MonoBehaviour
{
    public enum SkinType
    {
        CommonSkin,
        RareSkin,
        LegendarySkin,
        MythicSkin,
        YoutuberSkin
    }

    public  Dictionary<SkinType, BoxManager.BoxType> skinBoxTypeMapping;
    private List<SkinType> unlockedSkins = new List<SkinType>();

    private const string UnlockedSkinsKey = "UnlockedSkins";

    private void Start()
    {
        // Set up the mapping of skins to box types
        skinBoxTypeMapping = new Dictionary<SkinType, BoxManager.BoxType>
        {
            { SkinType.CommonSkin, BoxManager.BoxType.Rare },
            { SkinType.RareSkin, BoxManager.BoxType.Legendary },
            { SkinType.LegendarySkin, BoxManager.BoxType.Mythic },
            { SkinType.MythicSkin, BoxManager.BoxType.Mythic },
            { SkinType.YoutuberSkin, BoxManager.BoxType.Youtuber }
        };

        LoadUnlockedSkins();
    }

    public void UnlockSkin(SkinType skinType)
    {
        if (!unlockedSkins.Contains(skinType))
        {
            unlockedSkins.Add(skinType);
            SaveUnlockedSkins();
            Debug.Log($"Unlocked new skin: {skinType}");
        }
    }

    public bool IsSkinUnlocked(SkinType skinType)
    {
        return unlockedSkins.Contains(skinType);
    }

    private void SaveUnlockedSkins()
    {
        List<string> skinNames = new List<string>();
        foreach (SkinType skin in unlockedSkins)
        {
            skinNames.Add(skin.ToString());
        }
        string skinData = string.Join(",", skinNames);
        PlayerPrefs.SetString(UnlockedSkinsKey, skinData);
    }

    private void LoadUnlockedSkins()
    {
        string savedSkins = PlayerPrefs.GetString(UnlockedSkinsKey, string.Empty);
        if (!string.IsNullOrEmpty(savedSkins))
        {
            string[] skinNames = savedSkins.Split(',');
            foreach (string skinName in skinNames)
            {
                if (System.Enum.TryParse(skinName, out SkinType skinType))
                {
                    unlockedSkins.Add(skinType);
                }
            }
        }
    }

    public List<SkinType> GetUnlockedSkins()
    {
        return unlockedSkins;
    }
}
