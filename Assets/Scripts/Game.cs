using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    private GameObject _cube_obj;
    private GameObject _bomb_obj;
    private GameObject _bomb;

    private float _cube_scale = 0.3f;
    [SerializeField] private int _cube_steps_num = 10;
    [SerializeField] private float _bomb_drop_time = 2f;
    [SerializeField] private float _bomb_explosion_time = 4f;
    [SerializeField] private float _bomb_explosion_power = 700f;
    [SerializeField] private float _bomb_explosion_radius = 10f;
    private Vector3 _tower_center;
    private float _total_time = 0;
    private bool _dropped = false;
    private bool _exploded = false;
    private Vector3 _bomb_start_posi;

    void Awake()
    {
        _cube_obj = (GameObject)Resources.Load("Prefabs/Cube");
        _bomb_obj = (GameObject)Resources.Load("Prefabs/Bomb");
        _tower_center = new Vector3(0, _cube_scale / 2f, 0);
        _bomb_start_posi = new Vector3(0, 10, 0);
    }

    void CreatePyramid()
    {
        for (int i = _cube_steps_num; i > 0; i--)
        {
            Vector3 left_top = new Vector3(
              _tower_center.x + _cube_scale * (i - 1),
              _tower_center.y + _cube_scale * (_cube_steps_num - i),
              _tower_center.z + _cube_scale * (i - 1)
            );
            int cube_num = i * 2 - 1;
            for (int row = 0; row < cube_num; row++)
            {
                for (int col = 0; col < cube_num; col++)
                {
                    Vector3 center = left_top;
                    center.z -= _cube_scale * row;
                    center.x -= _cube_scale * col;
                    Instantiate(_cube_obj, center, Quaternion.identity);
                }
            }
        }
    }

    void DropBomb()
    {
        _total_time = 0;
        _dropped = true;
        _bomb = Instantiate(_bomb_obj, _bomb_start_posi, Quaternion.identity);
    }

    void ExplosionBomb()
    {
        _total_time = 0;
        _exploded = true;
        Vector3 posi = _bomb.transform.position;
        Collider[] cols = Physics.OverlapSphere(posi, _bomb_explosion_radius);
        foreach (Collider c in cols)
        {
            Rigidbody r = c.GetComponent<Rigidbody>();
            if (r != null)
            {
                r.AddExplosionForce(_bomb_explosion_power, posi, _bomb_explosion_radius);
            }
        }
    }

    void Start()
    {
        CreatePyramid();
    }

    void Update()
    {
        if (!_dropped)
        {
            _total_time += Time.deltaTime;
            if (_total_time > _bomb_drop_time)
            {
                DropBomb();
            }
        }
        else if (!_exploded)
        {
            _total_time += Time.deltaTime;
            if (_total_time > _bomb_explosion_time)
            {
                ExplosionBomb();
            }
        }
    }
}
