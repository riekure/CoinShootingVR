using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour {

    public GameObject mainCamera;


    public GameObject marker_1;     // 視点UIの対象Plane

    public int markerCount;         // カウント対象になるマーカーの数

    public GameObject gameClearLogo;    // ゲームクリアのロゴ

    private AudioSource shootingSE;

    // Use this for initialization
    void Start () {
        Debug.Log("Shooting Controller is started");
        shootingSE = GetComponent<AudioSource>();   // 効果音を鳴らす機能

        gameClearLogo.SetActive(false);             // 最初はロゴを非表示
        markerCount = 1;                            // マーカーが１つ
    }
    
    // Update is called once per frame
    void Update () {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);     // Sceneでのみ線が見える

            if (hit.collider.gameObject.tag == "Target")          // 視点UIの対象かをタグで判定
            {
                Debug.Log("hit");
                if (CheckHitGameObject(hit, marker_1) == true)
                {
                    shootingSE.PlayOneShot(shootingSE.clip);     // 消すときに音を鳴らす
                    marker_1.SetActive(false);
                    markerCount--;
                }
            }
        }

        if (markerCount == 0)
        {
            // ゲームクリア時のロゴを表示
            gameClearLogo.SetActive(true);
        }
    }

    public bool CheckHitGameObject(RaycastHit hit, GameObject obj)
    {
        bool result = false;
        if (hit.collider.gameObject == obj)
        {
            result = true;
        }
        return result;
    }
}
