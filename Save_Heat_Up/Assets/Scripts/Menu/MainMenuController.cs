using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public int _index;
    [SerializeField] bool _keyDown;
    [SerializeField] int _maxIndex;
    

    void Update()
    {
        if(Input.GetAxis ("vertical") != 0)
        {
            if(!_keyDown)
            {
                if(_index < _maxIndex)
                {
                    _index++;
                }
                else
                {
                    _index = 0;
                }
            }
            else if(Input.GetAxis ("vertical") < 0)
            {
                if(_index > 0)
                {
                    _index--;
                }
                else
                {
                    _index = _maxIndex;
                }
            }
            _keyDown = true;
        }
        else
        {
            _keyDown = false;
        }
    }

}
