using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
public class ActionManager : MonoBehaviour
{
    [Header("プレイヤー関連")]
    public GameObject  playerPlefab;

    [Header("カメラ設定")]
    public Transform CameraTransform;
    public CinemachineVirtualCamera virtualCamera;

    [Header("ステージ関連")]
    public string activeSceneName;
    string activeSceneNameBef;
    public string beforeSceneName; // 前回入っていたシーンの名前
    public Vector3 lastTransitionPosition; // 前回入っていたシーンでの座標
    GameObject playerObject;
    public List<Scene> LoadedScenes=new List<Scene>();

    [Header("メニュー関連")]
    bool isMenu=false;
    public Canvas Menu = new Canvas();

    void Start()
    {
        if (playerPlefab==null)
        {
            playerObject = new GameObject();
            playerObject.AddComponent<ViewingMode>();
            virtualCamera.GetComponent<CinemachineConfiner>().enabled=false;
            
        }else{
            playerObject=Instantiate(playerPlefab);
        }
        virtualCamera.Follow=playerObject.transform;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            setMenu(isMenu^true);
        }
        if(string.Compare(activeSceneNameBef,activeSceneName)!=0){
            activeSceneNameBef=activeSceneName;
            TransitionScene(activeSceneName,Vector3.zero);
        }
    }

    public void ChangePlayerfromPlefab(){
        ChangePlayerfromPlefab(playerPlefab);
    }
    public void ChangePlayerfromPlefab(GameObject plafab){
        playerObject=Instantiate(plafab);
    }

    #region SceneFunctions
    public void TransitionScene(string sceneName, Vector3 playerPosition){
        StartCoroutine(CoroutineTsScene(sceneName,playerPosition));
    }

    IEnumerator CoroutineTsScene(string sceneName, Vector3 playerPos){
        playerObject.SetActive(false);
        lastTransitionPosition=playerObject.transform.position;
        playerObject.transform.position=playerPos;
        foreach (Scene scene in LoadedScenes)
        {
            yield return SceneManager.UnloadSceneAsync(scene);
        }
        LoadedScenes.Clear();
        yield return SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
        beforeSceneName=activeSceneName;
        activeSceneName=activeSceneNameBef=sceneName;
        Scene loadedScene = SceneManager.GetSceneByName(sceneName);
        // Debug.Log(SceneManager.GetActiveScene().name);
        GameObject camColliderObj = GameObject.FindGameObjectWithTag("CameraCollider");
        if (camColliderObj==null)
        {
            virtualCamera.GetComponent<CinemachineConfiner>().enabled=false;
        }else{
            if(camColliderObj.GetComponent<Collider2D>()){
                virtualCamera.GetComponent<CinemachineConfiner>().m_BoundingShape2D = camColliderObj.GetComponent<Collider2D>();
            virtualCamera.GetComponent<CinemachineConfiner>().enabled=true;
            }
        }
        foreach (GameObject camObject in GameObject.FindGameObjectsWithTag("MainCamera"))
        {
            if(string.Compare(camObject.scene.name,sceneName)==0){
                Destroy(camObject);
            }
        }
        LoadedScenes.Add(loadedScene);
        playerObject.SetActive(true);
    }

    public void LoadAdditionalScene(string sceneName){
        StartCoroutine(CoroutineAddScene(sceneName));
    }
    public void UnloadAdditionalScene(string sceneName){
        StartCoroutine(CoroutineRemoveScene(sceneName));
    }
    IEnumerator CoroutineAddScene(string sceneName){
        yield return SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
        LoadedScenes.Add(SceneManager.GetSceneByName(sceneName));
    }
    IEnumerator CoroutineRemoveScene(string sceneName){
        Scene loadedScene = SceneManager.GetSceneByName(sceneName);
        yield return SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
        LoadedScenes.RemoveAll((s)=>loadedScene.Equals(s));
        
    }

    #endregion

    public void setMenu(bool b){
        isMenu=b;
        Menu.gameObject.SetActive(isMenu);
        
        if(isMenu){
            Time.timeScale=0;
        }else{
            Time.timeScale=1;
        }
    }
    public bool getIsMenu(){
        return isMenu;
    }
}
