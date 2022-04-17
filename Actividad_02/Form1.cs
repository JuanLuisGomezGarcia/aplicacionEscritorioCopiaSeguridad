using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Actividad_02
{
    public partial class Form1 : Form
    {
        
        public String nombreArchivos;

        public struct Datos { 
        public String seleccionGuardado;
        public String archivosSeleccionados;
        public String unidadDeMemoriaSeleccionada;
        public String fechaCopiaSeleccionadas;
        public String nombreCopiaSeleccionado;
        public String planCopiaSeleccinado;
        }
        String ArchivosSeleccinados1;
        


        double espacioCopia;
        double tamañoUnidadGigabyte;
        double espacioCopiaLimite;
        public Form1()
        {
          InitializeComponent();  
            this.toolStrip1.ImageScalingSize = new Size(30, 30);
            this.toolStripButton1.ImageScaling = ToolStripItemImageScaling.SizeToFit;
            this.toolStripButton2.ImageScaling = ToolStripItemImageScaling.SizeToFit;
            this.toolStripButton3.ImageScaling = ToolStripItemImageScaling.SizeToFit;
            this.toolStripButton4.ImageScaling = ToolStripItemImageScaling.SizeToFit;


            DTP_fechaCopia.MinDate = DateTime.Today;
            recogerUnidadesDisco();
            metodoPlanTiempo();



        }
        public double tamañoDisco()
        {
            DriveInfo[] discoDuros = DriveInfo.GetDrives();

            double tamaño = 0;
            int i = 1;
            foreach (DriveInfo unidad in discoDuros)
            {
                double tamañoUnidad = unidad.AvailableFreeSpace;

                if (ComboBox_DiscoDuros.SelectedIndex.Equals(i)) { tamaño = unidad.AvailableFreeSpace; }
                i++;
            }

            if (ComboBox_DiscoDuros.SelectedIndex.Equals(0)) { tamaño = 0; }

            return tamaño;
        
    }

        public String recogerUnidadesDisco()
        {
            ComboBox_DiscoDuros.Items.Add("Vacio");
            ComboBox_DiscoDuros.SelectedIndex = 0;
            DriveInfo[] discoDuros = DriveInfo.GetDrives();
            int i = 1;
            String nombreDiscoDuro = "";
            foreach (DriveInfo unidad in discoDuros)
            {
                String nombreUnidades = unidad.Name;
                double tamañoUnidad = unidad.AvailableFreeSpace;
                double tamañoUnidadMegabyte = tamañoUnidad / 1000000;
                tamañoUnidadGigabyte = tamañoUnidadMegabyte/1000;
                tamañoUnidadGigabyte = Math.Round(tamañoUnidadGigabyte,2);
                ComboBox_DiscoDuros.Items.Add("Nombre : " + nombreUnidades + "Espacio : " + tamañoUnidadGigabyte + " GB");
                if (ComboBox_DiscoDuros.SelectedIndex.Equals(i)) { nombreDiscoDuro = unidad.Name; }
                i++;
            }
            if(ComboBox_DiscoDuros.SelectedIndex.Equals(0)) { nombreDiscoDuro = ""; }
            return nombreDiscoDuro;
 }
        protected String metodoGuardado()
        {
            String seleccionGuardado = "";
            if (rb_completa.Checked)
            {
                seleccionGuardado = "completa";
            }
             if (rb_diferencial.Checked) { seleccionGuardado = "diferencial"; } 
            if (rb_incremental.Checked) { seleccionGuardado = "incremental"; }
            return seleccionGuardado;
        }
        public void metodoPlanTiempo()
        {
            ComboBox_planPeriodicoCopia.Items.Add("Ninguno");
            ComboBox_planPeriodicoCopia.Items.Add("Diario");
            ComboBox_planPeriodicoCopia.Items.Add("Semanal");
            ComboBox_planPeriodicoCopia.Items.Add("Quincenal");
            ComboBox_planPeriodicoCopia.Items.Add("Mensual");
            ComboBox_planPeriodicoCopia.Items.Add("Trimestral");
            ComboBox_planPeriodicoCopia.Items.Add("Semestral");
            ComboBox_planPeriodicoCopia.Items.Add("Anual");
            ComboBox_planPeriodicoCopia.SelectedIndex = 0;
        }

        public void activarBotonCopia()
        {
           // String listViewVacio = "";
            String filtroDiscoDuo="";
            int a = listView2.Items.Count;
            
                filtroDiscoDuo = ComboBox_DiscoDuros.SelectedItem.ToString();
            String filtroNombre = textBox1_nombreCopia.ToString();
            if(textBox1_nombreCopia.Text==String.Empty) { MessageBox.Show("Ponga un nombre a la copia por favor", "Cuidado", MessageBoxButtons.OK, MessageBoxIcon.Warning); } else { 
            if (a == 0) { MessageBox.Show("Seleccione los archivos que desee copiar por favor", "Cuidado", MessageBoxButtons.OK, MessageBoxIcon.Warning); } else { 


            if (!filtroDiscoDuo.Equals("Vacio"))
            {
                
                    if (rb_completa.Checked || rb_diferencial.Checked || rb_incremental.Checked)
                    { 
                        tamañoUnidadGigabyte = tamañoDisco();

                        if (espacioCopiaLimite < tamañoUnidadGigabyte)
                    {
                            Datos info;
                            info.seleccionGuardado = metodoGuardado();
                            info.archivosSeleccionados = ArchivosSeleccinados1;
                            info.unidadDeMemoriaSeleccionada = ComboBox_DiscoDuros.SelectedItem.ToString();
                            info.fechaCopiaSeleccionadas = DTP_fechaCopia.Text.ToString();
                            info.nombreCopiaSeleccionado = textBox1_nombreCopia.Text;
                            info.planCopiaSeleccinado = ComboBox_planPeriodicoCopia.SelectedItem.ToString();

                            Form2 objeto_form = new Form2(info);
                            
                            objeto_form.ShowDialog();
                            
                        
                    }
                    else { MessageBox.Show("No tienes espacio para almacenar la copia","Cuidado", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                    else { MessageBox.Show("Selecciona un tipo de copia", "Cuidado", MessageBoxButtons.OK, MessageBoxIcon.Warning); }


                }
                else { MessageBox.Show("Selecciona un disco duro donde realizar la copia","Cuidado", MessageBoxButtons.OK,MessageBoxIcon.Warning) ;  }
            }
            }
        }

        public void tamañoCopia(double espacioCopia)
        {
            if (espacioCopia > 999)
            {
                espacioCopia = espacioCopia / 1000;
                espacioCopia = Math.Round(espacioCopia,2);
                label2.Text = "Tamaño de la copia: " + espacioCopia.ToString() + " GB";
            }
            else {
                espacioCopia = Math.Round(espacioCopia, 2);
                label2.Text = "Tamaño de la copia: " + espacioCopia.ToString() + " MB"; }
        }
        public void seleccionArchivoFiltro()
        {    
        Form3 objeto_form = new Form3();
            if (objeto_form.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog2.ShowDialog() == DialogResult.Cancel)
                {
                    MessageBox.Show("No has seleccionado ningun archivo, operacion cancelada");
                }
                else { 

                    openFileDialog2.Filter = "Filtro|*." + objeto_form.abreviatura;
                
                foreach (String fichero in openFileDialog2.FileNames)
                {
                    ListViewItem elem = new ListViewItem();
                    elem = listView2.Items.Add(new FileInfo(fichero).Name);

                    String tamaño = new FileInfo(fichero).Length.ToString();
                    int convertirTamaño = (int)Int64.Parse(tamaño);
                    int tamañoMegabyte = convertirTamaño / 1000000;
                    int tamañoKilobyte = convertirTamaño / 1000;
                    if (convertirTamaño < 999999)
                    {
                        elem.SubItems.Add(tamañoKilobyte.ToString() + " KB");
                    }
                    else
                    {
                        elem.SubItems.Add(tamañoMegabyte.ToString() + " MB");
                    }
                    elem.SubItems.Add(tamañoMegabyte.ToString() + " MB");
                    elem.SubItems.Add(new FileInfo(fichero).Extension);
                    elem.SubItems.Add(fichero);

                    ArchivosSeleccinados1 += new FileInfo(fichero).Name + "\n";
                    espacioCopiaLimite += convertirTamaño;
                    nombreArchivos += new FileInfo(fichero).Name + "\n";
                    espacioCopia += tamañoMegabyte;


                }
                
                tamañoCopia(espacioCopia);
            }
            }
            else
            {
                MessageBox.Show("Operacion cancelada");
            }
        }
        public void rellenarListViewConArchivos()
        {
            
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                MessageBox.Show("No has seleccionado ningun archivo, operacion cancelada");
            }
            else
            {
                foreach (String fichero in openFileDialog1.FileNames)
                {
                    ListViewItem elem = new ListViewItem();
                    elem = listView2.Items.Add(new FileInfo(fichero).Name);
                    
                    String tamaño = new FileInfo(fichero).Length.ToString();

                    int convertirTamaño = (int)Int64.Parse(tamaño);
                    int tamañoMegabyte = convertirTamaño / 1000000;
                    int tamañoKilobyte = convertirTamaño / 1000;
                    if (convertirTamaño < 999999)
                    {
                        elem.SubItems.Add(tamañoKilobyte.ToString() + " KB");
                    }
                    else
                    {
                        elem.SubItems.Add(tamañoMegabyte.ToString() + " MB");
                    }
                    elem.SubItems.Add(tamañoMegabyte.ToString() + " MB");
                    elem.SubItems.Add(new FileInfo(fichero).Extension);
                    elem.SubItems.Add(fichero);

                    ArchivosSeleccinados1 += new FileInfo(fichero).Name + "\n";
                    espacioCopiaLimite += convertirTamaño;
                    nombreArchivos += new FileInfo(fichero).Name + "\n";
                    espacioCopia += tamañoMegabyte;
                }
                
                tamañoCopia(espacioCopia);
            }
        }

        public void rellenarListViewConCarpetas()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel)
            {
                MessageBox.Show("No has seleccionado ningun archivo, operacion cancelada");
            }
            else
            {
                String[] contenido = Directory.GetFiles(folderBrowserDialog1.SelectedPath);
                foreach (String fichero in contenido)
                {
                    ListViewItem elem = new ListViewItem();
                    elem = listView2.Items.Add(new FileInfo(fichero).Name);

                    String tamaño = new FileInfo(fichero).Length.ToString();

                    int convertirTamaño = (int)Int64.Parse(tamaño);
                    int tamañoMegabyte = convertirTamaño / 1000000;
                    int tamañoKilobyte = convertirTamaño / 1000;
                    if (convertirTamaño < 999999) {
                        elem.SubItems.Add(tamañoKilobyte.ToString() + " KB");
                    }
                    else
                    {
                        elem.SubItems.Add(tamañoMegabyte.ToString() + " MB");
                    }
                    elem.SubItems.Add(new FileInfo(fichero).Extension);
                    elem.SubItems.Add(fichero);

                    ArchivosSeleccinados1 += new FileInfo(fichero).Name + "\n";
                    espacioCopiaLimite += convertirTamaño;
                    nombreArchivos += new FileInfo(fichero).Name + "\n";
                    espacioCopia += tamañoMegabyte;
                }
                
                tamañoCopia(espacioCopia);
            }
        }


  
        private void button6_Click(object sender, EventArgs e)
        {
            activarBotonCopia();

           
        }

       /* public void pasarDatos()
        {
            Form2 objeto_form = new Form2();
            if (objeto_form.ShowDialog() == DialogResult.OK)
            {
              //  label2.Text = objeto_form.nombre;
                /*objeto_form.Show
            }
            else { label1.Text = "hayahyahay"; 
            }

        }*/








        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            rellenarListViewConCarpetas();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            rellenarListViewConArchivos();
            
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {   
            seleccionArchivoFiltro();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            espacioCopiaLimite = 0;
            espacioCopia = 0;
            label2.Text = "Tamaño de la copia: " + espacioCopia.ToString();
            
        }

        private void borrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            espacioCopiaLimite = 0;
            espacioCopia = 0;
            label2.Text = "Tamaño de la copia: " + espacioCopia.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
