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
            //Thread.Sleep(200); // 暫停 0.2 秒

            // 提示用戶輸入一個 double 變數
            double BottomPriceM = GetBottomPrice();
            //Thread.Sleep(100); // 暫停 0.1 秒

            // 提示用戶輸入一個 bool 變數
            bool ModOpItme = ModOverPower("不要更改OverPower的食物價格？(yes/no); Do NOT modify Over Power Items/Food(yes/no): ");
            //Thread.Sleep(700); // 暫停 0.7 秒
            // 提示用戶輸入兩個 bool 變數
            (bool,bool) RNDandNegPriceM = RNDandNegPrice("Randomize Min FoodPrice & Not Modify FoodPrice < 0. Enter:'1','0','00','01','10','11': ");//(max:16*MinPrice))
            //Thread.Sleep(700); // 暫停 0.7 秒

            Console.WriteLine($"Remember to backup any of your file. Or it will be very funny after Meow Meow CountDown for {PriceMulti * 8} sec");
            int MeowCountDown = (int)(PriceMulti * 8000);
            Thread.Sleep(MeowCountDown); // 暫停 PriceMulti*8 秒

            ModifyLPSFiles(rootDirectory, PriceMulti, ModOpItme, BottomPriceM, RNDandNegPriceM.Item1, RNDandNegPriceM.Item2);
            Console.WriteLine("操作完成。 Operation completed.");

            // 等待用户按下 ESC 键关闭程序
            WaitForExit();
        }
        static double GetPriceMulti()
        {
            double additionalPrice = 0.0;
            bool validInput = false;

            while (!validInput)
            {
                Console.Write("請輸入食物物品價格的倍率(官方推薦:1~0.7); Enter the Price Mutipling Value (Official recommend: 1~0.7)：");
                string input = Console.ReadLine();

                if (double.TryParse(input, out additionalPrice))
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("請輸入有效的數字。 Please enter a valid number.");
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
                Console.Write("請輸入食物物品最低價格，輸入的值>0(自動買東西吃:輸入的值必須>2); \nEnter Minimum food Price > 0 (Auto buy food require Price>2 )：");
                string input = Console.ReadLine();

                if (double.TryParse(input, out BottomPriceI))
                {
                    validInput = true;
                    //BottomPrice= Math.Abs(BottomPriceI);
                    BottomPrice = BottomPriceI;
                }
                else
                {
                    Console.WriteLine("請輸入有效的數字。 Please enter a valid number");
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

                if (input == "yes" || input == "y" || input == "true" || input == "t" || input == "1" || input == "tr")
                {
                    result = true;
                    validInput = true;
                }
                else if (input == "no" || input == "n" || input == "false" || input == "f" || input == "0" || input == "fa")
                {
                    result = false;
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("請輸入 yes 或 no。 Please enter 'yes' or 'no'");
                }
            }

            bool resultb = !result;
            return resultb;
        }
        static (bool,bool) RNDandNegPrice(string prompt)
        {
            bool validInput = false;
            bool result1 = false;
            bool result2 = false;

            while (!validInput)
            {
                Console.Write(prompt);
                string input = Console.ReadLine().Trim().ToLower();

                if (input == "nono" || input == "nn" || input == "falsefalse" || input == "00" || input == "ff" || input == "0")
                {
                    result1 = false;
                    result2 = false;
                    validInput = true;
                }
                else if (input == "noyes" || input == "ny" || input == "falsetrue" || input == "ft" || input == "01")
                {
                    result1 = false;
                    result2 = true;
                    validInput = true;
                }                
                else if (input == "yesyes" || input == "yy" || input == "truetrue" || input == "tt" || input == "11" || input == "1")
                {
                    result1 = true;
                    result2 = true;
                    validInput = true;
                }                
                else if (input == "yesno" || input == "yn" || input == "truefalse" || input == "ft" || input == "10")
                {
                    result1 = true;
                    result2 = false;
                    validInput = true;
                }                
                else
                {
                    Console.WriteLine("請輸入 0,1,00,01,10,11。 Please enter '0','1','00','01','10','11'");
                }
            }

            return (result1,result2);
        }

        static void ModifyLPSFiles(string directory, double PriceMultiT, bool ModOpItmeT, double BottomPriceT, bool RNDdiceT, bool NoMoNegPriceT)
        {
            try
            {
                string[] files = Directory.GetFiles(directory, "*.lps", SearchOption.AllDirectories);
                int Counter = 5;

                foreach (string file in files)
                {
                    if (Counter > 0)
                    {
                        Console.WriteLine($"BackUp Your File. CountDown {(Counter) * 0.8} sec \n BackUp Your File. CountDown {(Counter) * 0.8} sec \n BackUp Your File. CountDown {(Counter) * 0.8} sec \n BackUp Your File. CountDown {(Counter) * 0.8} sec \n BackUp Your File. CountDown {(Counter) * 0.8} sec \n BackUp Your File. CountDown {(Counter) * 0.8} sec \n BackUp Your File. CountDown {(Counter) * 0.8} sec \n");
                        Thread.Sleep(Counter * 850); // 在每次處理後暫停 Counter * 0.8 秒
                        Counter -= 1;
                    }
                    ModifyFileContent(file, PriceMultiT, ModOpItmeT, BottomPriceT, RNDdiceT, NoMoNegPriceT);
                    Thread.Sleep(70); // 在每次處理後暫停 0.7 秒
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"無法訪問目錄; Cannot access directory: {directory}");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"目錄不存在; Directory does not exist.: {directory}");
            }
        }

        static void ModifyFileContent(string filePath, double PriceMultiIn, bool ModOpItmes, double BottomPrice, bool RNDdice, bool NoMoNegPrice)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                // 使用正则表达式找到并替换价格数值和 Exp 数值
                Regex regexLine = new Regex(@"(?i)(\|price#)(-?\d+(\.\d+)?)|(\|Exp#)(-?\d+(\.\d+)?)|(\|Strength#)(-?\d+(\.\d+)?)|(\|StrengthFood#)(-?\d+(\.\d+)?)|(\|StrengthDrink#)(-?\d+(\.\d+)?)|(\|Feeling#)(-?\d+(\.\d+)?)|(\|Health#)(-?\d+(\.\d+)?)|(\|Likability#)(-?\d+(\.\d+)?)|(\|name#)(.*?)(:)");

                for (int i = 0; i < lines.Length; i++)
                {
                    double originalExp = 0;
                    double originalStrength = 0;
                    double originalStrengthFood = 0;
                    double originalStrengthDrink = 0;
                    double originalFeeling = 0;
                    double originalHealth = 0;
                    double originalLikability = 0;
                    double originalPrice = 0;
                    string originalname = null;
                    bool CatchName = false;
                    bool CatchPrice = false;

                    string line = lines[i];
                    MatchCollection matches = regexLine.Matches(line);
                    foreach (Match match in matches)
                    {
                        if (match.Groups[4].Success) // 如果匹配到了 Exp，提取其值
                        {
                            double expValue;
                            if (double.TryParse(match.Groups[5].Value, out expValue))
                            {
                                originalExp = expValue;
                                Console.WriteLine($" EXP 值為: {originalExp}");

                            }
                            else
                            {
                                Console.WriteLine($"EXP 值無法解析; EXP value error: {match.Value}");

                            }
                        }
                        if (match.Groups[7].Success)
                        {
                            double StrengthValue;
                            if (double.TryParse(match.Groups[8].Value, out StrengthValue))
                            {
                                originalStrength = StrengthValue;

                            }
                            else
                            {
                                Console.WriteLine($"Strength 值無法解析; StrengthFood value error: {match.Value}");

                            }
                        }
                        if (match.Groups[10].Success)
                        {
                            double StrengthFoodValue;
                            if (double.TryParse(match.Groups[11].Value, out StrengthFoodValue))
                            {
                                originalStrengthFood = StrengthFoodValue;

                            }
                            else
                            {
                                Console.WriteLine($"StrengthFood 值無法解析; StrengthFood value error: {match.Value}");

                            }
                        }
                        if (match.Groups[13].Success)
                        {
                            double StrengthDrinkValue;
                            if (double.TryParse(match.Groups[14].Value, out StrengthDrinkValue))
                            {
                                originalStrengthDrink = StrengthDrinkValue;

                            }
                            else
                            {
                                Console.WriteLine($"StrengthDrink 值無法解析; StrengthDrink value error: {match.Value}");

                            }
                        }
                        if (match.Groups[16].Success)
                        {
                            double FeelingValue;
                            if (double.TryParse(match.Groups[17].Value, out FeelingValue))
                            {
                                originalFeeling = FeelingValue;

                            }
                            else
                            {
                                Console.WriteLine($"Feeling 值無法解析; Feeling value error: {match.Value}");

                            }
                        }
                        if (match.Groups[19].Success)
                        {
                            double HealthValue;
                            if (double.TryParse(match.Groups[20].Value, out HealthValue))
                            {
                                originalHealth = HealthValue;

                            }
                            else
                            {
                                Console.WriteLine($"Health 值無法解析; Health value error: {match.Value}");

                            }
                        }
                        if (match.Groups[22].Success)
                        {
                            double LikabilityValue;
                            if (double.TryParse(match.Groups[23].Value, out LikabilityValue))
                            {
                                originalLikability = LikabilityValue;

                            }
                            else
                            {
                                Console.WriteLine($"Health 值無法解析; Heath value error: {match.Value}");

                            }
                        }
                        if (match.Groups[25].Success)
                        {
                            originalname = match.Groups[26].Value;
                            CatchName = true;

                        }
                        if (match.Groups[1].Success)
                        {
                            double PriceValue;
                            if (double.TryParse(match.Groups[2].Value, out PriceValue))
                            {

                                originalPrice = PriceValue;
                                CatchPrice = true;
                            }
                            else
                            {
                                Console.WriteLine($"Price 值無法解析; Price formation cannot been recognized: {match.Value}");
                            }
                        }
                    }
                    //else
                    //{
                    //    Console.WriteLine($"Value formation cannot been recognized: {match.Value}");
                    //}

                    ////////////////////////////Calculate and write///////////////////////////////////

                    if (CatchName && CatchPrice)
                    {

                            double RealPrice = (originalExp / 3 + originalStrength / 5 + originalStrengthFood / 3 + originalStrengthDrink / 2 + originalFeeling / 6) / 3
                                + originalHealth + originalLikability * 10;
                            double newPrice;
                            Random rnd = new Random();
                            // 產生一個隨機數，範圍在 1 到 20 之間
                            int Dice20 = rnd.Next(1, 21);

                            if (((RealPrice - 10) * PriceMultiIn) <= (0.8 * Dice20 * BottomPrice) && (!(originalPrice < ((RealPrice - 10) * 0.7)) || ModOpItmes)
                                && (originalPrice > 0 || !NoMoNegPrice))
                            {
                                newPrice = 0.8 * Dice20 * BottomPrice;
                            }
                            else if (((RealPrice - 10) * PriceMultiIn) > (0.8 * Dice20 * BottomPrice) && (originalPrice >= (BottomPrice + 0.4))
                                && (!(originalPrice < ((RealPrice - 10) * 0.7)) || ModOpItmes))
                            {
                                newPrice = (RealPrice - 10) * PriceMultiIn + 0.8 * Dice20 * BottomPrice;
                            }
                            else
                            {
                                newPrice = originalPrice;
                            }

                            Console.WriteLine($"Name:{originalname}, PriceMod:{originalPrice}$->{newPrice:F1}$" +
                                $" \nOriginalPrice:{originalPrice},Exp:{originalExp},Strength:{originalStrength},Food:{originalStrengthFood}" +
                                $",Drink:{originalStrengthDrink},Feeling:{originalFeeling},Health:{originalHealth},Likability:{originalLikability}");

                        //Regex regex = new Regex(@"\|price#(-?\d+(\.\d+)?)");
                        // 寫入修改後的內容
                        line = regexLine.Replace(line, m => m.Groups[1].Success ? $"|price#{newPrice:F1}" : m.Value);
                        // 将修改后的行写回文件对应的行
                        lines[i] = line;
                     }
                     else
                      {
                            Console.WriteLine($"No Price or Name in this line"); //: {match.Value}
                      }
                    CatchPrice = false;
                    CatchName = false;

                }
                // 将修改后的所有行写回文件
                File.WriteAllLines(filePath, lines);

                Console.WriteLine($"已修改檔案; File has been modified.: {filePath}");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"找不到檔案; File not found.: {filePath}");
            }
            catch (IOException)
            {
                Console.WriteLine($"無法讀寫檔案; Cannot read or write file.: {filePath}");
            }
            catch (FormatException)
            {
                Console.WriteLine($"價格或 Exp 等格式錯誤; Format error for Price, Exp, etcs...: {filePath}");
            }
        }
        static void WaitForExit()
        {
            Console.WriteLine("Program has finished running.Press ESC key to continue...(Escape key);程式執行結束，請按下ESC按鈕以繼續");

            // 等待用户按下 ESC 键
            while (true)
            {
                Thread.Sleep(400);
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(intercept: true);
                    if (key.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                    Thread.Sleep(120000);
                    break;
                }
            }

            Console.WriteLine("Program has been closed. 程式已關閉。");
        }
    }
}
