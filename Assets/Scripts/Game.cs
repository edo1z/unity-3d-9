using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    private GameObject _cube_obj;
    private GameObject _bomb_obj;

    private float _cube_scale = 0.3f;
    private int _cube_steps_num = 10;
    private Vector3 _tower_center;

    void Awake()
    {
      _cube_obj = (GameObject)Resources.Load("Prefabs/Cube");
      _bomb_obj = (GameObject)Resources.Load("Prefabs/Bomb");
      _tower_center =  new Vector3(0, _cube_scale/2f, 0);
    }


    void Start()
    {
      for (int i = _cube_steps_num; i > 0; i--)
      {
        Vector3 left_top = new Vector3(
          _tower_center.x + _cube_scale * (i-1),
          _tower_center.y + _cube_scale * (_cube_steps_num - i),
          _tower_center.z + _cube_scale * (i-1)
        );
        Debug.Log("left_top: " + left_top);
        int cube_num = i * 2 - 1; 
        Debug.Log("cube num: " + cube_num);
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

    void Update()
    {
        
    }
}
