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

public interface UIControl  //����, �Ǵ� ���� �� ���ᰡ ���� ��� ���
{
    public void UIActive();
}

public interface UIActiveEvent //����, ���� �� �̺�Ʈ�� �ٸ� ���
{
    public void InitUI();
    public void ExitUI();
    
}