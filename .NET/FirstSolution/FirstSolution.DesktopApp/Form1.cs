namespace FirstSolution.DesktopApp;

public partial class Form1 : Form
{
    decimal amount = 0;
    int num = 0;
    public Form1()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e) //meyve
    {
        listBox1.Items.Add("Meyve");
        amount += 10;
        labelAmount.Text = "₺" + amount.ToString("#,##0.00");

        num++;

        dG1.Rows.Add();
        int count = dG1.Rows.Count;
        dG1.Rows[count - 1].Cells[0].Value = num;
        dG1.Rows[count - 1].Cells[1].Value = "Meyve";
        dG1.Rows[count - 1].Cells[2].Value = 10;
    }

    private void button2_Click(object sender, EventArgs e) //sebze
    {
        listBox1.Items.Add("Sebze");
        amount += 20;
        labelAmount.Text = "₺" + amount.ToString("#,##0.00");

        num++;
        dG1.Rows.Add();
        int count = dG1.Rows.Count;
        dG1.Rows[count - 1].Cells[0].Value = num;
        dG1.Rows[count - 1].Cells[1].Value = "Sebze";
        dG1.Rows[count - 1].Cells[2].Value = 20;
    }

    private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        if(MessageBox.Show("Kaydı silmek istiyor musun","Sil?",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
        {
            object? select = listBox1.SelectedItem;
            if (select is not null)
            {
                if ((string)select == "Meyve") //unboxing
                {
                    amount -= 10;
                    labelAmount.Text = "₺" + amount.ToString("#,##0.00");
                }
                else
                {
                    amount -= 20;
                    labelAmount.Text = "₺" + amount.ToString("#,##0.00");
                }
                listBox1.Items.Remove(select);
            }
        }
         
    }
}
