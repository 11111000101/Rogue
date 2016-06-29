public interface ICallableObservable : IObservable<ICallableObservable, ICallableObserver>
{
    int getRequiredActionPoints();
    new void addObserver(ICallableObserver observer);
    new void removeObserver(ICallableObserver observer);
}
