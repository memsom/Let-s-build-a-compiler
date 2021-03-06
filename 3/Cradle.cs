using System;
using static LBAC.C;

namespace LBAC
{
    //This class is close to te original C.
    //I'm trying to not deviate to far initally to ensure 
    //that I can reproduce the code in a way that makes
    //a transition form the C to the C#.
    //As I go on with this, I'll add in a more optimal C#
    //version. Certainly, the string manipulation is currently
    //too C like.
    public class Cradle
    {    
        const byte MAX_BUF = 100;

        public static char[] tmp = new char[MAX_BUF];
        public static char[]  token_buf  = new char[MAX_BUF];

        public static char Look;

        public static char UPCASE(char c) 
        {
            return char.ToUpper(c);
        }

        public static void GetChar() 
        {
            Look = getchar();
        }

        public static void Error(char[] s)
        {
            printf($"\nError: {s}.");
        }

        public static void Abort(char[] s)
        {
            Error(s);
            exit(1);
        }

        public static void Expected(char[] s)
        {
            tmp = S2C($"{s} Expected");
            Abort(tmp);
        }

        public static void Match(char x)
        {
            if(Look == x) 
            {
                GetChar();
            } 
            else 
            {
                tmp =  S2C($"' {x} ' ");
                Expected(tmp);
            }
        }

        public static bool IsAlpha(char c)
        {
            return (UPCASE(c) >= 'A') && (UPCASE(c) <= 'Z');
        } 

        public static bool IsDigit(char c)
        {
            return (c >= '0') && (c <= '9');
        }

        public static bool IsAlNum(char c)
        {
            return IsAlpha(c) || IsDigit(c);
        }

        public static bool IsAddop(char c)
        {
            return (c == '+') || (c == '-');
        }

        public static bool IsWhite(char c)
        {
            return (c == ' ') || (c == '\t');
        }

        public static char[] GetName()
        {
            var index = 0;
            if( !IsAlNum(Look)) 
            {
                Expected(S2C("Name"));
            }

            while (IsAlNum(Look)) 
            {
                token_buf[index] = Look;
                index++;

                GetChar();
            }

            SkipWhite();

            token_buf[index] = '\0';
            return token_buf;
        }


        public static char[] GetNum()
        {
            var index = 0;

            if( !IsAlNum(Look)) 
            {
                Expected(S2C("Integer"));
            }

            while (IsDigit(Look)) 
            {
                token_buf[index] = Look;
                index++;

                GetChar();
            }

            SkipWhite();

            token_buf[index] = '\0';
            return token_buf;
        }

        public static void SkipWhite()
        {
            while (IsWhite(Look)) {
                GetChar();
            }
        }

        public static void Emit(char[] s)
        {
            printf($"\t{C2S(s)}");
        }

        public static void EmitLn(char[] s)
        {
            Emit(s);
            printf("\n");
        }

        public static void Init()
        {
            GetChar();
        }
    }   
}