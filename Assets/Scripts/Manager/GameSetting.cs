using System;

public class GameSetting:Singleton<GameSetting>
{
    public ObservableValue<bool> showHealthBar;
    public event Action<bool> OnShowHealthBarAction;
    public void Initialize()
    {
        showHealthBar = new(true, OnshowHealthBarChange);
    }
    void OnshowHealthBarChange(bool oldValue, bool newValue)
    {
        OnShowHealthBarAction?.Invoke(newValue);
    }
    void OnValidate()
    {
        showHealthBar.CallChangeEvent_OnlyNew();
    }
}
