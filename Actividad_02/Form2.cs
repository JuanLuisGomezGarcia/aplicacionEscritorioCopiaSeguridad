using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Actividad_02
{
    public partial class Form2 : Form
    {
        String archivos;

        public Form2(Form1.Datos info)
        {
            InitializeComponent();


            Form1 objeto_form = new Form1();

            label_fecha.Text = info.fechaCopiaSeleccionadas.ToString();
            label_metodo.Text = info.seleccionGuardado;
            label_nombre.Text = info.nombreCopiaSeleccionado;
            label_planCopia.Text = info.planCopiaSeleccinado;
            label_ubicacion.Text = info.unidadDeMemoriaSeleccionada;
            archivos = info.archivosSeleccionados;


      /*    info.seleccionGuardado = metodoGuardado();
            info.archivosSeleccionados = ArchivosSeleccinados1;
            info.unidadDeMemoriaSeleccionada = ComboBox_DiscoDuros.SelectedItem.ToString();
            info.fechaCopiaSeleccionadas = DTP_fechaCopia.ToString();
            info.nombreCopiaSeleccionado = textBox1_nombreCopia.ToString();
            info.planCopiaSeleccinado = ComboBox_planPeriodicoCopia.ToString(); */
        }





        private void button1_Click_1(object sender, EventArgs e)
        {
            
            MessageBox.Show(archivos, "Archivos copiados", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Gracias por usar nuestros servicios", "Copias de seguridad Juan luis Gomez Garrcia", MessageBoxButtons.OK); 
            this.Close();
        }
    
    }
}
