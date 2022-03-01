namespace SDPPS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Matlab = new System.Windows.Forms.TabPage();
            this.progressBarCMP2 = new System.Windows.Forms.ProgressBar();
            this.progressBarCMP1 = new System.Windows.Forms.ProgressBar();
            this.progressBarCKP = new System.Windows.Forms.ProgressBar();
            this.ObtenerArreglo = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.cmp2_textbox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmp1_textbox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.x_final = new System.Windows.Forms.TextBox();
            this.cmp2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ckp_texbox = new System.Windows.Forms.TextBox();
            this.x_inicial = new System.Windows.Forms.TextBox();
            this.cmp1 = new System.Windows.Forms.Button();
            this.ckp = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.dilatar = new System.Windows.Forms.Button();
            this.BW2 = new System.Windows.Forms.Button();
            this.bin = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.imcomplement = new System.Windows.Forms.Button();
            this.rgb2gray = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.Select_image = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.Matlab.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Matlab);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 29);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1208, 674);
            this.tabControl1.TabIndex = 6;
            // 
            // Matlab
            // 
            this.Matlab.Controls.Add(this.progressBarCMP2);
            this.Matlab.Controls.Add(this.progressBarCMP1);
            this.Matlab.Controls.Add(this.progressBarCKP);
            this.Matlab.Controls.Add(this.ObtenerArreglo);
            this.Matlab.Controls.Add(this.label8);
            this.Matlab.Controls.Add(this.cmp2_textbox);
            this.Matlab.Controls.Add(this.label10);
            this.Matlab.Controls.Add(this.cmp1_textbox);
            this.Matlab.Controls.Add(this.label9);
            this.Matlab.Controls.Add(this.x_final);
            this.Matlab.Controls.Add(this.cmp2);
            this.Matlab.Controls.Add(this.label6);
            this.Matlab.Controls.Add(this.label7);
            this.Matlab.Controls.Add(this.ckp_texbox);
            this.Matlab.Controls.Add(this.x_inicial);
            this.Matlab.Controls.Add(this.cmp1);
            this.Matlab.Controls.Add(this.ckp);
            this.Matlab.Controls.Add(this.label5);
            this.Matlab.Controls.Add(this.label4);
            this.Matlab.Controls.Add(this.textBox5);
            this.Matlab.Controls.Add(this.textBox4);
            this.Matlab.Controls.Add(this.dilatar);
            this.Matlab.Controls.Add(this.BW2);
            this.Matlab.Controls.Add(this.bin);
            this.Matlab.Controls.Add(this.label3);
            this.Matlab.Controls.Add(this.imcomplement);
            this.Matlab.Controls.Add(this.rgb2gray);
            this.Matlab.Controls.Add(this.textBox3);
            this.Matlab.Controls.Add(this.Select_image);
            this.Matlab.Location = new System.Drawing.Point(4, 22);
            this.Matlab.Name = "Matlab";
            this.Matlab.Padding = new System.Windows.Forms.Padding(3);
            this.Matlab.Size = new System.Drawing.Size(1200, 648);
            this.Matlab.TabIndex = 0;
            this.Matlab.Text = "Pestaña Matlab";
            this.Matlab.UseVisualStyleBackColor = true;
            this.Matlab.Click += new System.EventHandler(this.Matlab_Click);
            // 
            // progressBarCMP2
            // 
            this.progressBarCMP2.Location = new System.Drawing.Point(458, 292);
            this.progressBarCMP2.Name = "progressBarCMP2";
            this.progressBarCMP2.Size = new System.Drawing.Size(100, 23);
            this.progressBarCMP2.TabIndex = 33;
            // 
            // progressBarCMP1
            // 
            this.progressBarCMP1.Location = new System.Drawing.Point(458, 253);
            this.progressBarCMP1.Name = "progressBarCMP1";
            this.progressBarCMP1.Size = new System.Drawing.Size(100, 23);
            this.progressBarCMP1.TabIndex = 32;
            // 
            // progressBarCKP
            // 
            this.progressBarCKP.Location = new System.Drawing.Point(458, 213);
            this.progressBarCKP.Name = "progressBarCKP";
            this.progressBarCKP.Size = new System.Drawing.Size(100, 23);
            this.progressBarCKP.TabIndex = 31;
            // 
            // ObtenerArreglo
            // 
            this.ObtenerArreglo.Location = new System.Drawing.Point(512, 340);
            this.ObtenerArreglo.Name = "ObtenerArreglo";
            this.ObtenerArreglo.Size = new System.Drawing.Size(75, 23);
            this.ObtenerArreglo.TabIndex = 30;
            this.ObtenerArreglo.Text = "obtener";
            this.ObtenerArreglo.UseVisualStyleBackColor = true;
            this.ObtenerArreglo.Click += new System.EventHandler(this.ObtenerCKP_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(339, 298);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Y";
            // 
            // cmp2_textbox
            // 
            this.cmp2_textbox.Location = new System.Drawing.Point(359, 295);
            this.cmp2_textbox.Name = "cmp2_textbox";
            this.cmp2_textbox.Size = new System.Drawing.Size(64, 20);
            this.cmp2_textbox.TabIndex = 28;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(339, 253);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "Y";
            // 
            // cmp1_textbox
            // 
            this.cmp1_textbox.Location = new System.Drawing.Point(359, 253);
            this.cmp1_textbox.Name = "cmp1_textbox";
            this.cmp1_textbox.Size = new System.Drawing.Size(64, 20);
            this.cmp1_textbox.TabIndex = 26;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(382, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "X final";
            // 
            // x_final
            // 
            this.x_final.Location = new System.Drawing.Point(385, 179);
            this.x_final.Name = "x_final";
            this.x_final.Size = new System.Drawing.Size(64, 20);
            this.x_final.TabIndex = 24;
            // 
            // cmp2
            // 
            this.cmp2.Location = new System.Drawing.Point(276, 291);
            this.cmp2.Name = "cmp2";
            this.cmp2.Size = new System.Drawing.Size(57, 22);
            this.cmp2.TabIndex = 21;
            this.cmp2.Text = "CMP2";
            this.cmp2.UseVisualStyleBackColor = true;
            this.cmp2.Click += new System.EventHandler(this.cmp2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(339, 213);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(318, 161);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "X inicial";
            // 
            // ckp_texbox
            // 
            this.ckp_texbox.Location = new System.Drawing.Point(359, 210);
            this.ckp_texbox.Name = "ckp_texbox";
            this.ckp_texbox.Size = new System.Drawing.Size(64, 20);
            this.ckp_texbox.TabIndex = 18;
            // 
            // x_inicial
            // 
            this.x_inicial.Location = new System.Drawing.Point(306, 179);
            this.x_inicial.Name = "x_inicial";
            this.x_inicial.Size = new System.Drawing.Size(64, 20);
            this.x_inicial.TabIndex = 17;
            // 
            // cmp1
            // 
            this.cmp1.Location = new System.Drawing.Point(276, 246);
            this.cmp1.Name = "cmp1";
            this.cmp1.Size = new System.Drawing.Size(57, 25);
            this.cmp1.TabIndex = 16;
            this.cmp1.Text = "CMP1";
            this.cmp1.UseVisualStyleBackColor = true;
            this.cmp1.Click += new System.EventHandler(this.cmp1_Click);
            // 
            // ckp
            // 
            this.ckp.Location = new System.Drawing.Point(276, 205);
            this.ckp.Name = "ckp";
            this.ckp.Size = new System.Drawing.Size(57, 23);
            this.ckp.TabIndex = 15;
            this.ckp.Text = "CKP";
            this.ckp.UseVisualStyleBackColor = true;
            this.ckp.Click += new System.EventHandler(this.ckp_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(180, 263);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "dilatar";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(161, 217);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "piezas a dejar:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(172, 279);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(64, 20);
            this.textBox5.TabIndex = 12;
            this.textBox5.Text = "1";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(164, 233);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(64, 20);
            this.textBox4.TabIndex = 11;
            this.textBox4.Text = "50";
            // 
            // dilatar
            // 
            this.dilatar.Location = new System.Drawing.Point(27, 279);
            this.dilatar.Name = "dilatar";
            this.dilatar.Size = new System.Drawing.Size(122, 23);
            this.dilatar.TabIndex = 10;
            this.dilatar.Text = "dilatar";
            this.dilatar.UseVisualStyleBackColor = true;
            this.dilatar.Click += new System.EventHandler(this.dilatar_Click);
            // 
            // BW2
            // 
            this.BW2.Location = new System.Drawing.Point(27, 231);
            this.BW2.Name = "BW2";
            this.BW2.Size = new System.Drawing.Size(122, 23);
            this.BW2.TabIndex = 9;
            this.BW2.Text = "quitar piezas pequeñas";
            this.BW2.UseVisualStyleBackColor = true;
            this.BW2.Click += new System.EventHandler(this.BW2_Click);
            // 
            // bin
            // 
            this.bin.Location = new System.Drawing.Point(27, 189);
            this.bin.Name = "bin";
            this.bin.Size = new System.Drawing.Size(122, 23);
            this.bin.TabIndex = 8;
            this.bin.Text = "BINARIZAR";
            this.bin.UseVisualStyleBackColor = true;
            this.bin.Click += new System.EventHandler(this.bin_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(154, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(232, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "SOLO APUCHURRAR SI ES FONDO BLANCO";
            // 
            // imcomplement
            // 
            this.imcomplement.Location = new System.Drawing.Point(27, 130);
            this.imcomplement.Name = "imcomplement";
            this.imcomplement.Size = new System.Drawing.Size(122, 23);
            this.imcomplement.TabIndex = 6;
            this.imcomplement.Text = "invertir colores";
            this.imcomplement.UseVisualStyleBackColor = true;
            this.imcomplement.Click += new System.EventHandler(this.imcomplement_Click);
            // 
            // rgb2gray
            // 
            this.rgb2gray.Location = new System.Drawing.Point(27, 83);
            this.rgb2gray.Name = "rgb2gray";
            this.rgb2gray.Size = new System.Drawing.Size(122, 23);
            this.rgb2gray.TabIndex = 5;
            this.rgb2gray.Text = "hacer blanco y negro";
            this.rgb2gray.UseVisualStyleBackColor = true;
            this.rgb2gray.Click += new System.EventHandler(this.rgb2gray_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(164, 22);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(567, 20);
            this.textBox3.TabIndex = 4;
            // 
            // Select_image
            // 
            this.Select_image.Location = new System.Drawing.Point(27, 19);
            this.Select_image.Name = "Select_image";
            this.Select_image.Size = new System.Drawing.Size(122, 23);
            this.Select_image.TabIndex = 3;
            this.Select_image.Text = "Seleccionar imagen";
            this.Select_image.UseVisualStyleBackColor = true;
            this.Select_image.Click += new System.EventHandler(this.Select_image_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1200, 648);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(258, 155);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(137, 96);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(129, 31);
            this.textBox1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(386, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "q";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(329, 96);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(129, 31);
            this.textBox2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(166, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "p";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(118, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "label11";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 718);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Super Duper Programa de Señales";
            this.tabControl1.ResumeLayout(false);
            this.Matlab.ResumeLayout(false);
            this.Matlab.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Matlab;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox cmp2_textbox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox cmp1_textbox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox x_final;
        private System.Windows.Forms.Button cmp2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox ckp_texbox;
        private System.Windows.Forms.TextBox x_inicial;
        private System.Windows.Forms.Button cmp1;
        private System.Windows.Forms.Button ckp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button dilatar;
        private System.Windows.Forms.Button BW2;
        private System.Windows.Forms.Button bin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button imcomplement;
        private System.Windows.Forms.Button rgb2gray;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button Select_image;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button ObtenerArreglo;
        private System.Windows.Forms.ProgressBar progressBarCMP2;
        private System.Windows.Forms.ProgressBar progressBarCMP1;
        private System.Windows.Forms.ProgressBar progressBarCKP;
    }
}

