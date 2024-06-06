using LinePutScript.Converter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;


namespace FoodFileModifier
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Meow Meow Start! Meow Meow please do not blow up my PC");
            Thread.Sleep(660); // 暫停 0.5 秒

            string rootDirectory = Directory.GetCurrentDirectory(); // 取得當前目錄

            // 提示用戶輸入一個 double 變數
            double PriceMulti = GetPriceMulti();
            Thread.Sleep(200); // 暫停 0.2 秒

            // 提示用戶輸入一個 double 變數
            double BottomPriceM = GetBottomPrice();
            Thread.Sleep(100); // 暫停 0.1 秒

            // 提示用戶輸入一個 bool 變數
            bool ModOpItme = ModOverPower("請問是否要更改OverPower的食物價格？(yes/no): ");
            Thread.Sleep(700); // 暫停 0.7 秒

            Console.WriteLine($"Remember to backup any of your file. Or it will be very funny after Meow Meow CountDown for {PriceMulti*8} sec");
            int MeowCountDown = (int)(PriceMulti * 8000);
            Thread.Sleep(MeowCountDown); // 暫停 PriceMulti*8 秒
            ModifyLPSFiles(rootDirectory, PriceMulti, ModOpItme, BottomPriceM);
            Console.WriteLine("操作完成。");

            // 等待用户按下 ESC 键关闭程序
            WaitForExit();
        }
        static double GetPriceMulti()
        {
            double additionalPrice = 0.0;
            bool validInput = false;

            while (!validInput)
            {
                Console.Write("請輸入食物物品價格的倍率(官方推薦:1~0.7)：");
                string input = Console.ReadLine();

                if (double.TryParse(input, out additionalPrice))
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("請輸入有效的數字。");
                }
            }

            return additionalPrice;
        }
        static double GetBottomPrice()
        {
            double BottomPrice = 0.0;
            double BottomPriceI = 0.0;
            bool validInput = false;

            while (!validInput)
            {
                Console.Write("請輸入食物物品最低價格，輸入的值>0(自動買東西吃:輸入的值必須>2)：");
                string input = Console.ReadLine();

                if (double.TryParse(input, out BottomPriceI))
                {
                    validInput = true;
                    //BottomPrice= Math.Abs(BottomPriceI);
                    BottomPrice= BottomPriceI;
                }
                else
                {
                    Console.WriteLine("請輸入有效的數字。");
                }
            }

            return BottomPrice;
        }

        static bool ModOverPower(string prompt)
        {
            bool validInput = false;
            bool result = false;

            while (!validInput)
            {
                Console.Write(prompt);
                string input = Console.ReadLine().Trim().ToLower();

                if (input == "yes" || input == "y" || input == "true" || input == "t" || input == "1")
                {
                    result = true;
                    validInput = true;
                }
                else if (input == "no" || input == "n" || input == "false" || input == "f" || input == "0")
                {
                    result = false;
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("請輸入 yes 或 no。");
                }
            }

            return result;
        }

        static void ModifyLPSFiles(string directory, double PriceMultiT, bool ModOpItmeT, double BottomPriceT)
        {
            try
            {
                string[] files = Directory.GetFiles(directory, "*.lps", SearchOption.AllDirectories);
                int Counter = 5;

                foreach (string file in files)
                {
                    if (Counter > 0)
                    {
                        Console.WriteLine($"BackUp Your File. CountDown {(Counter) * 0.8} sec");
                        Thread.Sleep(Counter * 850); // 在每次處理後暫停 Counter * 0.8 秒
                        Counter -= 1;
                    }
                    ModifyFileContent(file, PriceMultiT, ModOpItmeT, BottomPriceT);
                    Thread.Sleep(70); // 在每次處理後暫停 0.7 秒
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"無法訪問目錄: {directory}");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"目錄不存在: {directory}");
            }
        }

        static void ModifyFileContent(string filePath, double PriceMultiIn, bool ModOpItmes, double BottomPrice)
        {
            try
            {
                string fileContent = File.ReadAllText(filePath);

                // 使用正則表達式找到並替換價格數值和 Exp 數值

                Regex regex = new Regex(@"(?i)(\|price#)(-?\d+(\.\d+)?)|(\|Exp#)(-?\d+(\.\d+)?)(.*?)|(\|Strength#)(-?\d+(\.\d+)?)(.*?)|(\|StrengthFood#)(-?\d+(\.\d+)?)(.*?)|(\|StrengthDrink#)(-?\d+(\.\d+)?)(.*?)|(\|Feeling#)(-?\d+(\.\d+)?)(.*?)|(\|Health#)(-?\d+(\.\d+)?)(.*?)|(\|Likability#)(-?\d+(\.\d+)?)");
                MatchCollection matches = regex.Matches(fileContent);
                // 如果匹配到了 Exp，提取其值
                double originalExp = 0;
                double originalStrength = 0;
                double originalStrengthFood = 0;
                double originalStrengthDrink = 0;
                double originalFeeling = 0;
                double originalHealth = 0;
                double originalLikability = 0;

                // 替換價格數值
                string modifiedContent = regex.Replace(fileContent, match =>
                {
                    if (match.Groups[4].Success) // 如果匹配到了 Exp，提取其值
                    {
                        double expValue;
                        if (double.TryParse(match.Groups[5].Value, out expValue))
                        {
                            originalExp = expValue;
                            return match.Value; // 不做任何替換
                        }
                        else
                        {
                            Console.WriteLine($"EXP 值無法解析: {match.Value}");
                            return match.Value; // 不做任何替換
                        }
                    }
                    else if (match.Groups[7].Success)
                    {
                        double StrengthValue;
                        if (double.TryParse(match.Groups[8].Value, out StrengthValue))
                        {
                            originalStrength = StrengthValue;
                            return match.Value; // 不做任何替換
                        }
                        else
                        {
                            Console.WriteLine($"Strength 值無法解析: {match.Value}");
                            return match.Value; // 不做任何替換
                        }
                    }
                    else if (match.Groups[10].Success)
                    {
                        double StrengthFoodValue;
                        if (double.TryParse(match.Groups[11].Value, out StrengthFoodValue))
                        {
                            originalStrengthFood = StrengthFoodValue;
                            return match.Value; // 不做任何替換
                        }
                        else
                        {
                            Console.WriteLine($"StrengthFood 值無法解析: {match.Value}");
                            return match.Value; // 不做任何替換
                        }
                    }
                    else if (match.Groups[13].Success)
                    {
                        double StrengthDrinkValue;
                        if (double.TryParse(match.Groups[14].Value, out StrengthDrinkValue))
                        {
                            originalStrengthDrink = StrengthDrinkValue;
                            return match.Value; // 不做任何替換
                        }
                        else
                        {
                            Console.WriteLine($"StrengthDrink 值無法解析: {match.Value}");
                            return match.Value; // 不做任何替換
                        }
                    }
                    else if (match.Groups[16].Success)
                    {
                        double FeelingValue;
                        if (double.TryParse(match.Groups[17].Value, out FeelingValue))
                        {
                            originalFeeling = FeelingValue;
                            return match.Value; // 不做任何替換
                        }
                        else
                        {
                            Console.WriteLine($"Feeling 值無法解析: {match.Value}");
                            return match.Value; // 不做任何替換
                        }
                    }
                    else if (match.Groups[19].Success)
                    {
                        double HealthValue;
                        if (double.TryParse(match.Groups[20].Value, out HealthValue))
                        {
                            originalHealth = HealthValue;
                            return match.Value; // 不做任何替換
                        }
                        else
                        {
                            Console.WriteLine($"Health 值無法解析: {match.Value}");
                            return match.Value; // 不做任何替換
                        }
                    }
                    else if (match.Groups[22].Success)
                    {
                        double LikabilityValue;
                        if (double.TryParse(match.Groups[23].Value, out LikabilityValue))
                        {
                            originalLikability = LikabilityValue;
                            return match.Value; // 不做任何替換
                        }
                        else
                        {
                            Console.WriteLine($"Health 值無法解析: {match.Value}");
                            return match.Value; // 不做任何替換
                        }
                    }
                    else // 如果匹配到了 Price，替換價格
                    {
                        double PriceValue;
                        if (double.TryParse(match.Groups[2].Value, out PriceValue))
                        {
                            double originalPrice = PriceValue;
                            double RealPrice = (originalExp / 3 + originalStrength / 5 + originalStrengthFood / 3 + originalStrengthDrink / 2 + originalFeeling / 6) / 3 + originalHealth + originalLikability * 10;
                            double newPrice;
                            if (((RealPrice - 10) * PriceMultiIn) <= BottomPrice && (!(originalPrice < ((RealPrice - 10) * 0.7)) || ModOpItmes))
                            {
                                newPrice = BottomPrice;
                            }
                            else if (((RealPrice - 10) * PriceMultiIn) > BottomPrice && (((RealPrice - 10) * PriceMultiIn) <= (BottomPrice+0.4)) && (!(originalPrice < ((RealPrice - 10) * 0.7)) || ModOpItmes))
                            {
                                newPrice = (RealPrice - 10) * PriceMultiIn + 0.3;
                            }
                            else
                            {
                                newPrice = originalPrice;
                            }
                            return $"{match.Groups[1].Value}{newPrice:F1}";
                        }
                        else
                        {
                            Console.WriteLine($"Value 值無法解析: {match.Value}");
                            return match.Value; // 不做任何替換
                        }
                    }
                });
                // 寫入修改後的內容
                File.WriteAllText(filePath, modifiedContent);
                Console.WriteLine($"已修改檔案: {filePath}");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"找不到檔案: {filePath}");
            }
            catch (IOException)
            {
                Console.WriteLine($"無法讀寫檔案: {filePath}");
            }
            catch (FormatException)
            {
                Console.WriteLine($"價格或 Exp 格式錯誤: {filePath}");
            }
        }
        static void WaitForExit()
        {
            Console.WriteLine("程序已运行结束，press ESC key to continue...(Escape key)");

            // 等待用户按下 ESC 键
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(intercept: true);
                    if (key.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
            }

            Console.WriteLine("程序已关闭。");
        }
    }
}
