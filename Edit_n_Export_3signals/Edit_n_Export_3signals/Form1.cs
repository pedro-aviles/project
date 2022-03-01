using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;

using Microsoft.Office.Interop.Excel;

namespace Edit_n_Export_3signals
{
    public partial class Form1 : Form
    {
        bool refresh = false;
        int size_samples = 0;

        int[] CKP;
        int[] CMP1;
        int[] CMP2;
        byte[] toSendBytes = { };

        public Form1()
        {
            InitializeComponent();

            //mouse wheel funtion
            this.chart_EditSignal.MouseWheel += chart_MouseWheel;
            //inicializar track bar 2
            tim_div_TrackBar.Minimum = 1;
            tim_div_TrackBar.Maximum = 10;
            tim_div_TrackBar.TickStyle = TickStyle.BottomRight;
            tim_div_TrackBar.TickFrequency = 1;
            //limpiar label


            /////-----------------Inicializar data grid-----------------------/////
            DataGrid_EditSignal.ColumnCount = 100;
            for (int i = 0; i < DataGrid_EditSignal.ColumnCount; i++) DataGrid_EditSignal.Columns[i].HeaderText = Convert.ToString(i);

            DataGrid_EditSignal.RowCount = 3;
            DataGrid_EditSignal.Rows[0].HeaderCell.Value = "CKP";
            DataGrid_EditSignal.Rows[1].HeaderCell.Value = "CMP1";
            DataGrid_EditSignal.Rows[1].HeaderCell.Value = "CMP2";

            DataGrid_EditSignal.AutoResizeRowHeadersWidth(0, DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            for (int i = 0; i < 100; i++) DataGrid_EditSignal.Rows[0].Cells[i].Value = 0;
            for (int i = 0; i < 100; i++) DataGrid_EditSignal.Rows[1].Cells[i].Value = 0;
            for (int i = 0; i < 100; i++) DataGrid_EditSignal.Rows[2].Cells[i].Value = 0;

            for (int i = 0; i < DataGrid_EditSignal.ColumnCount; i++)
            {
                DataGrid_EditSignal.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }


            //datagrid has calculated it's widths so we can store them
            for (int i = 0; i <= DataGrid_EditSignal.Columns.Count - 1; i++)
            {
                //store autosized widths
                int colw = DataGrid_EditSignal.Columns[i].Width;
                //remove autosizing
                DataGrid_EditSignal.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //set width to calculated by autosize
                DataGrid_EditSignal.Columns[i].Width = colw;
            }


        }
        ///-----------------------Botones---------------------------////
        private void load_data_BT_Click(object sender, EventArgs e)
        {
            int pppp = data_EditSignal_TB.Text.IndexOf(',');
            string[] k = data_EditSignal_TB.Text.Split(',');

            if (k.Length==1)//bytes_qty_TB.Text == "")
            {
                
                MessageBox.Show("copia con la catindad de bytes, baboso!!");
            }
            else
            {
                //size_samples = Convert.ToInt16(bytes_qty_TB.Text);
                size_samples = Convert.ToInt32(k[0]);
                //data_EditSignal_TB.Text = k[1];
                byte[] valores = new byte[size_samples*3];

                string[] words = k[1].Split('\t');

                for (int i = 0; i < size_samples*3; i++)
                {
                    valores[i] = Convert.ToByte(words[i+1]); ///mas uno porque copia un /t despues de la coma de la cantidad de bytes
                }

                
                ///////////////*************Converitr***********////////////////
                ///
                int size_per_signal = size_samples;

                int[] CKP_int = new int[size_per_signal];
                int[] CMP1_int = new int[size_per_signal];
                int[] CMP2_int = new int[size_per_signal];
                int v = 0;

                CKP = new int[size_per_signal * 8];
                CMP1 = new int[size_per_signal * 8];
                CMP2 = new int[size_per_signal * 8];


                for (int i = 0; i < size_per_signal; i++)
                {
                    CKP_int[i] = valores[i];
                    CMP1_int[i] = valores[i + size_per_signal];
                    CMP2_int[i] = valores[i + (size_per_signal * 2)];
                }


                for (int i = 0; i < CKP_int.Length; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        CKP[v] = (CKP_int[i] >> j) & 1;
                        CMP1[v] = (CMP1_int[i] >> j) & 1;
                        CMP2[v] = (CMP2_int[i] >> j) & 1;
                        v++;
                    }
                }

                imprimir_Chart(1);


            }
        }
        private void Graph_Click(object sender, EventArgs e)
        {
            imprimir_Chart(1); ////////////si uno??????????????????????????????????????

            //calcular e imprimir cantidad de Bytes
            // double temporal = CKP.Length;
            bits_out_TB.Text = Convert.ToString(CKP.Length);

            double pedro = CKP.Length;
            bytes_per_signal.Text = Convert.ToString(pedro / 8);
        }
        private void print_bytes_Click(object sender, EventArgs e)
        {
            printBitsToDG();
        }
        private void Refresh_but_Click(object sender, EventArgs e)
        {
            refresh_funtion();
        }
        private void delete_refresh_but_Click(object sender, EventArgs e)
        {
            int P = Convert.ToInt32(chart_EditSignal.ChartAreas[0].AxisX.ScaleView.Position);            
            P--;
            int columnCount = DataGrid_EditSignal.SelectedCells.Count;

            int[] CellToDeleteArray = new int[columnCount];
            for (int i = 0; i < columnCount; i++)
            {
                CellToDeleteArray[i] = DataGrid_EditSignal.SelectedCells[i].ColumnIndex;
            }

            Array.Sort(CellToDeleteArray);
            Array.Reverse(CellToDeleteArray);

            int variable = 0;
            for (int i = 0; i < columnCount; i++)
            {
                List<int> tmpCKP = new List<int>(CKP);
                tmpCKP.RemoveAt(CellToDeleteArray[i] + P);
                CKP = tmpCKP.ToArray();

                List<int> tmpCMP1 = new List<int>(CMP1);
                tmpCMP1.RemoveAt(CellToDeleteArray[i] + P);
                CMP1 = tmpCMP1.ToArray();

                List<int> tmpCMP2 = new List<int>(CMP2);
                tmpCMP2.RemoveAt(CellToDeleteArray[i] + P);
                CMP2 = tmpCMP2.ToArray();
                variable++;

            }
            size_samples = CKP.Length;
            if ((chart_EditSignal.ChartAreas[0].AxisX.ScaleView.Position+100)==(CKP.Length+variable)) //esta al final del chart  tiene que cambial la posicion desde donde va a imprimir 
            {
                imprimir_Chart(chart_EditSignal.ChartAreas[0].AxisX.ScaleView.Position - variable);
            }
            else
            {
                imprimir_Chart(chart_EditSignal.ChartAreas[0].AxisX.ScaleView.Position);
            }
            
            refresh = true;
            printBitsToDG();
            mouse_leave_funtion();
        }
        private void make_bytes_bt_Click(object sender, EventArgs e)
        {
            make_bytes();
        }
        private void Get_ClipBo_Click(object sender, EventArgs e)
        {
            make_file();
        }
        private void Save_But_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the text 
            // assigned to Save_But.  
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "text|*.txt|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.  
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the  
                // File type selected in the dialog box.  
                // NOTE that the FilterIndex property is one-based.  
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        //using (System.IO.FileStream fs = System.IO.File.Create(pathString))
                        //{
                        for (long i = 0; i < toSendBytes.Length; i++)
                        {
                            fs.WriteByte(toSendBytes[i]);
                        }
                        //}
                        break;

                    case 2:
                        this.Save_But.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        this.Save_But.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }
        }
        private void conv_arrayToByte_bt_Click(object sender, EventArgs e)
        {
            // convertir arreglos de bits con las muestras a arreglo de bytes 
            int sample_size = 0;
            byte[] ckp_local;
            byte[] cmp1_local;
            byte[] cmp2_local;
            if (tb_size_sample_arrayToBytes.Text == "")
            {
                MessageBox.Show("pon la catindad de bytes, baboso!!");
            }
            else
            {
                sample_size = Convert.ToInt16(tb_size_sample_arrayToBytes.Text);
                ckp_local = new byte[sample_size];
                cmp1_local = new byte[sample_size];
                cmp2_local = new byte[sample_size];

                tb_Data_arrayToByte.Text = tb_Data_arrayToByte.Text.Replace("\n", "");
                string[] solo_columnas = tb_Data_arrayToByte.Text.Split('\r');

                for (int i = 0; i < sample_size; i++)
                {
                    string[] words = solo_columnas[i].Split('\t');
                    ckp_local[i] = Convert.ToByte(words[0]);
                    cmp1_local[i] = Convert.ToByte(words[1]);
                    cmp2_local[i] = Convert.ToByte(words[2]);
                }

                //convertir a bytes

                int size_fixed = 0;
                int contador = 0;
                if ((sample_size % 8) != 0)
                {
                    size_fixed = (sample_size / 8) + 1;
                }
                else
                {
                    size_fixed = sample_size / 8;
                }
                int[] CKP_to_TB = new int[size_fixed];
                int[] CMP1_to_TB = new int[size_fixed];
                int[] CMP2_to_TB = new int[size_fixed];

                //calcular CKP
                for (int i = 0; i < CKP_to_TB.Length; i++)
                {
                    for (int j = 0; (j < 8) && (contador < ckp_local.Length); j++)
                    {
                        CKP_to_TB[i] >>= 1;
                        if (ckp_local[contador] == 0)
                        {
                            CKP_to_TB[i] &= 127;
                        }
                        if (ckp_local[contador] == 1)
                        {
                            CKP_to_TB[i] |= 128;
                        }

                        contador++;
                    }
                }
                //calcular cmp1
                contador = 0;
                for (int i = 0; i < CMP1_to_TB.Length; i++)
                {
                    for (int j = 0; (j < 8) && (contador < ckp_local.Length); j++)
                    {
                        CMP1_to_TB[i] >>= 1;
                        if (cmp1_local[contador] == 0)
                        {
                            CMP1_to_TB[i] &= 127;
                        }
                        if (cmp1_local[contador] == 1)
                        {
                            CMP1_to_TB[i] |= 128;
                        }

                        contador++;
                    }
                }
                //calcular cmp2
                contador = 0;
                for (int i = 0; i < CMP2_to_TB.Length; i++)
                {
                    for (int j = 0; (j < 8) && (contador < ckp_local.Length); j++)
                    {
                        CMP2_to_TB[i] >>= 1;
                        if (cmp2_local[contador] == 0)
                        {
                            CMP2_to_TB[i] &= 127;
                        }
                        if (cmp2_local[contador] == 1)
                        {
                            CMP2_to_TB[i] |= 128;
                        }

                        contador++;
                    }
                }
                /// recorrer bits aumentados en caso de que cantidad de bits no sean multiplo de 8
                /// 
                //calcular bits agregados

                int sumar = (size_fixed*8) - sample_size;
                
                CKP_to_TB[CKP_to_TB.Length-1] >>= sumar;
                CMP1_to_TB[CKP_to_TB.Length-1] >>= sumar;
                CMP2_to_TB[CKP_to_TB.Length-1] >>= sumar;
                //imprmir
                tb_Data_arrayToByte.Text = Convert.ToString(CMP2_to_TB.Length) +','+'\t'; ;
                for (int i = 0; i < CKP_to_TB.Length; i++) tb_Data_arrayToByte.Text += Convert.ToString(CKP_to_TB[i]) + '\t';
                for (int i = 0; i < CMP1_to_TB.Length; i++) tb_Data_arrayToByte.Text += Convert.ToString(CMP1_to_TB[i]) + '\t';
                for (int i = 0; i < CMP2_to_TB.Length; i++) tb_Data_arrayToByte.Text += Convert.ToString(CMP2_to_TB[i]) + '\t';

                //double pedro = CMP2_to_TB.Length;
                bytes_out.Text = Convert.ToString(CMP2_to_TB.Length * 3);
                textBox2.Text = Convert.ToString(CMP2_to_TB.Length);

            }
        }
        private void Direct_excel_Click(object sender, EventArgs e)
        {
            make_file_Excel();
        }
        private void print_bits_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CKP.Length; i++)
            {
                bits_TB.Text += Convert.ToString(CKP[i]) + '\t' + Convert.ToString(CMP1[i]) + '\t' + Convert.ToString(CMP2[i]) +'\r' + '\n';
            }

            MessageBox.Show("listo!! :D");
        }
        private void file_ctrl_BT_Click(object sender, EventArgs e)
        {
            make_control_file();
        }
        ///-----------------------Funciones---------------------------////
        private void imprimir_Chart(double InicialPosition)
        {

            
            //change values for chart

            int[]  CKP_int = new int[CKP.Length];
            int[]  CMP1_int = new int[CKP.Length];
            int[]  CMP2_int = new int[CKP.Length];

            for (int i = 0; i < CKP.Length; i++)
            {
                CKP_int[i] = CKP[i] * 3;
                CMP1_int[i] = CMP1[i] * 2;
                CMP2_int[i] = CMP2[i];
            }

            /////////////////******Graficar**********/////////////////////
            int blockSize = 100 * tim_div_TrackBar.Value;

            var valuesArrayCKP = CKP_int;
            var valuesArrayCMP1 = CMP1_int;
            var valuesArrayCMP2 = CMP2_int;

            // clear the chart
            chart_EditSignal.Series.Clear();

            // fill the chart
            var seriesCKP = chart_EditSignal.Series.Add("CKP");
            var seriesCMP1 = chart_EditSignal.Series.Add("CMP1");
            var seriesCMP2 = chart_EditSignal.Series.Add("CMP2");
            //seriesCMP.Color()
            seriesCKP.ChartType = SeriesChartType.StepLine;
            seriesCKP.XValueType = ChartValueType.Int32;
            seriesCMP1.ChartType = SeriesChartType.StepLine;
            seriesCMP1.XValueType = ChartValueType.Int32;
            seriesCMP2.ChartType = SeriesChartType.StepLine;
            seriesCMP2.XValueType = ChartValueType.Int32;
            for (int i = 0; i < valuesArrayCKP.Length; i++)
            {
                seriesCKP.Points.AddXY(i, valuesArrayCKP[i]);
                seriesCMP1.Points.AddXY(i, valuesArrayCMP1[i]);
                seriesCMP2.Points.AddXY(i, valuesArrayCMP2[i]);

            }

            var chartArea = chart_EditSignal.ChartAreas[seriesCKP.ChartArea];

            // set view range to [0,max]
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisX.Maximum = valuesArrayCKP.Length;

            // enable autoscroll
            chartArea.CursorX.AutoScroll = true;

            // let's zoom to [0,blockSize] (e.g. [0,100])
            chartArea.AxisX.ScaleView.Zoomable = true;
            chartArea.AxisX.ScaleView.SizeType = DateTimeIntervalType.Number;
            int position = 0;
            int size = blockSize;
            chartArea.AxisX.ScaleView.Zoom(position, size);

            // disable zoom-reset button (only scrollbar's arrows are available)
            chartArea.AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;

            // set scrollbar small change to blockSize (e.g. 100)
            chartArea.AxisX.ScaleView.SmallScrollSize = blockSize;
            chartArea.AxisX.MajorGrid.LineWidth = 0;
            chartArea.AxisY.MajorGrid.LineWidth = 0;

            //cambiar tamaño y color de lineas
            chart_EditSignal.Series[0].BorderWidth = 6;
            chart_EditSignal.Series[1].BorderWidth = 4;
            chart_EditSignal.Series[2].BorderWidth = 2;
            chart_EditSignal.Series[0].Color = Color.Blue;
            chart_EditSignal.Series[1].Color = Color.Yellow;
            chart_EditSignal.Series[2].Color = Color.Red;
            //cambiar fondo de char
            chartArea.BackColor = Color.Black;

            //calcular e imprimir cantidad de Bytes
            double temporal = CKP.Length;
            bits_out_TB.Text = Convert.ToString((CKP.Length/8)*3);

            double pedro = CKP.Length;
            bytes_per_signal.Text = Convert.ToString(pedro / 8);

            //acomodar posicion
            double asdas = chart_EditSignal.ChartAreas[0].AxisX.ScaleView.Position;
            chart_EditSignal.ChartAreas[0].AxisX.ScaleView.Position = InicialPosition;



        }
        private void mouse_leave_chart(object sender, EventArgs e)
        {
            mouse_leave_funtion();
        }
        private void make_control_file()
        {
            //leer desde un archivo de excel
            //abrir archivo

            //D: \usuarios\IPN OneDrive\OneDrive - Instituto Politecnico Nacional\mBeta
            //C:\Users\PJ2Q\OneDrive - Instituto Politecnico Nacional\mBeta


            string excelFinalPath = @"D:\usuarios\IPN OneDrive\OneDrive - Instituto Politecnico Nacional\mBeta\senales 2.4.2.xlsx";
            Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            Workbook workBook = application.Workbooks.Open(excelFinalPath);
            /////////////////////////////////////////////////////////////////
            string workbookName = workBook.Name;
            int numberOfSheets = workBook.Sheets.Count;


            Worksheet worksheet = workBook.Worksheets[1];//sheet 1 contiene la ultima actualizacion

            //guardar todas las variables
            
            object senales = ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[2, 5]).Value; ////////[Filas, Columnas]///empieza en 1//////////////////////////////////////

           

            /////////////////////////////////////////////////////////////////
            int Rows = Convert.ToInt32(senales);
            int Columns = 2;

            string stringtoConvert = "";

            byte[] Aux = { 0 };

            Range excelCell = worksheet.UsedRange;
            Object[,] sheetValues = (Object[,])excelCell.Value;

            string[,] fila_n_columnas = new string[Rows, Columns];

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                    fila_n_columnas[i, j] = Convert.ToString(sheetValues[i + 4, j + 4]);//aqui modofique hoy
            }
            workBook.Close(false, excelFinalPath, null);
            Marshal.ReleaseComObject(workBook);
            // cerrar el archivo
            //////////////////////////////////////////////////////////////
            ///
            byte[] toSendBytes = { }; //limpiar porsi 
            string SendString = "" ;


            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (fila_n_columnas[i, j] != "")
                    {
                        SendString += fila_n_columnas[i, j].Replace(":", ""); ;
                        if (j == 0)
                        {
                            SendString += "\r" + "\n";
                        }
                    }
                }
                SendString += "\r" + "\n";
            }


            byte[] byteSendString = new byte[SendString.Length];
            for (int i = 0; i < SendString.Length; i++)
            {
                byteSendString[i] = Convert.ToByte(SendString[i]);
            }

            // Displays a SaveFileDialog so the user can save the text 
            // assigned to Save_But.  
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "text|*.txt|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save Control file";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.  
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the  
                // File type selected in the dialog box.  
                // NOTE that the FilterIndex property is one-based.  
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        //using (System.IO.FileStream fs = System.IO.File.Create(pathString))
                        //{
                        fs.Write(byteSendString, 0, byteSendString.Length);
                        //for (long i = 0; i < SendString.Length; i++)
                        //{
                        //    fs.WriteByte(SendString[i]);
                        //}
                        ////}
                        break;

                    case 2:
                        this.Save_But.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        this.Save_But.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }


            MessageBox.Show("file control hecho !!");
        }
        private void mouse_leave_funtion()
        {
            if (refresh)
            {
                int P = Convert.ToInt32(chart_EditSignal.ChartAreas[0].AxisX.ScaleView.Position);
                P--;
                textBox1.Text = P.ToString();
                for (int i = 0; (i < 101) && (i < CKP.Length); i++)
                {
                    DataGrid_EditSignal.Rows[0].Cells[i].Style.BackColor = Color.White;
                    DataGrid_EditSignal.Rows[1].Cells[i].Style.BackColor = Color.White;
                    DataGrid_EditSignal.Rows[2].Cells[i].Style.BackColor = Color.White;
                }
                /////****************reimprimir en datagrid***********/////
                for (int j = 0; j < 101; j++) DataGrid_EditSignal.Columns[j].HeaderText = Convert.ToString(j + P);

                for (int i = 0; (i < 101) && (i < CKP.Length); i++) DataGrid_EditSignal.Rows[0].Cells[i].Value = CKP[i + P];
                for (int i = 0; (i < 101) && (i < CKP.Length); i++) DataGrid_EditSignal.Rows[1].Cells[i].Value = CMP1[i + P];
                for (int i = 0; (i < 101) && (i < CKP.Length); i++) DataGrid_EditSignal.Rows[2].Cells[i].Value = CMP2[i + P];

                ////colores!!!!!!!!!!
                for (int i = 0; (i < 101) && (i < CKP.Length); i++) if (Convert.ToInt16(DataGrid_EditSignal.Rows[0].Cells[i].Value) == 1) DataGrid_EditSignal.Rows[0].Cells[i].Style.BackColor = Color.Blue;
                for (int i = 0; (i < 101) && (i < CKP.Length); i++) if (Convert.ToInt16(DataGrid_EditSignal.Rows[1].Cells[i].Value) == 1) DataGrid_EditSignal.Rows[1].Cells[i].Style.BackColor = Color.Yellow;
                for (int i = 0; (i < 101) && (i < CKP.Length); i++) if (Convert.ToInt16(DataGrid_EditSignal.Rows[2].Cells[i].Value) == 1) DataGrid_EditSignal.Rows[2].Cells[i].Style.BackColor = Color.Red;
            }
        }
        private void printBitsToDG()
        {

            ///////////******************imprimir en datagrid************/////////////////////
            DataGrid_EditSignal.ColumnCount = 101;
            for (int i = 0; i < DataGrid_EditSignal.ColumnCount; i++) DataGrid_EditSignal.Columns[i].HeaderText = Convert.ToString(i);

            DataGrid_EditSignal.RowCount = 3;
            DataGrid_EditSignal.Rows[0].HeaderCell.Value = "CKP";
            DataGrid_EditSignal.Rows[1].HeaderCell.Value = "CMP1";
            DataGrid_EditSignal.Rows[2].HeaderCell.Value = "CMP2";

            DataGrid_EditSignal.AutoResizeRowHeadersWidth(0, DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);


            for (int i = 0; (i < 101) && (i < CKP.Length); i++) DataGrid_EditSignal.Rows[0].Cells[i].Value = CKP[i];
            for (int i = 0; (i < 101) && (i < CKP.Length); i++) DataGrid_EditSignal.Rows[1].Cells[i].Value = CMP1[i];
            for (int i = 0; (i < 101) && (i < CKP.Length); i++) DataGrid_EditSignal.Rows[2].Cells[i].Value = CMP2[i];


            for (int i = 0; i < DataGrid_EditSignal.ColumnCount; i++)
            {
                DataGrid_EditSignal.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }


            //datagrid has calculated it's widths so we can store them
            for (int i = 0; i <= DataGrid_EditSignal.Columns.Count - 1; i++)
            {
                //store autosized widths
                int colw = DataGrid_EditSignal.Columns[i].Width;
                //remove autosizing
                DataGrid_EditSignal.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //set width to calculated by autosize
                DataGrid_EditSignal.Columns[i].Width = colw;
            }
            ///quitar colores
            for (int i = 0; (i < 101) && (i < CKP.Length); i++)
            {
                DataGrid_EditSignal.Rows[0].Cells[i].Style.BackColor = Color.White;
                DataGrid_EditSignal.Rows[1].Cells[i].Style.BackColor = Color.White;
                DataGrid_EditSignal.Rows[2].Cells[i].Style.BackColor = Color.White;
            }

            ////colores!!!!!!!!!!
            for (int i = 0; (i < 101) && (i < CKP.Length); i++) if (Convert.ToInt16(DataGrid_EditSignal.Rows[0].Cells[i].Value) == 1) DataGrid_EditSignal.Rows[0].Cells[i].Style.BackColor = Color.Blue;
            for (int i = 0; (i < 101) && (i < CKP.Length); i++) if (Convert.ToInt16(DataGrid_EditSignal.Rows[1].Cells[i].Value) == 1) DataGrid_EditSignal.Rows[1].Cells[i].Style.BackColor = Color.Yellow;
            for (int i = 0; (i < 101) && (i < CKP.Length); i++) if (Convert.ToInt16(DataGrid_EditSignal.Rows[2].Cells[i].Value) == 1) DataGrid_EditSignal.Rows[2].Cells[i].Style.BackColor = Color.Red;

            refresh = true;

        }
        private void refresh_funtion()
        {
            int P = Convert.ToInt32(chart_EditSignal.ChartAreas[0].AxisX.ScaleView.Position);
            P--;
            for (int i = 0; i < (DataGrid_EditSignal.ColumnCount) && (i < CKP.Length); i++)
            {
                if (Convert.ToInt32(DataGrid_EditSignal.Rows[0].Cells[i].Value) != CKP[i + P])
                {
                    CKP[i + P] = Convert.ToInt32(DataGrid_EditSignal.Rows[0].Cells[i].Value);
                }
                if (Convert.ToInt32(DataGrid_EditSignal.Rows[1].Cells[i].Value) != CMP1[i + P])
                {
                    CMP1[i + P] = Convert.ToInt32(DataGrid_EditSignal.Rows[1].Cells[i].Value);
                }
                if (Convert.ToInt32(DataGrid_EditSignal.Rows[2].Cells[i].Value) != CMP2[i + P])
                {
                    CMP2[i + P] = Convert.ToInt32(DataGrid_EditSignal.Rows[2].Cells[i].Value);
                }
            }

            imprimir_Chart(chart_EditSignal.ChartAreas[0].AxisX.ScaleView.Position);
            ///////////******************imprimir en datagrid************/////////////////////
            ///
            //limpiar DG
            DataGrid_EditSignal.ColumnCount = 101;

            for (int i = 0; i < DataGrid_EditSignal.ColumnCount; i++) DataGrid_EditSignal.Rows[0].Cells[i].Value = null;
            for (int i = 0; i < DataGrid_EditSignal.ColumnCount; i++) DataGrid_EditSignal.Rows[1].Cells[i].Value = null;
            for (int i = 0; i < DataGrid_EditSignal.ColumnCount; i++) DataGrid_EditSignal.Rows[2].Cells[i].Value = null;

            printBitsToDG();
            mouse_leave_funtion();
        }
        private void make_bytes()
        {
            if ((CKP.Length % 8) != 0)
            {
                MessageBox.Show("cantidad de bits por señal no es multiplo de 8, corrige....baboso");
            }
            else
            {
                int[] CKP_to_TB = new int[CKP.Length/8];
                int[] CMP1_to_TB = new int[CKP.Length/8];
                int[] CMP2_to_TB = new int[CKP.Length/8];

                int contador = 0;

                //calcular CKP
                for (int i = 0; i < CKP_to_TB.Length; i++)
                {
                    for (int j = 0; (j < 8) && (contador < CKP.Length); j++)
                    {
                        CKP_to_TB[i] >>= 1;
                        if (CKP[contador] == 0)
                        {
                            CKP_to_TB[i] &= 127;
                        }
                        if (CKP[contador] == 1)
                        {
                            CKP_to_TB[i] |= 128;
                        }

                        contador++;
                    }
                }
                //calcular cmp1
                contador = 0;
                for (int i = 0; i < CMP1_to_TB.Length; i++)
                {
                    for (int j = 0; (j < 8) && (contador < CKP.Length); j++)
                    {
                        CMP1_to_TB[i] >>= 1;
                        if (CMP1[contador] == 0)
                        {
                            CMP1_to_TB[i] &= 127;
                        }
                        if (CMP1[contador] == 1)
                        {
                            CMP1_to_TB[i] |= 128;
                        }

                        contador++;
                    }
                }
                //calcular cmp2
                contador = 0;
                for (int i = 0; i < CMP2_to_TB.Length; i++)
                {
                    for (int j = 0; (j < 8) && (contador < CMP2.Length); j++)
                    {
                        CMP2_to_TB[i] >>= 1;
                        if (CMP2[contador] == 0)
                        {
                            CMP2_to_TB[i] &= 127;
                        }
                        if (CMP2[contador] == 1)
                        {
                            CMP2_to_TB[i] |= 128;
                        }

                        contador++;
                    }
                }
                //data_EditSignal_TB.Text = "";
                // data_EditSignal_TB.Text = bytes_per_signal.Text +','+ '\t';
                string tempo = bytes_per_signal.Text + ',' + '\t';
               
                for (int i = 0; i < CKP_to_TB.Length; i++) tempo += Convert.ToString(CKP_to_TB[i]) + '\t';
                for (int i = 0; i < CMP1_to_TB.Length; i++) tempo += Convert.ToString(CMP1_to_TB[i]) + '\t';
                for (int i = 0; i < CMP2_to_TB.Length; i++) tempo += Convert.ToString(CMP2_to_TB[i]) + '\t';

                data_EditSignal_TB.Text = tempo;

                //for (int i = 0; i < CKP_to_TB.Length; i++) data_EditSignal_TB.Text += Convert.ToString(CKP_to_TB[i]) + '\t';
                //for (int i = 0; i < CMP1_to_TB.Length; i++) data_EditSignal_TB.Text += Convert.ToString(CMP1_to_TB[i]) + '\t';
                //for (int i = 0; i < CMP2_to_TB.Length; i++) data_EditSignal_TB.Text += Convert.ToString(CMP2_to_TB[i]) + '\t';

                Clipboard.SetText(data_EditSignal_TB.Text);
                MessageBox.Show("listo!! :D, cantidad de bytes por señal y bytes copiados a portapapeles! ^^");
            }
            

        }
        private void make_file()
        {
            //Clipboard.ContainsData()
            string clipboard_data = Clipboard.GetText();

            int Columns = Convert.ToInt32(Max_col.Text);
            int Rows = Convert.ToInt32(Max_row.Text);
            //MessageBox.Show("copiado!");

            string stringtoConvert = "";

            byte[] bytess = { 0 };
            byte[] Aux = { 0 };


            /// programa nuevo :S aiudame diosito
            outTextFile_TB.Text = clipboard_data;
            clipboard_data = clipboard_data.Replace("\n", "");
            string[] solo_columnas = clipboard_data.Split('\r');

            string[,] fila_n_columnas = new string[Rows, Columns];
            string[] aux;

            for (int i = 0; i < Rows; i++)
            {
                aux = solo_columnas[i].Split('\t');
                for (int j = 0; j < Columns; j++) fila_n_columnas[i, j] = aux[j];
            }
            int num_of_bytes = 0;   //numeros de bytes por fila
                                   
            ///programa nuevo, ayudame diosito
            for (int i = 0; i < Rows; i++)
            {
                if (i != 0)
                {
                    Aux[0] = 13;
                    toSendBytes = toSendBytes.Concat(Aux).ToArray();
                    Aux[0] = 10;
                    toSendBytes = toSendBytes.Concat(Aux).ToArray();
                }
                stringtoConvert = fila_n_columnas[i, 0];
                toSendBytes = toSendBytes.Concat(Encoding.ASCII.GetBytes(stringtoConvert)).ToArray();

                stringtoConvert = fila_n_columnas[i, 1];
                toSendBytes = toSendBytes.Concat(Encoding.ASCII.GetBytes(stringtoConvert)).ToArray();

                stringtoConvert = fila_n_columnas[i, 2];
                toSendBytes = toSendBytes.Concat(Encoding.ASCII.GetBytes(stringtoConvert)).ToArray();

                stringtoConvert = stringtoConvert.Replace(",", "");
                num_of_bytes = (3 * Convert.ToInt16(stringtoConvert)) + 3;
                //num_of_bytes *= 3;
                for (int j = 3; j < num_of_bytes; j++)
                {
                    Aux[0] = Convert.ToByte(fila_n_columnas[i, j]);
                    toSendBytes = toSendBytes.Concat(Aux).ToArray();
                }

            }
        }
        private void make_file_Excel()
        {

            //leer desde un archivo de excel
            //abrir archivo
            string excelFinalPath = @"D:\usuarios\IPN OneDrive\OneDrive - Instituto Politecnico Nacional\mBeta\senales 2.4.2.xlsx";
            Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            Workbook workBook = application.Workbooks.Open(excelFinalPath);
            /////////////////////////////////////////////////////////////////
            string workbookName = workBook.Name;
            int numberOfSheets = workBook.Sheets.Count;


            Worksheet worksheet = workBook.Worksheets[1];//sheet 5 contiene la ultima actualizacion

            //guardar todas las variables
            object bytes = ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, 5]).Value;  ////////////////////////////////////////////////////////[Filas, Columnas]///empieza en 1//////////////////////////////////////
            object senales = ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[2, 5]).Value;
            object cellValue = ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, 1]).Value;

            // cerrar el archivo
           
            /////////////////////////////////////////////////////////////////

            //modificar todo ese pedo
           

            int Columns = Convert.ToInt32(bytes);
            int Rows = Convert.ToInt32(senales);
            //MessageBox.Show("copiado!");

            string stringtoConvert = "";

            //byte[] bytess = { 0 };
            byte[] Aux = { 0 };


            ///// programa nuevo :S aiudame diosito
            //outTextFile_TB.Text = clipboard_data;
            //clipboard_data = clipboard_data.Replace("\n", "");
            //string[] solo_columnas = clipboard_data.Split('\r');

            Range excelCell = worksheet.UsedRange;
            Object[,] sheetValues = (Object[,])excelCell.Value;

            string[,] fila_n_columnas = new string[Rows, Columns];

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)

                    fila_n_columnas[i, j] = Convert.ToString(sheetValues[i + 4, j + 5]);
                //if (((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[i+4, j+4]).Value!=null)
                //{
                //    //fila_n_columnas[i, j] =Convert.ToString(((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[i + 4, j + 4]).Value);

                //}
                //else
                //{
                //    //MessageBox.Show("copiado!");
                //    //workBook.Close(false, excelFinalPath, null);
                //    //Marshal.ReleaseComObject(workBook);
                //}

            }

            

            workBook.Close(false, excelFinalPath, null);
            Marshal.ReleaseComObject(workBook);

            int num_of_bytes = 0;   //numeros de bytes por fila
            ///programa nuevo, ayudame diosito
            for (int i = 0; i < Rows; i++)
            {
                if (i != 0)
                {
                    Aux[0] = 13;
                    toSendBytes = toSendBytes.Concat(Aux).ToArray();
                    Aux[0] = 10;
                    toSendBytes = toSendBytes.Concat(Aux).ToArray();
                }
                stringtoConvert = fila_n_columnas[i, 0];
                toSendBytes = toSendBytes.Concat(Encoding.ASCII.GetBytes(stringtoConvert)).ToArray();

                stringtoConvert = fila_n_columnas[i, 1];
                toSendBytes = toSendBytes.Concat(Encoding.ASCII.GetBytes(stringtoConvert)).ToArray();

                stringtoConvert = fila_n_columnas[i, 2];
                toSendBytes = toSendBytes.Concat(Encoding.ASCII.GetBytes(stringtoConvert)).ToArray();

                stringtoConvert = stringtoConvert.Replace(",", "");
                num_of_bytes = (3 * Convert.ToInt16(stringtoConvert)) + 3;
                //num_of_bytes *= 3;
                for (int j = 3; j < num_of_bytes; j++)
                {
                    Aux[0] = Convert.ToByte(fila_n_columnas[i, j]);
                    toSendBytes = toSendBytes.Concat(Aux).ToArray();
                }

            }
            MessageBox.Show("file thr hecho click guardar!!");

        }

        private void chart_MouseWheel(object sender, MouseEventArgs e)
        {

            int valor = tim_div_TrackBar.Value;
            if (e.Delta > 0) //sube rueda
            {
                valor++;


            }
                
            else if (e.Delta < 0)//baja rueda
            {
                valor--;
            }
            textBox1.Text = Convert.ToString(valor);
            if (valor < tim_div_TrackBar.Minimum ) {
                valor = tim_div_TrackBar.Minimum;

            }
            if (valor > tim_div_TrackBar.Maximum) {
               valor = tim_div_TrackBar.Maximum;
            }
             tim_div_TrackBar.Value = valor;
            imprimir_Chart(1);
        }


        //control curosores

        bool jaja = false;
        private void mouse_move_over_chart_event(object sender, MouseEventArgs e)
        {
            if (jaja)
            {
                Cursor_1.Location = new System.Drawing.Point((e.X), 122);
            }
            
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        private void clicl(object sender, EventArgs e)
        {
            jaja = false;
        }
        private void mouse_click(object sender, MouseEventArgs e)
        {

        }

        private void DragOver_cursor1(object sender, DragEventArgs e)
        {
           
        }

        private void mouse_down_cursor1(object sender, MouseEventArgs e)
        {
            jaja = true;
        }

        private void mouse_un_cursor1(object sender, MouseEventArgs e)
        {
            
        }

        private void mouse_enter_cursor1(object sender, EventArgs e)
        {

        }

        private void chart_mouseHober(object sender, EventArgs e)
        {
            chart_EditSignal.Focus();
        }
    }
}
