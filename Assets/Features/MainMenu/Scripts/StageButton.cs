using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StageButton : MonoBehaviour
{
    public GameObject lockImg;
    public GameObject starContainer;
    public TextMeshProUGUI stageNameText;       // 스테이지 이름 표시

    [SerializeField] private int stageId;
    [SerializeField] private string sceneName;

    public void Initialize(int stageId, string stageName, string sceneName, int clearGrade, bool isOpen)
    {
        this.stageId = stageId;
        this.sceneName = sceneName;
        
        // Open or Lock
        if(isOpen)
        {
            lockImg.SetActive(false);
            stageNameText.text = stageName;
            stageNameText.color = Color.green;
            stageNameText.gameObject.SetActive(true);
            GetComponent<Button>().interactable = true;
        }
        else
        {
            lockImg.SetActive(true);
            stageNameText.gameObject.SetActive(false);
            GetComponent<Button>().interactable = false;
        }

        // 클리어 등급 (별) 초기화 (검은색 별)
        for(int i = 0 ; i < 3; i++)
        {
            starContainer.transform.GetChild(i).GetComponent<Image>().color = Color.black;
        }

        // 클리어 등급에 따라 별 활성화
        for(int i = 0 ; i < clearGrade; i++)
        {
            starContainer.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }

        // 클리어 했다면 텍스트 초록색
        if(clearGrade > 0)
        {
            stageNameText.color = Color.green;
        }

        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        Observer.Instance.RequestStageSelect(stageId, sceneName);
        // Debug.Log(GameDataBase.Instance.GetStageInfo(stageId).timeLimit1);
    }
}