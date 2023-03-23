using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{
    public GameObject monster;

    public int activeFromNight = 0;
    bool isActive = false;

    [SerializeField] AudioSource spawnSound;

    void Awake()
    {
        DayNightCycle.updateNightCountUI += CheckNight;
    }

    void Start()
    {
        monster = Instantiate(monster, transform.position, transform.rotation);
        monster.SetActive(false);
    }
    void OnDestroy()
    {
        DayNightCycle.updateNightCountUI -= CheckNight;
    }

    void CheckNight(int night)
    {
        if (night >= activeFromNight)
        {
            isActive = true;
        }
    }

    public bool SpawnMonster()
    {
        if (!isActive || monster.activeSelf) return false;

        spawnSound.Play();
        monster.transform.position = transform.position;
        monster.SetActive(true);

        return true;
    }

}
