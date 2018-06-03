using System.Collections.Concurrent;

namespace HTTPListenerSimple
{
  public interface IRepository<T>
  {
    ConcurrentBag<T> Repository { get; set; }

    void Add(T item);
    int CountItems();
  }
}