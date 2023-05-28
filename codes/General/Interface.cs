public interface IconClick
{
    public void ClickEvent();
}

public interface WarpObject
{
    public void LoadNextScene();
}

public interface GameReward
{
    public void RewardIntoInventory<T>(T item);
}

public interface UIControl  //시작, 또는 시작 및 종료가 같은 경우 사용
{
    public void UIActive();
}

public interface UIActiveEvent //시작, 종료 시 이벤트가 다른 경우
{
    public void InitUI();
    public void ExitUI();
    
}