using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApp.Services
{
    using System.Diagnostics;

    public class DummyService : IDummyService
    {
        public void Act(string message = null)
        {
            Debug.WriteLine($"Wiadomosc: {message}");
        }
    }
}
