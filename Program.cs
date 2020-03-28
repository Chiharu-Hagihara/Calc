using System;

namespace Calc
{
    class ProgramClass
    {
        //入力された文字はなんでもここに入れる
        private static string i = null;
        
        //iが数字だったらここに入れる
        private static string input = null;
        
        //計算結果を入れる
        //double型だと割り算の時に誤差が出るのでdecimal型
        static decimal result = 0;

        //iが四則演算子と＝だったらこれに入れる
        static string ope = null;

        static void Main(string[] args)
        {
            Console.WriteLine("電卓です。以下のどれかを項目ごとに順に入力してください。\r\n" +
                              "数字、四則演算子、数字、......＝の順に入力すると計算できます。\r\n" +
                              "四則演算子：+　-　*　/　\r\n" +
                              "数字：いわゆる数字です。数字を連続で入力すると上書きできます\r\n" +
                              "=：押すと計算結果が出ます。また、それまでの入力は初期化されます。\r\n" +
                              "AC：初期化されます。\r\n" +
                              "END：終了します。");
            
            
        //延々ループさせ入力待ち状態にする
            //whileの条件、他にいいのないのかな？今回はこれで。
            while(i!="END"){
                Console.WriteLine("入力してください");
                i = Console.ReadLine();
                decimal d;

                if(decimal.TryParse(i, out d)){
                    //iがdecimalに変換できるか（つまり数字かどうか）を判定
                    //数字だったらinputに入れる
                    input = i;
                }else if (ope == null && i == "="){
                    //いきなり押すとエラーがでる？ので無理やり回避。
                    //詳細がわかったらもうちょっと見栄え良く直せそう
                    Console.WriteLine("いきなり＝を押さないでね");
                }else if(i=="+"||i == "-"||i == "*"||i == "/"||i == "="){
                    //iが四則演算子または＝だったらKeisannメソッドを呼ぶ。
                    Keisann();
                }else if(i=="AC"){
                    //iがACだったらAll_Clearメソッドを呼ぶ。
                    All_Clear();
                }else if(i=="END"){
                    //iがENDだったら何もしないでもう一周する
                }else{
                    //上記のどれでもなかったら以下を表示
                    Console.WriteLine("入力が間違っているみたいです");
                }
            }
            //コンソールを終了させる
            Environment.Exit(0);  
        }

        public static void Keisann()
        {//計算するところ
            decimal num1 = result;
            decimal num2 = decimal.Parse(input);

            //opeで判定。前回の演算子か＝を判定するよ
            switch (ope)
            {
                case "+":
                    //足し算
                    result = num1 + num2;  
                    break;

                case "-":
                    //引き算
                    result = num1 - num2;
                    break;

                case "*":
                    //掛け算
                    result = num1 * num2;
                    break;

                case "/":
                    //割り算
                    if(num2!=0)
                    {//０で割ることはできないので避けます
                        result = num1 / num2;
                        
                    }else{
                        //もし0で割り算しようとしたらメッセージを出した上で初期状態にします。
                        Console.WriteLine("0で割ることができません\r\n初期状態にしました");
                        All_Clear();
                    }
                    break;

                 case null:
                    //1回目の演算子入力ではただinputをresultに入れるだけです
                    result = decimal.Parse(input);
                    break;

                default:
                    //念のため
                    break;
            }

            ope = i;
            //演算子をopeに入れる

            if(ope=="=")
            {//もしopeが＝だったら（今回＝が押されていたら）計算結果を表示
                //型変換することで不要な0を消す
                string str = result.ToString();
                double d = double.Parse(str);

                Console.WriteLine(d+"\r\n計算がおわったよ");
                All_Clear();
            }
        }
        public static void All_Clear()
        {//全部初期化って時に呼びます。
            input = null;
            result = 0;
            ope = null;
            i = null;
        }
    }
}