﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Ordenamiento_Interno_Felix_Lopez
{
    public partial class Intercalacion_de_Archivos : Form
    {
        TextWriter archivo;

        int file = 1,
        cantidad,
        file1,
        file2,
        inicio = 0;

        int[] datos;
        public Intercalacion_de_Archivos()
        {
            InitializeComponent();
            gbDato.Enabled = false;
            gbDatos.Enabled = false;
            btnOrdenar.Enabled = false;
            gbMostrar.Visible = false;
            btnNuevoRegistro.Enabled = false;
        }
        private void btnCrearArchivo_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"El archivo F{file} se ha creado corretamente", "Felicidades", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            if (file == 1)
            {
                archivo = new StreamWriter($"archivo{file}.txt");
            }
            else
            {
                archivo = new StreamWriter("archivo2.txt");
            }
            gbDato.Enabled = false;
            gbDatos.Enabled = true;
            btnCrearArchivo.Enabled = false;
            btnNuevoRegistro.Enabled = true;
            txtCantidad.Focus();
            txtCantidad.Clear();
        }
        private void btnCantidad_Click(object sender, EventArgs e)
        {
            try
            {
                inicio = 0;
                cantidad = Convert.ToInt32(txtCantidad.Text);

                if (cantidad <= 0 || txtCantidad.Text.Length > 8)
                {
                    MessageBox.Show("La cantidad de elementos ingresada no es valida!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"El archivo F{file} puede contener un total de {cantidad} elementos", "Feliciddes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    datos = new int[cantidad];
                    gbDatos.Enabled = false;
                    gbDato.Enabled = true;
                    txtDato.Focus();

                    if (file == 1)
                        file1 = cantidad;
                    else
                        file2 = cantidad;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show($"Los datos ingresados no son validos, se ha encontrado el siguiente error: \n {error}", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
        private void btnAsignarValor_Click(object sender, EventArgs e)
        {
            int dato;
            if (inicio != cantidad)
            {
                try
                {
                    dato = Convert.ToInt32(txtDato.Text);

                    if (dato <= 0 || txtDato.Text.Length > 8)
                    {
                        MessageBox.Show("El valor ingresado no es valida!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        datos[inicio] = dato;
                        MessageBox.Show($"{dato} se ha agregado al archivo F{file}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDato.Focus();
                        txtCantidad.Text = (Convert.ToInt32(txtCantidad.Text) - 1).ToString();
                        inicio++;
                    }
                    if (inicio == cantidad)
                    {
                        AsignarContenido(datos);
                        MostrarContenido();
                        gbDato.Enabled = false;
                    }

                    if (inicio == cantidad && file == 2)
                    {
                        MessageBox.Show($"Pulse crear archivo para crear el archivo F{file}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    if (inicio == cantidad && file == 3)
                    {
                        btnOrdenar.Enabled = true;
                    }
                    gbMostrar.Visible = true;
                    txtDato.Clear();
                }
                catch (Exception error)
                {
                    MessageBox.Show($"Los datos ingresados no son validos, se ha encontrado el siguiente error: \n {error}", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show($"Ya no puedes agregar mas elementos. \n Acontinucion se mostraran los datos del archivo F{file}", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void btnOrdenar_Click(object sender, EventArgs e)
        {
            StreamReader leerArchivo1 = new StreamReader("archivo1.txt");
            StreamReader leerArchivo2 = new StreamReader("archivo2.txt");
            TextWriter archivo3 = new StreamWriter("archivo3.txt");

            int[] files1 = new int[file1 + 1];
            int[] files2 = new int[file2 + 1];

            int i = 0,
                j = 0,
                k;

            while (!leerArchivo1.EndOfStream)
            {
                files1[i] = int.Parse(leerArchivo1.ReadLine());
                i++;
            }

            while (!leerArchivo2.EndOfStream)
            {
                files2[j] = int.Parse(leerArchivo2.ReadLine());
                j++;
            }

            for (i = k = j = 0; i < (files1.Length - 1) && j < (files2.Length - 1); k++)
            {
                if (files1[i] < files2[j])
                {
                    archivo3.WriteLine(files1[i]);
                    i++;
                }
                else
                {
                    archivo3.WriteLine(files2[j]);
                    j++;
                }
            }

            for (; i < files1.Length - 1; i++)
            {
                archivo3.WriteLine(files1[i]);
            }
            for (; j < files2.Length - 1; j++)
            {
                archivo3.WriteLine(files2[j]);
            }

            leerArchivo1.Close();
            leerArchivo2.Close();
            archivo3.Close();

            StreamReader leerArchivo3 = new StreamReader("archivo3.txt");
            Process.Start("archivo3.txt");
            leerArchivo3.Close();
            btnOrdenar.Enabled = false;
        }

        private void btnNuevoRegistro_Click_1(object sender, EventArgs e)
        {
            file = 1;
            gbDatos.Enabled = false;
            gbMostrar.Visible = false;
            btnCrearArchivo.Enabled = true;
            btnNuevoRegistro.Enabled = false;
            dgvF1.Rows.Clear();
            dgvF2.Rows.Clear();
            btnOrdenar.Enabled = true;
            MessageBox.Show("Se ha creado un nuevo registro!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnVolver_Click_1(object sender, EventArgs e)
        {
            Form1 menu = new Form1();
            menu.Show();
            this.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #region Asignar
        public void AsignarContenido(int[] dates)
        {
            int axu, j;

            for (int i = (dates.Length - 1); i > 0; i--)
            {
                for (j = (i - 1); j >= 0; j--)
                {
                    if (dates[i] < dates[j])
                    {
                        axu = dates[i];
                        dates[i] = dates[j];
                        dates[j] = axu;
                    }
                }
            }

            foreach (var date in dates)
            {
                if (file == 1)
                {
                    dgvF1.Rows.Add(date, null);

                }
                else
                {
                    dgvF2.Rows.Add(date, null);
                }

                archivo.WriteLine(date);
            }

            archivo.Close();
            btnCrearArchivo.Enabled = true;

            if (file == 1)
            {
                file = 2;
            }
            else
            {
                file = 3;
                btnCrearArchivo.Enabled = false;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 menu = new Form1();
            menu.Show();
            this.Close();
        }
        #endregion
        #region Mostrar
        public void MostrarContenido()
        {
            TextReader leerArchivo;

            if (file == 2)
            {
                leerArchivo = new StreamReader($"archivo1.txt");
                MessageBox.Show($"El contenido del archivo F1 es: \n{leerArchivo.ReadToEnd()}", "Content", MessageBoxButtons.OK, MessageBoxIcon.Information);
                leerArchivo.Close();
            }
            else
            {
                leerArchivo = new StreamReader($"archivo2.txt");
                MessageBox.Show($"El contenido del archivo F2 es: \n{leerArchivo.ReadToEnd()}", "Content", MessageBoxButtons.OK, MessageBoxIcon.Information);
                leerArchivo.Close();
            }
        }

        private void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            file = 1;
            gbDatos.Enabled = false;
            gbMostrar.Visible = false;
            btnCrearArchivo.Enabled = true;
            btnNuevoRegistro.Enabled = false;
            dgvF1.Rows.Clear();
            dgvF2.Rows.Clear();
            btnOrdenar.Enabled = true;
            MessageBox.Show("Se ha creado un nuevo registro!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
    }
}
