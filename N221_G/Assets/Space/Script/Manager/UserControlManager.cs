using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 사용자 매니저 
/// </summary>
public class UserControlManager : MonoBehaviour
{

    /// <summary>
    /// 사용자 매니저 정보 
    /// </summary>
    public static UserControlManager instance
    {
        get
        {
            return GameObject.FindObjectOfType<UserControlManager>();
        }
    }



    //사용자 컨트롤러 리스트
    public List<UserControl> userControlList;

    


}

