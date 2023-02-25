using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Win Conditions")]

    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private GameObject[] _structuresToProtect;
    [SerializeField] private GameObject[] _structuresToDestroy;
    [SerializeField] private Transform _player;
    [Header("Scene Management")]
    [SerializeField] private GameObject _boatTank;
    [SerializeField] private GameObject _armadillo;
    [SerializeField] private GameObject _armoredTruck;
    [SerializeField] private CameraFollow _camera;
    [SerializeField] private Transform _respawnPoints;
    [SerializeField] private WeaponUI inventoryUI;

    public static GameManager _instance;
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

    public Transform GetClosestEnemy () {
        float minDistance = float.PositiveInfinity;
        Transform closestEnemy = _enemies[0].transform;
        foreach (GameObject enemy in _enemies)
        {
            if(enemy.activeInHierarchy) {
                Vector3 enemyPosition = enemy.transform.position;
                float distanceToEnemy = Vector3.Distance(_player.position, enemyPosition);
                if( distanceToEnemy < minDistance) {
                    minDistance = distanceToEnemy;
                    closestEnemy = enemy.transform;
                }
            }
        }
        return closestEnemy;
    }

    void Awake()
    {
        _instance = this;
        string selectedCar = PlayerPrefs.GetString("SelectedCar");
        
        // Initial declaration handles default case.
        var carInstance = _boatTank;
        Transform selectedCarTransform = _boatTank.transform;
        
        switch (selectedCar)
        {
            case "Tank Boat":
                Debug.Log("Tank Boat");
                _boatTank.SetActive(true);
                selectedCarTransform = _boatTank.transform;
                break;
            case "Armadillo":
                Debug.Log("armadillo");
                carInstance = _armadillo;
                selectedCarTransform = _armadillo.transform;
                break;
            case "Armored Truck":
                carInstance = _armoredTruck;
                selectedCarTransform = _armoredTruck.transform;
                Debug.Log("Armored Truck");
                break;
        }

        foreach (GameObject enemy in _enemies)
        {
            randomDriverAI enemyAI = enemy.GetComponent<randomDriverAI>();
            enemyAI.AI_target = selectedCarTransform;
            enemyAI.player = selectedCarTransform;
            enemy.GetComponent<EnemyWeapons>().SetTarget(selectedCarTransform);
        }
        _player = selectedCarTransform;
        inventoryUI.SetWeaponInventory(selectedCarTransform.GetComponent<WeaponInventory>());

        // Set selected car active.
        carInstance.SetActive(true);
        
        // Set camera to follow CameraTarget transform. Fall back to car transform
        // if no component is found.
        var cameraTarget = carInstance.GetComponentInChildren<CameraTarget>();
        _camera.ChangeCameraTarget(cameraTarget ? cameraTarget.transform : carInstance.transform);
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

    public void RespawnCar(Transform stuckPosition) {
        float minDistance = float.MaxValue;
        Vector3 respawnPosition = new Vector3(0, 0.5f, 0);
        foreach (Transform children in _respawnPoints)
        {
            float newDistance = Vector3.Distance(stuckPosition.position, children.position);
            if(newDistance < minDistance) {
                minDistance = newDistance;
                respawnPosition = children.position;
            }
        }
        stuckPosition.position = respawnPosition;
    }
}
