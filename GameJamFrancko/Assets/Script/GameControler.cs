using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour {

    [SerializeField]
    private List<GameObject> _waves;
    [SerializeField]
    private Vector2 _spawnRangeY;
    [SerializeField]
    private float _spawnX;

    private List<GameObject> _sangliers = new List<GameObject>();
    private List<Wave> _waveList = new List<Wave>(); 
    private float _timer;
    private int _currentWave = -1;
    private bool _haveSpawn = true;

    private void Start()
    {
        foreach (var _wave in _waves)
        {
            Debug.Log(1);
            _waveList.Add(_wave.GetComponent<Wave>());
        }
    }

    private void Update()
    {
        if (_timer > 0) _timer -= Time.deltaTime;
        if (_timer <= 0 && !_haveSpawn && _currentWave >= 0) Spawn();
        if (_sangliers.Count == 0 && _haveSpawn && _currentWave < _waveList.Count-1) NextWave();
    }

    private void NextWave()
    {
        _haveSpawn = false;
        _currentWave++;
        _timer = _waveList[_currentWave]._cooldown;
    }

    private void Spawn()
    {
        _haveSpawn = true;
        foreach (var _sanglier in _waveList[_currentWave]._enemys)
        {
            float y = UnityEngine.Random.Range(_spawnRangeY.x, _spawnRangeY.y);
            var instance = Instantiate(_sanglier, new Vector3(_spawnX, y, 0), new Quaternion());
            _sangliers.Add(instance);
            _sangliers.Remove(instance);
        }
    }
}
