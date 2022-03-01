namespace Edit_n_Export_3signals
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.MakeFile = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Cursor_1 = new System.Windows.Forms.Label();
            this.cursor1 = new System.Windows.Forms.Label();
            this.print_bits = new System.Windows.Forms.Button();
            this.bits_TB = new System.Windows.Forms.TextBox();
            this.chart_EditSignal = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.load_data_BT = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.print_bytes = new System.Windows.Forms.Button();
            this.Refresh_but = new System.Windows.Forms.Button();
            this.bytes_per_signal = new System.Windows.Forms.TextBox();
            this.bits_out_TB = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.delete_refresh_but = new System.Windows.Forms.Button();
            this.DataGrid_EditSignal = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.make_bytes_bt = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.data_EditSignal_TB = new System.Windows.Forms.TextBox();
            this.tim_div_TrackBar = new System.Windows.Forms.TrackBar();
            this.Graph = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.file_ctrl_BT = new System.Windows.Forms.Button();
            this.Direct_excel = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.Save_But = new System.Windows.Forms.Button();
            this.outTextFile_TB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Max_row = new System.Windows.Forms.TextBox();
            this.Max_col = new System.Windows.Forms.TextBox();
            this.Get_ClipBo = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.bytes_out = new System.Windows.Forms.TextBox();
            this.conv_arrayToByte_bt = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_size_sample_arrayToBytes = new System.Windows.Forms.TextBox();
            this.tb_Data_arrayToByte = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.MakeFile.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_EditSignal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid_EditSignal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tim_div_TrackBar)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // MakeFile
            // 
            this.MakeFile.Controls.Add(this.tabPage2);
            this.MakeFile.Controls.Add(this.tabPage1);
            this.MakeFile.Controls.Add(this.tabPage3);
            this.MakeFile.Location = new System.Drawing.Point(3, 12);
            this.MakeFile.Name = "MakeFile";
            this.MakeFile.SelectedIndex = 0;
            this.MakeFile.Size = new System.Drawing.Size(1238, 666);
            this.MakeFile.TabIndex = 18;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Cursor_1);
            this.tabPage2.Controls.Add(this.cursor1);
            this.tabPage2.Controls.Add(this.print_bits);
            this.tabPage2.Controls.Add(this.bits_TB);
            this.tabPage2.Controls.Add(this.chart_EditSignal);
            this.tabPage2.Controls.Add(this.load_data_BT);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label24);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.print_bytes);
            this.tabPage2.Controls.Add(this.Refresh_but);
            this.tabPage2.Controls.Add(this.bytes_per_signal);
            this.tabPage2.Controls.Add(this.bits_out_TB);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.delete_refresh_but);
            this.tabPage2.Controls.Add(this.DataGrid_EditSignal);
            this.tabPage2.Controls.Add(this.make_bytes_bt);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.data_EditSignal_TB);
            this.tabPage2.Controls.Add(this.tim_div_TrackBar);
            this.tabPage2.Controls.Add(this.Graph);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1230, 640);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "EditSignal";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // Cursor_1
            // 
            this.Cursor_1.BackColor = System.Drawing.Color.Gray;
            this.Cursor_1.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.Cursor_1.Location = new System.Drawing.Point(9, 124);
            this.Cursor_1.Name = "Cursor_1";
            this.Cursor_1.Size = new System.Drawing.Size(3, 248);
            this.Cursor_1.TabIndex = 23;
            this.Cursor_1.DragOver += new System.Windows.Forms.DragEventHandler(this.DragOver_cursor1);
            this.Cursor_1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouse_down_cursor1);
            this.Cursor_1.MouseEnter += new System.EventHandler(this.mouse_enter_cursor1);
            this.Cursor_1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouse_un_cursor1);
            // 
            // cursor1
            // 
            this.cursor1.AutoSize = true;
            this.cursor1.Location = new System.Drawing.Point(9, 560);
            this.cursor1.Name = "cursor1";
            this.cursor1.Size = new System.Drawing.Size(0, 13);
            this.cursor1.TabIndex = 22;
            // 
            // print_bits
            // 
            this.print_bits.Location = new System.Drawing.Point(1130, 586);
            this.print_bits.Name = "print_bits";
            this.print_bits.Size = new System.Drawing.Size(75, 23);
            this.print_bits.TabIndex = 21;
            this.print_bits.Text = "print bits";
            this.print_bits.UseVisualStyleBackColor = true;
            this.print_bits.Click += new System.EventHandler(this.print_bits_Click);
            // 
            // bits_TB
            // 
            this.bits_TB.Location = new System.Drawing.Point(775, 570);
            this.bits_TB.Multiline = true;
            this.bits_TB.Name = "bits_TB";
            this.bits_TB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.bits_TB.Size = new System.Drawing.Size(349, 52);
            this.bits_TB.TabIndex = 20;
            // 
            // chart_EditSignal
            // 
            chartArea1.Name = "ChartArea1";
            this.chart_EditSignal.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart_EditSignal.Legends.Add(legend1);
            this.chart_EditSignal.Location = new System.Drawing.Point(-40, 97);
            this.chart_EditSignal.Name = "chart_EditSignal";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart_EditSignal.Series.Add(series1);
            this.chart_EditSignal.Size = new System.Drawing.Size(1258, 322);
            this.chart_EditSignal.TabIndex = 19;
            this.chart_EditSignal.Text = "chart1";
            this.chart_EditSignal.Click += new System.EventHandler(this.clicl);
            this.chart_EditSignal.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mouse_click);
            this.chart_EditSignal.MouseLeave += new System.EventHandler(this.mouse_leave_chart);
            this.chart_EditSignal.MouseHover += new System.EventHandler(this.chart_mouseHober);
            this.chart_EditSignal.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mouse_move_over_chart_event);
            // 
            // load_data_BT
            // 
            this.load_data_BT.Location = new System.Drawing.Point(12, 21);
            this.load_data_BT.Name = "load_data_BT";
            this.load_data_BT.Size = new System.Drawing.Size(75, 50);
            this.load_data_BT.TabIndex = 0;
            this.load_data_BT.Text = "Load Data";
            this.load_data_BT.UseVisualStyleBackColor = true;
            this.load_data_BT.Click += new System.EventHandler(this.load_data_BT_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(493, 574);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "number of bytes per signal";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(650, 573);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(55, 13);
            this.label24.TabIndex = 16;
            this.label24.Text = "total bytes";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(157, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "copia con cantidad de bytes por señal";
            // 
            // print_bytes
            // 
            this.print_bytes.Location = new System.Drawing.Point(29, 573);
            this.print_bytes.Name = "print_bytes";
            this.print_bytes.Size = new System.Drawing.Size(95, 42);
            this.print_bytes.TabIndex = 12;
            this.print_bytes.Text = "print bits to DG";
            this.print_bytes.UseVisualStyleBackColor = true;
            this.print_bytes.Click += new System.EventHandler(this.print_bytes_Click);
            // 
            // Refresh_but
            // 
            this.Refresh_but.Location = new System.Drawing.Point(139, 573);
            this.Refresh_but.Name = "Refresh_but";
            this.Refresh_but.Size = new System.Drawing.Size(95, 42);
            this.Refresh_but.TabIndex = 3;
            this.Refresh_but.Text = "Refresh";
            this.Refresh_but.UseVisualStyleBackColor = true;
            this.Refresh_but.Click += new System.EventHandler(this.Refresh_but_Click);
            // 
            // bytes_per_signal
            // 
            this.bytes_per_signal.Location = new System.Drawing.Point(505, 595);
            this.bytes_per_signal.Name = "bytes_per_signal";
            this.bytes_per_signal.Size = new System.Drawing.Size(100, 20);
            this.bytes_per_signal.TabIndex = 15;
            // 
            // bits_out_TB
            // 
            this.bits_out_TB.Location = new System.Drawing.Point(631, 595);
            this.bits_out_TB.Name = "bits_out_TB";
            this.bits_out_TB.Size = new System.Drawing.Size(100, 20);
            this.bits_out_TB.TabIndex = 15;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1087, 65);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 13;
            // 
            // delete_refresh_but
            // 
            this.delete_refresh_but.Location = new System.Drawing.Point(250, 573);
            this.delete_refresh_but.Name = "delete_refresh_but";
            this.delete_refresh_but.Size = new System.Drawing.Size(107, 42);
            this.delete_refresh_but.TabIndex = 4;
            this.delete_refresh_but.Text = "Delete n refresh";
            this.delete_refresh_but.UseVisualStyleBackColor = true;
            this.delete_refresh_but.Click += new System.EventHandler(this.delete_refresh_but_Click);
            // 
            // DataGrid_EditSignal
            // 
            this.DataGrid_EditSignal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid_EditSignal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.DataGrid_EditSignal.Location = new System.Drawing.Point(3, 426);
            this.DataGrid_EditSignal.Name = "DataGrid_EditSignal";
            this.DataGrid_EditSignal.Size = new System.Drawing.Size(1215, 128);
            this.DataGrid_EditSignal.TabIndex = 10;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // make_bytes_bt
            // 
            this.make_bytes_bt.Location = new System.Drawing.Point(376, 574);
            this.make_bytes_bt.Name = "make_bytes_bt";
            this.make_bytes_bt.Size = new System.Drawing.Size(91, 41);
            this.make_bytes_bt.TabIndex = 5;
            this.make_bytes_bt.Text = "Make bytes";
            this.make_bytes_bt.UseVisualStyleBackColor = true;
            this.make_bytes_bt.Click += new System.EventHandler(this.make_bytes_bt_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1106, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "position";
            // 
            // data_EditSignal_TB
            // 
            this.data_EditSignal_TB.Location = new System.Drawing.Point(109, 21);
            this.data_EditSignal_TB.Multiline = true;
            this.data_EditSignal_TB.Name = "data_EditSignal_TB";
            this.data_EditSignal_TB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.data_EditSignal_TB.Size = new System.Drawing.Size(831, 74);
            this.data_EditSignal_TB.TabIndex = 8;
            // 
            // tim_div_TrackBar
            // 
            this.tim_div_TrackBar.Location = new System.Drawing.Point(953, 3);
            this.tim_div_TrackBar.Name = "tim_div_TrackBar";
            this.tim_div_TrackBar.Size = new System.Drawing.Size(265, 45);
            this.tim_div_TrackBar.TabIndex = 6;
            // 
            // Graph
            // 
            this.Graph.Location = new System.Drawing.Point(1006, 62);
            this.Graph.Name = "Graph";
            this.Graph.Size = new System.Drawing.Size(75, 23);
            this.Graph.TabIndex = 1;
            this.Graph.Text = "Graph";
            this.Graph.UseVisualStyleBackColor = true;
            this.Graph.Click += new System.EventHandler(this.Graph_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.file_ctrl_BT);
            this.tabPage1.Controls.Add(this.Direct_excel);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.Save_But);
            this.tabPage1.Controls.Add(this.outTextFile_TB);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.Max_row);
            this.tabPage1.Controls.Add(this.Max_col);
            this.tabPage1.Controls.Add(this.Get_ClipBo);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1230, 640);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "MakeFile";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // file_ctrl_BT
            // 
            this.file_ctrl_BT.Location = new System.Drawing.Point(597, 25);
            this.file_ctrl_BT.Name = "file_ctrl_BT";
            this.file_ctrl_BT.Size = new System.Drawing.Size(79, 45);
            this.file_ctrl_BT.TabIndex = 9;
            this.file_ctrl_BT.Text = "make control file";
            this.file_ctrl_BT.UseVisualStyleBackColor = true;
            this.file_ctrl_BT.Click += new System.EventHandler(this.file_ctrl_BT_Click);
            // 
            // Direct_excel
            // 
            this.Direct_excel.Location = new System.Drawing.Point(484, 25);
            this.Direct_excel.Name = "Direct_excel";
            this.Direct_excel.Size = new System.Drawing.Size(76, 45);
            this.Direct_excel.TabIndex = 8;
            this.Direct_excel.Text = "From excel";
            this.Direct_excel.UseVisualStyleBackColor = true;
            this.Direct_excel.Click += new System.EventHandler(this.Direct_excel_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(334, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "max 3 columns";
            // 
            // Save_But
            // 
            this.Save_But.Location = new System.Drawing.Point(50, 61);
            this.Save_But.Name = "Save_But";
            this.Save_But.Size = new System.Drawing.Size(75, 23);
            this.Save_But.TabIndex = 6;
            this.Save_But.Text = "Save";
            this.Save_But.UseVisualStyleBackColor = true;
            this.Save_But.Click += new System.EventHandler(this.Save_But_Click);
            // 
            // outTextFile_TB
            // 
            this.outTextFile_TB.Location = new System.Drawing.Point(6, 170);
            this.outTextFile_TB.Multiline = true;
            this.outTextFile_TB.Name = "outTextFile_TB";
            this.outTextFile_TB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outTextFile_TB.Size = new System.Drawing.Size(1283, 348);
            this.outTextFile_TB.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(234, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Max Rows";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(233, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Max Columns";
            // 
            // Max_row
            // 
            this.Max_row.Location = new System.Drawing.Point(228, 64);
            this.Max_row.Name = "Max_row";
            this.Max_row.Size = new System.Drawing.Size(100, 20);
            this.Max_row.TabIndex = 2;
            // 
            // Max_col
            // 
            this.Max_col.Location = new System.Drawing.Point(228, 23);
            this.Max_col.Name = "Max_col";
            this.Max_col.Size = new System.Drawing.Size(100, 20);
            this.Max_col.TabIndex = 1;
            // 
            // Get_ClipBo
            // 
            this.Get_ClipBo.Location = new System.Drawing.Point(20, 20);
            this.Get_ClipBo.Name = "Get_ClipBo";
            this.Get_ClipBo.Size = new System.Drawing.Size(133, 23);
            this.Get_ClipBo.TabIndex = 0;
            this.Get_ClipBo.Text = "Get from Clipboard";
            this.Get_ClipBo.UseVisualStyleBackColor = true;
            this.Get_ClipBo.Click += new System.EventHandler(this.Get_ClipBo_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.textBox2);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.bytes_out);
            this.tabPage3.Controls.Add(this.conv_arrayToByte_bt);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.tb_size_sample_arrayToBytes);
            this.tabPage3.Controls.Add(this.tb_Data_arrayToByte);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1230, 640);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "array bits to byte";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(506, 182);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "bytes por señal";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(499, 198);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(506, 127);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "bytes totales de salida";
            // 
            // bytes_out
            // 
            this.bytes_out.Location = new System.Drawing.Point(499, 143);
            this.bytes_out.Name = "bytes_out";
            this.bytes_out.Size = new System.Drawing.Size(100, 20);
            this.bytes_out.TabIndex = 5;
            // 
            // conv_arrayToByte_bt
            // 
            this.conv_arrayToByte_bt.Location = new System.Drawing.Point(73, 58);
            this.conv_arrayToByte_bt.Name = "conv_arrayToByte_bt";
            this.conv_arrayToByte_bt.Size = new System.Drawing.Size(75, 23);
            this.conv_arrayToByte_bt.TabIndex = 4;
            this.conv_arrayToByte_bt.Text = "Convertir";
            this.conv_arrayToByte_bt.UseVisualStyleBackColor = true;
            this.conv_arrayToByte_bt.Click += new System.EventHandler(this.conv_arrayToByte_bt_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 104);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(154, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "cantidad de muestras por señal";
            // 
            // tb_size_sample_arrayToBytes
            // 
            this.tb_size_sample_arrayToBytes.Location = new System.Drawing.Point(48, 120);
            this.tb_size_sample_arrayToBytes.Name = "tb_size_sample_arrayToBytes";
            this.tb_size_sample_arrayToBytes.Size = new System.Drawing.Size(100, 20);
            this.tb_size_sample_arrayToBytes.TabIndex = 2;
            // 
            // tb_Data_arrayToByte
            // 
            this.tb_Data_arrayToByte.Location = new System.Drawing.Point(164, 28);
            this.tb_Data_arrayToByte.Multiline = true;
            this.tb_Data_arrayToByte.Name = "tb_Data_arrayToByte";
            this.tb_Data_arrayToByte.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_Data_arrayToByte.Size = new System.Drawing.Size(321, 492);
            this.tb_Data_arrayToByte.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(407, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Esta operacion conviernte arreblos de bits de la señal  muestreada a arreglo de b" +
    "ytes";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(510, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(125, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Edita y exporta 3 señales";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1242, 681);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.MakeFile);
            this.Name = "Form1";
            this.Text = "Form1";
            this.MakeFile.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_EditSignal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid_EditSignal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tim_div_TrackBar)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl MakeFile;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button load_data_BT;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button print_bytes;
        private System.Windows.Forms.Button Refresh_but;
        private System.Windows.Forms.TextBox bytes_per_signal;
        private System.Windows.Forms.TextBox bits_out_TB;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button delete_refresh_but;
        private System.Windows.Forms.DataGridView DataGrid_EditSignal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Button make_bytes_bt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox data_EditSignal_TB;
        private System.Windows.Forms.TrackBar tim_div_TrackBar;
        private System.Windows.Forms.Button Graph;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Save_But;
        private System.Windows.Forms.TextBox outTextFile_TB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Max_row;
        private System.Windows.Forms.TextBox Max_col;
        private System.Windows.Forms.Button Get_ClipBo;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox bytes_out;
        private System.Windows.Forms.Button conv_arrayToByte_bt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_size_sample_arrayToBytes;
        private System.Windows.Forms.TextBox tb_Data_arrayToByte;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_EditSignal;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button Direct_excel;
        private System.Windows.Forms.Button print_bits;
        private System.Windows.Forms.TextBox bits_TB;
        private System.Windows.Forms.Button file_ctrl_BT;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label Cursor_1;
        private System.Windows.Forms.Label cursor1;
    }
}

