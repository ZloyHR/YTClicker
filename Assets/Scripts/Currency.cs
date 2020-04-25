using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine;

public struct Currency
{
    public static readonly Currency zero = new Currency(0);

    public static string[] getTypeChar = new string[]
    {
        "","K","M","B","C"
    };

    public static CultureInfo cultureInfo = new CultureInfo("en-US");
    
    public static CurrencyTypes[] order = new[]
    {
        CurrencyTypes.Ones,
        CurrencyTypes.Thousands,
        CurrencyTypes.Millions,
        CurrencyTypes.Billions,
        CurrencyTypes.Customs
    };
    public float money;
    [SerializeField]private int orderId;

    public Currency(float money = 0) : this()
    {
        this.money = money;
        ConvertTo();
    }

    public Currency(string unformated)
    {
        string money = "", type = "";
        for (int i = 0; i < unformated.Length; ++i)
        {
            if (Char.IsLetter(unformated[i]))
            {
                unformated = unformated.Remove(0, i);
                type = unformated;
                break;
            }
            else
            {
                money += unformated[i];
            }
        }
        if (money.Length == 0) this.money = 0;
        else
            this.money = float.Parse(money,cultureInfo);
        for (int i = 1; i < getTypeChar.Length; ++i)
        {
            if (type == getTypeChar[i])
            {
                orderId = i;
                return;
            }
        }
        orderId = 0;
    }
    
    public void ConvertTo(int id = -1)
    {
        if (id >= 0)
        {
            while (orderId != id)
            {
                if (orderId > id)
                {
                    orderId--;
                    money *= 1000;
                }
                else
                {
                    orderId++;
                    money /= 1000;
                }
            }
        }
        else
        {
            while (money >= 1000)
            {
                money /= 1000;
                ++orderId;
            }

            if (money == 0 || orderId == 0) { orderId = 0;return;}
            while (money < 1)
            {
                money *= 1000;
                --orderId;
            }
        }
    }

    public override string ToString()
    {
        ConvertTo();
        if (orderId == 0)
        {
            return ((int)money) + getTypeChar[orderId];
        }
        else
        {
            return (String.Format(cultureInfo,"{0:0.#}",Mathf.Floor(money * 10) / 10f)) + getTypeChar[orderId];
        }
    }

    public static void FormatByMax(ref Currency a, ref Currency b)
    {
        a.ConvertTo();b.ConvertTo();
        int maxId = Math.Max(a.orderId, b.orderId);
        a.ConvertTo(maxId);b.ConvertTo(maxId);
    }
    
    public static Currency operator +(Currency a,Currency b)
    {
        FormatByMax(ref a,ref b);
        a.money += b.money;
        a.ConvertTo();
        return a;
    }

    public static Currency operator -(Currency a, Currency b)
    {
        FormatByMax(ref a,ref b);
        a.money -= b.money;
        a.ConvertTo();
        return a;
    }
    
    public static Currency operator *(Currency a, Currency b)
    {
        FormatByMax(ref a,ref b);
        a.money *= b.money;
        a.orderId += b.orderId;
        a.ConvertTo();
        return a;
    }
    
    public static Currency operator *(Currency a, float b)
    {
        a.money *= b;
        a.ConvertTo();
        return a;
    }
    
    public static bool operator <(Currency a, Currency b)
    {
        FormatByMax(ref a,ref b);
        return a.money < b.money;
    }

    public static bool operator >(Currency a, Currency b)
    {
        return b < a;
    }
    
    public static bool operator ==(Currency a, Currency b)
    {
        FormatByMax(ref a,ref b);
        return Math.Abs(a.money - b.money) < 1e-12f;
    }

    public static bool operator !=(Currency a, Currency b)
    {
        return !(a == b);
    }
    
    public static bool operator <=(Currency a, Currency b)
    {
        return (a < b) || (a == b);
    }

    public static bool operator >=(Currency a, Currency b)
    {
        return (a > b) || (a == b);
    }
}

public enum CurrencyTypes
{
    Ones,
    Thousands,
    Millions,
    Billions,
    Customs
}
