using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace as2cs_home.Parser
{
    public abstract class AbstractState : IState
    {

        public AbstractState( )
        {
        }
        public abstract void handle(IContext context);
    }
}