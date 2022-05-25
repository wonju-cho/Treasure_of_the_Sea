using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class WeightedRandom<T>
{
    public List<Pair> randomList = new List<Pair>();
    private float totalWeight = 0;

    [System.Serializable] public struct Pair
    {
        public T item;
        public float weight;
        public Pair(T item, float weight)
        {
            this.item = item;
            this.weight = weight;
        }
    }

    public T GetRandom()
    {
        float value = Random.value * totalWeight;
        float sumWeight = 0;

        foreach(Pair p in randomList)
        {
            sumWeight += p.weight;

            if(sumWeight >= value)
            {
                return p.item;
            }
        }

        return randomList[0].item;
    }

    public void Add(T item, float weight)
    {
        randomList.Add(new Pair(item, weight));
        totalWeight += weight;
    }

}
