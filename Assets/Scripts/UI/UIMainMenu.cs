using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject characterInfo;
    [SerializeField] private GameObject menuButton;
    [SerializeField] private Image expBar;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI characterLevel;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI characterDescription;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 메인 메뉴가 활성화되는 도중, 플레이어 정보를 계속해서 업데이트한다 ( gold, exp 등의 정보를 갱신 )
        // gold와 exp등을 얻는 주체는 Player지만 정보를 표시하는 주체는 UIMainMenu이므로 갱신은 이곳에서 실시간으로 이루어지도록 설계
        UpdateCharacterInfo();
    }

    public void Show()
    {
        characterInfo.SetActive(true);

        if (!menuButton.activeSelf)
        {
            menuButton.SetActive(true);
        }
    }

    public void UpdateCharacterInfo()
    {
        characterName.text = GameManager.Instance.Player.controller.CharacterName;
        characterLevel.text = GameManager.Instance.Player.controller.CharacterLevel.ToString();
        expText.text = GameManager.Instance.Player.controller.CurrentExp.ToString() + " / " + GameManager.Instance.Player.controller.MaxExp.ToString();
        goldText.text = GameManager.Instance.Player.controller.Gold.ToString();
        characterDescription.text = GameManager.Instance.Player.controller.CharacterDescription;

        // 경험치 바 갱신
        expBar.fillAmount = (float)GameManager.Instance.Player.controller.CurrentExp / GameManager.Instance.Player.controller.MaxExp;
    }

    public void HideMenuButton()
    {
        menuButton.SetActive(false);
    }
}
