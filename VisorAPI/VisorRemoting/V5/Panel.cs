using System;
using VisorRemoting.V4;
using System.Windows.Forms;

namespace VisorRemoting.V5
{
    public class Panel : Form, IPanel
    {
        #region InitializeComponet
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblAlarmaSeguridad = new System.Windows.Forms.Label();
            this.lblFallaElectrica = new System.Windows.Forms.Label();
            this.lblSeco = new System.Windows.Forms.Label();
            this.lblPresionNor = new System.Windows.Forms.Label();
            this.lblEsperandoPresion = new System.Windows.Forms.Label();
            this.lblCaminando = new System.Windows.Forms.Label();
            this.lblHabilitado = new System.Windows.Forms.Label();
            this.lblSentido = new System.Windows.Forms.Label();
            this.lblAplicacion = new System.Windows.Forms.Label();
            this.lblPresion = new System.Windows.Forms.Label();
            this.lblTension = new System.Windows.Forms.Label();
            this.lblAngulo = new System.Windows.Forms.Label();
            this.lblEquipo = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.groupBox1.Controls.Add(this.lblAlarmaSeguridad);
            this.groupBox1.Controls.Add(this.lblFallaElectrica);
            this.groupBox1.Controls.Add(this.lblSeco);
            this.groupBox1.Controls.Add(this.lblPresionNor);
            this.groupBox1.Controls.Add(this.lblEsperandoPresion);
            this.groupBox1.Controls.Add(this.lblCaminando);
            this.groupBox1.Controls.Add(this.lblHabilitado);
            this.groupBox1.Controls.Add(this.lblSentido);
            this.groupBox1.Controls.Add(this.lblAplicacion);
            this.groupBox1.Controls.Add(this.lblPresion);
            this.groupBox1.Controls.Add(this.lblTension);
            this.groupBox1.Controls.Add(this.lblAngulo);
            this.groupBox1.Controls.Add(this.lblEquipo);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(635, 204);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lblAlarmaSeguridad
            // 
            this.lblAlarmaSeguridad.AutoSize = true;
            this.lblAlarmaSeguridad.Location = new System.Drawing.Point(441, 99);
            this.lblAlarmaSeguridad.Name = "lblAlarmaSeguridad";
            this.lblAlarmaSeguridad.Size = new System.Drawing.Size(106, 13);
            this.lblAlarmaSeguridad.TabIndex = 12;
            this.lblAlarmaSeguridad.Text = "Alarma de seguridad:";
            // 
            // lblFallaElectrica
            // 
            this.lblFallaElectrica.AutoSize = true;
            this.lblFallaElectrica.Location = new System.Drawing.Point(441, 70);
            this.lblFallaElectrica.Name = "lblFallaElectrica";
            this.lblFallaElectrica.Size = new System.Drawing.Size(75, 13);
            this.lblFallaElectrica.TabIndex = 11;
            this.lblFallaElectrica.Text = "Falla electrica:";
            // 
            // lblSeco
            // 
            this.lblSeco.AutoSize = true;
            this.lblSeco.Location = new System.Drawing.Point(441, 40);
            this.lblSeco.Name = "lblSeco";
            this.lblSeco.Size = new System.Drawing.Size(35, 13);
            this.lblSeco.TabIndex = 10;
            this.lblSeco.Text = "Seco:";
            // 
            // lblPresionNor
            // 
            this.lblPresionNor.AutoSize = true;
            this.lblPresionNor.Location = new System.Drawing.Point(222, 160);
            this.lblPresionNor.Name = "lblPresionNor";
            this.lblPresionNor.Size = new System.Drawing.Size(65, 13);
            this.lblPresionNor.TabIndex = 9;
            this.lblPresionNor.Text = "Presión Nor:";
            // 
            // lblEsperandoPresion
            // 
            this.lblEsperandoPresion.AutoSize = true;
            this.lblEsperandoPresion.Location = new System.Drawing.Point(222, 130);
            this.lblEsperandoPresion.Name = "lblEsperandoPresion";
            this.lblEsperandoPresion.Size = new System.Drawing.Size(99, 13);
            this.lblEsperandoPresion.TabIndex = 8;
            this.lblEsperandoPresion.Text = "Esperando Presión:";
            // 
            // lblCaminando
            // 
            this.lblCaminando.AutoSize = true;
            this.lblCaminando.Location = new System.Drawing.Point(222, 99);
            this.lblCaminando.Name = "lblCaminando";
            this.lblCaminando.Size = new System.Drawing.Size(63, 13);
            this.lblCaminando.TabIndex = 7;
            this.lblCaminando.Text = "Caminando:";
            // 
            // lblHabilitado
            // 
            this.lblHabilitado.AutoSize = true;
            this.lblHabilitado.Location = new System.Drawing.Point(222, 70);
            this.lblHabilitado.Name = "lblHabilitado";
            this.lblHabilitado.Size = new System.Drawing.Size(57, 13);
            this.lblHabilitado.TabIndex = 6;
            this.lblHabilitado.Text = "Habilitado:";
            // 
            // lblSentido
            // 
            this.lblSentido.AutoSize = true;
            this.lblSentido.Location = new System.Drawing.Point(222, 40);
            this.lblSentido.Name = "lblSentido";
            this.lblSentido.Size = new System.Drawing.Size(43, 13);
            this.lblSentido.TabIndex = 5;
            this.lblSentido.Text = "Sentido";
            // 
            // lblAplicacion
            // 
            this.lblAplicacion.AutoSize = true;
            this.lblAplicacion.Location = new System.Drawing.Point(21, 160);
            this.lblAplicacion.Name = "lblAplicacion";
            this.lblAplicacion.Size = new System.Drawing.Size(59, 13);
            this.lblAplicacion.TabIndex = 4;
            this.lblAplicacion.Text = "Aplicación:";
            // 
            // lblPresion
            // 
            this.lblPresion.AutoSize = true;
            this.lblPresion.Location = new System.Drawing.Point(21, 130);
            this.lblPresion.Name = "lblPresion";
            this.lblPresion.Size = new System.Drawing.Size(47, 13);
            this.lblPresion.TabIndex = 3;
            this.lblPresion.Text = "Presíon:";
            // 
            // lblTension
            // 
            this.lblTension.AutoSize = true;
            this.lblTension.Location = new System.Drawing.Point(21, 99);
            this.lblTension.Name = "lblTension";
            this.lblTension.Size = new System.Drawing.Size(48, 13);
            this.lblTension.TabIndex = 2;
            this.lblTension.Text = "Tensión:";
            // 
            // lblAngulo
            // 
            this.lblAngulo.AutoSize = true;
            this.lblAngulo.Location = new System.Drawing.Point(21, 70);
            this.lblAngulo.Name = "lblAngulo";
            this.lblAngulo.Size = new System.Drawing.Size(43, 13);
            this.lblAngulo.TabIndex = 1;
            this.lblAngulo.Text = "Angulo:";
            // 
            // lblEquipo
            // 
            this.lblEquipo.AutoSize = true;
            this.lblEquipo.Location = new System.Drawing.Point(21, 40);
            this.lblEquipo.Name = "lblEquipo";
            this.lblEquipo.Size = new System.Drawing.Size(43, 13);
            this.lblEquipo.TabIndex = 0;
            this.lblEquipo.Text = "Equipo:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 239);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Marcha";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 239);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Parada";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 268);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Foward";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(93, 268);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "Reversa";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 332);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(668, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Panel
            // 
            this.ClientSize = new System.Drawing.Size(668, 354);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Panel";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private GroupBox groupBox1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;

       
        private Label lblPresion;
        private Label lblTension;
        private Label lblAngulo;
        private Label lblEquipo;
        private Label lblEsperandoPresion;
        private Label lblCaminando;
        private Label lblHabilitado;
        private Label lblSentido;
        private Label lblAplicacion;
        private Label lblAlarmaSeguridad;
        private Label lblFallaElectrica;
        private Label lblSeco;
        private Label lblPresionNor;
        private StatusStrip statusStrip1;
        #endregion

        #region Panel

        public Panel(string id)
        {
            InitializeComponent();
            this.PanelD = id;
        }
        IRemotingController controller;
        public string PanelD;
        public string Nombre;
        public int Angulo;
        public int Tension;
        public int Presion;
        public int Aplicacion;
        public bool Sentido;
        public bool Habilitado;
        public bool Caminando;
        public bool EsperandoPresion;
        public bool PresionNor;
        public bool FallaElectrica;
        public bool AlarmaDeSeguridad;
        public bool Seco { get; set; }
        private string data = string.Empty;        
        public string Data
        {
            set
            {
                this.data = value;
            }
        }

  
        void IPanel.Marcha()
        {

        }
        void IPanel.Parada()
        {

        }
        void IPanel.Foward()
        {

        }
        void IPanel.Reversa()
        {

        }

        public void AddListener(IRemotingController controller)
        {
            this.controller = controller;
        }
        private void ProcessData()
        {
            long est;
            bool aux;

            try
            {

                if (data[0] == '(' && data[32] == Convert.ToChar(13))
                {
                    //.....................................................................................
                    this.PanelD = data.Substring(4, 3);
                    this.Angulo = Convert.ToInt32(data.Substring(12, 3));
                    this.Tension = Convert.ToInt32(data.Substring(15, 3));
                    this.Presion = Convert.ToInt32(data.Substring(18, 3));
                    this.Aplicacion = Convert.ToInt32(data.Substring(21, 3));
                    est = long.Parse(data.Substring(23, 6), System.Globalization.NumberStyles.HexNumber);
                    this.Sentido = Convert.ToBoolean(est & 0x80000);
                    this.Habilitado = Convert.ToBoolean(est & 0x40000);
                    aux = Convert.ToBoolean(est & 0x20000);
                    this.Caminando = Convert.ToBoolean(est & 0x20000);
                    this.EsperandoPresion = Convert.ToBoolean(est & 0x10000);
                    this.PresionNor = Convert.ToBoolean(est & 0x200);
                    this.Seco = Convert.ToBoolean(est & 0x1000);
                    this.FallaElectrica = Convert.ToBoolean(est & 0x80);
                    this.AlarmaDeSeguridad = Convert.ToBoolean(est & 0x40);

                    lblEquipo.Text = "Equipo: " + this.PanelD;
                    lblAngulo.Text = "Angulo: " + this.Angulo;
                    lblTension.Text = "Tensión: " + this.Tension;
                    lblPresion.Text = "Presión: " + this.Presion;
                    lblAplicacion.Text = "Aplicación: " + this.Aplicacion;
                    lblSentido.Text = "Sentido: " + this.Sentido;
                    lblHabilitado.Text = "Habilitado: " + this.Habilitado;
                    lblCaminando.Text = "Caminando: " + this.Caminando;
                    lblEsperandoPresion.Text = "Esperando Presión: " + this.EsperandoPresion;
                    lblPresionNor.Text = "Presión Nor: " + this.PresionNor;
                    lblSeco.Text = "Seco: " + this.Seco;
                    lblFallaElectrica.Text = "Falla electrica: " + this.FallaElectrica;
                    lblAlarmaSeguridad.Text = "Alarma de seguridad: " + this.AlarmaDeSeguridad;
                }
            }
            catch (ArgumentOutOfRangeException ae) { 
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
                public void UpdateDisplay(string data)
        {
            throw new NotImplementedException();
        }
    }
}