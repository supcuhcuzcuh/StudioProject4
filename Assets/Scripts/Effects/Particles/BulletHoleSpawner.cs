using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletHoleSpawner : MonoBehaviour
{

    [SerializeField] protected BulletHoleEffect bulletHoleEffect;

     // stack-based ObjectPool available with Unity 2021 and above
    protected IObjectPool<BulletHoleEffect> objectPool;

    // throw an exception if we try to return an existing item, already in the pool
    [SerializeField] private bool collectionCheck = true;

    // extra options to control the pool capacity and maximum size
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 100;


    private void Awake()
    {
        objectPool = new ObjectPool<BulletHoleEffect>(CreateProjectile,
            OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            collectionCheck, defaultCapacity, maxSize);
    }

    // invoked when creating an item to populate the object pool
    private BulletHoleEffect CreateProjectile()
    {
        BulletHoleEffect bulletHoleInstance = Instantiate(bulletHoleEffect);
        bulletHoleInstance.ObjectPool = objectPool;
        return bulletHoleInstance;
    }

    // invoked when returning an item to the object pool
    private void OnReleaseToPool(BulletHoleEffect pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
        pooledObject.gameObject.transform.parent = null;
    }

    // invoked when retrieving the next item from the object pool
    private void OnGetFromPool(BulletHoleEffect pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    // invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
    private void OnDestroyPooledObject(BulletHoleEffect pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }


    public void CreateBulletHole(Vector3 _hitPosition, Vector3 _normal, Transform objectHit)
    {
        if (objectPool != null)
        {
            BulletHoleEffect bulletHoleObject = objectPool.Get();

            if (bulletHoleObject == null)
                return;


            
            // align to gun barrel/muzzle position
            bulletHoleObject.transform.forward = _normal;
            bulletHoleObject.transform.SetPositionAndRotation(_hitPosition + (bulletHoleObject.transform.forward * 0.015f), bulletHoleObject.transform.rotation);
            //bulletHoleObject.transform.localScale = new Vector3(1 / objectHit.transform.localScale.x, 1 / objectHit.transform.localScale.y, 1 / objectHit.transform.localScale.z);  //set the scale of the object to follow it's own and not the parent
            bulletHoleObject.transform.SetParent(objectHit);

            // turn off after a few seconds
            bulletHoleObject.Deactivate();
        }
    }
    
}


