using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UserTableSettingBase;

public class NewBehaviourScript : MonoBehaviour
{
    public UserTotalControl userTotalControl;
    public void OnEnable()
    {
        for (int i=0; i< userTotalControl.userTableSettingList.Count; i++) {
            userTotalControl.userTableSettingList[i].starVideoType = StarVideoType.ConnectionUser;
            userTotalControl.userTableSettingList[i].isDisable = false;
        }
    }
}
