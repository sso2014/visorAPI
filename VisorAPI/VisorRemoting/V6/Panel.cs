using System;
using VisorRemoting.V4;
using System.Windows.Forms;
using System.Data;

namespace VisorRemoting.V6
{
    public class Panel : Form, IPanel
    {
        #region InitializeComponet
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblActualizado = new System.Windows.Forms.Label();
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.groupBox1.Controls.Add(this.lblActualizado);
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
            this.groupBox1.Size = new System.Drawing.Size(631, 277);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lblActualizado
            // 
            this.lblActualizado.AutoSize = true;
            this.lblActualizado.Location = new System.Drawing.Point(26, 30);
            this.lblActualizado.Name = "lblActualizado";
            this.lblActualizado.Size = new System.Drawing.Size(68, 13);
            this.lblActualizado.TabIndex = 13;
            this.lblActualizado.Text = "Actualizado: ";
            // 
            // lblAlarmaSeguridad
            // 
            this.lblAlarmaSeguridad.AutoSize = true;
            this.lblAlarmaSeguridad.Location = new System.Drawing.Point(385, 124);
            this.lblAlarmaSeguridad.Name = "lblAlarmaSeguridad";
            this.lblAlarmaSeguridad.Size = new System.Drawing.Size(106, 13);
            this.lblAlarmaSeguridad.TabIndex = 12;
            this.lblAlarmaSeguridad.Text = "Alarma de seguridad:";
            // 
            // lblFallaElectrica
            // 
            this.lblFallaElectrica.AutoSize = true;
            this.lblFallaElectrica.Location = new System.Drawing.Point(385, 95);
            this.lblFallaElectrica.Name = "lblFallaElectrica";
            this.lblFallaElectrica.Size = new System.Drawing.Size(75, 13);
            this.lblFallaElectrica.TabIndex = 11;
            this.lblFallaElectrica.Text = "Falla electrica:";
            // 
            // lblSeco
            // 
            this.lblSeco.AutoSize = true;
            this.lblSeco.Location = new System.Drawing.Point(385, 65);
            this.lblSeco.Name = "lblSeco";
            this.lblSeco.Size = new System.Drawing.Size(35, 13);
            this.lblSeco.TabIndex = 10;
            this.lblSeco.Text = "Seco:";
            // 
            // lblPresionNor
            // 
            this.lblPresionNor.AutoSize = true;
            this.lblPresionNor.Location = new System.Drawing.Point(159, 185);
            this.lblPresionNor.Name = "lblPresionNor";
            this.lblPresionNor.Size = new System.Drawing.Size(65, 13);
            this.lblPresionNor.TabIndex = 9;
            this.lblPresionNor.Text = "Presión Nor:";
            // 
            // lblEsperandoPresion
            // 
            this.lblEsperandoPresion.AutoSize = true;
            this.lblEsperandoPresion.Location = new System.Drawing.Point(159, 155);
            this.lblEsperandoPresion.Name = "lblEsperandoPresion";
            this.lblEsperandoPresion.Size = new System.Drawing.Size(99, 13);
            this.lblEsperandoPresion.TabIndex = 8;
            this.lblEsperandoPresion.Text = "Esperando Presión:";
            // 
            // lblCaminando
            // 
            this.lblCaminando.AutoSize = true;
            this.lblCaminando.Location = new System.Drawing.Point(159, 124);
            this.lblCaminando.Name = "lblCaminando";
            this.lblCaminando.Size = new System.Drawing.Size(63, 13);
            this.lblCaminando.TabIndex = 7;
            this.lblCaminando.Text = "Caminando:";
            // 
            // lblHabilitado
            // 
            this.lblHabilitado.AutoSize = true;
            this.lblHabilitado.Location = new System.Drawing.Point(159, 95);
            this.lblHabilitado.Name = "lblHabilitado";
            this.lblHabilitado.Size = new System.Drawing.Size(57, 13);
            this.lblHabilitado.TabIndex = 6;
            this.lblHabilitado.Text = "Habilitado:";
            // 
            // lblSentido
            // 
            this.lblSentido.AutoSize = true;
            this.lblSentido.Location = new System.Drawing.Point(159, 65);
            this.lblSentido.Name = "lblSentido";
            this.lblSentido.Size = new System.Drawing.Size(43, 13);
            this.lblSentido.TabIndex = 5;
            this.lblSentido.Text = "Sentido";
            // 
            // lblAplicacion
            // 
            this.lblAplicacion.AutoSize = true;
            this.lblAplicacion.Location = new System.Drawing.Point(26, 185);
            this.lblAplicacion.Name = "lblAplicacion";
            this.lblAplicacion.Size = new System.Drawing.Size(59, 13);
            this.lblAplicacion.TabIndex = 4;
            this.lblAplicacion.Text = "Aplicación:";
            // 
            // lblPresion
            // 
            this.lblPresion.AutoSize = true;
            this.lblPresion.Location = new System.Drawing.Point(26, 155);
            this.lblPresion.Name = "lblPresion";
            this.lblPresion.Size = new System.Drawing.Size(47, 13);
            this.lblPresion.TabIndex = 3;
            this.lblPresion.Text = "Presíon:";
            // 
            // lblTension
            // 
            this.lblTension.AutoSize = true;
            this.lblTension.Location = new System.Drawing.Point(26, 124);
            this.lblTension.Name = "lblTension";
            this.lblTension.Size = new System.Drawing.Size(48, 13);
            this.lblTension.TabIndex = 2;
            this.lblTension.Text = "Tensión:";
            // 
            // lblAngulo
            // 
            this.lblAngulo.AutoSize = true;
            this.lblAngulo.Location = new System.Drawing.Point(26, 95);
            this.lblAngulo.Name = "lblAngulo";
            this.lblAngulo.Size = new System.Drawing.Size(43, 13);
            this.lblAngulo.TabIndex = 1;
            this.lblAngulo.Text = "Angulo:";
            // 
            // lblEquipo
            // 
            this.lblEquipo.AutoSize = true;
            this.lblEquipo.Location = new System.Drawing.Point(26, 65);
            this.lblEquipo.Name = "lblEquipo";
            this.lblEquipo.Size = new System.Drawing.Size(43, 13);
            this.lblEquipo.TabIndex = 0;
            this.lblEquipo.Text = "Equipo:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 295);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Marcha";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 295);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Parada";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 324);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Foward";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(93, 324);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "Reversa";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 408);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(657, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(12, 353);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(270, 45);
            this.trackBar1.TabIndex = 6;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(255, 295);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 8;
            this.button7.Text = "Parada";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(174, 295);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 7;
            this.button8.Text = "Marcha";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // Panel
            // 
            this.ClientSize = new System.Drawing.Size(657, 430);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Panel";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Panel_FormClosed);
            this.Load += new System.EventHandler(this.Panel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
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
        private Timer timer1;
        private System.ComponentModel.IContainer components;
        private Label lblActualizado;
        private TrackBar trackBar1;
        private Button button7;
        private Button button8;
        private StatusStrip statusStrip1;
        #endregion
        #region Panel

        public Panel(string id)
        {
            InitializeComponent();
            this.PanelD = id;
            timer1.Start();
        }
        public string PanelD;
        //public string Nombre;
        //public int Angulo;
        //public int Tension;
        //public int Presion;
        //public int Aplicacion;
        //public bool Sentido;
        //public bool Habilitado;
        //public bool Caminando;
        //public bool EsperandoPresion;
        //public bool PresionNor;
        //public bool FallaElectrica;
        //public bool AlarmaDeSeguridad;
        //public bool Seco { get; set; }
        private Display display = null;
        private IPanelController pController;

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

        #endregion
        private void button1_Click(object sender, EventArgs e) {
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (display != null)
            {
                this.lblActualizado.Text = "Actualizado: " + display.FechaUpdate.ToString();
                this.lblEquipo.Text ="Equipo: " + display.Id;
                this.lblAngulo.Text ="Angulo: " + display.Angulo;
                this.lblTension.Text ="Tensión: " + display.Tension;
                this.lblPresion.Text ="Presión: " + display.Presion;
                this.lblAplicacion.Text = "Aplicación: " + display.Aplicacion;
                this.lblSentido.Text = "Sentido: " + display.Sentido;
                this.lblHabilitado.Text = "Habilitado: " + display.Habilitado;
                this.lblCaminando.Text = "Caminando: " + display.Caminando;
                this.lblEsperandoPresion.Text = "Esperando Presión: " + display.EsperandoPresion;
                this.lblPresionNor.Text = "Presión Nor: " + display.PresionNor;
                this.lblSeco.Text = "Seco: " + display.Seco;
                this.lblFallaElectrica.Text = "Falla Electrica: " + display.FallaElectrica;
                this.lblAlarmaSeguridad.Text = "Alarma de Seguridad: " + display.AlarmaDeSeguridad;
            }

            this.pController.UpDate();
        }
        Display IPanel.Display
        {
            get
            {
                return this.display;
            }
            set
            {
                this.display = value;
            }
        }
        void IPanel.AddListener(IPanelController Control)
        {
            this.pController = Control;
        }
        private void Panel_Load(object sender, EventArgs e)
        {

        }
        private void Panel_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.pController.AllClose();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.pController.OnClick(ValleyCommandType.Foward);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.pController.OnClick(ValleyCommandType.Reversa);
        }
    }
}