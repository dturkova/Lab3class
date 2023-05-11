using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
class String
{
    protected char[] data;
    protected byte length;

    // without parameters
    public String()
    {
        data = new char[0];
        length = 0;
    }

    // string as parameter
    public String(string str)
    {
        data = str.ToCharArray();
        length = (byte)data.Length;
    }

    // char as parameter
    public String(char c)
    {
        data = new char[] { c };
        length = 1;
    }

    public byte GetLength()
    {
        return length;
    }

    // clear string
    public void Clear()
    {
        data = new char[0];
        length = 0;
    }
}
class BitString : String
{
    public BitString(string str)
    {
        bool isValid = true;

        foreach (char symbol in str)
        {
            if (symbol != '0' && symbol != '1')
            {
                isValid = false;
                break;
            }
        }

        if (isValid)
        {

            data = str.ToCharArray();
            length = (byte)data.Length;
        }
        else
        {
            data = new char[1];
            length = 0;
        }
    }

    public void Invert()
    {
        // Determine the sign of the number
        bool isNegative = (data[0] == '1');

        // Invert all bits if the number is negative
        if (isNegative)
        {
            for (int i = 0; i < length; i++)
            {
                if (data[i] == '0')
                {
                    data[i] = '1';
                }
                else
                {
                    data[i] = '0';
                }
            }
            bool carry = true;
            for (int i = length - 1; i >= 0; i--)
            {
                if (data[i] == '0' && carry)
                {
                    data[i] = '1';
                    carry = false;
                }
                else if (data[i] == '1' && carry)
                {
                    data[i] = '0';
                }
            }
            data[0] = '1';
        }

        // Add 1 to the inverted number if it was negative

    }

    public BitString Sum(BitString other)
    {
        string result = "";
        int carry = 0;

        int i = this.length - 1;
        int j = other.length - 1;

        while (i >= 0 || j >= 0 || carry > 0)
        {
            int sum = carry;
            if (i >= 0) sum += this.data[i--] - '0';
            if (j >= 0) sum += other.data[j--] - '0';
            result = (sum % 2) + result;
            carry = sum / 2;
        }

        return new BitString(result);
    }

    public bool IsEqual(BitString other)
    {
        if (length != other.length)
        {
            return false;
        }

        for (int i = 0; i < length; i++)
        {
            if (data[i] != other.data[i])
            {
                return false;
            }
        }

        return true;
    }

    public override string ToString()
    {
        return new string(data);
    }
}

internal class Program
{
    static void Main(string[] args)
    {

        String str1 = new String("Hello Yevhen");
        Console.WriteLine("Length at first:");
        byte length1 = str1.GetLength();
        Console.WriteLine(length1); 

        str1.Clear();
        Console.WriteLine("Length after clear method:");
        byte length2 = str1.GetLength();
        Console.WriteLine(length2);


        BitString bitStr1 = new BitString("10100001");

        Console.WriteLine("\nBitstrings after invert method:");
        bitStr1.Invert();
        Console.WriteLine(bitStr1.ToString()); // 11011111

    
        BitString bitStr2 = new BitString("01011101");
        bitStr2.Invert();
        Console.WriteLine(bitStr2.ToString()); //01011101

        Console.WriteLine("\nBitstring Sum method:");
        BitString sum = bitStr1.Sum(bitStr2);
        Console.WriteLine(sum.ToString()); //100111100

        Console.WriteLine("\nBitstring IsEqual method:");
        BitString bitStr3 = new BitString("11011111");
        bool isEqual = bitStr1.IsEqual(bitStr3);
        Console.WriteLine(isEqual); //True
    }
}