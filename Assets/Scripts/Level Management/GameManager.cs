using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private GameObject[] _structuresToProtect;
    [SerializeField] private GameObject[] _structuresToDestroy;
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = new GameObject().AddComponent<GameManager>();
                // name it for easy recognition
                _instance.name = _instance.GetType().ToString();
                // mark root as DontDestroyOnLoad();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public GameObject[] GetEnemies () {
        return _enemies;
    }

    void Awake()
    {
        _instance = this;
    }

    void CheckWinCondition () {
        foreach (GameObject structure in _structuresToProtect)
        {
            if(!structure.activeInHierarchy) return;
        }
        int numberOfenemies = _enemies.Length;
        int enemiesDefeated = 0;
        foreach (GameObject enemy in _enemies)
        {
            if(enemy.activeInHierarchy) enemiesDefeated++;
        }

        if (enemiesDefeated != numberOfenemies) return;

        int numberOfStructures = _structuresToDestroy.Length;
        int structuresDestroyed = 0;
        foreach (GameObject structure in _structuresToDestroy)
        {
            if(structure.activeInHierarchy) structuresDestroyed++;
        }

        if(structuresDestroyed != numberOfStructures) return;

        return; // replace with switch to next level
    }

    bool CheckLoseCondition() {
        int numberOfStructures = _structuresToProtect.Length;
        int structuresProtected = 0;
        foreach (GameObject structure in _structuresToProtect)
        {
            if(structure.activeInHierarchy) structuresProtected++;
        }

        if(structuresProtected == numberOfStructures) return true;

        return false;
    }
}
