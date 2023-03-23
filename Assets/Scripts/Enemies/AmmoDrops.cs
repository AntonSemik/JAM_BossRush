using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDrops : MonoBehaviour
{
    [SerializeField] GameObject ammoBox;
    [SerializeField] int poolSize;

    public delegate void DropAmmo(Vector3 value);
    public static DropAmmo dropAmmo;

    Queue<GameObject> ammoPool = new Queue<GameObject>();
    GameObject tempObj;

    private void Awake()
    {
        InitialisePool();

        dropAmmo += DropAmmoBox;
    }

    private void OnDestroy()
    {
        dropAmmo -= DropAmmoBox;
    }

    void InitialisePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            tempObj = Instantiate(ammoBox, transform.position, transform.rotation);
            tempObj.SetActive(false);
            ammoPool.Enqueue(tempObj);

            tempObj = null;
        }
    }

    private void DropAmmoBox(Vector3 position)
    {
        if (!ammoPool.Peek().activeSelf)
        {
            tempObj = ammoPool.Dequeue();
            ammoPool.Enqueue(tempObj);

            tempObj.transform.position = position;
            tempObj.SetActive(true);
        }
    }
}
