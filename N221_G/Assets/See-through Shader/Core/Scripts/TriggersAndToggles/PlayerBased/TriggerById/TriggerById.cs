using ShaderCrew.SeeThroughShader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShaderCrew.SeeThroughShader
{
    [AddComponentMenu(Strings.COMPONENTMENU_TRIGGER_BY_ID)]
    public class TriggerById : MonoBehaviour
    {
        public string triggerID;
        public bool thisIsEnterTrigger = false;
        public bool thisIsExitTrigger = false;
        //public bool addSeeThroughShaderAtStart = false;
        Shader seeThroughShader;

        // Start is called before the first frame update    

        private TriggerBox triggerBox;
        //public Material refMaterial;
        //public List<Material> listMaterialNoApply;
        private TransitionController seeThroughShaderController;
        List<Material> listM;
        private List<GameObject> listPlayerInside = new List<GameObject>();
        TriggerById[] groupMembersFoundScene;
        private string seeThroughShaderName;

        void Start()
        {
            if (this.isActiveAndEnabled)
            {
                if (this.gameObject.GetComponent<Collider>() != null && this.gameObject.GetComponent<Collider>().isTrigger == false)
                {
                    this.gameObject.GetComponent<Collider>().isTrigger = true;
                }
                seeThroughShaderName = GeneralUtils.getUnityVersionAndRenderPipelineCorrectedShaderString().versionAndRPCorrectedShader;
                listM = buildListMaterial();
                if (listM.Count > 0)
                {

                    seeThroughShaderController = new TransitionController(listM);
                }
                groupMembersFoundScene = GameObject.FindObjectsOfType<TriggerById>();
                //seeThroughShader = Shader.Find(seeThroughShaderName);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnDisable()
        {
            if (seeThroughShaderController != null)
            {
                seeThroughShaderController.destroy();
            }

        }

        private void OnDestroy()
        {
            if (seeThroughShaderController != null)
            {
                seeThroughShaderController.destroy();
            }
        }

        public void SetSeeThroughActive(Collider other)
        {
            //listM = buildListMaterial();
            if (listM != null && listM.Count > 0)
            {
                if (seeThroughShaderController == null)
                {

                    seeThroughShaderController = new TransitionController(listM);
                }
                seeThroughShaderController.notifyOnTriggerEnter(listM, other.transform);
            }

        }

        public void SetSeeThroughInActive(Collider other)
        {
            if (listM != null && listM.Count > 0)
            {
                if (seeThroughShaderController == null)
                {
                    seeThroughShaderController = new TransitionController(listM);  // why not new SeeThroughShaderController(listM);?
                }
                seeThroughShaderController.notifyOnTriggerExit(listM, other.transform);
            }         
        }

        private void OnTriggerEnter(Collider other)
        {
            if (this.isActiveAndEnabled && other.gameObject.GetComponent<SeeThroughShaderPlayer>() != null)
            {
                if (thisIsEnterTrigger)
                {

                    bool isInside = false;
                    List<GameObject> groupMembersFound = new List<GameObject>();
                    foreach (Transform t in this.gameObject.transform.root)
                    {
                        if (t.gameObject != this.gameObject)
                        {
                            if (t.gameObject.GetComponent<TriggerById>() && t.gameObject.GetComponent<TriggerById>().triggerID == this.triggerID)
                            {
                                if (t.gameObject.GetComponent<TriggerById>().listPlayerInside.Count > 0 && t.gameObject.GetComponent<TriggerById>().listPlayerInside.Contains(other.gameObject))
                                {
                                    isInside = true;
                                    groupMembersFound.Add(t.gameObject);
                                }
                            }
                        }
                    }

                    //TriggerById[] groupMembersFoundScene = GameObject.FindObjectsOfType<TriggerById>();
                    if (groupMembersFoundScene.Length > 0)
                    {
                        foreach (TriggerById item in groupMembersFoundScene)
                        {
                            if (item.listPlayerInside.Count > 0)
                            {
                                if (item.listPlayerInside.Contains(other.gameObject))
                                {
                                    isInside = true;
                                    groupMembersFound.Add(item.transform.gameObject);
                                }
                            }
                        }
                    }

                    if (!listPlayerInside.Contains(other.gameObject) && !isInside)
                    {
                        listPlayerInside.Add(other.gameObject);
                        SetSeeThroughActive(other);
                    }

                }
                else if (thisIsExitTrigger)
                {
                    bool isInside = false;
                    List<GameObject> groupMembersFound = new List<GameObject>();
                    foreach (Transform t in this.gameObject.transform.root)
                    {
                        if (t.gameObject != this.gameObject)
                        {
                            if (t.gameObject.GetComponent<TriggerById>() && t.gameObject.GetComponent<TriggerById>().triggerID == this.triggerID)
                            {
                                if (t.gameObject.GetComponent<TriggerById>().listPlayerInside.Count > 0 && t.gameObject.GetComponent<TriggerById>().listPlayerInside.Contains(other.gameObject))
                                {
                                    isInside = true;
                                    groupMembersFound.Add(t.gameObject);
                                }
                            }
                        }
                    }

                    //TriggerById[] groupMembersFoundScene = GameObject.FindObjectsOfType<TriggerById>();
                    if (groupMembersFoundScene.Length > 0)
                    {
                        foreach (TriggerById item in groupMembersFoundScene)
                        {
                            if (item.listPlayerInside.Count > 0)
                            {
                                if (item.listPlayerInside.Contains(other.gameObject))
                                {
                                    isInside = true;
                                    groupMembersFound.Add(item.transform.gameObject);
                                }
                            }
                        }
                    }

                    if (isInside)
                    {
                        if (groupMembersFound.Count > 0)
                        {
                            foreach (GameObject item in groupMembersFound)
                            {
                                if (item.GetComponent<TriggerById>() != null && item.GetComponent<TriggerById>().listPlayerInside.Count > 0)
                                {
                                    if (item.GetComponent<TriggerById>().listPlayerInside.Contains(other.gameObject))
                                    {
                                        item.GetComponent<TriggerById>().listPlayerInside.Remove(other.gameObject);
                                    }
                                }
                            }
                        }
                        SetSeeThroughInActive(other);

                    }

                }
            }

        }

        public List<Material> buildListMaterial()
        {

            if (!string.IsNullOrEmpty(triggerID))
            {
                TriggerObjectId[] idObjects = GameObject.FindObjectsOfType<TriggerObjectId>();
                List<Material> listMaterial = new List<Material>();

                foreach (TriggerObjectId item in idObjects)
                {
                    if (item.gameObject.GetComponent<TriggerObjectId>() != null && item.gameObject.GetComponent<TriggerObjectId>().triggerID == triggerID)
                    {
                        GeneralUtils.AddIfSeeThroughShaderMaterial(item.gameObject, seeThroughShaderName, listMaterial);
                    }
                }

                return listMaterial;
            }
            return null;
        }

    }
    
}