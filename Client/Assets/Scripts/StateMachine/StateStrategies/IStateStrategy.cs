using System.Collections.Generic;

public interface IStateStrategy
{
    List<ButtonHelper> GetAvailableButtonsList();
    string InformationText { get; }
    string Scene { get; }
    public virtual void OnEntry()
    {

    }
}
