using UnityEngine;
using System.Collections;

public class GameMusicManager : MonoBehaviour
{
    private static GameMusicManager instance = null;

    public static GameMusicManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
