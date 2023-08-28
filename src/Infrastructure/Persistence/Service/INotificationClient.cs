using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Service
{
    public interface INotificationClient
    {
        Task ReceiveMessage(String message);
    }
}
