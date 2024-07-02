using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum SingleplayerMenuStates
{
    StartPauseMenu,
    OptionsMenu,
    Objectives,
    CombatLog,
    Equiptment,
    Abilities,
    Magic,
    SpellCrafter,
    Stats
}

public class SingleplayerMenuUIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI SensValueText;
    [SerializeField]
    private Slider SensSlider;

    [SerializeField]
    private GameObject MiniMap;

    [SerializeField]
    private GameObject PauseMenu;

    [SerializeField]
    private GameObject GrimoreMenu;

    [SerializeField]
    private GameObject ObjectivesMenu;
    [SerializeField]
    private GameObject CombatLogMenu;
    [SerializeField]
    private GameObject EquiptmentMenu;
    [SerializeField]
    private GameObject AbilitiesMenu;
    [SerializeField]
    private GameObject MagicMenu;
    [SerializeField]
    private GameObject StatsMenu;

    [SerializeField]
    private GameObject ObjectivesMenuIcon;
    [SerializeField]
    private GameObject CombatLogMenuIcon;
    [SerializeField]
    private GameObject EquiptmentMenuIcon;
    [SerializeField]
    private GameObject AbilitiesMenuIcon;
    [SerializeField]
    private GameObject MagicMenuIcon;
    [SerializeField]
    private GameObject StatsMenuIcon;

    SingleplayerMenuStates state;

    private void Awake()
    {
        PlayerManager.OpenPauseMenuEvent += OpenPauseMenu;
        PlayerManager.OpenGrimoreEvent += OpenGrimoreMenu;

        GameManager.MenuTabNavEvent += TabNav;
        GameManager.MenuCloseEvent += CloseAllEnterGame;
    }

    void Start()
    {
        SensSlider.maxValue = 10f;

        SensSlider.minValue = 0.1f;

        SensSlider.value = SETTINGS.SENSITIVITY * 100f;

        CloseAll();
    }

    void Update()
    {
        float sensValue = SETTINGS.SENSITIVITY * 100f;

        SensValueText.text = sensValue.ToString("0.00");

        GameManager.instance.setSens(SensSlider.value / 100);
    }

    void CloseAllEnterGame()
    {
        CloseAll();
        GameManager.instance.ChangeInput(InputMode.Game);

        MiniMap.SetActive(true);
    }

    //close all pages
    void CloseAll()
    {
        PauseMenu.SetActive(false);

        GrimoreTabCloseAll();
        GrimoreIconCloseAll();
        GrimoreMenu.SetActive(false);
    }

    //closes all tabs in grimore but leaves grimore page open
    void GrimoreTabCloseAll()
    {
        ObjectivesMenu.SetActive(false);
        EquiptmentMenu.SetActive(false);
        CombatLogMenu.SetActive(false);
        AbilitiesMenu.SetActive(false);
        MagicMenu.SetActive(false);
        StatsMenu.SetActive(false);
    }

    void GrimoreIconCloseAll()
    {
        ObjectivesMenuIcon.SetActive(false);
        EquiptmentMenuIcon.SetActive(false);
        CombatLogMenuIcon.SetActive(false);
        AbilitiesMenuIcon.SetActive(false);
        MagicMenuIcon.SetActive(false);
        StatsMenuIcon.SetActive(false);
    }

    void OpenPauseMenu(bool open)
    {
        PauseMenu.SetActive(open);
        state = SingleplayerMenuStates.StartPauseMenu;
    }

    void OpenGrimoreMenu()
    {
        MiniMap.SetActive(false);
        GrimoreMenu.SetActive(true);
        OpenObjectivesMenu();
    }

    void OpenObjectivesMenu()
    {
        GrimoreTabCloseAll();
        GrimoreIconCloseAll();
        ObjectivesMenuIcon.SetActive(true);
        ObjectivesMenu.SetActive(true);
        state = SingleplayerMenuStates.Objectives;
    }

    void OpenCombatLogMenu() 
    {
        GrimoreTabCloseAll();
        GrimoreIconCloseAll();
        CombatLogMenuIcon.SetActive(true);
        CombatLogMenu.SetActive(true);
        state = SingleplayerMenuStates.CombatLog;
    }

    void OpenEquiptmentMenu()
    {
        GrimoreTabCloseAll();
        GrimoreIconCloseAll();
        EquiptmentMenuIcon.SetActive(true);
        EquiptmentMenu.SetActive(true);
        state = SingleplayerMenuStates.Equiptment;
    }

    void OpenAbilitiesMenu()
    {
        GrimoreTabCloseAll();
        GrimoreIconCloseAll();
        AbilitiesMenuIcon.SetActive(true);
        AbilitiesMenu.SetActive(true);
        state = SingleplayerMenuStates.Abilities;
    }

    void OpenMagicMenu()
    {
        GrimoreTabCloseAll();
        GrimoreIconCloseAll();
        MagicMenuIcon.SetActive(true);
        MagicMenu.SetActive(true);
        state = SingleplayerMenuStates.Magic;
    }

    void OpenStatsMenu()
    {
        GrimoreTabCloseAll();
        GrimoreIconCloseAll();
        StatsMenuIcon.SetActive(true);
        StatsMenu.SetActive(true);
        state = SingleplayerMenuStates.Stats;
    }

    void TabNav(float value)
    {
        switch (state)
        {
            case SingleplayerMenuStates.StartPauseMenu:
                break;
            case SingleplayerMenuStates.OptionsMenu:
                break;
            case SingleplayerMenuStates.Objectives:
                if (value > 0)
                    OpenCombatLogMenu();
                else
                    OpenStatsMenu();
                break;
            case SingleplayerMenuStates.CombatLog:
                if (value > 0)
                    OpenEquiptmentMenu();
                else
                    OpenObjectivesMenu();
                break;
            case SingleplayerMenuStates.Equiptment:
                if (value > 0)
                    OpenAbilitiesMenu();
                else
                    OpenCombatLogMenu();
                break;
            case SingleplayerMenuStates.Abilities:
                if (value > 0)
                    OpenMagicMenu();
                else
                    OpenEquiptmentMenu();
                break;
            case SingleplayerMenuStates.Magic:
                if (value > 0)
                    OpenStatsMenu();
                else
                    OpenAbilitiesMenu();
                break;
            case SingleplayerMenuStates.SpellCrafter:
                break;
            case SingleplayerMenuStates.Stats:
                if (value > 0)
                    OpenObjectivesMenu();
                else
                    OpenMagicMenu();
                break;
        }
    }
}
