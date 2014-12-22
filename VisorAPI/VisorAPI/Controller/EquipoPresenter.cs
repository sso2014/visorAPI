using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisorAPI.Core;
using VisorAPI.Core.VO;

namespace VisorAPI.Controller
{
    public class EquiposPresenter:IDisposable {
        private readonly IEquipoView view;
        private readonly IEquipoRepository EquiposRepository;      
        public EquiposPresenter(IEquipoView view, IEquipoRepository equiposRepository){
            this.view = view;
            this.EquiposRepository = equiposRepository;
            var equipos = equiposRepository.SelectAll();
            this.view.EquipoSelected += new Action(OnEquipoSelected);
            this.view.EquipoCreated += new Action(OnEquipoCreated);
            this.view.LoadEquipos(equipos);
        }
        void OnEquipoSelected(){
            if (this.view.SelectedEquipo != null){
                var id = this.view.SelectedEquipo.EquipoID;
                var equipo = this.EquiposRepository.SelectByID((string)id);
                this.view.LoadEquipo(equipo);
            }
        }
        void OnEquipoCreated(){
            if (this.view.CreatedEquipo != null){
                var equipo = this.view.CreatedEquipo;
                this.EquiposRepository.Insert(view.CreatedEquipo);
                EquiposRepository.Save();
                view.CreatedEquipo.Dispose();
                var equipos = EquiposRepository.SelectAll();
                view.LoadEquipos(equipos);
            }
        }
        void DrawerSuperficies(){        
        }
        public void Dispose(){
            //throw new NotImplementedException();
        }
    }
}