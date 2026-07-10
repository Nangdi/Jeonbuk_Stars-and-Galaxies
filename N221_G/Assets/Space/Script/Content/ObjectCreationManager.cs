using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(ObjectCreationManager))]
public class ObjectCreationManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        ObjectCreationManager objectCreationManager = target as ObjectCreationManager;
        
        
        if (GUILayout.Button("유저 1 영역 생성"))
        {
            objectCreationManager.GUILayoutLoad(0);
        }

        if (GUILayout.Button("유저 1 영역 생성"))
        {
            
            objectCreationManager.CreateUserObject(objectCreationManager.user1Data, 0);
        }

        /*
        if (GUILayout.Button("유저 1 영역 자세보정"))
        {
            objectCreationManager.AngleChange();
        }*/


        if (GUILayout.Button("유저 2 영역 생성"))
        {
            objectCreationManager.GUILayoutLoad(1);
        }

        if (GUILayout.Button("유저 2 영역 생성-1"))
        {
            objectCreationManager.CreateUserObject(objectCreationManager.user2Data,0);
        }
        if (GUILayout.Button("유저 2 영역 생성-2"))
        {
            objectCreationManager.CreateUserObject(objectCreationManager.user2Data, 1);
        }

        if (GUILayout.Button("유저 3 영역 생성"))
        {

        }


        if (GUILayout.Button("유저 4 영역 생성"))
        {

        }

        if (GUILayout.Button("유저 5 영역 생성"))
        {

        }
    }
}
#endif
//사물 생성 매니저
public class ObjectCreationManager : MonoBehaviour
{
    /**
    public User1Data user1Data;

    [System.Serializable]
    public class User1Data {
        public RectAreaDataView rectAreaDataView;

    }*/

    public UserRectAreaDataView user1Data;
    public UserRectAreaDataView user2Data;
    public UserRectAreaDataView user3Data;
    public UserRectAreaDataView user4Data;
    public UserRectAreaDataView user5Data;

    public ObjectDeployment objectDeployment;

    [System.Serializable]
    public class UserRectAreaDataView {

        [Header("생성 오브젝트")]
        public GameObject createObject;

        public List<RectAreaDataView> rectAreaDataViewList;
       
    }

    public int playIndex = 0;


    public void UserRectAreaDataViewResetActive() {
        user1Data.createObject.SetActive(false);
        user2Data.createObject.SetActive(false);
        user3Data.createObject.SetActive(false);
        user4Data.createObject.SetActive(false);
        user5Data.createObject.SetActive(false);
    }

    public void ResetPlayIndex() {
        playIndex = 0;
    }


    //각도 변경
    public void AngleChange() {
        objectDeployment.AngleChange();
    }

    //GUI 레이어 로드
    public void GUILayoutLoad(int userCount) {
        switch (userCount) {
            case 1://사용자 1명일경우
                if (!user1Data.createObject.activeSelf)
                {
                    BaseObjectLoad();
                    user1Data.createObject.SetActive(true);
                }
                else
                {
                    user1Data.createObject.SetActive(false);
                    OnDesData(user1Data);
                }
                break;
            case 2://사용자 2명일경우
                if (!user2Data.createObject.activeSelf)
                {
                    BaseObjectLoad();
                    user2Data.createObject.SetActive(true);
                }
                else
                {
                    user2Data.createObject.SetActive(false);
                    OnDesData(user2Data);
                }
                break;
            case 3://사용자 3명일경우
                if (!user3Data.createObject.activeSelf)
                {
                    BaseObjectLoad();
                    user3Data.createObject.SetActive(true);
                }
                else
                {
                    user3Data.createObject.SetActive(false);
                    OnDesData(user3Data);
                }
                break;
            case 4://사용자 4명일경우
                if (!user4Data.createObject.activeSelf)
                {
                    BaseObjectLoad();
                    user4Data.createObject.SetActive(true);
                }
                else
                {
                    user4Data.createObject.SetActive(false);
                    OnDesData(user4Data);
                }
                break;
            case 5://사용자 5명일경우
                if (!user5Data.createObject.activeSelf)
                {
                    BaseObjectLoad();
                    user5Data.createObject.SetActive(true);
                }
                else
                {
                    user5Data.createObject.SetActive(false);
                    OnDesData(user5Data);
                }
                break;
        }
    }



    //오브젝트 새롭게 생성 컨트롤(렌덤하게 표시 진행)
    public void BaseObjectLoad() {
        objectDeployment.CreateFloorObject();
        objectDeployment.CreateFloor2Object();
        objectDeployment.CreateSkyObject();
    }

    /// <summary>
    /// 오브젝트 생성
    /// </summary>
    /// <param name="userCount">사용자 인원</param>
    /// <param name="index">보여지는 인덱스 번호 정보</param>
    public void CreateUserObject(UserRectAreaDataView userData, int index = 0) {
        List<RectAreaDataView> rectAreaDataViewList = userData.rectAreaDataViewList;
        ArrangementObject[] customArrangementObjectList = rectAreaDataViewList[index].customArrangementObjectList;
        CreateObjectPlay(customArrangementObjectList);
    }

    //배열 랜덤 
    private T[] ShuffleArray<T>(T[] array)
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < array.Length; ++i)
        {
            random1 = Random.Range(0, array.Length);
            random2 = Random.Range(0, array.Length);

            temp = array[random1];
            array[random1] = array[random2];
            array[random2] = temp;
        }

        return array;
    }

    //생성시킬 오브젝트 에니메이션 작동 
    private void CreateObjectPlay(ArrangementObject[] customArrangementObjectList)
    {
        
        StartCreateObjectPlay(customArrangementObjectList);

    }

    private Coroutine onCreateObjectPlay = null;

    

    //생성 코루틴 작동
    private void StartCreateObjectPlay(ArrangementObject[] customArrangementObjectList)
    {
        //StopCreateObjectPlay();
        onCreateObjectPlay = StartCoroutine(OnCreateObjectPlay(customArrangementObjectList));
    }

 
    //오브젝트 생성 루틴 딜레이 적용
    IEnumerator OnCreateObjectPlay(ArrangementObject[] customArrangementObjectList) {

        customArrangementObjectList = ShuffleArray(customArrangementObjectList);
        for (int i = 0; i < customArrangementObjectList.Length; i++)
        {
            float time = UnityEngine.Random.Range(0, 0.3f);
            yield return new WaitForSeconds(time);
            customArrangementObjectList[i].targetAnimator.SetBool("Add", true);
        }
    }

    //에니메이션 초기화 영역
    public void OnDesData(UserRectAreaDataView userData) {
        List<RectAreaDataView> rectAreaDataViewList = userData.rectAreaDataViewList;
        for (int i=0; i< rectAreaDataViewList.Count; i++){
            ArrangementObject[] customArrangementObjectList=rectAreaDataViewList[i].customArrangementObjectList;
            for (int j=0;j< customArrangementObjectList.Length; j++)
            {
                customArrangementObjectList[j].targetAnimator.SetBool("Add", false);

            }
        }
    }

    //코루틴 정지
    private void StopCreateObjectPlay() {
        if (onCreateObjectPlay != null) {
            StopCoroutine(onCreateObjectPlay);
            onCreateObjectPlay = null;
        }
    }

}
