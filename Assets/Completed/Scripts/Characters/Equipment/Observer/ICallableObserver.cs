public interface ICallableObserver : IObserver<ICallableObserver, ICallableObservable> {
    new void update(ICallableObservable observable);
}
