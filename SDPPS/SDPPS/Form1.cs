using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDPPS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            progressBarCKP.Maximum = 100;
            progressBarCKP.Minimum = 0;

            progressBarCMP1.Maximum = 100;
            progressBarCMP1.Minimum = 0;

            progressBarCMP2.Maximum = 100;
            progressBarCMP2.Minimum = 0;

        }
        MLApp.MLApp matlab;

        double tamano = 0;

        private void Select_image_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\Users\PJ2Q\OneDrive - Instituto Politecnico Nacional\mBeta\sañeles\Todas\",
                Title = "Seleccionar imagen",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "Images",
                Filter = "Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF",
                FilterIndex = 1,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = openFileDialog1.FileName;

                Cursor.Current = Cursors.WaitCursor;


                matlab = new MLApp.MLApp();
                string toSend = "orig=imread('" + openFileDialog1.FileName + "'); imshow(orig);";
                //"resamp = resample(car," + p + ',' + q + ",0); plot(resamp); ";
                matlab.Execute(toSend);

                Cursor.Current = Cursors.Default;

            }
        }

        private void rgb2gray_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string toSend = "rgb2gray(orig); imshow(ans);";
            matlab.Execute(toSend);

            Cursor.Current = Cursors.Default;
        }

        private void imcomplement_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            matlab = new MLApp.MLApp();

            string toSend = "imcomplement(ans); imshow(ans);";
            matlab.Execute(toSend);

            Cursor.Current = Cursors.Default;
        }

        private void bin_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string toSend = "ans = imbinarize(ans, 'adaptive'); imshow(ans);";
            matlab.Execute(toSend);

            Cursor.Current = Cursors.Default;
        }

        private void BW2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string toSend = "ans = bwareafilt(ans, " + textBox4.Text + "); imshow(ans);";
            matlab.Execute(toSend);

            Cursor.Current = Cursors.Default;
        }

        private void dilatar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string toSend = "ans = imdilate(ans, strel('disk', " + textBox5.Text + ")); imshow(ans);";
            matlab.Execute(toSend);

            Cursor.Current = Cursors.Default;
        }

        private void ckp_Click(object sender, EventArgs e)
        {
            if (x_inicial.Text == "")
            {
                MessageBox.Show("poner x inicial");
            }
            else
            {
                if (x_final.Text == "")
                {
                    MessageBox.Show("poner x final");
                }
                else
                {
                    if (ckp_texbox.Text == "")
                    {
                        MessageBox.Show("poner y en ckp");
                    }
                    else
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        tamano = Convert.ToDouble(x_final.Text) - Convert.ToDouble(x_inicial.Text);  //calcular tamaño

                        System.Array pr = new double[Convert.ToInt32(tamano)];// variables para recibir el arreglo
                        System.Array pi = new double[Convert.ToInt32(tamano)];


                        //pr.SetValue(0, 0);
                        //pr.SetValue(1, 1);
                        //pr.SetValue(0, 2);
                        //pr.SetValue(1, 3);
                        //pr.SetValue(0, 4);
                        //pr.SetValue(1, 5);
                        //pr.SetValue(0, 6);
                        //pr.SetValue(1, 7);


                        


                        string toSend = "CKP = ans(" + ckp_texbox.Text + ":" + ckp_texbox.Text + "," + x_inicial.Text + ":" + x_final.Text + ");";
                        matlab.Execute(toSend);


                        matlab.Execute("plot(CKP)");
                        progressBarCKP.Value = 0;
                        progressBarCKP.Value = 100;
                        // matlab.Execute("openvar('CKP')");


                        //System.Array pr1 = new double[Convert.ToInt32(tamano + 1)];
                        //System.Array pi1 = new double[Convert.ToInt32(tamano + 1)];

                        ////matlab.Execute("carro(:, 1) = CKP; carro=uint8(carro); plot(carro);");
                        //matlab.PutFullMatrix("test", "base", pr1, pi1);

                        //matlab.Execute("test = int8(CKP)");
                        ////matlab.Execute("CKP = uint8(CKP); plot(CKP);");
                        ////matlab.Execute("openvar('CKP')");

                        //matlab.GetFullMatrix("test", "base", ref pr1, ref pi1);

                        Cursor.Current = Cursors.Default;
                    }
                }
            }
            
        }

        private void ObtenerCKP_Click(object sender, EventArgs e)
        {

            //int p, q = 0;

            //int size = 8;

            //p = Convert.ToInt32(textBox1.Text);
            //q = Convert.ToInt32(textBox2.Text);


            //System.Array senal = new double[size];
            //senal.SetValue(0, 0);
            //senal.SetValue(1, 1);
            //senal.SetValue(0, 2);
            //senal.SetValue(1, 3);
            //senal.SetValue(0, 4);
            //senal.SetValue(1, 5);
            //senal.SetValue(0, 6);
            //senal.SetValue(1, 7);

            //System.Array CMP1 = new double[size];
            //for (int i = 0; i < size; i++)
            //{
            //    CMP1.SetValue(0, i);
            //}

            //matlab.PutFullMatrix("car", "base", senal, CMP1);


            //System.Array prresult = new double[Convert.ToInt32(tamano)];
            //System.Array piresult = new double[Convert.ToInt32(tamano)];//

            if (progressBarCMP2.Value == 0)//// si es de 2 señales
            {

                matlab.Execute("CMP2 = zeros(1," + Convert.ToString(tamano+1) + ", 'uint32');");

            }
            //string toSend = "plot(CKP);";

            matlab.Execute("carro(:, 1) = CKP; carro(:, 2) = CMP1; carro(:, 3) = CMP2;");

            matlab.Execute("carro=uint8(carro);plot(carro)");

            matlab.Execute("openvar('carro')");
            //matlab.GetFullMatrix("CKP", "base", ref prresult, ref piresult);
        }

        private void cmp1_Click(object sender, EventArgs e)
        {
            if (x_inicial.Text == "")
            {
                MessageBox.Show("poner x inicial");
            }
            else
            {
                if (x_final.Text == "")
                {
                    MessageBox.Show("poner x final");
                }
                else
                {
                    if (cmp1_textbox.Text == "")
                    {
                        MessageBox.Show("poner y en cmp1");
                    }
                    else
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        tamano = Convert.ToDouble(x_final.Text) - Convert.ToDouble(x_inicial.Text);  //calcular tamaño

                        System.Array pr = new double[Convert.ToInt32(tamano)];// variables para recibir el arreglo
                        System.Array pi = new double[Convert.ToInt32(tamano)];

                        string toSend = "CMP1 = ans(" + cmp1_textbox.Text + ":" + cmp1_textbox.Text + "," + x_inicial.Text + ":" + x_final.Text + ");";
                        matlab.Execute(toSend);

                        matlab.Execute("plot(CMP1)");

                        progressBarCMP2.Value = 0;
                        progressBarCMP1.Value = 100;
                        Cursor.Current = Cursors.Default;
                    }
                }
            }

        }

        private void cmp2_Click(object sender, EventArgs e)
        {
            if (x_inicial.Text == "")
            {
                MessageBox.Show("poner x inicial");
            }
            else
            {
                if (x_final.Text == "")
                {
                    MessageBox.Show("poner x final");
                }
                else
                {
                    if (cmp2_textbox.Text == "")
                    {
                        MessageBox.Show("poner y en cmp1");
                    }
                    else
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        tamano = Convert.ToDouble(x_final.Text) - Convert.ToDouble(x_inicial.Text);  //calcular tamaño

                        System.Array pr = new double[Convert.ToInt32(tamano)];// variables para recibir el arreglo
                        System.Array pi = new double[Convert.ToInt32(tamano)];

                        string toSend = "CMP2 = ans(" + cmp2_textbox.Text + ":" + cmp2_textbox.Text + "," + x_inicial.Text + ":" + x_final.Text + ");";
                        matlab.Execute(toSend);

                        matlab.Execute("plot(CMP2)");

                        progressBarCMP2.Value = 0;
                        progressBarCMP2.Value = 100;
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Matlab_Click(object sender, EventArgs e)
        {

        }
    }
}
