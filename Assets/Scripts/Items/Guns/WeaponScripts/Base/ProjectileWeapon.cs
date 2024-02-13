using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class ProjectileWeapon : Weapon
{
    [Tooltip("Prefab to shoot")]
    [SerializeField] protected RevisedProjectile projectilePrefab;
    [Tooltip("Projectile force")]
    [SerializeField] protected float muzzleVelocity = 500f;

    // stack-based ObjectPool available with Unity 2021 and above
    protected IObjectPool<RevisedProjectile> objectPool;

    // throw an exception if we try to return an existing item, already in the pool
    [SerializeField] private bool collectionCheck = true;

    // extra options to control the pool capacity and maximum size
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 100; 

    private float nextTimeToShoot;

    private void Awake()
    {
        objectPool = new ObjectPool<RevisedProjectile>(CreateProjectile,
            OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            collectionCheck, defaultCapacity, maxSize);
    }

    // invoked when creating an item to populate the object pool
    private RevisedProjectile CreateProjectile()
    {
        RevisedProjectile projectileInstance = Instantiate(projectilePrefab);
        projectileInstance.ObjectPool = objectPool;
        return projectileInstance;
    }

    // invoked when returning an item to the object pool
    private void OnReleaseToPool(RevisedProjectile pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    // invoked when retrieving the next item from the object pool
    private void OnGetFromPool(RevisedProjectile pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    // invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
    private void OnDestroyPooledObject(RevisedProjectile pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    public void SpawnBulletHole(Vector3 _point, Vector3 _normal, Transform _objectHit)
    {
        bulletHoleSpawner.CreateBulletHole(_point, _normal, _objectHit);
    }

    public virtual Transform GetTarget()
    {
        return null;
    }

}
