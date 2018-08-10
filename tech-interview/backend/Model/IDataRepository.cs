using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tech_interview.backend.Model
{
    interface IDataRepository
    {
        Root getDataFromLevels(string level);
    }
}
