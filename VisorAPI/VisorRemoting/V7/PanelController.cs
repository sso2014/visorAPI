using System;
namespace VisorRemoting.V7 {
	public class PanelController:IPanelController {

		private IPanelModel model;
		private IPanel view;

		public PanelController(IPanelModel model, IPanel view) {

            this.model = model;
            this.view = view;
            this.view.AddListener(this);
		}
        public void UpDate() {

            view.Display = model.GetDisplay();
		}
        public void OnClick(ValleyCommandType command)
        {
            model.SetCommand(command);
        }
        public void AllClose() {
            model.Disconnect();
        }
	}
}
