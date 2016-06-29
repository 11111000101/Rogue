
public interface IObserver <Tin, Tout>
    where Tin : IObserver<Tin, Tout>
    where Tout : IObservable<Tout, Tin>
{
    void update(Tout observable);
}
