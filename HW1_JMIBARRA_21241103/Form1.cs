using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using kXtensions;

namespace HW1_JMIBARRA_21241103
{
    public enum A2R_Values
    {
        I = 1,
        V = 5,
        X = 10,
        L = 50,
        C = 100,
        D = 500,
        M = 1000
    }

    public partial class Form1 : Form
    {
        private const int EM_SETCUEBANNER = 0x1501;
        private static ArrayList AL = new ArrayList();

        public Form1()
        {
            InitializeComponent();

            //Init Values cmb
            cmbFrom.Items.Add("Arabic");
            cmbFrom.Items.Add("Roman");
            cmbTo.Items.Add("Arabic");
            cmbTo.Items.Add("Roman");
            
            SendMessage(txtInput.Handle, EM_SETCUEBANNER, 0, "Input here...");
            txtAns.Text = "Wait for Answer...";
            cmbFrom.Text = "Arabic";
            cmbTo.Text = "Roman";
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam,
            [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        public static Boolean Validate_Arabic(string from)
        {
            if (!from.IsNumber())
            {
                MessageBox.Show("ERROR: ONLY NUMBERS", "Syntax Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return false;
            }

            return true;
        }

        public static bool Validate_Range(string number)
        {
            int x= Int32.Parse(number);
            if (!Enumerable.Range(1, 3999).Contains(x))
            {
                MessageBox.Show("ERROR: Range(1 to 3999)","Range Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
                return false;
            }
            return true;
        }

        public static bool Validate_Romanic(string nomeclature)
        {
            const string romanPattern = @"^M{0,4}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$";

            return Regex.Match(nomeclature, romanPattern).Success;

        }

        public static int Positional_Value(int pos)
        {
            int value=-1;  
            switch (pos)
            {
                case 4:
                    value = 1000;
                    break;
                case 3:
                    value = 100;
                    break;
                case 2:
                    value = 10;
                    break;
                case 1:
                    value = 1;
                    break;
            }
            return value;
        }

        public static string Convert_A2R(string input)
        {
            var x = input.Length;
            ArrayList answer = new ArrayList();
            for (var i = 0; i < input.Length; i++)
                AL.Add(input.ElementAt(i));
            
            foreach (var value in AL)
            {
               answer.Add(Convert_A2R(Int32.Parse(value.ToString()),Positional_Value(x)));
                x--;
            }
            AL.Clear();
            return String.Join(String.Empty, answer.ToArray()); ;
        }

        public static string Convert_A2R(int value, int posValue)
        {
            string nomeclature="";
            switch (posValue)
            {
                case 1:
                    switch (value)
                    {
                        case 1: nomeclature = A2R_Values.I.ToString();
                            break;
                        case 2: nomeclature = A2R_Values.I + A2R_Values.I.ToString();
                            break;
                        case 3: nomeclature = A2R_Values.I.ToString()+A2R_Values.I+A2R_Values.I;
                            break;
                        case 4: nomeclature = A2R_Values.I+A2R_Values.V.ToString();
                            break;
                        case 5: nomeclature = A2R_Values.V.ToString();
                            break;
                        case 6: nomeclature = A2R_Values.V + A2R_Values.I.ToString();
                            break;
                        case 7: nomeclature = A2R_Values.V + A2R_Values.I.ToString()+ A2R_Values.I;
                            break;
                        case 8: nomeclature = A2R_Values.V + A2R_Values.I.ToString() + A2R_Values.I+ A2R_Values.I;
                            break;
                        case 9: nomeclature = A2R_Values.I + A2R_Values.X.ToString();
                            break;
                    }
                    break;
                case 10:
                    switch (value)
                    {
                        case 1: nomeclature = A2R_Values.X.ToString();
                            break;
                        case 2: nomeclature = A2R_Values.X + A2R_Values.X.ToString();
                            break;
                        case 3: nomeclature = A2R_Values.X.ToString() + A2R_Values.X + A2R_Values.X;
                            break;
                        case 4: nomeclature = A2R_Values.X.ToString() + A2R_Values.L;
                            break;
                        case 5: nomeclature = A2R_Values.L.ToString();
                            break;
                        case 6: nomeclature = A2R_Values.L + A2R_Values.X.ToString();
                            break;
                        case 7: nomeclature = A2R_Values.L + A2R_Values.X.ToString() + A2R_Values.X;
                            break;
                        case 8: nomeclature = A2R_Values.L + A2R_Values.X.ToString() + A2R_Values.X + A2R_Values.X;
                            break;
                        case 9: nomeclature = A2R_Values.X + A2R_Values.C.ToString();
                            break;
                    }
                    break;
                case 100:
                    switch (value)
                    {
                        case 1: nomeclature = A2R_Values.C.ToString();
                            break;
                        case 2: nomeclature = A2R_Values.C + A2R_Values.C.ToString();
                            break;
                        case 3: nomeclature = A2R_Values.C.ToString() + A2R_Values.C + A2R_Values.C;
                            break;
                        case 4: nomeclature = A2R_Values.C + A2R_Values.D.ToString();
                            break;
                        case 5: nomeclature = A2R_Values.D.ToString();
                            break;
                        case 6: nomeclature = A2R_Values.D + A2R_Values.C.ToString();
                            break;
                        case 7: nomeclature = A2R_Values.D + A2R_Values.C.ToString() + A2R_Values.C;
                            break;
                        case 8: nomeclature = A2R_Values.D + A2R_Values.C.ToString() + A2R_Values.C + A2R_Values.C;
                            break;
                        case 9: nomeclature = A2R_Values.C + A2R_Values.M.ToString();
                            break;
                    }
                    break;
                case 1000:
                    switch (value)
                    {
                        case 1: nomeclature = A2R_Values.M.ToString();
                            break;
                        case 2: nomeclature = A2R_Values.M + A2R_Values.M.ToString();
                            break;
                        case 3: nomeclature = A2R_Values.M + A2R_Values.M.ToString()+A2R_Values.M;
                            break;
                    }
                    break;
            }
            return nomeclature;
        }
        
        static ArrayList values = new ArrayList();
        static ArrayList valuesCorrect = new ArrayList();
        static readonly Dictionary<char, int> RomanNomInts = new Dictionary<char, int>()
	    {
	        {'I', 1},
	        {'V', 5},
	        {'X', 10},
	        {'L', 50},
            {'C', 100},
	        {'D', 500},
	        {'M', 1000},
	    };
        public static void Convert_R2A(string input)
        {
            input = input.ToUpper();
            for (int i = 0; i < input.Length; i++)
            {
                if (RomanNomInts.ContainsKey(input[i]))
                {
                    int value = RomanNomInts[input[i]];
                    values.Add(value);
                }
            }
        }

        public static void Convert_R2A()
        {
            for (int i = 0; i < values.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (Int32.Parse(values[j].ToString()) < Int32.Parse(values[j + 1].ToString()))
                    {
                        valuesCorrect.Add(Int32.Parse(values[j + 1].ToString()) - Int32.Parse(values[j].ToString()));
                    }
                    else
                    {
                        valuesCorrect.Add(Int32.Parse(values[i].ToString()));
                    }


                }
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            Boolean IsNum=false;
            Boolean IsRanged=false;
            Boolean IsRomanic = false;
            //validate Arabic number
            if (!cmbFrom.Text.Equals("") && !cmbTo.Text.Equals("")) { 
                if (cmbFrom.Text.Equals("Arabic"))
                {
                    IsNum=Validate_Arabic(txtInput.Text);
                    if (IsNum == true)
                        IsRanged=Validate_Range(txtInput.Text);

                }
            //end validate Arabic Number

            //validate Roman nomeclature
                if (cmbFrom.Text.Equals("Roman")) { 
                    if (Validate_Romanic(txtInput.Text.ToUpper()))
                        IsRomanic = true;
                
                    if(!IsRomanic)
                        MessageBox.Show("ERROR: Not a Valid Roman Number", "Syntax Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    else
                    {
                        Convert_R2A(txtInput.Text);
                        //Convert_R2A();
                    }
                }
           //end validate Roman Nomeclature

            }

            if (cmbFrom.Text.Equals("Arabic") && cmbTo.Text.Equals("Roman")) { 

                if (IsRanged)
                   txtAns.Text = Convert_A2R(txtInput.Text);

            }else if (cmbFrom.Text.Equals(cmbTo.Text)&&IsNum){

                txtAns.Text = txtInput.Text;

            }
            else if (cmbFrom.Text.Equals(cmbTo.Text)&&IsRomanic)
            {
                txtAns.Text = txtInput.Text.ToUpper();
            }

        }

    }
}