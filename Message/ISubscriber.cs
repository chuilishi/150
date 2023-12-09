public interface ISubscriber{}
public interface ISubscriber<in T>:ISubscriber
{
    public void Subscribe();
    public void OnMessage(T message);
}
