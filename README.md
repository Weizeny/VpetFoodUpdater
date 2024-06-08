My cute meow meow finally got the cheaper foods to eat now.  

  
Program with ChatGPT and a cute innocent MeowMeow.
This Mod is mainly aiming to Upgrade the Foods/gifts/items/etcs mods which is out of date.  
So tuning down the Prices of Foods is the most thing what I want to do, but truly this is only the practice for my next mod VpetWorkUpdater.  
So please sit tight, after I got this programming experience. I can make it much more faster ASAP.  
I will explain “How to do” and “What to do” below.  

1.How to use it?  
  Find the "FoodPriceCompile.exe" file location and drag your mod under it then double click on FoodPriceCompile.exe to proceed your mod data.  
!!WARNING!!  
  【DO NOT drag】 "VpetFoodUpdater file" or "FoodPriceCompile.exe" into any 【"System File folder"】,【"..\Steam\steamapps\workshop\content\1920960"】  
  【DO BACKUP】 your Food/Item/ANY MOD.  
  【DO drag】 your Food/Item MOD 【Under】FoodPriceCompile.exe file Location. "..\FoodPriceCompile\bin\Debug\net8.0"  
  (Surely you can drag the whole MOD folder into the folder where FoodPriceCompile.exe is, but please always backup your file before doing it)
  
2.What functions does this Mod have?  
  
	(1) Enter the Price Multiplier value. Official suggest is from 0.7 to 1. So the Price will set become: (OfficailDefinePrice+10)x PriceMultiplier  
If the PriceMultiplier is below 0.7, then the Food Price will become OP/overload/overpower/too low.  
  
	(2) Enter the Minimum Price value. Then the Price come to the output will always greater than Minimum Price, but also Price> BottomPrice(aka MinPrice)>0.  
Food Price =   
(((Exp / 3 + Strength / 5 + StrengthFood / 2 + StrengthDrink / 2 + Feeling / 5) / 3)-10) x PriceMultiplier + BottomPrice  
  
	(3)Whether you want to modify the Food which is OP/overload/overpower/too low.(Only when Food Price > 0) !  
		Enter 【No】 to modify the OP Food.  
  
	(4) Whether you want to add a Random Number into BottomPrice & Whether you want to modify while FoodPrice < 0.  
		Enter【11】 to add both functions. (All option: 【00】, 【01】,【11】,【10】).  
	Random Number is create by rolling two twenty sided dice (2D20). Rolling consequence as below:  
   
Roll2&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-> BottomPrice x 55   
3 ~ 4&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-> BottomPrice x 35   
5 ~ 9&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-> BottomPrice x 6   
10 ~ 15&nbsp;&nbsp;&nbsp;&nbsp;-> BottomPrice x 1.4   
16 ~ 26&nbsp;&nbsp;&nbsp;&nbsp;-> BottomPrice x 1   
27 ~ 32&nbsp;&nbsp;&nbsp;&nbsp;-> BottomPrice x 0.9   
33 ~ 36&nbsp;&nbsp;&nbsp;&nbsp;-> BottomPrice x 0.7   
37 ~ 39&nbsp;&nbsp;&nbsp;&nbsp;-> BottomPrice x 0.3   
Roll40&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-> BottomPrice x 0.1  
  
Food Price =   
(OfficailPrice-10) x PriceMultiplier +  DiceMultify x BottomPrice  
  
  
