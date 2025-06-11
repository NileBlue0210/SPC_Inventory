using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStatus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI defenseText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI criticalText;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);    // 시작 시 비활성화
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStatus();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void BackToMainMenu()
    {
        Hide();
        UIManager.Instance.ShowMainMenu();
    }

    public void UpdateStatus()
    {
        attackText.text = GameManager.Instance.Player.controller.Attack.ToString();
        defenseText.text = GameManager.Instance.Player.controller.Defense.ToString();
        healthText.text = GameManager.Instance.Player.controller.Health.ToString();
        criticalText.text = GameManager.Instance.Player.controller.Critical.ToString() + "%";
    }
}
