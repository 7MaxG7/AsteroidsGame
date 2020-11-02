using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectsPool {
    static List<GameObject> bulletsPool = new List<GameObject>(16);
    static List<GameObject> asteroidsPool = new List<GameObject>(20);
    static List<GameObject> unpooledObj = new List<GameObject>(36);
    static GameObject bulletPref, asteroidPref;

    public static void Initialize() {
        bulletPref = Resources.Load<GameObject>(@"prefabs\BulletPref");
        asteroidPref = Resources.Load<GameObject>(@"prefabs\AsteroidPref");
        FillObjectsPool(bulletsPool, bulletPref);
        FillObjectsPool(asteroidsPool, asteroidPref);
    }

    static void FillObjectsPool(List<GameObject> pool, GameObject pref) {
        for (int objCounter = pool.Count; objCounter < pool.Capacity; ++objCounter) {
            pool.Add(CreateNewObject(pref));
        }
    }

    static GameObject CreateNewObject(GameObject pref) {
        GameObject newObj = GameObject.Instantiate(pref);
        Bullet newObj_BulletComp = newObj.GetComponent<Bullet>();
        Asteroid newObj_AsteroidComp = newObj.GetComponent<Asteroid>();
        if (newObj_AsteroidComp != null) {
            newObj_AsteroidComp.Initialize();
        } else if (newObj_BulletComp != null) {
            newObj_BulletComp.Initialize();
        }
        newObj.SetActive(false);
        GameObject.DontDestroyOnLoad(newObj);
        return newObj;
    }
    public static GameObject GetBulletFromPool(Vector2 position) {
        return GetObjectFromPool(bulletsPool, bulletPref, position);
    }
    public static GameObject GetAsteroidFromPool(Vector2 position) {
        return GetObjectFromPool(asteroidsPool, asteroidPref, position);
    }
    public static GameObject GetObjectFromPool(List<GameObject> pool, GameObject pref, Vector2 position) {
        GameObject obj_tmp;
        if (pool.Count > 0) {
            obj_tmp = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
        } else {
            pool.Capacity++;
            obj_tmp = CreateNewObject(pref);
        }
        obj_tmp.transform.position = position;
        obj_tmp.SetActive(true);
        unpooledObj.Add(obj_tmp);
        return obj_tmp;
    }
    public static void ReturnObjectToPool(GameObject obj) {
        Bullet obj_BulComp = obj.GetComponent<Bullet>();
        Asteroid obj_AsterComp = obj.GetComponent<Asteroid>();
        if (obj_BulComp != null) {
            bulletsPool.Add(obj);
        } else if (obj_AsterComp != null) {
            obj_AsterComp.RestoreSmallAsteroid();
            asteroidsPool.Add(obj);
        }
        obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        unpooledObj.Remove(obj);
        obj.SetActive(false);
    }
    public static void ReturnAllObjectsToPool() {
        for (int i = unpooledObj.Count - 1; i >= 0; --i) {
            ReturnObjectToPool(unpooledObj[i]);
        }
    }
}
