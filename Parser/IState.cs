using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace as2cs_home.Parser
{
    public interface IState
    {
        void handle(IContext context);
    }
}