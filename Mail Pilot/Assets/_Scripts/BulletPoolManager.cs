using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// REFERENCE CODE: https://www.raywenderlich.com/847-object-pooling-in-unity#toc-anchor-005
//i used this website to further understand the implications of object pooling and how it would work for our scenario(this assignment)
//some parts are similar but i genuinely used it to understand what's going on

//Bonus - make this class a Singleton!

[System.Serializable]
public class BulletPoolManager : MonoBehaviour
{
    public GameObject bullet;
    public int bulletNum;
    private bool expandPool = true;

    //create a structure to contain a collection of bullets
    private Queue<GameObject> bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        _BuildBulletPool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //modify this function to return a bullet from the Pool
    public GameObject GetBullet()
    {
        GameObject temp;

        //finding bullet within queue and using it if not active
  
        if (!EmptyPool())
        {
            temp = bulletPool.Dequeue();
            temp.SetActive(true);
            return temp;
        }
        

        //expanding the pool to accomodate for more bullets if needed and will return the new bullet
        if (expandPool)
        {
            GameObject bullet_t = (GameObject)Instantiate(bullet);
            bullet_t.SetActive(false);
            bulletPool.Enqueue(bullet_t);
            return bullet_t;
        }
        else
        {
            return null;
        }
    }

    //modify this function to reset/return a bullet back to the Pool 
    public void ResetBullet(GameObject bullet_t)
    {
        bullet_t.SetActive(false);
        bulletPool.Enqueue(bullet_t);
    }

    //Builds Bullet Poo;
    private void _BuildBulletPool()
    {
        //add a series of bullets to the Bullet Pool
        bulletPool = new Queue<GameObject>();
        for (int i = 0; i < bulletNum; i++)
        {
            GameObject bullet_t = (GameObject)Instantiate(bullet);
            bullet_t.SetActive(false);
            bulletPool.Enqueue(bullet_t);
        }
    }

    //Returns pool size
    public int BulletPoolSize()
    {
        return bulletPool.Count;
    }

    //checks if pool is empty
    public bool EmptyPool()
    {
        if (BulletPoolSize() <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
