
public interface IObservable <Tout, Tin>
    where Tin : IObserver<Tin, Tout>
    where Tout : IObservable<Tout, Tin>
{
    void addObserver(Tin observer);
    void removeObserver(Tin observer);
}
