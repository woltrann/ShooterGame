using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TargetSpawner : MonoBehaviour
{
    public static TargetSpawner Instance;
    public GameObject[] targetPrefabs;

    private Vector2[] xRanges = new Vector2[]
    {
        new Vector2(-399.36f, -395.43f), // z: -4
        new Vector2(-399.9f,  -395.07f), // z: -2.5
        new Vector2(-400.32f, -394.54f), // z: -1
        new Vector2(-400.8f,  -394.09f)  // z: 0.5
    };
    private float[] zPositions = new float[] { -4f, -2.5f, -1f, 0.5f };

    public float jumpHeight = 5f;
    public float lateralDistance = 3f;
    public float fallDuration = 0.4f;

    private string textComponent;

    private void Awake() => Instance = this;

    public void StartGame()
    {
        InvokeRepeating(nameof(SpawnTarget), 1f, Random.Range(2f, 3f)); // her 2 saniyede bir hedef f�rlar
    }

    public void SpawnTarget()
    {    if (GameTimer.Instance.currentTime> 0)
        {
            int index = Random.Range(0, 4);      // Rastgele �izgi se� (0�3)
            int sign= Random.Range(0, targetPrefabs.Length);        //
    
            float x = Random.Range(xRanges[index].x, xRanges[index].y);     // Se�ilen �izginin X aral���nda rastgele X de�eri
            float y = -1.5f;
            float z = zPositions[index];


            Vector3 spawnPos = new Vector3(x, y, z);
            GameObject target = Instantiate(targetPrefabs[sign], spawnPos, Quaternion.identity);


            //if (sign == 0)
            //{
            //    Text text = target.GetComponentInChildren<Text>();

            //    if (text != null)
            //    {
            //        text.text = TextUpdater.Instance.inputField.text;
            //    }
            //    else
            //    {
            //        Debug.LogWarning("Text bile�eni bulunamad�.");
            //    }
            //}

            Text text = target.GetComponentInChildren<Text>();
            if (text != null)
            {
                string newText = TextUpdater.Instance.GetSavedText(sign);

                if (string.IsNullOrEmpty(newText))
                    newText = "Varsay�lan Yaz�";

                text.text = newText;
            }
            else
            {
                Debug.LogWarning("Text bile�eni bulunamad�.");
            }




            Rotate rotateScript = target.GetComponent<Rotate>();
            if (rotateScript != null)
            {
                rotateScript.prefabIndex = sign;
            }

            Sequence seq = DOTween.Sequence();

            seq.Append(target.transform.DOMoveY(y + jumpHeight, 0.2f).SetEase(Ease.OutQuad));       // Yukar� ��k

            float direction = x >= -397.43f ? -1f : 1f;        // Hangi y�ne gidece�ine karar ver
            Vector3 lateralPos = target.transform.position + new Vector3(direction * lateralDistance, 0f, 0f);

       
            Tween moveX = target.transform.DOMoveX(lateralPos.x, 4f).SetEase(Ease.Linear);       // X ekseni hareketi (yana)

            // Y ekseni sal�n�m� (ayn� anda ba�lar, looping)
            //Tween bounceY = target.transform.DOMoveY(transform.position.y + 0.5f, 0.25f)
            //    .SetLoops(8, LoopType.Yoyo)
            //    .SetEase(Ease.InOutSine);

            // �kisini ayn� anda oynat
            seq.Append(moveX);
            //bounceY.Play();


            seq.Append(target.transform.DOMoveY(y, fallDuration).SetEase(Ease.InQuad));      // A�a�� in

            seq.AppendCallback(() => Destroy(target));       // Destroy et
         }
        
    }
}
