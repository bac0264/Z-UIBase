using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadResourceController
{
    public static readonly Dictionary<string, Object> resourceCache = new Dictionary<string, Object>();
    public static readonly Dictionary<string, Object[]> resourcesCache = new Dictionary<string, Object[]>();

    public static T LoadFromResource<T>(string path, string fileName = " ") where T : Object
    {
        string fullPath;
        if (!fileName.Equals(" "))
            fullPath = Path.Combine(path, fileName);
        else fullPath = path;
        if (!resourceCache.ContainsKey(fullPath))
        {
            resourceCache.Add(fullPath, Resources.Load<T>(fullPath));
        }
        return resourceCache[fullPath] as T;
    }
    public static T[] LoadFromResources<T>(string path, string fileName = " ") where T : Object
    {
        string fullPath;
        if (!fileName.Equals(" "))
            fullPath = Path.Combine(path, fileName);
        else fullPath = path;
        if (!resourcesCache.ContainsKey(fullPath))
        {
            T[] datas = Resources.LoadAll<T>(fullPath) as T[];
            resourcesCache.Add(fullPath, datas);
            return datas;
        }

        return resourcesCache[fullPath] as T[];
    }
    public static T[] LoadFromResourcesWithNoCache<T>(string path, string fileName = " ") where T : Object
    {
        string fullPath;
        if (!fileName.Equals(" "))
            fullPath = Path.Combine(path, fileName);
        else fullPath = path;
        return Resources.LoadAll<T>(fullPath) as T[];
    }

    public static Sprite GetItemIcon(int type, int id)
    {
        var _type = type / 1000;
        return LoadFromResource<Sprite>(string.Format(PathUtils.itemResourceIcons, _type, id));
    }
    
    public static Sprite GetCharacterItem(int id)
    {
        return LoadFromResource<Sprite>(string.Format(PathUtils.itemCharacters, id));
    }
    
    public static Sprite GetFrameWithPriority(int priority)
    {
        return LoadFromResource<Sprite>(string.Format(PathUtils.itemFrames, priority));
    }
    
    public static Sprite GetRawPackIcon(int id)
    {
        return LoadFromResource<Sprite>(string.Format(PathUtils.itemRawPackIcons, id));
    }
}
