   M S B A M L   `   `   `  ????E  ACanvas WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Canvas_WPF
Canvas_WPF  5   	       Canvas_WPF.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Canvas_WPF     5   	   -   5          ? Title$  
MainWindow??5   	   $	??600??6   $	??800??6)   
????  ?$	??600??5	   	   $	??600??6   
???????$??28(?5
   	   .??5      ?  ? RowDefinitions 5   
   ?? 5      $
??auto ?6   ?? 5      $??* ?6    ? ColumnDefinitions 5   
   ?? 5      $??* ?6   ?? 5      $
??auto ?6   ?? 5      $??* ?6   ??5   
   ?? 5       ?? ImageSource$ Assets/Background.png??6   .+?5   
   (? $??1??6   $??1??6"   $??0,0,0,20q?5      .??5      ?? -   5         ??Name$ 
GameCanvas??5      ???#101010$	??500??5      $	??250??5      }?. 5!      5"      ?? 5$   
   -   5         ??Name$ 	ScoreText??5$      $??0??5%      $??1??5&      $??Score??5'       ?? TextAlignment$ Center=?5(      $
??0,10q?5)      ?? 5+   
   -   5         ??Name$ 	NextBlock??5+      $??1??5,      $??0??5-      $??80,190,80,190q?5.      ?? 50   
   -   5        $ 	TimerText??50      $??0??51      $??0??52      $??Timer??53      $ Center=?54      $
??0,10q?55      ? 57   
   -   5         ?Name$ GameOverMenu??57      ???	#CC000000$??2??59      $??3??5:      $y?Hidden=?5;      .+?5>      ?? $??Center=?6   $??Center=?5?      .+?5A      ?? $ Center=?6   $??Center=?5B      $??
Game Over!??5C      ?? 5E      $??Final Score??6   ?? 5F      -   5        $ 
scoreFinal??5F      5I      5J      5K      5L      