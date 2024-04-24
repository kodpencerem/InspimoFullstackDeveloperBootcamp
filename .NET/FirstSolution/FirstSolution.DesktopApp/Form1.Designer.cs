namespace FirstSolution.DesktopApp;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        button1 = new Button();
        button2 = new Button();
        listBox1 = new ListBox();
        labelAmount = new Label();
        dG1 = new DataGridView();
        Index = new DataGridViewTextBoxColumn();
        Product = new DataGridViewTextBoxColumn();
        Price = new DataGridViewTextBoxColumn();
        ((System.ComponentModel.ISupportInitialize)dG1).BeginInit();
        SuspendLayout();
        // 
        // button1
        // 
        button1.Font = new Font("Segoe UI", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 162);
        button1.Image = (Image)resources.GetObject("button1.Image");
        button1.ImageAlign = ContentAlignment.TopCenter;
        button1.Location = new Point(34, 31);
        button1.Margin = new Padding(4);
        button1.Name = "button1";
        button1.Size = new Size(275, 263);
        button1.TabIndex = 0;
        button1.Text = "Meyve";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // button2
        // 
        button2.Font = new Font("Segoe UI", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 162);
        button2.Image = (Image)resources.GetObject("button2.Image");
        button2.Location = new Point(332, 31);
        button2.Margin = new Padding(4);
        button2.Name = "button2";
        button2.Size = new Size(275, 263);
        button2.TabIndex = 1;
        button2.Text = "Sebze";
        button2.UseVisualStyleBackColor = true;
        button2.Click += button2_Click;
        // 
        // listBox1
        // 
        listBox1.FormattingEnabled = true;
        listBox1.ItemHeight = 21;
        listBox1.Location = new Point(650, 31);
        listBox1.Name = "listBox1";
        listBox1.Size = new Size(199, 403);
        listBox1.TabIndex = 2;
        listBox1.MouseDoubleClick += listBox1_MouseDoubleClick;
        // 
        // labelAmount
        // 
        labelAmount.Font = new Font("Segoe UI", 36F, FontStyle.Regular, GraphicsUnit.Point, 162);
        labelAmount.Location = new Point(650, 447);
        labelAmount.Name = "labelAmount";
        labelAmount.Size = new Size(199, 73);
        labelAmount.TabIndex = 3;
        labelAmount.Text = "30.000";
        labelAmount.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // dG1
        // 
        dG1.AllowUserToAddRows = false;
        dG1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dG1.Columns.AddRange(new DataGridViewColumn[] { Index, Product, Price });
        dG1.EditMode = DataGridViewEditMode.EditProgrammatically;
        dG1.Location = new Point(34, 311);
        dG1.Name = "dG1";
        dG1.RowHeadersVisible = false;
        dG1.Size = new Size(573, 223);
        dG1.TabIndex = 4;
        // 
        // Index
        // 
        Index.HeaderText = "#";
        Index.Name = "Index";
        // 
        // Product
        // 
        Product.HeaderText = "Ürün";
        Product.Name = "Product";
        // 
        // Price
        // 
        Price.HeaderText = "Ücret";
        Price.Name = "Price";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(9F, 21F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(885, 546);
        Controls.Add(dG1);
        Controls.Add(labelAmount);
        Controls.Add(listBox1);
        Controls.Add(button1);
        Controls.Add(button2);
        Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
        Margin = new Padding(4);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Form1";
        ((System.ComponentModel.ISupportInitialize)dG1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Button button1;
    private Button button2;
    private ListBox listBox1;
    private Label labelAmount;
    private DataGridView dG1;
    private DataGridViewTextBoxColumn Index;
    private DataGridViewTextBoxColumn Product;
    private DataGridViewTextBoxColumn Price;
}
