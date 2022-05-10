using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Fruits> _fruitsList = new List<Fruits>();
    [SerializeField] private GameObject _spawnerPoint;
    [SerializeField] private Button _disabled;


    void Start()
    {
        for (int i = 0; i < _fruitsList.Count; i++)
        {
            var index = i;
            _fruitsList[index].ButtonFruits.onClick.AddListener(() =>  SpawnFruits(_fruitsList[index].Fruit)); 
        }
        _disabled.onClick.AddListener(() => DisabledButton());
        
    }

    private void DisabledButton()
    {
        for (int i = 0; i < _fruitsList.Count; i++)
        {
            var index = i;
            _fruitsList[index].ButtonFruits.interactable = false;
        }
    }
    private void SpawnFruits(GameObject fruits)
    {
        Instantiate(fruits,_spawnerPoint.transform);
    }
}

 [System.Serializable]
public class Fruits
{
    public Button ButtonFruits;
    public GameObject Fruit;
}
