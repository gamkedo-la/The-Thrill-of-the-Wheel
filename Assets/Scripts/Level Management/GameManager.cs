using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Win Conditions")]

    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private GameObject[] _structuresToProtect;
    [SerializeField] private GameObject[] _structuresToDestroy;
    [Header("Scene Management")]
    [SerializeField] private GameObject _boatTank;
    [SerializeField] private GameObject _armadillo;
    [SerializeField] private CameraFollow _camera;

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
        string selectedCar = PlayerPrefs.GetString("SelectedCar");
        switch (selectedCar)
        {
            case "Tank Boat":
                Debug.Log("asd");
                _boatTank.SetActive(true);
                _camera.ChangeCameraTarget(_boatTank.transform);
                break;
            case "Armadillo":
                Debug.Log("armadillo");
                _armadillo.SetActive(true);
                _camera.ChangeCameraTarget(_armadillo.transform);
                break;
            default:
                _camera.ChangeCameraTarget(_boatTank.transform);
                _boatTank.SetActive(true);
                break;
        }
        Debug.Log(selectedCar);
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
