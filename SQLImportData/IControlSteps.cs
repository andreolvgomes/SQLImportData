using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLImportData
{
    public interface IControlSteps
    {
        void SetController(ContextController controller);
        bool Next();
        bool Valida();
    }
}