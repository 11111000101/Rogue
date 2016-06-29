namespace Completed
{
    public interface ISkillObservable
    {
        void addObserver(ISkillObserver observer);
        void removeObserver(ISkillObserver observer);
    }
}
