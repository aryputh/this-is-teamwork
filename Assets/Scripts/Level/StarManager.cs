using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarManager : MonoBehaviour
{
    [SerializeField] private GameObject starSprite;
    [SerializeField] private GameObject starFlashSprite;
    [SerializeField] private GameObject collectSoundEffect;
    [SerializeField] private ParticleSystem starParticles;
    [SerializeField] private int starNum = 1;

    private string starId;

    void Start()
    {
        starId = SceneManager.GetActiveScene().name + "Star" + starNum;

        if (PlayerPrefs.GetInt(starId, 0) == 1)
        {
            gameObject.SetActive(false);
        }
    }

    public void isCollected() {
        starSprite.SetActive(false);
        starFlashSprite.SetActive(false);
        collectSoundEffect.SetActive(true);

        starParticles.Stop();
        
        PlayerPrefs.SetInt("starsCollected", PlayerPrefs.GetInt("starsCollected", 0) + 1);
        PlayerPrefs.SetInt(starId, 1);
    }
}
