namespace FirstSolution.DesktopApp;
public partial class Login : Form
{
    public Login()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if(textBox1.Text == "admin" && textBox2.Text == "1")
        {
            Form1 form1 = new();
            form1.Show();
        }
        else
        {
            MessageBox.Show("Kullanıcı adı ya da şifre yanlış","Hata!",MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
