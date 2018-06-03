using System.Drawing;

namespace HTTPListenerSimple
{
  public interface IOutputWriter
  {
    void Write(string output, Color color);
  }
}