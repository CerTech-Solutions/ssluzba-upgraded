using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.DAO;

public interface IAccess
{
    int Id { get; set; }
    void Copy(IAccess obj);
}
