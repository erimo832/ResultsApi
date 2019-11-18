using ResultManager.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResultManager.Rules
{
    public interface IHcpRule
    {
        double Calculate(Player player);
    }
}
