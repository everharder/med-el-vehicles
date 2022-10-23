using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Common.Interfaces
{
    public interface ITablePrintable
    {
        public string ToTableHeader();
        public string ToTableRow();
    }
}
