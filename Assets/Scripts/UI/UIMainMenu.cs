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
        // ���� �޴��� Ȱ��ȭ�Ǵ� ����, �÷��̾� ������ ����ؼ� ������Ʈ�Ѵ� ( gold, exp ���� ������ ���� )
        // gold�� exp���� ��� ��ü�� Player���� ������ ǥ���ϴ� ��ü�� UIMainMenu�̹Ƿ� ������ �̰����� �ǽð����� �̷�������� ����
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
        characterName.text = GameManager.Instance.Player.CharacterName;
        characterLevel.text = GameManager.Instance.Player.CharacterLevel.ToString();
        expText.text = GameManager.Instance.Player.CurrentExp.ToString() + " / " + GameManager.Instance.Player.MaxExp.ToString();
        goldText.text = GameManager.Instance.Player.Gold.ToString();
        characterDescription.text = GameManager.Instance.Player.CharacterDescription;

        // ����ġ �� ����
        expBar.fillAmount = (float)GameManager.Instance.Player.CurrentExp / GameManager.Instance.Player.MaxExp;
    }

    public void HideMenuButton()
    {
        menuButton.SetActive(false);
    }
}
