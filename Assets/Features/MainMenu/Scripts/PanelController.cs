using System;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public enum PanelType
    {
        UnitUpgrade,
        StageSelect,
        Inventory,
        Settings
    }

    [Serializable]
    private struct PanelMapping
    {
        public PanelType type;
        public GameObject panel;
    }

    [SerializeField] private List<PanelMapping> panelMappings; // 패널 매핑 리스트
    private Dictionary<PanelType, GameObject> panelDictionary = new();

    private GameObject currentPanel; // 현재 활성화된 패널

    private void Start()
    {
        // Dictionary 초기화
        foreach (var mapping in panelMappings)
        {
            panelDictionary[mapping.type] = mapping.panel;
            mapping.panel.SetActive(false); // 모든 패널 비활성화
        }

        if (panelDictionary.Count > 0)
        {
            // 초기 활성 패널 설정
            currentPanel = panelDictionary[PanelType.StageSelect]; // 예제: 첫 번째 패널
            SwitchPanel(currentPanel);
        }
    }

    private void OnEnable()
    {
        Observer.Instance.OnRequestPanelSwitch += ActivatePanel;
    }

    private void OnDisable()
    {
        Observer.Instance.OnRequestPanelSwitch -= ActivatePanel;
    }

    // Observer에서 호출할 전환 메서드 (string 대신 enum 사용)
    public void ActivatePanel(PanelType panelType)
    {
        if (panelDictionary.TryGetValue(panelType, out var panel))
        {
            SwitchPanel(panel);
        }
        else
        {
            Debug.LogWarning($"Panel '{panelType}' not found!");
        }
    }

    // 특정 패널 활성화 메서드
    private void SwitchPanel(GameObject panel)
    {
        if (currentPanel != null) currentPanel.SetActive(false);
        panel.SetActive(true);
        currentPanel = panel;
    }
}