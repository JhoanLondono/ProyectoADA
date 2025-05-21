namespace Proyecto_final_ADA
{
    public partial class Form1 : Form
    {
        Listapuntos lista1 = new Listapuntos();
        Listapuntos lista2 = new Listapuntos();
        Listapuntos lista3 = new Listapuntos();
        Listapuntos lista4 = new Listapuntos();

        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("Lista 1");
            comboBox1.Items.Add("Lista 2");
            comboBox1.Items.Add("Lista 3");
            comboBox1.Items.Add("Lista 4");
            comboBox1.SelectedIndex = 0;

            comboBox2.Items.Add("Lista 1");
            comboBox2.Items.Add("Lista 2");
            comboBox2.Items.Add("Lista 3");
            comboBox2.Items.Add("Lista 4");
            comboBox2.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool esXValido = int.TryParse(textBox1.Text, out int x);
            bool esYValido = int.TryParse(textBox2.Text, out int y);

            if (!esXValido || !esYValido)
            {
                MessageBox.Show("Ingresa un número válido para X y Y");
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Focus();
                return;
            }
            else
            {
                Punto nuevoPunto = new Punto(x, y);
                switch (comboBox1.SelectedIndex)
                {
                    case 0: lista1.agregarPunto(nuevoPunto); break;
                    case 1: lista2.agregarPunto(nuevoPunto); break;
                    case 2: lista3.agregarPunto(nuevoPunto); break;
                    case 3: lista4.agregarPunto(nuevoPunto); break;
                }
                MessageBox.Show($"Punto ({x},{y}) agregado a {comboBox1.SelectedItem}");
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                Pen ejePen = new Pen(Color.Gray, 1);
                int centroX = pictureBox1.Width / 2;
                int centroY = pictureBox1.Height / 2;
                int escala = 10;
                int ticksize = 4;
                g.DrawLine(ejePen, 0, centroY, pictureBox1.Width, centroY); //eje x
                g.DrawLine(ejePen, centroX, 0, centroX, pictureBox1.Height); //eje y

                for(int x = centroX; x < pictureBox1.Width; x += escala)
                    g.DrawLine(ejePen, x, centroY - ticksize, x, centroY + ticksize);

                for (int x = centroX; x > 0; x -= escala)
                    g.DrawLine(ejePen, x, centroY - ticksize, x, centroY + ticksize);

                for (int y = centroY; y < pictureBox1.Height; y += escala)
                    g.DrawLine(ejePen, centroX - ticksize, y, centroX + ticksize, y);

                for (int y = centroY; y > 0; y -= escala)
                    g.DrawLine(ejePen, centroX - ticksize, y, centroX + ticksize, y);

                List<Punto> puntos = new List<Punto>();
                switch (comboBox2.SelectedIndex)
                {
                    case 0: puntos = lista1.obtenerPuntos(); break;
                    case 1: puntos = lista2.obtenerPuntos(); break;
                    case 2: puntos = lista3.obtenerPuntos(); break;
                    case 3: puntos = lista4.obtenerPuntos(); break;
                }

                foreach (var punto in puntos)
                {
                    int x = centroX + punto.X * escala;
                    int y = centroY - punto.Y * escala;
                    g.FillEllipse(Brushes.Red, x - 3, y - 3, 6, 6);
                }
            }
            pictureBox1.Image = bmp;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Punto> puntos1 = null;

            switch (comboBox2.SelectedIndex)
            {
                case 0: puntos1 = lista1.obtenerPuntos(); break;
                case 1: puntos1 = lista2.obtenerPuntos(); break;
                case 2: puntos1 = lista3.obtenerPuntos(); break;
                case 3: puntos1 = lista4.obtenerPuntos(); break;
            }

            if (puntos1 != null && puntos1.Count >= 4)
            {
                string resultado = Geometria.Analizar(puntos1);
                MessageBox.Show(resultado, "Analisis de figuras");
            }
            else
            {
                MessageBox.Show("Se necesitan al menos 4 puntos para analizar");
            }
        }
    }
}
