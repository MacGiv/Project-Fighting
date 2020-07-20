using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFighting.FirstRound
{
    public class PlayerAfterImagePool : MonoBehaviour
    {
        [SerializeField] GameObject afterImagePrefab = null;

        Queue<GameObject> availableGameObjects = new Queue<GameObject>();

        public static PlayerAfterImagePool Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            GrowPool();
        }

        private void GrowPool()
        {
            for (int i = 0; i < 10; i++)
            {
                var instanceToAdd = Instantiate(afterImagePrefab);
                instanceToAdd.transform.SetParent(transform);
                AddToPool(instanceToAdd);
            }
        }

        public void AddToPool(GameObject instance)
        {
            instance.SetActive(false);
            availableGameObjects.Enqueue(instance);
        }

        public GameObject GetFromPool()
        {
            if (availableGameObjects.Count == 0)
            {
                GrowPool();
            }

            var instance = availableGameObjects.Dequeue();
            instance.SetActive(true);
            return instance;
        }

    }

}

