using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyMonoBehavious : MonoBehaviour
{

    void Awake()
    {
        this.name = this.name + " (" + SceneManager.GetActiveScene().name + ")" ;

        // Tìm object cùng tên (hoặc cùng tag)
        var objs = GameObject.FindObjectsOfType<DontDestroyMonoBehavious>();
        var count = 0;
        foreach(var obj in objs)
        {
            if(obj.name == this.name) count++;
        }
        if (count > 1)
        {
            Destroy(gameObject); // Đã có một cái tồn tại → xóa cái mới
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
