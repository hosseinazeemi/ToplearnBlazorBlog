using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB.Infrastructure.Interfaces
{
    public interface IMessageSender
    {
        Task SendAsync(string target , string title , string message);
    }
}
