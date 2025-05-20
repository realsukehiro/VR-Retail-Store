//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SpriteManager : MonoBehaviour
//{
//    public static SpriteManager Instance;
//    private Dictionary<string, Sprite> spriteDict = new Dictionary<string, Sprite>();

//    void Awake()
//    {
//        if (Instance != null && Instance != this)
//        {
//            Destroy(gameObject);
//            return;
//        }

//        Instance = this;
//        DontDestroyOnLoad(gameObject);
//        LoadAllSprites();
//    }

//    private void LoadAllSprites()
//    {
//        Sprite[] sprites = Resources.LoadAll<Sprite>("ProductSprites");
//        foreach (Sprite s in sprites) spriteDict[s.name] = s;
//    }

//    public Sprite GetSprite(string name)
//    {
//        if (spriteDict.TryGetValue(name, out Sprite result)) return result;
//        Debug.LogWarning("Missing sprite: " + name);
//        return null;
//    }

//    #if UNITY_EDITOR
//    void OnApplicationQuit()
//    {
//        Instance = null;
//    }
//    #endif
//}

