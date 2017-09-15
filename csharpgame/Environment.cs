using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{
    public class Environment
    {

        private static Environment currentEnvironment = null;
        private static List<Environment> environmentList = null;

        public static Environment getEnvironment()
        {
            if(currentEnvironment == null)
            {
                currentEnvironment = new Environment();
                environmentList.Add(currentEnvironment);
                return currentEnvironment;
            }
            return currentEnvironment;
        }

    }
}
