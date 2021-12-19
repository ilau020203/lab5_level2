using System;
using System.Collections.Generic;
using System.Text;

namespace lab5_level2
{
    public interface IGetElement<T>
    {
        T GetElement(int i, int j);
    }
}
