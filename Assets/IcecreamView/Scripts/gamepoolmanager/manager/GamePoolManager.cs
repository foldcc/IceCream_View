using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IcecreamView {

    [System.Serializable]
    public struct PoolObject
    {

        public string Table;

        public GamePoolAbstract Poolobject;

        public PoolObject(string table , GamePoolAbstract poolobject) {
            Table = table;
            Poolobject = poolobject;
        }
    }


    public class GamePoolManager : SingletonTemplate<GamePoolManager>
    {

        public Dictionary<string, GamePoolAbstract> GamePoolDic;

        public GamePoolConfig poolConfig;

        public List<PoolObject> GamePoolList;

        /// <summary>
        /// 初始化
        /// </summary>
        public GamePoolManager() {

            GamePoolDic = new Dictionary<string, GamePoolAbstract>();

            GamePoolList = new List<PoolObject>();

            SetPoolObject(poolConfig);

        }

        /// <summary>
        /// 设置对象池预制体
        /// </summary>
        /// <param name="PoolDic"></param>
        public void SetPoolObject(Dictionary<string, GamePoolAbstract> PoolDic) {
            foreach (var item in PoolDic.Keys)
            {

                if (!GamePoolDic.ContainsKey(item))
                    GamePoolDic.Add(item, PoolDic[item]);
                
            }
        }
        public void SetPoolObject(string table , GamePoolAbstract poolObject) {
            if (!GamePoolDic.ContainsKey(table))
                GamePoolDic.Add(table, poolObject);
        }
        public void SetPoolObject(GamePoolConfig config) {
            if (config != null)
            {
                config.PoolList.ForEach(poolObject => {

                    if (!GamePoolDic.ContainsKey(poolObject.Table))
                    {

                        GamePoolDic.Add(poolObject.Table, poolObject.Poolobject);

                    }
                });
            }
        }

        /// <summary>
        /// 查询是否加载了指定标签的对象
        /// </summary>
        /// <param name="ObjectName"></param>
        /// <returns></returns>
        public bool SelectObject(string ObjectName)
        {
            return GamePoolDic.ContainsKey(ObjectName);
        }

        /// <summary>
        /// 获取场景中所有激活状态下的指定对象
        /// </summary>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public List<GameObject> GetActiveObjectAll(string objectName)
        {
            List<GameObject> gameL = new List<GameObject>();
            GamePoolList.ForEach(go => {
                if (go.Table.Equals(objectName) && go.Poolobject.gameObject.activeSelf)
                {
                    gameL.Add(go.Poolobject.gameObject);
                }
            });
            return gameL;
        }

        /// <summary>
        /// 禁用指定table的所有对象
        /// </summary>
        /// <param name="table"></param>
        public void DisableObjectWitchTable(string table)
        {
            GamePoolList.ForEach(go => {
                if (go.Table.Equals(table) && go.Poolobject.gameObject.activeSelf)
                {
                    go.Poolobject.gameObject.SetActive(false);
                }
            });
        }

        /// <summary>
        /// 返回一个激活的对象到指定位置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="postion"></param>
        /// <returns></returns>
        public T InstantiatePoolObject<T>(string table, Vector3 postion , Quaternion quaternion, Vector3 scale , Transform parent = null)
        {
            if (GamePoolDic.ContainsKey(table))
            {
                foreach (PoolObject poolObject in GamePoolList)
                {
                    if (poolObject.Table.Equals(table) && !poolObject.Poolobject.gameObject.activeSelf)
                    {
                        poolObject.Poolobject.gameObject.transform.position = postion;
                        poolObject.Poolobject.gameObject.transform.rotation = quaternion;
                        poolObject.Poolobject.gameObject.transform.localScale = scale;
                        poolObject.Poolobject.gameObject.transform.parent = parent;
                        poolObject.Poolobject.pool_hash = System.Guid.NewGuid().ToString("N");
                        poolObject.Poolobject.gameObject.SetActive(true);
                        poolObject.Poolobject.Pool_Enable();
                        return poolObject.Poolobject.gameObject.GetComponent<T>();
                    }
                }

                GamePoolAbstract go = Instantiate<GamePoolAbstract>(GamePoolDic[table]);

                go.transform.position = postion;
                go.transform.rotation = quaternion;
                go.transform.localScale = scale;
                go.transform.parent = parent;
                go.pool_hash = System.Guid.NewGuid().ToString("N");
                go.Pool_Enable();
                GamePoolList.Add(new PoolObject(table, go));
                return go.GetComponent<T>();
            }

            Debug.Log("<color=bule> 错误的从对象池实例化 </color>");
            return default(T);
        }

        public T InstantiatePoolObject<T>(string table, Vector3 postion, Quaternion quaternion, Transform parent = null) {
            return InstantiatePoolObject<T>(table, postion, quaternion, Vector3.zero);
        }

        public T InstantiatePoolObject<T>(string table, Vector3 postion, Transform parent = null) {
            return InstantiatePoolObject<T>(table, postion, Quaternion.identity, Vector3.zero);
        }

        public T InstantiatePoolObject<T>(string table, Transform parent = null)
        {
            return InstantiatePoolObject<T>(table, Vector3.zero, Quaternion.identity, Vector3.zero);
        }

        public GameObject InstantiatePoolObject(string table, Vector3 postion, Quaternion quaternion, Vector3 scale, Transform parent = null)
        {
            if (GamePoolDic.ContainsKey(table))
            {
                foreach (PoolObject poolObject in GamePoolList)
                {
                    if (poolObject.Table.Equals(table) && !poolObject.Poolobject.gameObject.activeSelf)
                    {
                        poolObject.Poolobject.gameObject.transform.position = postion;
                        poolObject.Poolobject.gameObject.transform.rotation = quaternion;
                        poolObject.Poolobject.gameObject.transform.localScale = scale;
                        poolObject.Poolobject.gameObject.transform.parent = parent;
                        poolObject.Poolobject.pool_hash = System.Guid.NewGuid().ToString("N");
                        poolObject.Poolobject.gameObject.SetActive(true);
                        poolObject.Poolobject.Pool_Enable();
                        return poolObject.Poolobject.gameObject;
                    }
                }

                GamePoolAbstract go = Instantiate<GamePoolAbstract>(GamePoolDic[table]);
                go.transform.position = postion;
                go.transform.rotation = quaternion;
                go.transform.localScale = scale;
                go.transform.parent = parent;
                go.pool_hash = System.Guid.NewGuid().ToString("N");
                go.Pool_Enable();
                GamePoolList.Add(new PoolObject(table, go));
                return go.gameObject;
            }

            Debug.Log("<color=bule> 错误的从对象池实例化 </color>");
            return null;
        }

        public GameObject InstantiatePoolObject(string table, Vector3 postion, Quaternion quaternion, Transform parent = null)
        {
            return InstantiatePoolObject(table, postion, quaternion, Vector3.zero);
        }

        public GameObject InstantiatePoolObject(string table, Vector3 postion, Transform parent = null)
        {
            return InstantiatePoolObject(table, postion, Quaternion.identity, Vector3.zero);
        }

        public GameObject InstantiatePoolObject(string table, Transform parent = null)
        {
            return InstantiatePoolObject(table, Vector3.zero, Quaternion.identity, Vector3.zero);
        }

        public void Destory(GamePoolAbstract poolObject) {
            poolObject.Pool_Disable();
            poolObject.gameObject.SetActive(false);
        }
    }
}


