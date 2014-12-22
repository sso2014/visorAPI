using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace V.Data.Net
{
    public class RemoteController
    {
        public RemoteController(EquipoRepository repository) {
            this.repository = repository;
        }
        private EquipoRepository repository;
        private void Start() {
        }
    }
}
